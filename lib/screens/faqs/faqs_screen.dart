import 'package:fcd_flutter/base/widgets/language_switch.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class FaqsScreen extends StatelessWidget {
  FaqsScreen({Key? key}) : super(key: key);
  ValueNotifier<bool> isVietnamese = ValueNotifier(true);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
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
        "Faqs",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
      actions: [
        InkResponse(
          child: Container(
            height: 20,
            width: 20,
            margin: EdgeInsets.only(right: 10),
            child: ValueListenableBuilder(
              valueListenable: isVietnamese,
              builder: (context, value, child) {
                return Image.asset(value
                    ? 'asset/images/icon_lang_united_kingdom.png'
                    : 'asset/images/icon_lang_vietnam.png');
              },
            ),
          ),
          onTap: () {
            isVietnamese.value = !isVietnamese.value;
          },
        ),
        Container(
            child: Icon(
              Icons.search,
              color: Colors.white,
            ),
            margin: EdgeInsets.only(right: 10))
      ],
    );
  }
}
