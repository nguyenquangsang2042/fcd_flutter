import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/download_file.dart';
import 'package:fcd_flutter/base/functions.dart';
import 'package:fcd_flutter/base/widgets/connectivity_widget.dart';
import 'package:flutter/material.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

import '../../base/alert_dialog.dart';

class PayrollScreen extends StatelessWidget {
  PayrollScreen({super.key});
  ValueNotifier<String> startDate = ValueNotifier(Functions.instance
      .formatDateToStringWithFormat(
          DateTime.now().add(const Duration(days: -365)), "yyyy/MM/dd"));
  ValueNotifier<String> endDate = ValueNotifier(Functions.instance
      .formatDateToStringWithFormat(
          DateTime.now().add(Duration(days: 1)), "yyyy/MM/dd"));
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: ConnectivityWidget(
        offlineWidget: Center(
          child: Text("Vui lòng kết nối lại internet"),
        ),
        onlineWidget: Container(
          child: MultiValueListenableBuilder(
            valueListenables: [startDate, endDate],
            builder: (context, values, child) {
              return FutureBuilder(
                future: Constants.api.getSalaryBetweenTwoDate(
                    Constants.sharedPreferences.get('set-cookie').toString(),
                    startDate.value,
                    endDate.value),
                builder: (context, snapshot) {
                  if (snapshot.hasData && snapshot.data!.data.isNotEmpty) {
                    return ListView.builder(
                      itemCount: snapshot.data!.data.length,
                      itemBuilder: (context, index) {
                        return InkResponse(
                          child: ListTile(
                            tileColor: index % 2 == 0
                                ? Colors.grey.shade50
                                : Colors.white,
                            title: Text(snapshot.data!.data[index].Title),
                            subtitle: Text(Functions.instance.formatDateString(
                                snapshot.data!.data[index].AtDate,
                                "dd MMM yyyy")),
                          ),
                          onTap: () {
                            DownloadFile.downloadFile(
                                    context,
                                    '${Constants.baseURL}/${snapshot.data!.data[index].FilePath}',
                                    snapshot.data!.data[index].FileName)
                                .then((value) {
                              if (value.isNotEmpty) {
                                AlertDialogController.instance.showAlert(
                                    context, "FCD 919", value, "Thoát", () {});
                              }
                            });
                          },
                        );
                      },
                    );
                  } else if (snapshot.connectionState ==
                      ConnectionState.waiting) {
                    return Center(
                      child: Text("Đang tải dữ liệu"),
                    );
                  } else {
                    return Center(
                      child: Text("Không có dữ liệu"),
                    );
                  }
                },
              );
            },
          ),
        ),
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
        "Payroll",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
      actions: [
        Container(
          margin: EdgeInsets.only(right: 15),
          height: 30,
          width: 30,
          child: InkResponse(
            child: Icon(
              Icons.calendar_month,
              color: Colors.white,
            ),
            onTap: () {},
          ),
        )
      ],
    );
  }
}
