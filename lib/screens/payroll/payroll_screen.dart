import 'package:fcd_flutter/base/functions.dart';
import 'package:fcd_flutter/base/widgets/connectivity_widget.dart';
import 'package:flutter/material.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

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
    print(startDate.value);
    print(endDate.value);
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
              return Container();
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
