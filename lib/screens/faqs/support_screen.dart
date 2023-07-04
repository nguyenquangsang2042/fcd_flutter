import 'package:fcd_flutter/screens/faqs/faqs_screen.dart';
import 'package:fcd_flutter/screens/faqs/helpdesk_screen.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

class SupportScreen extends StatelessWidget {
  SupportScreen({super.key});

  ValueNotifier<bool> isVietnamese = ValueNotifier(true);
  ValueNotifier<bool> isShowLang = ValueNotifier(true);
  ValueNotifier<bool> isShowSearch = ValueNotifier(false);
  ValueNotifier<String> keySearch = ValueNotifier("");
  TextEditingController _controller = TextEditingController(text: "");

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: DefaultTabController(
          length: 2,
          child: Column(
            children: [
              TabBar(
                tabs: [
                  Tab(icon: Text("Faqs")),
                  Tab(icon: Text("Helpdesk")),
                ],
                onTap:(value) {
                  isShowLang.value=(value==0);
                },
              ),
              ValueListenableBuilder(
                valueListenable: isShowSearch,
                builder: (context, value, child) {
                  return Visibility(visible: value, child: buildTextSearch());
                },
              ),
              Flexible(child: TabBarView(
                  children: [
                    MultiValueListenableBuilder(
                      valueListenables: [isVietnamese, keySearch],
                      builder: (context, values, child) {
                        return FaqsScreen(isVietnamese: isVietnamese.value,
                          keySearch: keySearch.value,);
                      },
                    ),
                    ValueListenableBuilder(valueListenable: keySearch,
                      builder: (context, value, child) {
                        return HelpdeskScreen(
                          isShowAppBar: false, keySearch: keySearch.value,);
                      },)
                  ]))
            ],
          )),
    );
  }

  Widget buildTextSearch() {
    _controller = TextEditingController(text: keySearch.value);
    return Container(
      padding: EdgeInsets.all(5.0),
      color: Colors.grey.shade400,
      child: TextField(
        controller: _controller,
        onChanged: (value) {
          keySearch.value = value;
        },
        decoration: InputDecoration(
          filled: true,
          fillColor: Colors.white,
          contentPadding: EdgeInsets.symmetric(horizontal: 10.0),
          hintText: "Search",
          prefixIcon: Icon(Icons.search),
          suffixIcon: IconButton(
            icon: Icon(Icons.cancel),
            onPressed: () {
              keySearch.value = "";
              _controller.clear();
            }, // Replace with delete functionality
          ),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(6.0),
            borderSide: BorderSide.none,
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
        "Faqs",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
      actions: [
        ValueListenableBuilder(
          valueListenable: isShowLang, builder: (context, value, child) {
          return Visibility(visible: value, child: InkResponse(
            child: Container(
              height: 20,
              width: 20,
              margin: EdgeInsets.only(right: 10),
              child: ValueListenableBuilder(
                valueListenable: isVietnamese,
                builder: (context, value, child) {
                  return Image.asset(!value
                      ? 'asset/images/icon_lang_united_kingdom.png'
                      : 'asset/images/icon_lang_vietnam.png');
                },
              ),
            ),
            onTap: () {
              isVietnamese.value = !isVietnamese.value;
            },
          ),);
        },),
        InkResponse(child: Container(
            child: Icon(
              Icons.search,
              color: Colors.white,
            ),
            margin: EdgeInsets.only(right: 10)), onTap: () {
          isShowSearch.value = !isShowSearch.value;
        },)
      ],
    );
  }
}
