import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../notification/news_screen.dart';

class NoticeTrainingScreen extends StatelessWidget {
  NoticeTrainingScreen({super.key, required this.lstAnnounCategoryId,required this.keyWord});
  List<String> lstAnnounCategoryId;
  String keyWord;
  @override
  Widget build(BuildContext context) {
    return StreamBuilder(
      stream: Constants.db.notifyDao
          .getListNotifyWithLstAnnounCategoryId(lstAnnounCategoryId),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.active) {
          if (snapshot.hasData &&
              snapshot.data != null &&
              snapshot.data!.isNotEmpty) {
            List<Notify> data= snapshot.data!;
            if(keyWord.isNotEmpty)
            {
              data= data.where((element) =>element.content.toLowerCase().contains(keyWord.toLowerCase())).toList();
            }
            if(data.isNotEmpty)
              {
                return ListView.builder(
                  itemCount: data.length,
                  itemBuilder: (context, index) {

                    return Container(
                      color:
                      index % 2 != 0 ? Colors.white : Colors.blueGrey.shade50,
                      child: itemNotify(data, index, context),
                    );
                  },
                );
              }
            return Container(child: Center(child: Text("No data"),),);
          } else {
            return Container(
              child: Center(
                child: Text("No data"),
              ),
            );
          }
        } else {
          return Container(
            child: Center(
              child: CircularProgressIndicator(),
            ),
          );
        }
      },
    );
  }

  Widget itemNotify(List<Notify> data, int index, BuildContext context) {


        return InkResponse(
          child: Padding(
            padding: const EdgeInsets.only(
                left: 7, right: 7, top: 5, bottom: 5),
            child: ListTile(
              leading: SizedBox(
                height: 50,
                width: 50,
                child: ImageWithCookie(
                    imageUrl:
                    '${Constants.baseURL}${data[index].iconPath!}',
                    errImage: 'asset/images/logo_vna120.png'),
              ),
              title: Text(
                data[index].title != null
                    ? data[index].title!
                    : data[index].content,
                style: TextStyle(
                    fontWeight: ((data[index].flgRead !=
                        null &&
                        !data[index].flgRead) ||
                        (data[index].flgConfirm &&
                            data[index].flgConfirmed ==
                                0) ||
                        (data[index].flgReply &&
                            data[index].flgReplied))
                        ? FontWeight.bold
                        : FontWeight.normal),
              ),
              subtitle: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  Text(data[index].content),
                  Row(
                    children: [
                      Expanded(
                        flex: 8,
                        child: Text(Functions.instance
                            .formatDateString(
                            data[index].created,
                            "dd MMM yyyy" )),
                      ),
                      Expanded(
                        flex: 2,
                        child: Row(
                          children: [
                            if (data[index].flgSurvey)
                              Expanded(
                                flex: 1,
                                child: SizedBox(
                                  height: 15,
                                  width: 15,
                                  child: Image.asset(
                                      'asset/images/icon_reply.png'),
                                ),
                              ),
                            if (data[index].flgReply &&
                                !data[index].flgReplied)
                              Expanded(
                                flex: 1,
                                child: SizedBox(
                                  height: 15,
                                  width: 15,
                                  child: Image.asset(
                                      'asset/images/icon_answer.png'),
                                ),
                              ),
                            if (data[index].flgConfirm)
                              Expanded(
                                flex: 1,
                                child: SizedBox(
                                  height: 15,
                                  width: 15,
                                  child: Image.asset(
                                      'asset/images/icon_confirm.png'),
                                ),
                              ),
                            if (data[index].flgImmediately)
                              Expanded(
                                flex: 1,
                                child: SizedBox(
                                  height: 15,
                                  width: 15,
                                  child: Image.asset(
                                      'asset/images/icon_flaghigh.png'),
                                ),
                              ),
                          ],
                        ),
                      ),
                    ],
                  )
                ],
              ),
            ),
          ),
          onTap: () {
            Navigator.push(
                context,
                MaterialPageRoute(
                    builder: (context) => NewsScreen(
                      notify: data[index],
                    )));
          },
        );
  }
}
