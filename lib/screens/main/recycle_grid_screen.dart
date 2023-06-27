import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/screens/application/application_screen.dart';
import 'package:fcd_flutter/screens/faqs/faqs_screen.dart';
import 'package:fcd_flutter/screens/faqs/support_screen.dart';
import 'package:fcd_flutter/screens/library/library_screen.dart';
import 'package:fcd_flutter/screens/licence/licence_screen.dart';
import 'package:fcd_flutter/screens/main/navigation_grid.dart';
import 'package:fcd_flutter/screens/notification/notification_screen.dart';
import 'package:fcd_flutter/screens/payroll/payroll_screen.dart';
import 'package:fcd_flutter/screens/schedule/flight_schedule_md_screen.dart';
import 'package:flutter/material.dart';
import 'package:responsive_grid_list/responsive_grid_list.dart';

import '../admin_notice/admin_notice_screen.dart';
import '../contacts/contacts_screen.dart';
import '../schedule/flight_schedule_screen.dart';
import 'main_controller.dart';

class RecycleGridScreen extends StatelessWidget {
  const RecycleGridScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return StreamBuilder(
      stream: Constants.db.menuHomeDao.getAll(),
      builder: (context, snapshot) {
        if (snapshot.hasData) {
          return ResponsiveGridList(
              horizontalGridSpacing: 10,
              // Horizontal space between grid items
              verticalGridSpacing: 10,
              // Vertical space between grid items
              horizontalGridMargin: 10,
              // Horizontal space around the grid
              verticalGridMargin: 10,
              // Vertical space around the grid
              minItemWidth: 300,
              // The minimum item width (can be smaller, if the layout constraints are smaller)
              minItemsPerRow: 2,
              // The minimum items to show in a single row. Takes precedence over minItemWidth
              maxItemsPerRow: 5,
              // The maximum items to show in a single row. Can be useful on large screens
              listViewBuilderOptions: ListViewBuilderOptions(
                  controller: MainController.instance.scrollController),
              // Options that are getting passed to the ListView.builder() function
              children: snapshot.data!
                  .map((e) => InkResponse(
                        onTap: () {
                          Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) => NavigationGridScreen(childView: redirectToView(e.key))));
                        },
                        child: Container(
                          height: 115,
                          decoration: BoxDecoration(
                            color: const Color(0xFF00485C),
                            borderRadius: BorderRadius.circular(10),
                            border: Border.all(
                              color: const Color(0xFF00485C),
                              width: 10,
                            ),
                          ),
                          child: Center(
                            child: Column(
                                mainAxisAlignment: MainAxisAlignment.center,
                                children: [
                                  SizedBox(
                                    height: 50,
                                    width: 80,
                                    child: Image.asset(
                                        'asset/images/${pathImage(e.key)}.png'),
                                  ),
                                  const SizedBox(
                                    height: 5,
                                  ),
                                  Text(
                                    e.title,
                                    textAlign: TextAlign.center,
                                    style: const TextStyle(
                                        fontSize: 14,
                                        fontWeight: FontWeight.bold,
                                        color: Colors.white),
                                  )
                                ]),
                          ),
                        ),
                      ))
                  .toList()
              // The list of widgets in the list
              );
        } else {
          return Container();
        }
      },
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
        if (Constants.currentUser.userType == 1) {
          return const FlightScheduleScreen();
        } else {
          return FlightScheduleMDScreen();
        }
      case 'Ticket request':
        return 'icon_ticket_booking30';
      case 'Training':
        return 'icon_training';
      case 'Payroll':
        return PayrollScreen();
      case 'Library':
        return LibraryScreen();
      case 'Contacts':
        return ContactScreen();
      case 'FAQs':
        return SupportScreen();
      case 'Report':
        return 'icon_report';
      case 'Application':
        return ApplicationScreen();
      default:
        return NotificationScreen();
    }
  }
}
