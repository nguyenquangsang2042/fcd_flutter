import 'package:fcd_flutter/screens/schedule/flight_schedule_pilot.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class FlightScheduleScreen extends StatelessWidget {
  const FlightScheduleScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: DefaultTabController(
          length: 2,
          child: Column(
            children: [
              Container(height: 40,child: TabBar(tabs: [
                Text("Flight"),
                Text("Work"),
              ],),),
              Flexible(child: TabBarView(children: [
                FlightSchedulePilot(),
                FlightSchedulePilot(),
              ]))
            ],
          )),
    );
  }

  AppBar buildAppBar(BuildContext context) {
    return AppBar(
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
        "Schedule",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
    );
  }
}
