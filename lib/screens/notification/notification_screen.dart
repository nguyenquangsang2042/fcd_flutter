import 'package:declarative_refresh_indicator/declarative_refresh_indicator.dart';
import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:fcd_flutter/screens/notification/news_screen.dart';
import 'package:flutter/material.dart';

class NotificationScreen extends StatelessWidget {
  NotificationScreen({super.key});

  late List<String> defaultSafety;
  String keyNew = '';
  late String beanAnnounceID;
  ValueNotifier<bool> isRefreshing = ValueNotifier(false);
  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
        future: setDefaultSafety(),
        builder: (context, snapshot) {
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
                'Safety',
                style: TextStyle(color: Colors.white, fontSize: 18),
              ),
              backgroundColor: const Color(0xFF006784),
              centerTitle: true,
              actions: [
                Container(
                  width: 50,
                  height: 50,
                  child: IconButton(
                    icon: Image.asset(
                      'asset/images/icon_filter.png',
                      color: Colors.white,
                      height: 20,
                      width: 40,
                    ),
                    onPressed: () {},
                  ),
                ),
                Container(
                  width: 50,
                  height: 50,
                  child: IconButton(
                    icon: Image.asset(
                      'asset/images/icon_sort_descending.png',
                      color: Colors.white,
                      height: 20,
                      width: 40,
                    ),
                    onPressed: () {},
                  ),
                ),
                Container(
                  width: 50,
                  height: 50,
                  child: IconButton(
                    icon: Image.asset(
                      'asset/images/icon_search26.png',
                      color: Colors.white,
                      height: 20,
                      width: 40,
                    ),
                    onPressed: () {},
                  ),
                ),
              ],
            ),
            body: Column(
              children: [
                Container(
                  width: MediaQuery.of(context).size.width,
                  child: Row(
                    children: [
                      Expanded(
                        flex: 1,
                        child:
                            TextButton(onPressed: () {}, child: Text('Safety')),
                      ),
                      Expanded(
                        flex: 1,
                        child: TextButton(
                            onPressed: () {}, child: Text('Operation')),
                      ),
                    ],
                  ),
                ),
                Expanded(
                  flex: 1,
                  child: ValueListenableBuilder<bool>(
                    valueListenable: isRefreshing,
                    builder: (context,value,_){
                      return DeclarativeRefreshIndicator(
                        refreshing: value,
                        color: const Color(0xFF006784),
                        onRefresh: () async {
                          isRefreshing.value=true;
                          Constanst.apiController.updateNotify();
                          Future.delayed(Duration(seconds: 3)).then((value) => {
                            isRefreshing.value=false
                          });
                        },
                        child: StreamBuilder(
                            stream: Constanst.db.notifyDao
                                .getListNotifyWithAnnounceCategory(
                                defaultSafety, keyNew),
                            builder: (context, snapshot) {
                              if (snapshot.hasData && snapshot.data != null) {
                                return ListView.builder(
                                    itemCount: snapshot.data?.length,
                                    itemBuilder: (context, index) {
                                      return Container(
                                        color: index % 2 != 0
                                            ? Colors.white
                                            : Colors.blueGrey.shade50,
                                        child: InkResponse(
                                          child: Padding(
                                            padding: const EdgeInsets.only(
                                                left: 7,
                                                right: 7,
                                                top: 5,
                                                bottom: 5),
                                            child: ListTile(
                                              leading: SizedBox(
                                                height: 50,
                                                width: 50,
                                                child: ImageWithCookie(
                                                    imageUrl:
                                                    '${Constanst.baseURL}${snapshot.data![index].iconPath!}',
                                                    errImage:
                                                    'asset/images/logo_vna120.png'),
                                              ),
                                              title: Text(
                                                  snapshot.data![index].content),
                                              subtitle: Column(
                                                crossAxisAlignment:
                                                CrossAxisAlignment.start,
                                                mainAxisAlignment:
                                                MainAxisAlignment.start,
                                                children: [
                                                  Text(snapshot
                                                      .data![index].content),
                                                  Row(
                                                    children: [
                                                      Expanded(
                                                        flex: 8,
                                                        child: Text(snapshot
                                                            .data![index].content),
                                                      ),
                                                      Expanded(
                                                        flex: 2,
                                                        child: Row(
                                                          children: [
                                                            if (snapshot
                                                                .data![index]
                                                                .flgSurvey)
                                                              Expanded(
                                                                flex: 1,
                                                                child: SizedBox(
                                                                  height: 15,
                                                                  width: 15,
                                                                  child: Image.asset(
                                                                      'asset/images/icon_reply.png'),
                                                                ),
                                                              ),
                                                            if (snapshot
                                                                .data![index]
                                                                .flgReply &&
                                                                !snapshot
                                                                    .data![index]
                                                                    .flgReplied)
                                                              Expanded(
                                                                flex: 1,
                                                                child: SizedBox(
                                                                  height: 15,
                                                                  width: 15,
                                                                  child: Image.asset(
                                                                      'asset/images/icon_answer.png'),
                                                                ),
                                                              ),
                                                            if (snapshot
                                                                .data![index]
                                                                .flgConfirm)
                                                              Expanded(
                                                                flex: 1,
                                                                child: SizedBox(
                                                                  height: 15,
                                                                  width: 15,
                                                                  child: Image.asset(
                                                                      'asset/images/icon_confirm.png'),
                                                                ),
                                                              ),
                                                            if (snapshot
                                                                .data![index]
                                                                .flgImmediately)
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
                                                    builder: (context) =>
                                                        NewsScreen(
                                                          notify:
                                                          snapshot.data![index],
                                                          safetyID:
                                                          defaultSafety[0],
                                                          qualificationID:
                                                          defaultSafety[1],
                                                        )));
                                          },
                                        ),
                                      );
                                    });
                              } else {
                                return const Center(
                                  child: Text("Không có dữ liệu"),
                                );
                              }
                            }),
                      );
                    },
                  ),
                )
              ],
            ),
          );
        });
  }

  Future<void> setDefaultSafety() async {
    defaultSafety = [];
    await Constanst.db.settingDao
        .findSettingByKey("SAFETY_CATEGORY_ID")
        .then((value) => defaultSafety.add(value!.VALUE.toString()))
        .onError((error, stackTrace) => debugPrint(error.toString()));
    await Constanst.db.settingDao
        .findSettingByKey("QUALIFICATION_CATEGORY_ID")
        .then((value) => defaultSafety.add(value!.VALUE.toString()))
        .onError((error, stackTrace) => debugPrint(error.toString()));
    await Constanst.db.settingDao
        .findSettingByKey("NEWS_CATEGORY_ID")
        .then((value) => keyNew = value!.VALUE.toString());

    beanAnnounceID = "'${defaultSafety.join("','")}'";
  }
}
