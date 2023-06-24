import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/user.dart';
import 'package:fcd_flutter/base/widgets/circle_image_cookie.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:flutter/material.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';

class ContactScreen extends StatelessWidget {
  const ContactScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: FutureBuilder(
        future: Future.delayed(Duration(milliseconds: 350)),
        builder: (context, snapshot) {
          if(snapshot.connectionState == ConnectionState.done)
            {
              return Column(
                children: [
                  StreamBuilder(
                    stream: Constants.db.userDao.findAll(),
                    builder: (context, snapshot) {
                      if (snapshot.connectionState == ConnectionState.active) {
                        if (snapshot.data!.isNotEmpty) {
                          return Flexible(
                              child: ListView.builder(
                                itemCount: snapshot.data!.length,
                                itemBuilder: (context, index) {
                                  User item = snapshot.data![index];
                                  return ListTile(
                                      tileColor: snapshot.data!.indexOf(item)%2==0?Colors.grey.shade50:Colors.white,
                                      title: Text("${item.fullName}",style: TextStyle(color: Color(0xFF006784)),),
                                      subtitle: Flexible(child: Column(
                                        crossAxisAlignment: CrossAxisAlignment.start,
                                        children: [
                                          Text("CREWCODE: ${item.code2}"),
                                          if(item.mobile!=null)
                                            Text("${item.mobile}"),
                                        ],
                                      ),),
                                      leading: CircleImageCookie(
                                          imageUrl:
                                          '${Constants.baseURL}/${snapshot.data![index].avatar}?ver=${Functions.instance.formatDateToStringWithFormat(DateTime.now(),"yyyyMMddHHmmss")}',
                                          errImage: 'asset/images/icon_avatar64.png',
                                          width: 35,
                                          height: 35));
                                },
                              ));
                        }
                        return Expanded(
                            child: Container(
                              child: Center(
                                child: Text("No data"),
                              ),
                            ));
                      } else {
                        return Expanded(
                            child: Container(
                              child: Center(
                                child: SpinKitRing(
                                  color: Color(0xFF006784),
                                  size: 50.0,
                                )
                              ),
                            ));
                      }
                    },
                  )
                ],
              );
            }
          else
            {
              return Expanded(
                  child: Container(
                    child: Center(
                      child: SpinKitRing(
                        color: Color(0xFF006784),
                        size: 50.0,
                      )
                    ),
                  ));
            }
        },
      ),
    );
  }

  AppBar buildAppBar(BuildContext context) {
    return AppBar(
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
        "Contacts",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
    );
  }
}
