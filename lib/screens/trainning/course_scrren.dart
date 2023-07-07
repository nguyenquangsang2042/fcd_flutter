import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/screens/notification/news_screen.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class CourseScreen extends StatelessWidget {
  const CourseScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return StreamBuilder(
      stream: Constants.db.studentDao.getAllStudents(),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.active) {
          if (snapshot.hasData &&
              snapshot.data != null &&
              snapshot.data!.isNotEmpty) {
            return ListView.builder(
              itemCount: snapshot.data!.length,
              itemBuilder: (context, index) {
                return InkResponse(child: ListTile(
                  tileColor: index % 2 != 0 ? Colors.white : Colors.blueGrey.shade50,
                  title: Text(
                    snapshot.data![index].title,
                    style: TextStyle(color: const Color(0xFF006784),fontWeight: FontWeight.w500),
                  ),
                  trailing: Text(Functions.instance.formatDateString(snapshot.data![index].modified, "dd MMM yyyy")),
                  subtitle: Text(snapshot.data![index].description),
                ),onTap: () {
                  // Navigator.push(
                  //     context,
                  //     MaterialPageRoute(
                  //         builder: (context) => NewsScreen(
                  //           notify: snapshot.data![index],
                  //         )));
                },);
              },
            );
          } else {
            return Container(
              child: Center(
                child: Text("No data"),
              ),
            );
          }
        } else {
          return Container(
            child: Center(
              child: CircularProgressIndicator(),
            ),
          );
        }
      },
    );
  }
}
