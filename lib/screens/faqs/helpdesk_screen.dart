import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/screens/faqs/form_add_helpdesk.dart';
import 'package:fcd_flutter/screens/faqs/reply_helpdesk_screen.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class HelpdeskScreen extends StatelessWidget {
  const HelpdeskScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
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
}
