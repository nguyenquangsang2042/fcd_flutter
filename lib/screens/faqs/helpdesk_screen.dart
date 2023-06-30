import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/screens/faqs/form_add_helpdesk.dart';
import 'package:fcd_flutter/screens/faqs/reply_helpdesk_screen.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class HelpdeskScreen extends StatelessWidget {
  HelpdeskScreen({super.key, required this.isShowAppBar});
  bool isShowAppBar;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: isShowAppBar?buildAppBar(context): null,
      body: StreamBuilder(
        stream: Constants.db.helpdeskDao.getAllHelpDeskStatusEquals1(),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.active) {
            if (snapshot.hasData && snapshot.data != null) {
              return ListView.builder(
                itemCount: snapshot.data!.length,
                itemBuilder: (context, index) {
                  return GestureDetector(
                    child: ListTile(
                      tileColor:
                          index % 2 != 0 ? Colors.white : Colors.grey.shade50,
                      title: Text(
                        snapshot.data![index].content,
                        style: TextStyle(
                            color: Color(0xFF006784),
                            fontWeight: FontWeight.w500),
                      ),
                      trailing: Text(snapshot.data![index].created != null
                          ? Functions.instance.formatDateString(
                              snapshot.data![index].created!, "dd MMM yyyy")
                          : ""),
                    ),
                    onTap: () {
                      Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => ReplyHelpdeskScreen(
                                    data: snapshot.data![index],
                                  )));
                    },
                  );
                },
              );
            } else {
              return Text("Không có dữ liệu");
            }
          } else {
            return Container(
              child: Center(
                child: CircularProgressIndicator(),
              ),
            );
          }
        },
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) => FormAddHelpDesk()));
        },
        child: Icon(
          Icons.add,
          color: Colors.white,
        ),
        backgroundColor: Color(0xFF006784),
        elevation: 2.0,
        shape: CircleBorder(),
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
      title: Text(
        "HelpDesk",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,

    );
  }

}
