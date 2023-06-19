import 'package:declarative_refresh_indicator/declarative_refresh_indicator.dart';
import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:fcd_flutter/screens/notification/news_screen.dart';
import 'package:flutter/material.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

class NotificationScreen extends StatelessWidget {
  NotificationScreen({super.key});

  late List<String> defaultSafety;
  String keyNew = '';
  ValueNotifier<bool> isRefreshing = ValueNotifier(false);
  ValueNotifier<bool> isSafety = ValueNotifier(true);
  ValueNotifier<String> sortType = ValueNotifier("");
  ValueNotifier<String> filterType = ValueNotifier("");
  ValueNotifier<String> keyWord = ValueNotifier("");
  ValueNotifier<Stream<List<Notify>>> streamList = ValueNotifier(Stream.fromIterable([]));

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
                  width: MediaQuery
                      .of(context)
                      .size
                      .width,
                  child: Row(
                    children: [
                      Expanded(
                        flex: 1,
                        child: TextButton(
                            onPressed: () async {
                              isSafety.value = true;

                              Setting? SAFETY_CATEGORY_ID= await Constanst.db.settingDao
                                  .findSettingByKey("SAFETY_CATEGORY_ID");
                              Setting? QUALIFICATION_CATEGORY_ID=
                              await Constanst.db.settingDao
                                  .findSettingByKey("QUALIFICATION_CATEGORY_ID");
                              defaultSafety=[SAFETY_CATEGORY_ID!.VALUE,QUALIFICATION_CATEGORY_ID!.VALUE];
                              streamList.value=setStreamGetData();
                            },
                            child: Text('Safety')),
                      ),
                      Expanded(
                        flex: 1,
                        child: TextButton(
                            onPressed: () {
                              isSafety.value = false;
                              defaultSafety=["3"];
                              streamList.value=setStreamGetData();
                            },
                            child: Text('Operation')),
                      ),
                    ],
                  ),
                ),
                Expanded(
                  flex: 1,
                  child: MultiValueListenableBuilder(
                    valueListenables: [isRefreshing, isSafety,streamList],
                    builder: (context, value, _) {
                      return DeclarativeRefreshIndicator(
                        refreshing: isRefreshing.value,
                        color: const Color(0xFF006784),
                        onRefresh: () async {
                          isRefreshing.value = true;
                          Constanst.apiController.updateNotify();
                          Future.delayed(Duration(seconds: 3))
                              .then((value) => {isRefreshing.value = false});
                        },
                        child: ListSafety(),
                      );
                    },
                  ),
                )
              ],
            ),
          );
        });
  }

  StreamBuilder<List<Notify>> ListSafety() {
    return StreamBuilder(
        stream: streamList.value,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.active&&snapshot.hasData && snapshot.data != null) {
            return ListView.builder(
                itemCount: snapshot.data?.length,
                itemBuilder: (context, index) {
                  return Container(
                    color:
                    index % 2 != 0 ? Colors.white : Colors.blueGrey.shade50,
                    child: InkResponse(
                      child: Padding(
                        padding: const EdgeInsets.only(
                            left: 7, right: 7, top: 5, bottom: 5),
                        child: ListTile(
                          leading: SizedBox(
                            height: 50,
                            width: 50,
                            child: ImageWithCookie(
                                imageUrl:
                                '${Constanst.baseURL}${snapshot.data![index]
                                    .iconPath!}',
                                errImage: 'asset/images/logo_vna120.png'),
                          ),
                          title: Text(snapshot.data![index].title!=null?snapshot.data![index].title!:snapshot.data![index].content),
                          subtitle: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            mainAxisAlignment: MainAxisAlignment.start,
                            children: [
                              Text(snapshot.data![index].content),
                              Row(
                                children: [
                                  Expanded(
                                    flex: 8,
                                    child: Text(snapshot.data![index].created),
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
                                        if (snapshot
                                            .data![index].flgImmediately)
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
                        print(defaultSafety);
                        Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) =>
                                    NewsScreen(
                                      notify: snapshot.data![index],
                                    )));
                      },
                    ),
                  );
                });
          } else if (snapshot.connectionState == ConnectionState.done) {
            return const Center(
              child: Text("Không có dữ liệu"),
            );
          } else {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }
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
    streamList= ValueNotifier(Constanst.db.notifyDao
        .getListNotHaveKeywordFilterTypeOrder01ORDER_BY_Created_DESC(
        defaultSafety,
        keyNew));
  }

  Stream<List<Notify>> setStreamGetData()  {

    if (keyWord.value.isNotEmpty) {
      if (filterType.value.contains("0") || filterType.value.contains("-1")) {
        switch (sortType.value) {
          case "unread":
            return Constanst.db.notifyDao.getListHaveKeywordFilterType01ORDER_BY_FlgRead_Created_DESC(
                keyNew, keyWord.value, defaultSafety);
          case "emergency":
            return Constanst.db.notifyDao.getListHaveKeywordFilterType01ORDER_BY_flgImmediately_DESC_Created_DESC(
                keyNew,
                keyWord.value,
                defaultSafety);
          case "confirm":
            return Constanst.db.notifyDao.getListHaveKeywordFilterType01ORDER_BY_flgConfirm_DESC_flgConfirmed_Created_DESC(
                keyNew,
                keyWord.value,
                defaultSafety);
          default:
            return Constanst.db.notifyDao.getListHaveKeywordFilterType01ORDER_BY_Created_DESC(
                keyNew,
                keyWord.value,
                defaultSafety);
        }
      }
      else {
        switch (sortType.value) {
          case "unread":
            return Constanst.db.notifyDao.getListHaveKeywordFilterTypeOrder01ORDER_BY_FlgRead_Created_DESC(
                keyNew, keyWord.value, defaultSafety);
          case "emergency":
            return Constanst.db.notifyDao.getListHaveKeywordFilterTypeOrder01ORDER_BY_flgImmediately_DESC_Created_DESC(
                keyNew,
                keyWord.value,
                defaultSafety);
          case "confirm":
            return Constanst.db.notifyDao.getListHaveKeywordFilterTypeOrder01ORDER_BY_flgConfirm_DESC_flgConfirmed_Created_DESC(
                keyNew,
                keyWord.value,
                defaultSafety);
          default:
            return Constanst.db.notifyDao.getListHaveKeywordFilterTypeOrder01ORDER_BY_Created_DESC(
                keyNew,
                keyWord.value,
                defaultSafety);
        }
      }
    }
    else {
      if (filterType.value.contains("0") || filterType.value.contains("-1")) {
        switch (sortType.value) {
          case "unread":
            return Constanst.db.notifyDao.getListNotHaveKeywordFilterType01ORDER_BY_FlgRead_Created_DESC(
                keyNew, defaultSafety);
          case "emergency":
            return Constanst.db.notifyDao.getListNotHaveKeywordFilterType01ORDER_BY_flgImmediately_DESC_Created_DESC(
                keyNew,
                defaultSafety
                );
          case "confirm":
            return Constanst.db.notifyDao.getListNotHaveKeywordFilterType01ORDER_BY_flgConfirm_DESC_flgConfirmed_Created_DESC(
                keyNew,
                defaultSafety);
          default:
            return Constanst.db.notifyDao.getListNotHaveKeywordFilterType01ORDER_BY_Created_DESC(
                keyNew,
                defaultSafety);
        }
      }
      else {
        switch (sortType.value) {
          case "unread":
            return Constanst.db.notifyDao
                .getListNotHaveKeywordFilterTypeOrder01ORDER_BY_FlgRead_Created_DESC(
                defaultSafety, keyNew);
          case "emergency":
            return Constanst.db.notifyDao
                .getListNotHaveKeywordFilterTypeOrder01ORDER_BY_flgImmediately_DESC_Created_DESC(
                defaultSafety,
                keyNew);
          case "confirm":
            return Constanst.db.notifyDao
                .getListNotHaveKeywordFilterTypeOrder01ORDER_BY_flgConfirm_DESC_flgConfirmed_Created_DESC(
                defaultSafety,
                keyNew);
          default:
            return Constanst.db.notifyDao
                .getListNotHaveKeywordFilterTypeOrder01ORDER_BY_Created_DESC(
                defaultSafety,
                keyNew);
        }
      }
    }
  }
}