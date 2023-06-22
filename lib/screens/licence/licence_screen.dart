import 'package:declarative_refresh_indicator/declarative_refresh_indicator.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:flutter/material.dart';

import '../../base/constanst.dart';
import '../../base/model/app/licence.dart';

class LicenceScreen extends StatelessWidget {
  LicenceScreen({Key? key}) : super(key: key);
  ValueNotifier<bool> isAll = ValueNotifier(false);
  ValueNotifier<bool> isSync= ValueNotifier(false);
  List<License> allLiscense = [];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        leading: SizedBox(
          width: 50,
          height: 50,
          child: IconButton(
            icon: Image.asset(
              'asset/images/icon_back30.png',
              color: Colors.white,
              height: 20,
              width: 40,
            ),
            onPressed: () {
              Navigator.pop(context);
            },
          ),
        ),
        title: const Text(
          'Licence',
          style: TextStyle(color: Colors.white, fontSize: 18),
        ),
        backgroundColor: const Color(0xFF006784),
        centerTitle: true,
      ),
      body: FutureBuilder(
        future: Constanst.apiController.updateLicence(),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.done) {
            return ValueListenableBuilder(valueListenable: isAll, builder: (context, value, child) {
              return StreamBuilder(
                stream: Constanst.db.licenceDao.findAll(),
                builder: (context, snapshot) {
                  if (snapshot.hasData) {
                    allLiscense = snapshot.data!;
                    List<License> dataFilter = !isAll.value
                        ? allLiscense
                        .where((element) => Functions.instance
                        .stringToDate(element.expireDate, null)
                        .isBefore(DateTime.now()))
                        .toList()
                        : allLiscense;
                    return Column(
                      children: [
                        Row(
                          children: [
                            Expanded(flex: 1,child: TextButton(onPressed: (){
                              isAll.value=false;
                            }, child: Text("Expired",style: TextStyle(
                                color: !isAll.value
                                    ? Color(0xFFDBA40D)
                                    : Color(0xFFAAAAAA)),)),),
                            Expanded(flex: 1,child: TextButton(onPressed: (){
                              isAll.value=true;
                            }, child: Text("All",style: TextStyle(
                                color: isAll.value
                                    ? Color(0xFFDBA40D)
                                    : Color(0xFFAAAAAA)),)),),
                          ],
                        ),
                        Flexible(child: DeclarativeRefreshIndicator(
                          refreshing: isSync.value,
                          color: const Color(0xFF006784),
                          onRefresh: () async {
                            isSync.value = true;
                            await Constanst.apiController.updateLicence();
                            isSync.value = false;
                          },
                          child: ListView.builder(
                              itemCount: dataFilter.length,
                              itemBuilder: (_, index) {
                                TextStyle style = getExpireDateTextStyle(
                                    Functions.instance.stringToDate(
                                        dataFilter[index].expireDate, null),
                                    dataFilter[index].status);
                                return ListTile(
                                  title: Row(
                                    mainAxisAlignment:
                                    MainAxisAlignment.spaceBetween,
                                    children: [
                                      Flexible(
                                          child: Text(
                                            dataFilter[index].licenseType,
                                            style: style,
                                          )),
                                      Flexible(
                                          child: Text(
                                            dataFilter[index].number,
                                            style: style,
                                          )),
                                    ],
                                  ),
                                  subtitle: Row(
                                    mainAxisAlignment:
                                    MainAxisAlignment.spaceBetween,
                                    children: [
                                      Flexible(
                                          child: Text(
                                            dataFilter[index].note,
                                            style: style,
                                          )),
                                      Flexible(
                                          child: Text(
                                            Functions.instance.formatDateString(
                                                dataFilter[index].expireDate,
                                                Constanst.formatDateddmmyyy),
                                            style: style,
                                          )),
                                    ],
                                  ),
                                );
                              }),
                        ))
                        
                      ],
                    );
                  } else {
                    return Container();
                  }
                },
              );
            },);
          } else {
            return Container(
                child: Center(
              child: CircularProgressIndicator(),
            ));
          }
        },
      ),
    );
  }

  TextStyle getExpireDateTextStyle(DateTime expireDate, int status) {
    if (expireDate.isBefore(DateTime.now()) && status == -1) {
      return TextStyle(color: Color(0xFF525252));
    } else if (expireDate.isBefore(DateTime.now()) && status == 1) {
      return TextStyle(color: Colors.red);
    } else if (expireDate.difference(DateTime.now()).inDays < 7) {
      return TextStyle(color: Colors.black);
    } else {
      return TextStyle();
    }
  }
}
