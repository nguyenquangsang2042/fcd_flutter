import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/menu_app.dart';
import 'package:fcd_flutter/screens/application/application_webview.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

class ApplicationScreen extends StatelessWidget {
  ApplicationScreen({super.key});
  ValueNotifier<int> langCode = ValueNotifier(1066);
  ValueNotifier<bool> isShowSearch = ValueNotifier(false);
  ValueNotifier<String> searchKey = ValueNotifier("");
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: Column(
        children: [
          ValueListenableBuilder(
            valueListenable: langCode,
            builder: (context, value, child) => Row(
              children: [
                Expanded(
                  flex: 1,
                  child: TextButton(
                      onPressed: () async {
                        langCode.value = 1066;
                      },
                      child: Text(
                        'VN',
                        style: TextStyle(
                            color: langCode.value == 1066
                                ? Color(0xFFDBA40D)
                                : Color(0xFFAAAAAA)),
                      )),
                ),
                Expanded(
                  flex: 1,
                  child: TextButton(
                      onPressed: () {
                        langCode.value = 1033;
                      },
                      child: Text(
                        'EN',
                        style: TextStyle(
                            color: langCode.value != 1066
                                ? Color(0xFFDBA40D)
                                : Color(0xFFAAAAAA)),
                      )),
                ),
              ],
            ),
          ),
          ValueListenableBuilder(
            valueListenable: isShowSearch,
            builder: (context, value, child) {
              return Visibility(visible: value, child: buildTextSearch());
            },
          ),
          MultiValueListenableBuilder(valueListenables: [langCode,searchKey], builder: (context, values, child) {
            return Flexible(child:  StreamBuilder(
              stream: Constants.db.menuAppDao
                  .getMenuAppByStatusAndLanguageID(1, langCode.value),
              builder: (context, snapshot) {
                if (snapshot.hasData) {
                  if (snapshot.data != null) {
                    List<MenuApp>data= snapshot.data!;
                    if(searchKey.value.isNotEmpty)
                      {
                        print(searchKey.value);
                        data=data.where((element) => Functions.instance.removeDiacritics(element.title).toLowerCase().contains(searchKey.value.toLowerCase())).toList();
                      }
                    return ListView.builder(
                      itemCount: data.length,
                      itemBuilder: (context, index) {
                        return InkResponse(child: ListTile(
                          tileColor: index%2==0?Colors.grey.shade100:Colors.white,
                          title: Text(data[index].title.toUpperCase(),style: TextStyle(fontWeight: FontWeight.bold,color: Color(0xFF006784))),
                        ),
                        onTap:(){
                          Future.delayed(Duration(milliseconds: 200),() {
                            Navigator.push(
                                context,
                                MaterialPageRoute(
                                    builder: (context) => ApplicationWebview(data: data[index])));
                          },);
                        },);
                      },
                    );
                  } else {
                    return Container(
                      child: Center(
                        child: Text("Không có dữ liệu",),
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
            ));
          },)
        ],
      ),
    );
  }
  Widget buildTextSearch() {
    TextEditingController contollerSearch = TextEditingController(text: searchKey.value);
    return Container(
      padding: EdgeInsets.all(5.0),
      color: Colors.grey.shade400,
      child: TextField(
        controller: contollerSearch,
        onChanged: (value) {
          searchKey.value = value;
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
              searchKey.value = "";
              contollerSearch.clear();
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
        "Application",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
      actions: [
        Container(
          width: 40,
          height: 40,
          child: IconButton(
            icon: Image.asset(
              'asset/images/icon_search26.png',
              color: Colors.white,
              height: 20,
              width: 40,
            ),
            onPressed: () {
              isShowSearch.value = !isShowSearch.value;
              if (!isShowSearch.value) {
                searchKey.value = "";
              }
            },
          ),
        )
      ],
    );
  }
}
