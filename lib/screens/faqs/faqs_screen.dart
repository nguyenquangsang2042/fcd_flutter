import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/functions.dart';
import 'package:fcd_flutter/base/widgets/html_content_widget.dart';
import 'package:fcd_flutter/base/widgets/language_switch.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../../base/model/app/settings.dart';

class FaqsScreen extends StatelessWidget {
  FaqsScreen({Key? key, required this.isVietnamese}) : super(key: key);
  bool isVietnamese;

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
      future: getSetting(),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.done) {
          if (snapshot.data == null) {
            return Center(
              child: Text("Không có dữ liệu"),
            );
          } else {
            return StreamBuilder(
              stream: Constants.db.faqDao.getListFaqsDifLstIDAndLang(
                  snapshot.data!, isVietnamese ? "2" : "1"),
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.active) {
                  if (snapshot.hasData && snapshot.data != null) {
                    return Flexible(
                        child: ListView.builder(
                            itemCount: snapshot.data!.length,
                            itemBuilder: (context, index) {
                              return Container(
                                color: index % 2 != 0
                                    ? Colors.grey.shade50
                                    : Colors.white,
                                child: ExpansionTile(
                                  backgroundColor: Colors.grey.shade100,
                                  title: Text(snapshot.data![index].question),
                                  trailing: Icon(
                                      Icons.arrow_drop_down_circle
                                  ),
                                  children: [
                                    Container(
                                      color: Colors.white,
                                      child: ListTile(
                                      tileColor: Colors.white,
                                      title: HtmlContentWidget(htmlContent: snapshot.data![index].answer),
                                    ),)
                                  ],
                                ),
                              );
                            }));
                  } else {
                    return Center(
                      child: Text("Không có dữ liệu"),
                    );
                  }
                }
                return const Center(
                  child: CircularProgressIndicator(),
                );
              },
            );
          }
        } else {
          return const Center(
            child: CircularProgressIndicator(),
          );
        }
      },
    );
  }

  Future<List<int>> getSetting() async {
    List<int> data = [];
    Setting? Helpdesk_Mobile =
        await Constants.db.settingDao.findSettingByKey("Helpdesk_Mobile_ID");
    if (Helpdesk_Mobile != null) {
      data.add(int.parse(Helpdesk_Mobile!.VALUE));
    }
    Setting? Helpdesk_Mobile_ID_2 =
        await Constants.db.settingDao.findSettingByKey("Helpdesk_Mobile_ID_2");
    if (Helpdesk_Mobile_ID_2 != null) {
      data.add(int.parse(Helpdesk_Mobile_ID_2!.VALUE));
    }
    return data;
  }
}
