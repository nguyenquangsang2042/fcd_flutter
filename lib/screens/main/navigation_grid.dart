import 'package:awesome_bottom_bar/awesome_bottom_bar.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/screens/notification/notification_screen.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class NavigationGridScreen extends StatelessWidget {
  NavigationGridScreen({super.key, required this.childView});
  ValueNotifier<int> indexSelected = ValueNotifier(0);
  Widget childView;
  @override
  Widget build(BuildContext context) {
    if (childView is NotificationScreen) {
      indexSelected.value = 1;
    }
    return Scaffold(
      body: childView,
      bottomNavigationBar: ValueListenableBuilder(
        valueListenable: indexSelected,
        builder: (context, value, child) {
          return BottomNavigationBar(
            selectedItemColor: Colors.orange,
            backgroundColor: Colors.grey.shade50,
            unselectedItemColor: Colors.grey.shade500,
            type: BottomNavigationBarType.fixed,
            currentIndex: value,
            onTap: (value) => indexSelected.value = value,
            items: [
              BottomNavigationBarItem(
                icon: ImageIcon(AssetImage("asset/images/ic_jhome.png")),
                label: 'Home',
              ),
              BottomNavigationBarItem(
                icon: Stack(
                  children: [
                    ImageIcon(AssetImage("asset/images/icon_shield.png")),
                    Positioned(
                      top: 0,
                      left: 11,
                      child: FutureBuilder(
                        future: getData(),
                        builder: (context, snapshot) {
                          if (snapshot.connectionState ==
                              ConnectionState.done) {
                            if (snapshot.hasData && snapshot.data!.isNotEmpty) {
                              return StreamBuilder(
                                stream: Constants.db.notifyDao.getCountSafety(
                                    snapshot.data![0].VALUE,
                                    snapshot.data![1].VALUE),
                                builder: (context, snapshot) {
                                  if (snapshot.connectionState ==
                                      ConnectionState.active) {
                                    if (snapshot.hasData &&
                                        snapshot.data!.isNotEmpty) {
                                      return Container(
                                        width: 13,
                                        height: 13,
                                        decoration: BoxDecoration(
                                          color: Colors.red,
                                          shape: BoxShape.circle,
                                        ),
                                        child: ClipOval(
                                          child: Center(child: Text(
                                              snapshot.data!.length > 99
                                                  ? "99+"
                                                  : snapshot.data!.length
                                                  .toString(),
                                              style: TextStyle(
                                                color: Colors.white,
                                                fontSize: 5,
                                              )),),
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
              ),
              BottomNavigationBarItem(
                icon: Stack(
                  children: [
                    ImageIcon(AssetImage("asset/images/ic_news.png")),
                    Positioned(
                      top: 0,
                      left: 11,
                      child: Container(
                        padding: EdgeInsets.all(2),
                        decoration: BoxDecoration(
                          color: Colors.red,
                          borderRadius: BorderRadius.circular(10),
                        ),
                        constraints: BoxConstraints(
                            minWidth: 13,
                            minHeight: 13,
                            maxWidth: 13,
                            maxHeight: 13),
                        child: Text(
                          '3',
                          style: TextStyle(
                            color: Colors.white,
                            fontSize: 8,
                          ),
                          textAlign: TextAlign.center,
                        ),
                      ),
                    ),
                  ],
                ),
                label: 'Notice',
              ),
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

  Future<List<Setting>> getData() async {
    return Constants.db.settingDao.getListSettingInLstKey(
        ["QUALIFICATION_CATEGORY_ID", "SAFETY_CATEGORY_ID"]);
  }
}
