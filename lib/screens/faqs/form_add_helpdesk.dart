import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class FormAddHelpDesk extends StatelessWidget {
  const FormAddHelpDesk({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: buildAppBar(context),
        body: GestureDetector(child: Column(
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
                  Padding(
                    padding: EdgeInsets.all(8),
                    child: Text('Select Department'),
                  ),
                  Expanded(child: Container()),
                  // Empty container to push the icon to the right
                  Padding(
                    padding: EdgeInsets.all(8),
                    child: InkWell(
                      onTap: () {
                        // Handle icon tap
                        // Show popup menu
                      },
                      child: Icon(Icons.expand_more),
                    ),
                  ),
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
              child: TextField(
                decoration: InputDecoration(
                    border: InputBorder.none,
                    contentPadding: EdgeInsets.all(16),
                    hintText: 'Enter text here',
                    hintStyle: TextStyle(fontWeight: FontWeight.normal)),
              ),
            ),
            Expanded(child: Container(color: Colors.transparent, width:double.infinity,height: double.infinity,),flex: 1,)
          ],
        ), onTap: () {
          // hide soft keyboard
          FocusScope.of(context).unfocus();
        },),
        bottomNavigationBar: Container(
          color: Color(0xFF006784),
          child: TextButton(
            child: Text("Send",style: TextStyle(color: Colors.white),),
            onPressed: () {},
          ),
        ));
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
