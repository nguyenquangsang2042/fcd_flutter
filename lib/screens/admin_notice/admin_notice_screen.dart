import 'package:declarative_refresh_indicator/declarative_refresh_indicator.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/announcement_category.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:fcd_flutter/screens/notification/news_screen.dart';
import 'package:flutter/material.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

class AdminNoticeScreen extends StatelessWidget {
  AdminNoticeScreen({super.key});

  ValueNotifier<List<String>> defaultSafety = ValueNotifier([]);
  String keyNew = '';
  ValueNotifier<bool> isRefreshing = ValueNotifier(false);
  ValueNotifier<String> sortType = ValueNotifier("");
  ValueNotifier<String> filterType = ValueNotifier("0");
  ValueNotifier<String> keyWord = ValueNotifier("");
  ValueNotifier<Stream<List<Notify>>> streamList =
      ValueNotifier(Stream.fromIterable([]));
  String SAFETY_CATEGORY_ID = "";
  String QUALIFICATION_CATEGORY_ID = "";
  ValueNotifier<bool> isShowIconFilter = ValueNotifier(true);
  ValueNotifier<bool> isShowSearch = ValueNotifier(false);
  late List<AnnouncementCategory> listFilter;
  int _groupValue = 0;
  String _groupValueSort = "Date";
  TextEditingController contollerSearch = TextEditingController(text: '');
  List<String> trainingNotify = ["1000", "1010", "5", "0", "3"];

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
                'Notice',
                style: TextStyle(color: Colors.white, fontSize: 18),
              ),
              backgroundColor: const Color(0xFF006784),
              centerTitle: true,
              actions: [
                ValueListenableBuilder(
                  valueListenable: isShowIconFilter,
                  builder: (context, value, child) {
                    return Visibility(
                        visible: value,
                        child: Container(
                          width: 50,
                          height: 50,
                          child: IconButton(
                            icon: Image.asset(
                              'asset/images/icon_filter.png',
                              color: Colors.white,
                              height: 20,
                              width: 40,
                            ),
                            onPressed: () {
                              isShowSearch.value = false;
                              _showPopupFilter(context);
                            },
                          ),
                        ));
                  },
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
                    onPressed: () {
                      isShowSearch.value = false;
                      _showPopupSort(context);
                    },
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
                    onPressed: () {
                      isShowSearch.value = !isShowSearch.value;
                      if (!isShowSearch.value) {
                        keyWord.value = "";
                        streamList.value = setStreamGetData();
                      }
                    },
                  ),
                ),
              ],
            ),
            body: Column(
              children: [
                ValueListenableBuilder(
                  valueListenable: isShowSearch,
                  builder: (context, value, child) {
                    return Visibility(visible: value, child: buildTextSearch());
                  },
                ),
                Expanded(
                  flex: 1,
                  child: MultiValueListenableBuilder(
                    valueListenables: [isRefreshing, streamList, defaultSafety],
                    builder: (context, value, _) {
                      return DeclarativeRefreshIndicator(
                        refreshing: isRefreshing.value,
                        color: const Color(0xFF006784),
                        onRefresh: () async {
                          isRefreshing.value = true;
                          Constants.apiController.updateNotify();
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

  Widget buildTextSearch() {
    contollerSearch = TextEditingController(text: keyWord.value);
    return Container(
      padding: EdgeInsets.all(5.0),
      color: Colors.grey.shade400,
      child: TextField(
        controller: contollerSearch,
        onChanged: (value) {
          keyWord.value = value;
          streamList.value = setStreamGetData();
        },
        decoration: InputDecoration(
          filled: true,
          fillColor: Colors.white,
          contentPadding: EdgeInsets.symmetric(horizontal: 10.0),
          hintText: "Search",
          prefixIcon: Icon(Icons.search),
          suffixIcon: IconButton(
            icon: Icon(Icons.cancel),
            onPressed: () {
              keyWord.value = "";
              contollerSearch.clear();
              streamList.value = setStreamGetData();
            }, // Replace with delete functionality
          ),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(6.0),
            borderSide: BorderSide.none,
          ),
        ),
      ),
    );
  }

  void _showPopupFilter(BuildContext context) async {
    if (!trainingNotify.contains(SAFETY_CATEGORY_ID)) {
      trainingNotify.add(SAFETY_CATEGORY_ID);
    }
    if (!trainingNotify.contains(QUALIFICATION_CATEGORY_ID)) {
      trainingNotify.add(QUALIFICATION_CATEGORY_ID);
    }
    Constants.db.announcementCategoryDao
        .getAnnouncementCategoryNotInListID(trainingNotify)
        .listen((event) {
      listFilter = [];
      listFilter.add(AnnouncementCategory.none(0, "All"));
      listFilter.addAll(event);
      showDialog(
          context: context,
          builder: (_) {
            return Container(
              height: null,
              margin: EdgeInsets.only(top: 55),
              child: Column(
                children: [
                  Flexible(
                    child: ListView.builder(
                        shrinkWrap: true,
                        physics: NeverScrollableScrollPhysics(),
                        itemCount: listFilter.length,
                        itemBuilder: (_, index) {
                          return Material(
                            child: InkResponse(
                              child: ListTile(
                                title: Text("${listFilter[index]!.title}"),
                                leading: Radio(
                                  value: listFilter[index].id,
                                  groupValue: _groupValue,
                                  onChanged: (value) {
                                    Navigator.of(context).pop();
                                    if (value == 0) {
                                      defaultSafety.value = [
                                        SAFETY_CATEGORY_ID,
                                        QUALIFICATION_CATEGORY_ID
                                      ];
                                      defaultSafety.value.addAll(trainingNotify);

                                    } else {
                                      defaultSafety.value = [value.toString()];
                                    }
                                    streamList.value = setStreamGetData();
                                    _groupValue = value!;
                                  },
                                ),
                              ),
                              onTap: () {
                                Navigator.of(context).pop();
                                if (listFilter[index].id == 0) {
                                  defaultSafety.value = [
                                    SAFETY_CATEGORY_ID,
                                    QUALIFICATION_CATEGORY_ID
                                  ];
                                  defaultSafety.value.addAll(trainingNotify);

                                } else {
                                  defaultSafety.value = [
                                    listFilter[index].id.toString()
                                  ];
                                }
                                streamList.value = setStreamGetData();
                                _groupValue = listFilter[index].id;
                              },
                            ),
                          );
                        }),
                  ),
                  Expanded(
                    child: Container(),
                    flex: 1,
                  )
                ],
              ),
            );
          });
    });
  }

  void _showPopupSort(BuildContext context) async {
    List<String> lstSortType = ["Date", "Unread", "Emergency", "Confirm"];
    showDialog(
        context: context,
        builder: (_) {
          return Container(
            height: null,
            margin: EdgeInsets.only(top: 55),
            child: Column(
              children: [
                ListView.builder(
                    shrinkWrap: true,
                    physics: NeverScrollableScrollPhysics(),
                    itemCount: lstSortType.length,
                    itemBuilder: (_, index) {
                      return Material(
                          child: InkResponse(
                        onTap: () {
                          Navigator.of(context).pop();
                          sortType.value = lstSortType[index];
                          streamList.value = setStreamGetData();
                          _groupValueSort = lstSortType[index];
                        },
                        child: ListTile(
                          title: Text(lstSortType[index]),
                          leading: Radio(
                            value: lstSortType[index],
                            groupValue: _groupValueSort,
                            onChanged: (value) {
                              Navigator.of(context).pop();
                              sortType.value = lstSortType[index];
                              streamList.value = setStreamGetData();
                              _groupValueSort = lstSortType[index];
                            },
                          ),
                        ),
                      ));
                    }),
                Expanded(
                  child: Container(),
                  flex: 1,
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
          if (snapshot.connectionState == ConnectionState.active &&
              snapshot.hasData &&
              snapshot.data != null) {
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
                                    '${Constants.baseURL}${snapshot.data![index].iconPath}',
                                errImage: 'asset/images/logo_vna120.png'),
                          ),
                          title: Text(snapshot.data![index].announCategoryId==7
                              ? "News"
                              : snapshot.data![index].title!=null?snapshot.data![index].title!:""),
                          subtitle: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            mainAxisAlignment: MainAxisAlignment.start,
                            children: [
                              Text(snapshot.data![index].announCategoryId==7?snapshot.data![index].title!:snapshot.data![index].content),
                              Row(
                                children: [
                                  Expanded(
                                    flex: 8,
                                    child: Text(Functions.instance.formatDateString(snapshot.data![index].created, Constants.formatDateddmmyyy)),
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
                                builder: (context) => NewsScreen(
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
    defaultSafety.value = [];
    Setting? value;
    value =
        await Constants.db.settingDao.findSettingByKey("SAFETY_CATEGORY_ID");
    SAFETY_CATEGORY_ID = value == null ? "" : value.VALUE;
    value = await Constants.db.settingDao
        .findSettingByKey("QUALIFICATION_CATEGORY_ID");
    QUALIFICATION_CATEGORY_ID = value == null ? "" : value.VALUE;
    defaultSafety.value = [SAFETY_CATEGORY_ID, QUALIFICATION_CATEGORY_ID];
    await Constants.db.settingDao
        .findSettingByKey("NEWS_CATEGORY_ID")
        .then((value) => keyNew = value!.VALUE.toString());
    defaultSafety.value.addAll(trainingNotify);
    streamList = ValueNotifier(Constants.db.notifyDao
        .caseDefaultSwitch3(
            defaultSafety.value));
  }

  Stream<List<Notify>> setStreamGetData() {
    if (keyWord.value.isNotEmpty) {
      if (filterType.value.contains("0") || filterType.value.contains("-1")) {
        switch (sortType.value.toLowerCase()) {
          case "unread":
            return Constants.db.notifyDao
                .caseUnreadSwitch1("%${keyWord.value}%", defaultSafety.value);
          case "emergency":
            return Constants.db.notifyDao.caseEmergencySwitch1("%${keyWord.value}%", defaultSafety.value);
          case "confirm":
            return Constants.db.notifyDao
                .caseConfirmSwitch1("%${keyWord.value}%", defaultSafety.value);
          default:
            return Constants.db.notifyDao
                .caseDefaultSwitch1("%${keyWord.value}%", defaultSafety.value);
        }
      } else {
        switch (sortType.value.toLowerCase()) {
          case "unread":
            return Constants.db.notifyDao
                .caseUnreadSwitch2("%${keyWord.value}%", defaultSafety.value);
          case "emergency":
            return Constants.db.notifyDao.caseEmergencySwitch2("%${keyWord.value}%", defaultSafety.value);
          case "confirm":
            return Constants.db.notifyDao
                .caseConfirmSwitch2("%${keyWord.value}%", defaultSafety.value);
          default:
            return Constants.db.notifyDao
                .caseDefaultSwitch2("%${keyWord.value}%", defaultSafety.value);
        }
      }
    } else {
      if (filterType.value.contains("0") || filterType.value.contains("-1")) {
        switch (sortType.value.toLowerCase()) {
          case "unread":
            return Constants.db.notifyDao
                .caseUnreadSwitch3(defaultSafety.value);
          case "emergency":
            return Constants.db.notifyDao.caseEmergencySwitch3(defaultSafety.value);
          case "confirm":
            return Constants.db.notifyDao
                .caseConfirmSwitch3(defaultSafety.value);
          default:
            return Constants.db.notifyDao
                .caseDefaultSwitch3(defaultSafety.value);
        }
      } else {
        switch (sortType.value.toLowerCase()) {
          case "unread":
            return Constants.db.notifyDao
                .caseUnreadSwitch4(defaultSafety.value);
          case "emergency":
            return Constants.db.notifyDao.caseEmergencySwitch4(defaultSafety.value);
          case "confirm":
            return Constants.db.notifyDao
                .caseConfirmSwitch4(defaultSafety.value);
          default:
            return Constants.db.notifyDao
                .caseDefaultSwitch4(defaultSafety.value);
        }
      }
    }
  }
}
