import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/student.dart';
import 'package:fcd_flutter/screens/trainning/course_webview.dart';
import 'package:flutter/material.dart';

class CourseScreen extends StatelessWidget {
  CourseScreen({super.key, required this.keySearch});
  String keySearch;
  @override
  Widget build(BuildContext context) {
    return StreamBuilder(
      stream: Constants.db.studentDao.getAllStudents(),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.active) {
          if (snapshot.hasData &&
              snapshot.data != null &&
              snapshot.data!.isNotEmpty) {
            List<Student> data = snapshot.data!;
            if (keySearch.isNotEmpty) {
              data = data
                  .where((element) => element.title
                      .toLowerCase()
                      .contains(keySearch.toLowerCase()))
                  .toList();
            }
            if (data.isNotEmpty) {
              return ListView.builder(
                itemCount: data.length,
                itemBuilder: (context, index) {
                  return InkResponse(
                    child: ListTile(
                      tileColor: index % 2 != 0
                          ? Colors.white
                          : Colors.blueGrey.shade50,
                      title: Text(
                        data[index].title,
                        style: const TextStyle(
                            color: Color(0xFF006784),
                            fontWeight: FontWeight.w500),
                      ),
                      trailing: Text(Functions.instance.formatDateString(
                          data[index].modified, "dd MMM yyyy")),
                      subtitle: Text(data[index].description),
                    ),
                    onTap: () {
                      Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => CourseWebview(
                                    data: data[index],
                                  )));
                    },
                  );
                },
              );
            } else {
              return const Center(
                child: Text("No data"),
              );
            }
          } else {
            return const Center(
              child: Text("No data"),
            );
          }
        } else {
          return const Center(
            child: CircularProgressIndicator(),
          );
        }
      },
    );
  }
}
