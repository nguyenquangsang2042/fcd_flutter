import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/model/app/pilot_schedule.dart';
import 'package:fcd_flutter/base/widgets/nodata.dart';
import 'package:flutter/material.dart';
import 'package:table_calendar/table_calendar.dart';

class FlightSchedulePilot extends StatelessWidget {
  FlightSchedulePilot({super.key});
  ValueNotifier<bool> isCalendar = ValueNotifier(true);
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(10),
      child: DefaultTabController(
          length: 2,
          child: Column(
            children: [buildHeader(), buildBody()],
          )),
    );
  }

  Widget buildBody() {
    return ValueListenableBuilder(
      valueListenable: isCalendar,
      builder: (context, value, child) {
        return StreamBuilder(
          stream: Constants.db.pilotScheduleDao.getScheduleEvents(),
          builder: (context, snapshot) {
            if(snapshot.connectionState==ConnectionState.active)
              {
                if((snapshot.hasData && snapshot.data!=null) || (snapshot.data==null && value))
                  {
                    return value ? buildTableCalendar(context,snapshot.data) : Container();
                  }
                else
                  {
                    return const NoData();
                  }
              }
            else
              {
                return const Center(child: CircularProgressIndicator(),);
              }
          },
        );
      },
    );
  }

  TableCalendar<dynamic> buildTableCalendar(BuildContext context,List<PilotSchedule>? data) {
    return TableCalendar(
        onDaySelected: (selectedDay, focusedDay) {
          showDialog(context: context, builder: (context) {
            return Container();
          },);
        },
        eventLoader: (day) {
          if (day.weekday == DateTime.monday && data!=null) {
            return data.where((element) => element.id==1).toList();
          }

          return [];
        },
        headerStyle:
            const HeaderStyle(formatButtonVisible: false, titleCentered: true),
        focusedDay: DateTime.now().add(const Duration(days: 7)),
        firstDay: DateTime.now().add(const Duration(days: -365)),
        lastDay: DateTime.now().add(const Duration(days: 365)));
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
                            color:
                                value ? const Color(0xFF006784) : Colors.black),
                      ),
                      onTap: () {
                        isCalendar.value = true;
                      },
                    ),
                    const SizedBox(
                      width: 10,
                    ),
                    InkWell(
                      child: Text(
                        "List",
                        style: TextStyle(
                            color: !value
                                ? const Color(0xFF006784)
                                : Colors.black),
                      ),
                      onTap: () {
                        isCalendar.value = false;
                      },
                    )
                  ],
                ));
          },
        ),
        const Expanded(
          flex: 1,
          child: Text("Daily Flight"),
        )
      ],
    );
  }
}
