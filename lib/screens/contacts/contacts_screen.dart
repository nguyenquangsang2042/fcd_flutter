import 'package:fcd_flutter/base/constants.dart';
import 'package:flutter/material.dart';

class ContactScreen extends StatelessWidget {
  const ContactScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: Column(
        children: [
          StreamBuilder(
            stream: Constants.db.userDao.findAll(),
            builder: (context, snapshot) {
              if(snapshot.connectionState == ConnectionState.active)
                {
                  if(snapshot.data!.isNotEmpty)
                    {
                      return Flexible(child: ListView.builder(
                          itemCount: snapshot.data!.length,
                          itemBuilder: (context, index) {
                            return Text("data $index");
                          },));
                    }
                  return Expanded(child: Container(child: Center(child: Text("No data"),),));
                }
              else {
                return Expanded(child: Container(
                  child: Center(child: CircularProgressIndicator(),),));
              }
            },
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
      title: const Text("Contacts",style: TextStyle(color: Colors.white,fontWeight: FontWeight.bold),),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
    );
  }

}
