import 'package:fcd_flutter/base/constanst.dart';
import 'package:flutter/material.dart';

class NotificationScreen extends StatelessWidget {
  NotificationScreen({super.key});

  late List<String> defaultSafety;
  late String keyNews;
  late String beanAnnounceID;

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
        future: setDefaultSafety(),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.done) {
            return Scaffold(
              appBar: AppBar(
                leading: Container(
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
                title: Text(
                  'Safety',
                  style: TextStyle(color: Colors.white, fontSize: 18),
                ),
                backgroundColor: Color(0xFF006784),
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
                    width: MediaQuery.of(context).size.width,
                    child: Row(
                      children: [
                        Expanded(
                          child: TextButton(
                              onPressed: () {}, child: Text('Safety')),
                          flex: 1,
                        ),
                        Expanded(
                          child: TextButton(
                              onPressed: () {}, child: Text('Operation')),
                          flex: 1,
                        ),
                      ],
                    ),
                  )
                ],
              ),
            );
          } else {
            return const Placeholder();
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
        .then((value) => keyNews = value!.VALUE.toString());

    beanAnnounceID = "'${defaultSafety.join("','")}'";
  }
}
