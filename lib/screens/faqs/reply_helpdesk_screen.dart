import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/helpdesk.dart';
import 'package:fcd_flutter/base/widgets/circle_image_cookie.dart';
import 'package:fcd_flutter/base/widgets/html_content_widget.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class ReplyHelpdeskScreen extends StatelessWidget {
  ReplyHelpdeskScreen({super.key, required this.data});

  Helpdesk data;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: Column(
        children: [
          ListTile(
            leading: const CircleImageCookie(width: 40,height: 40,errImage: 'asset/images/icon_avatar64.png',imageUrl: 'asset/images/icon_avatar64.png'),
            title: Text(
              Constants.currentUser.fullName!,
              style:
                  const TextStyle(color: Colors.black, fontWeight: FontWeight.w500),
            ),
            subtitle: Column(
              children: [
                Align(
                  alignment: Alignment.centerLeft,
                  child: Text(
                    data.content,
                    style: TextStyle(color: Colors.black),
                  ),
                ),
                const SizedBox(
                  height: 10,
                ),
                if (data.created != null && data.created!.isNotEmpty)
                  Align(
                    alignment: Alignment.centerLeft,
                    child: Text(
                      "Date created - ${ Functions.instance
                          .formatDateString(data.created!, "dd/MM/yy HH:mm")}",
                      style: TextStyle(color: Colors.black),
                    ),
                  ),
              ],
            ),
          ),
          ListTile(
            tileColor: Colors.grey.shade50,
            leading: const CircleImageCookie(width: 40,height: 40,errImage: 'asset/images/icon_user2.png',imageUrl: 'asset/images/icon_user2.png'),
            title: const Text(
              "Admin",
              style:
              TextStyle(color: Colors.black, fontWeight: FontWeight.w500),
            ),
            subtitle: Column(
              children: [
                Align(
                  alignment: Alignment.centerLeft,
                  child: data.replyContent!=null?HtmlContentWidget(htmlContent: data.replyContent!):Text("Your request is processing..."),
                ),
                const SizedBox(
                  height: 10,
                ),
                if (data.dateReply != null && data.dateReply!.isNotEmpty)
                  Align(
                    alignment: Alignment.centerLeft,
                    child: Text("Reply date - ${Functions.instance
                        .formatDateString(data.dateReply!, "dd/MM/yy HH:mm")}"
                      ,
                      style: const TextStyle(color: Colors.black),
                    ),
                  ),
              ],
            ),
          )
        ],
      ),
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
        "Helpdesk",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.w500),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
    );
  }
}
