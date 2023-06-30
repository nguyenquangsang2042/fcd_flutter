import 'dart:convert';

import 'package:fcd_flutter/base/alert_dialog.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/model/app/helpdesk.dart';
import 'package:fcd_flutter/base/model/app/helpdesk_linhvuc.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_overlay_loader/flutter_overlay_loader.dart';

class FormAddHelpDesk extends StatelessWidget {
  FormAddHelpDesk({super.key});
  ValueNotifier<HelpDeskLinhVuc?> helpdeskLinhVuc= ValueNotifier(null);
  TextEditingController _controller = TextEditingController(text: "");
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: buildAppBar(context),
        body: GestureDetector(
          child: Column(
            mainAxisSize: MainAxisSize.max,
            children: [
              Text(
                "Please submit your question, admin will answer soon as soon possible. Thank you.",
                style: TextStyle(color: Colors.black),
                textAlign: TextAlign.center,
              ),
              Container(
                margin: EdgeInsets.only(left: 10, top: 10, right: 10),
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(6),
                  border: Border.all(color: Colors.grey),
                ),
                child: Row(
                  children: [
                    ValueListenableBuilder(valueListenable: helpdeskLinhVuc, builder: (context, value, child) {
                      return Padding(
                        padding: EdgeInsets.all(8),
                        child: Text(value ==null?'Select Department':value!.titleEn.trim()),
                      );
                    },),
                    Expanded(child: Container()),
                    // Empty container to push the icon to the right
                    StreamBuilder(
                      stream: Constants.db.helpDeskLinhVucDao
                          .getAllHelpdeskLinhVuc(),
                      builder: (context, snapshot) {
                        if (snapshot.connectionState ==
                            ConnectionState.active) {
                          if (snapshot.hasData && snapshot.data != null) {
                            return PopupMenuButton(
                              offset: Offset(0, 40),
                              icon: Icon(Icons.expand_more),
                              itemBuilder: (BuildContext context) {
                                return snapshot.data!.map((e) =>
                                    PopupMenuItem(
                                      child: Text(e.titleEn),
                                      value: e,
                                    )).toList();
                              },
                              onSelected: (value) {
                                helpdeskLinhVuc.value=value;
                              },
                            );
                          } else {
                            return Container();
                          }
                        }
                        return Container(
                          child: Center(
                            child: CircularProgressIndicator(),
                          ),
                        );
                      },
                    )
                  ],
                ),
              ),
              Container(
                constraints: BoxConstraints(minHeight: 150),
                margin: EdgeInsets.only(left: 10, top: 10, right: 10),
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(6),
                  border: Border.all(color: Colors.grey),
                ),
                child: TextFormField(
                  controller: _controller,
                  validator: (value) {
                    if (value!.isEmpty) {
                      return 'Please enter some text';
                    }
                    return null;
                  },
                  decoration: InputDecoration(
                      border: InputBorder.none,
                      contentPadding: EdgeInsets.all(16),
                      hintText: 'Enter text here',
                      hintStyle: TextStyle(fontWeight: FontWeight.normal)),
                ),
              ),
              Expanded(
                child: Container(
                  color: Colors.transparent,
                  width: double.infinity,
                  height: double.infinity,
                ),
                flex: 1,
              )
            ],
          ),
          onTap: () {
            // hide soft keyboard
            FocusScope.of(context).unfocus();
          },
        ),
        bottomNavigationBar: Container(
          color: Color(0xFF006784),
          child: TextButton(
            child: Text(
              "Send",
              style: TextStyle(color: Colors.white),
            ),
            onPressed: ()=>onPressButtonSend(context),
          ),
        ));
  }

  void onPressButtonSend(BuildContext context) {
    if(helpdeskLinhVuc.value==null)
      {
        AlertDialogController.instance.showAlert(context, "Alert", "Please choose Department!", "Cancel", () { });
      }
    else
      {
        if(_controller.text.isEmpty)
          {
            AlertDialogController.instance.showAlert(context, "Alert", "Please enter your question!", "Cancel", () { });
          }
        else
          {
            Loader.show(context);
            Helpdesk data = Helpdesk.none();
            data.departmentId= helpdeskLinhVuc.value!.id.toInt();
            data.categoryId=helpdeskLinhVuc.value!.id.toInt();
            data.title="More Question";
            data.content=_controller.text;
            data.status=1;
            Constants.api.sendQuestionToHelpDesk(Constants.sharedPreferences.get("set-cookie").toString(), json.encode({
              "DepartmentId":helpdeskLinhVuc.value!.id.toInt(),
              "CategoryId":helpdeskLinhVuc.value!.id.toInt(),
              "Content":_controller.text,
              "Status":1
            })).then((value) async {
              if(value.status.toLowerCase().contains("err"))
                {
                  print(value.mess.value);
                  AlertDialogController.instance.showAlert(context, "Alert", "Something went wrong. Please try late!", "Cancel", () { });
                }
              else
                {
                  await Constants.apiController.updateHelpdesk();
                  Navigator.pop(context);
                }
              Loader.hide();
            });
          }
      }
  }

  AppBar buildAppBar(BuildContext context) {
    return AppBar(
      leading: SizedBox(
        width: 50,
        height: 50,
        child: IconButton(
          icon: Image.asset(
            'asset/images/icon_back30.png',
            color: Colors.black,
            height: 20,
            width: 40,
          ),
          onPressed: () {
            Navigator.pop(context);
          },
        ),
      ),
      title: const Text(
        "More Questions",
        style: TextStyle(color: Colors.black, fontWeight: FontWeight.w500),
      ),
      centerTitle: true,
    );
  }
}
