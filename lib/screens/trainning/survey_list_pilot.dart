import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/model/api_list.dart';
import 'package:fcd_flutter/base/model/app/survey.dart';
import 'package:fcd_flutter/base/model/app/survey_list_pilot.dart';
import 'package:fcd_flutter/base/widgets/nodata.dart';
import 'package:flutter/material.dart';

class SurveyListPilot extends StatelessWidget {
  SurveyListPilot({Key? key, required this.data}) : super(key: key);
  Survey data;
  bool isCoupleSurvey = false;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: buildBody(),
      floatingActionButton: buildFloatingActionButton(),
    );
  }

  Widget buildBody() {
    return Container(
      child: FutureBuilder(
        future:
            Constants.db.settingDao.findSettingByKey("SurveyCoupleCategory"),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.done) {
            if (snapshot.hasData && snapshot.data != null) {
              if (data.testForm == "1" || data.testForm == "2") {
                isCoupleSurvey = true;
              }
              return dataListPilot();
            } else {
              return  const NoData();
            }
          } else {
            return Center(
              child: CircularProgressIndicator(),
            );
          }
        },
      ),
    );
  }

  dataListPilot() {
    return FutureBuilder(
      future: Constants.api.getSurveyListPilot(
          Constants.sharedPreferences.get('set-cookie').toString(), data.id),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.done) {
         if(snapshot.hasData&& snapshot.data!=null && snapshot.data!.data!.isNotEmpty)
           {
             return Text("data");
           }
         return const NoData();
        } else {
          return Center(child: CircularProgressIndicator(),);
        }
      },
    );
  }

  FloatingActionButton buildFloatingActionButton() {
    return FloatingActionButton(
      shape: const CircleBorder(),
      backgroundColor: const Color(0xFFD09B2C),
      child: const Icon(
        Icons.add,
        color: Colors.white,
      ),
      onPressed: () {},
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
        "Pilot",
        style: TextStyle(color: Colors.white),
      ),
      centerTitle: true,
      backgroundColor: const Color(0xFF006784),
      actions: [
        InkResponse(
          child: Padding(
            padding: EdgeInsets.only(right: 10),
            child: ImageIcon(
              AssetImage('asset/images/icon_editpencil.png'),
              color: Colors.white,
            ),
          ),
          onTap: () {},
        )
      ],
    );
  }
}
