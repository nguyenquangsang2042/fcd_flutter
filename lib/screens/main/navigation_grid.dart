import 'package:awesome_bottom_bar/awesome_bottom_bar.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/menu_home.dart';
import 'package:fcd_flutter/screens/admin_notice/admin_notice_screen.dart';
import 'package:fcd_flutter/screens/application/application_screen.dart';
import 'package:fcd_flutter/screens/contacts/contacts_screen.dart';
import 'package:fcd_flutter/screens/faqs/support_screen.dart';
import 'package:fcd_flutter/screens/library/library_screen.dart';
import 'package:fcd_flutter/screens/licence/licence_screen.dart';
import 'package:fcd_flutter/screens/notification/notification_screen.dart';
import 'package:fcd_flutter/screens/payroll/payroll_screen.dart';
import 'package:fcd_flutter/screens/report/report_screen.dart';
import 'package:fcd_flutter/screens/trainning/parent_training.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class NavigationGridScreen extends StatelessWidget {
  NavigationGridScreen({super.key, required this.childView});

  ValueNotifier<int> indexSelected = ValueNotifier(0);
  Widget childView;
  late ValueNotifier<Widget> currentView;

  @override
  Widget build(BuildContext context) {
    currentView = ValueNotifier(childView);
    if (childView is NotificationScreen) {
      indexSelected.value = 1;
    } else if (childView is AdminNoticeScreen) {
      indexSelected.value = 2;
    } else {
      indexSelected.value = 4;
    }
    return Scaffold(
      body: ValueListenableBuilder(
        valueListenable: currentView,
        builder: (context, value, child) {
          return currentView.value;
        },
      ),
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
              if (value != 4) indexSelected.value = value;
              switch (value) {
                case 0:
                  Navigator.pop(context);
                  break;
                case 1:
                  currentView.value = NotificationScreen();
                  break;
                case 2:
                  currentView.value = AdminNoticeScreen();
                  break;
                case 3:
                  //schedule chua lam
                  break;
                default:
                  buildShowDialog(context);
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

  Future<dynamic> buildShowDialog(BuildContext context) {
    return showDialog(
      builder: (context) {
        return Container(
          margin: EdgeInsets.only(bottom: 60),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              const Spacer(),
              StreamBuilder(
                stream: Constants.db.menuHomeDao.getAll(),
                builder: (context, snapshot) {
                  if (snapshot.connectionState == ConnectionState.active) {
                    if (snapshot.hasData && snapshot.data!.isNotEmpty) {
                      List<MenuHome> data = snapshot.data!;
                      data = data
                          .where((element) =>
                              !element.key.contains("Safety") &&
                              !element.key.contains("News") &&
                              !element.key.contains("Schedule"))
                          .toList();
                      return Container(
                        child: Material(
                            child: Column(
                          children: data
                              .map((e) => InkResponse(
                                    child: ListTile(
                                      leading: Icon(Icons.holiday_village),
                                      title: Text(e.title),
                                    ),
                                    onTap: () {
                                      Navigator.pop(context);
                                      indexSelected.value=4;
                                      currentView.value = redirectToView(e.key);
                                    },
                                  ))
                              .toList(),
                        )),
                      );
                    }
                    return SizedBox(
                      height: 0,
                      width: 0,
                    );
                  }
                  return SizedBox(
                    height: 0,
                    width: 0,
                  );
                },
              ),
              Container(
                constraints: BoxConstraints(
                  minWidth: MediaQuery.of(context).size.width - 20,
                ),
                child: ElevatedButton(
                    onPressed: () {
                      Navigator.pop(context);
                    },
                    child: Text("Cancel")),
              )
            ],
          ),
        );
      },
      context: context,
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

  String pathImage(String key) {
    switch (key) {
      case 'Safety':
        return 'icon_shield2';
      case 'News':
        return 'ic_news';
      case 'Licence':
        return 'icon_lisence30';
      case 'Schedule':
        return 'icon_plane30Gray';
      case 'Ticket request':
        return 'icon_ticket_booking30';
      case 'Training':
        return 'icon_training';
      case 'Payroll':
        return 'icon_payroll';
      case 'Library':
        return 'icon_library30';
      case 'Contacts':
        return 'icon_user2';
      case 'FAQs':
        return 'icon_FAQs';
      case 'Report':
        return 'icon_report';
      case 'Application':
        return 'ic_menu_application';
      default:
        return 'icon_shield2';
    }
  }

  redirectToView(String key) {
    switch (key) {
      case 'Safety':
        return NotificationScreen();
      case 'News':
        return AdminNoticeScreen();
      case 'Licence':
        return LicenceScreen();
      case 'Schedule':
        // if (Constants.currentUser.userType == 1) {
        //   return const FlightScheduleScreen();
        // } else {
        //   return FlightScheduleMDScreen();
        // }
        return "";
      case 'Ticket request':
        // return TicketRequestScreen();
        return "";
      case 'Training':
        return ParentTrainingScreen();
      case 'Payroll':
        return PayrollScreen();
      case 'Library':
        return LibraryScreen();
      case 'Contacts':
        return ContactScreen();
      case 'FAQs':
        return SupportScreen();
      case 'Report':
        return ReportScreen();
      case 'Application':
        return ApplicationScreen();
      default:
        return NotificationScreen();
    }
  }

  Future<List<Setting>> getData() async {
    return Constants.db.settingDao.getListSettingInLstKey(
        ["QUALIFICATION_CATEGORY_ID", "SAFETY_CATEGORY_ID"]);
  }
}
