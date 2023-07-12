import 'package:fcd_flutter/base/model/app/survey.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class DetailExam extends StatelessWidget {
  DetailExam({Key? key, required this.data}) : super(key: key);
  Survey data;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
    );
  }

  AppBar buildAppBar(BuildContext context) {
    return AppBar(
      title: Text(
        data.title,
        style: TextStyle(color: Colors.white),
      ),
      centerTitle: true,
      backgroundColor: const Color(0xFF006784),
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
      actions: [
        if (data.fileExport != null)
          IconButton(
            icon: Image.asset(
              'asset/images/icon_down.png',
              color: Colors.white,
              height: 20,
              width: 20,
            ),
            onPressed: () {
              Navigator.pop(context);
            },
          ),
        IconButton(
          icon: Image.asset(
            'asset/images/icon_menu30.png',
            color: Colors.white,
            height: 20,
            width: 20,
          ),
          onPressed: () {
            Navigator.pop(context);
          },
        )
      ],
    );
  }
}
