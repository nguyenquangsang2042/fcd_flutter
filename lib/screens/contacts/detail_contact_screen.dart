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
        Container(
          margin: EdgeInsets.all(10),
          child: SingleChildScrollView(child:
          Column(
            mainAxisSize: MainAxisSize.max,
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              Container(
                width: double.infinity,
                child: Text("Phone"),),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(info.mobile!=null? info.mobile!: "",style: TextStyle(color: Color(0xFF006784)),),
                  Row(
                    children: [
                      Container(margin: EdgeInsets.only(right: 5), height: 20,width: 20,child: InkResponse(child: Image.asset("asset/images/icon_message_textting.png",color: Colors.deepOrange,),onTap: () {
                        Functions.instance.launchCustomUrl('sms', info.mobile);
                      },),),
                      Container(margin: EdgeInsets.only(right: 5),height: 20,width: 20,child: InkResponse(child: Image.asset("asset/images/icon_phone.png",color: Colors.green,),onTap: () {
                        Functions.instance.launchCustomUrl('tel', info.mobile);
                      },),),
                      Container(margin: EdgeInsets.only(right: 5),height: 20,width: 20,child: InkResponse(child: Image.asset("asset/images/icon_viber.png",),onTap: () {
                        Functions.instance.launchVibe(info.mobile.toString());
                      },),),
                      Container(margin: EdgeInsets.only(right: 5),height: 20,width: 20,child: InkResponse(child: Image.asset("asset/images/icon_zalo.png",),onTap: () {
                        Functions.instance.launchZalo(info.mobile.toString());
                      },),),
                    ],
                  )
                ],
              ),
              Container(margin: EdgeInsets.only(top: 5,bottom: 5),width: double.infinity,height: 0.5,color: Colors.grey,),
              Container(
                width: double.infinity,
                child: Text("Email"),),
              InkResponse(child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Container(
                    child: Text(info.mobile!=null? info.email!: "",style: TextStyle(color: Color(0xFF006784)),),
                  ),
                  Container(
                      height: 20,width: 20,
                      child: Image.asset('asset/images/icon_email.png')),

                ],
              ),
              onTap: () {
                Functions.instance.launchCustomUrl("mailto", info.email);
              },),
              Container(margin: EdgeInsets.only(top: 5,bottom: 5),width: double.infinity,height: 0.5,color: Colors.grey,),
              Container(width: double.infinity,child: Text("Position: ${info.positionName!=null?info.positionName:"N/A"}")),
              Container(margin: EdgeInsets.only(top: 5,bottom: 5),width: double.infinity,height: 0.5,color: Colors.grey,),
              Container(width: double.infinity,child: Text("Department: ${info.departmentName!=null?info.departmentName:"N/A"}")),
              Container(margin: EdgeInsets.only(top: 5,bottom: 5),width: double.infinity,height: 0.5,color: Colors.grey,),
              Container(width: double.infinity,child: Text("Base: ${info.base!=null?info.base:"N/A"}")),
              Container(margin: EdgeInsets.only(top: 5,bottom: 5),width: double.infinity,height: 0.5,color: Colors.grey,),
              Container(width: double.infinity,child: Text("My id travel account : ${info.code3!=null?info.code3:"N/A"}")),
              Container(margin: EdgeInsets.only(top: 5,bottom: 5),width: double.infinity,height: 0.5,color: Colors.grey,),
              Container(width: double.infinity,child: Text("Crew code : ${info.code2!=null?info.code2:"N/A"}")),
              Container(margin: EdgeInsets.only(top: 5,bottom: 5),width: double.infinity,height: 0.5,color: Colors.grey,),
              Container(width: double.infinity,child: Text("IDNumber : ${info.idNumber!=null?info.idNumber:"N/A"}")),
              Container(margin: EdgeInsets.only(top: 5,bottom: 5),width: double.infinity,height: 0.5,color: Colors.grey,),
              Container(width: double.infinity,child: Text("Special Content : ${info.specialContent!=null?info.specialContent:"N/A"}")),
              Container(margin: EdgeInsets.only(top: 5,bottom: 5),width: double.infinity,height: 0.5,color: Colors.grey,),

            ],
          ),),
        )
      ],
    );
  }
}
