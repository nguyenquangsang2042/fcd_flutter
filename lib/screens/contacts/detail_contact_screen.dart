import 'package:fcd_flutter/base/download_file.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/user.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:flutter/material.dart';

import '../../base/constants.dart';

class DetailContactScreen extends StatelessWidget {
  DetailContactScreen({super.key, required this.info});
  User info;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: buildInfo(context),
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
        "Contacts",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
    );
  }

  Column buildInfo(context) {
    return Column(
      children: [
        Align(
          alignment: Alignment.center,
          child: Container(
            margin: EdgeInsets.only(top: 10),
            child: SizedBox(
              height: 90,
              width: 90,
              child: InkResponse(child: ImageWithCookie(
                  imageUrl: '${Constants.baseURL}/${info.avatar}',
                  errImage: 'asset/images/icon_avatar64.png'),
              onTap: (){
                String? fileName ="${info.avatar?.split("/")[info.avatar!.split("/").length-2]}${info.avatar!.split("/").last}";
                DownloadFile.downloadFile(context, '${Constants.baseURL}/${info.avatar}', fileName);
              },),),
          ),
        ),
      ],
    );
  }
}
