import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../notification/news_screen.dart';

class NoticeTrainingScreen extends StatelessWidget {
  NoticeTrainingScreen({super.key, required this.lstAnnounCategoryId});
  List<String> lstAnnounCategoryId;
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
            return ListView.builder(
              itemCount: snapshot.data!.length,
              itemBuilder: (context, index) {
                return Container(
                  color:
                      index % 2 != 0 ? Colors.white : Colors.blueGrey.shade50,
                  child: itemNotify(snapshot, index, context),
                );
              },
            );
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

  InkResponse itemNotify(AsyncSnapshot<List<Notify>> snapshot, int index, BuildContext context) {
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
                                '${Constants.baseURL}${snapshot.data![index].iconPath!}',
                            errImage: 'asset/images/logo_vna120.png'),
                      ),
                      title: Text(
                        snapshot.data![index].title != null
                            ? snapshot.data![index].title!
                            : snapshot.data![index].content,
                        style: TextStyle(
                            fontWeight: ((snapshot.data![index].flgRead !=
                                            null &&
                                        !snapshot.data![index].flgRead) ||
                                    (snapshot.data![index].flgConfirm &&
                                        snapshot.data![index].flgConfirmed ==
                                            0) ||
                                    (snapshot.data![index].flgReply &&
                                        snapshot.data![index].flgReplied))
                                ? FontWeight.bold
                                : FontWeight.normal),
                      ),
                      subtitle: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: [
                          Text(snapshot.data![index].content),
                          Row(
                            children: [
                              Expanded(
                                flex: 8,
                                child: Text(Functions.instance
                                    .formatDateString(
                                        snapshot.data![index].created,
                                        "dd MMM yyyy" )),
                              ),
                              Expanded(
                                flex: 2,
                                child: Row(
                                  children: [
                                    if (snapshot.data![index].flgSurvey)
                                      Expanded(
                                        flex: 1,
                                        child: SizedBox(
                                          height: 15,
                                          width: 15,
                                          child: Image.asset(
                                              'asset/images/icon_reply.png'),
                                        ),
                                      ),
                                    if (snapshot.data![index].flgReply &&
                                        !snapshot.data![index].flgReplied)
                                      Expanded(
                                        flex: 1,
                                        child: SizedBox(
                                          height: 15,
                                          width: 15,
                                          child: Image.asset(
                                              'asset/images/icon_answer.png'),
                                        ),
                                      ),
                                    if (snapshot.data![index].flgConfirm)
                                      Expanded(
                                        flex: 1,
                                        child: SizedBox(
                                          height: 15,
                                          width: 15,
                                          child: Image.asset(
                                              'asset/images/icon_confirm.png'),
                                        ),
                                      ),
                                    if (snapshot.data![index].flgImmediately)
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
                                  notify: snapshot.data![index],
                                )));
                  },
                );
  }
}
