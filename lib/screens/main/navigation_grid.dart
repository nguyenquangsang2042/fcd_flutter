import 'package:awesome_bottom_bar/awesome_bottom_bar.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/screens/admin_notice/admin_notice_screen.dart';
import 'package:fcd_flutter/screens/notification/notification_screen.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class NavigationGridScreen extends StatelessWidget {
  NavigationGridScreen({super.key, required this.childView});

  ValueNotifier<int> indexSelected = ValueNotifier(0);
  Widget childView;
  late ValueNotifier <Widget> currentView;

  @override
  Widget build(BuildContext context) {
    currentView = ValueNotifier(childView);
    if (childView is NotificationScreen) {
      indexSelected.value = 1;
    }
    return Scaffold(
      body: ValueListenableBuilder(valueListenable: currentView,builder: (context, value, child) {
        return currentView.value;
      },),
      bottomNavigationBar: ValueListenableBuilder(
        valueListenable: indexSelected,
        builder: (context, value, child) {
          return BottomNavigationBar(
            selectedItemColor: Colors.orange,
            backgroundColor: Colors.grey.shade50,
            unselectedItemColor: Colors.grey.shade500,
            type: BottomNavigationBarType.fixed,
            currentIndex: value,
            onTap: (value) {
              indexSelected.value = value;
              switch (value) {
                case 0:
                  Navigator.pop(context);
                  break;
                case 1:
                  currentView.value=NotificationScreen();
                  break;
                case 2:
                  currentView.value=AdminNoticeScreen();
                  break;
                case 3:
                  //schedule chua lam
                  break;
                default:
                  showBottomSheet(context: context, builder: (context) {
                    return Container();
                  },);
                  break;
              }
            },
            items: [
              BottomNavigationBarItem(
                icon: ImageIcon(AssetImage("asset/images/ic_jhome.png")),
                label: 'Home',
              ),
              bottomSafety(),
              bottomNotice(),
              BottomNavigationBarItem(
                icon: ImageIcon(AssetImage("asset/images/ic_schedule.png")),
                label: 'Schedule',
              ),
              BottomNavigationBarItem(
                icon: Icon(Icons.more_horiz),
                label: 'More',
              ),
            ],
          );
        },
      ),
    );
  }

  BottomNavigationBarItem bottomNotice() {
    return BottomNavigationBarItem(
      icon: Stack(
        children: [
          ImageIcon(AssetImage("asset/images/ic_news.png")),
          Positioned(
            top: 0,
            left: 11,
            child: FutureBuilder(
              future: getData(),
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.done) {
                  if (snapshot.hasData && snapshot.data!.isNotEmpty) {
                    return StreamBuilder(
                      stream: Constants.db.notifyDao
                          .getListNotifyNotInLstAnnounCategoryId([
                        snapshot.data![0].VALUE,
                        snapshot.data![1].VALUE,
                        "1000",
                        "1010",
                        "5",
                        "0"
                      ]),
                      builder: (context, snapshot) {
                        if (snapshot.connectionState ==
                            ConnectionState.active) {
                          if (snapshot.hasData && snapshot.data!.isNotEmpty) {
                            return Container(
                              width: 13,
                              height: 13,
                              decoration: BoxDecoration(
                                color: Colors.red,
                                shape: BoxShape.circle,
                              ),
                              child: ClipOval(
                                child: Center(
                                  child: Text(
                                      snapshot.data!.length > 99
                                          ? "99+"
                                          : snapshot.data!.length.toString(),
                                      style: TextStyle(
                                        color: Colors.white,
                                        fontSize: 5,
                                      )),
                                ),
                              ),
                            );
                          } else {
                            return SizedBox(
                              height: 0,
                              width: 0,
                            );
                          }
                        } else {
                          return SizedBox(
                            height: 0,
                            width: 0,
                          );
                        }
                      },
                    );
                  } else {
                    return SizedBox(
                      height: 0,
                      width: 0,
                    );
                  }
                } else {
                  return SizedBox(
                    height: 0,
                    width: 0,
                  );
                }
              },
            ),
          ),
        ],
      ),
      label: 'Notice',
    );
  }

  BottomNavigationBarItem bottomSafety() {
    return BottomNavigationBarItem(
      icon: Stack(
        children: [
          ImageIcon(AssetImage("asset/images/icon_shield.png")),
          Positioned(
            top: 0,
            left: 11,
            child: FutureBuilder(
              future: getData(),
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.done) {
                  if (snapshot.hasData && snapshot.data!.isNotEmpty) {
                    return StreamBuilder(
                      stream: Constants.db.notifyDao.getCountSafety(
                          snapshot.data![0].VALUE, snapshot.data![1].VALUE),
                      builder: (context, snapshot) {
                        if (snapshot.connectionState ==
                            ConnectionState.active) {
                          if (snapshot.hasData && snapshot.data!.isNotEmpty) {
                            return Container(
                              width: 13,
                              height: 13,
                              decoration: BoxDecoration(
                                color: Colors.red,
                                shape: BoxShape.circle,
                              ),
                              child: ClipOval(
                                child: Center(
                                  child: Text(
                                      snapshot.data!.length > 99
                                          ? "99+"
                                          : snapshot.data!.length.toString(),
                                      style: TextStyle(
                                        color: Colors.white,
                                        fontSize: 5,
                                      )),
                                ),
                              ),
                            );
                          } else {
                            return SizedBox(
                              height: 0,
                              width: 0,
                            );
                          }
                        } else {
                          return SizedBox(
                            height: 0,
                            width: 0,
                          );
                        }
                      },
                    );
                  } else {
                    return SizedBox(
                      height: 0,
                      width: 0,
                    );
                  }
                } else {
                  return SizedBox(
                    height: 0,
                    width: 0,
                  );
                }
              },
            ),
          ),
        ],
      ),
      label: 'Safety',
    );
  }

  Future<List<Setting>> getData() async {
    return Constants.db.settingDao.getListSettingInLstKey(
        ["QUALIFICATION_CATEGORY_ID", "SAFETY_CATEGORY_ID"]);
  }
}
