import 'package:flutter/material.dart';

class NoData extends StatelessWidget {
  const NoData({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(child: Column(
      mainAxisSize: MainAxisSize.min,
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        SizedBox(height: 90, width: 90, child:
          Image.asset('asset/images/icon_list_empty.png',color: Colors.grey,)),
        const Text("Data Not Found",style: TextStyle(color: Colors.grey),)
      ],
    ),);
  }
}
