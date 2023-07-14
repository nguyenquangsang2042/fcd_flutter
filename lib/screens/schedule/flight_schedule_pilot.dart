import 'package:flutter/material.dart';
import 'package:table_calendar/table_calendar.dart';

class FlightSchedulePilot extends StatelessWidget {
  FlightSchedulePilot({super.key});
  ValueNotifier<bool> isCalendar = ValueNotifier(true);
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.all(10),
      child: DefaultTabController(
          length: 2,
          child: Column(
            children: [buildHeader(), buildBody()],
          )),
    );
  }

  Widget buildBody() {
    return ValueListenableBuilder(valueListenable: isCalendar, builder: (context, value, child) {
      return value?buildTableCalendar():Container();
    },);
  }

  TableCalendar<dynamic> buildTableCalendar() {
    return TableCalendar(
      headerStyle: HeaderStyle(
        formatButtonVisible: false,
        titleCentered: true
      ),
      focusedDay: DateTime.now().add(Duration(days: 7)),
      firstDay: DateTime.now().add(Duration(days: -365)),
      lastDay: DateTime.now().add(Duration(days: 365)));
  }

  Row buildHeader() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      mainAxisSize: MainAxisSize.max,
      children: [
        ValueListenableBuilder(
          valueListenable: isCalendar,
          builder: (context, value, child) {
            return Expanded(
                flex: 3,
                child: Row(
                  children: [
                    InkWell(
                      child: Text(
                        "Calendar",
                        style: TextStyle(
                            color: value ? Color(0xFF006784) : Colors.black),
                      ),
                      onTap: () {
                        isCalendar.value = true;
                      },
                    ),
                    SizedBox(
                      width: 10,
                    ),
                    InkWell(
                      child: Text(
                        "List",
                        style: TextStyle(
                            color: !value ? Color(0xFF006784) : Colors.black),
                      ),
                      onTap: () {
                        isCalendar.value = false;
                      },
                    )
                  ],
                ));
          },
        ),
        Expanded(
          flex: 1,
          child: Text("Daily Flight"),
        )
      ],
    );
  }
}
