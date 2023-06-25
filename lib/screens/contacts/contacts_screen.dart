import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/user.dart';
import 'package:fcd_flutter/base/widgets/circle_image_cookie.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:flutter/material.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

import '../../base/model/app/department.dart';

class ContactScreen extends StatelessWidget {
  ContactScreen({super.key});
  ValueNotifier<double> departmentID = ValueNotifier(-1.0);
  ValueNotifier<String> textSearch = ValueNotifier("");
  ValueNotifier<bool> isShowSearch = ValueNotifier(false);
  TextEditingController contollerSearch = TextEditingController(text: "");
  List<Department> listFilter =[];
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: FutureBuilder(
        future: Future.delayed(const Duration(milliseconds: 350)),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.done) {
            return Column(
              children: [
                ValueListenableBuilder(
                  valueListenable: isShowSearch,
                  builder: (context, value, child) {
                    return Visibility(visible: value, child: buildTextSearch());
                  },
                ),
                MultiValueListenableBuilder(
                  valueListenables: [textSearch, departmentID],
                  builder: (context, values, child) {
                    return StreamBuilder(
                      stream: setStreamListUser(),
                      builder: (context, snapshot) {
                        if (snapshot.connectionState ==
                            ConnectionState.active) {
                          if (snapshot.data!.isNotEmpty) {
                            return Flexible(
                                child: ListView.builder(
                              itemCount: snapshot.data!.length,
                              itemBuilder: (context, index) {
                                User item = snapshot.data![index];
                                return ListTile(
                                    tileColor:
                                        snapshot.data!.indexOf(item) % 2 == 0
                                            ? Colors.grey.shade50
                                            : Colors.white,
                                    title: Text(
                                      "${item.fullName}",
                                      style:
                                          TextStyle(color: Color(0xFF006784)),
                                    ),
                                    subtitle: Flexible(
                                      child: Column(
                                        crossAxisAlignment:
                                            CrossAxisAlignment.start,
                                        children: [
                                          Text("CREWCODE: ${item.code2}"),
                                          Text(item.specialContent == null
                                              ? "N/A"
                                              : item.specialContent!),
                                          if (item.mobile != null)
                                            Text("${item.mobile}"),
                                        ],
                                      ),
                                    ),
                                    leading: CircleImageCookie(
                                        imageUrl:
                                            '${Constants.baseURL}/${snapshot.data![index].avatar}?ver=${Functions.instance.formatDateToStringWithFormat(DateTime.now(), "yyyyMMddHHmmss")}',
                                        errImage:
                                            'asset/images/icon_avatar64.png',
                                        width: 35,
                                        height: 35));
                              },
                            ));
                          }
                          return const Expanded(
                              child: Center(
                                child: Text("No data"),
                              ));
                        } else {
                          return const Expanded(
                              child: Center(
                                  child: SpinKitRing(
                                color: Color(0xFF006784),
                                size: 50.0,
                              )));
                        }
                      },
                    );
                  },
                )
              ],
            );
          } else {
            return Expanded(
                child: Container(
              child: const Center(
                  child: SpinKitRing(
                color: Color(0xFF006784),
                size: 50.0,
              )),
            ));
          }
        },
      ),
    );
  }

  Stream<List<User>> setStreamListUser() {
    if (textSearch.value.isEmpty) {
      if (departmentID.value != -1) {
        return Constants.db.userDao.findWithDepartmentID(departmentID.value);
      }
      return Constants.db.userDao.findAll();
    } else {
      if(departmentID.value!=-1)
        {
          return Constants.db.userDao.findByFullnameOrMobileAndDepartment("%${Functions.instance.removeDiacritics(textSearch.value)}%", departmentID.value);
        }
      return Constants.db.userDao.findByFullnameOrMobile("%${Functions.instance.removeDiacritics(textSearch.value)}%");
    }
  }
  Widget buildTextSearch() {
    contollerSearch = TextEditingController(text: textSearch.value);
    return Container(
      padding: EdgeInsets.all(5.0),
      color: Colors.grey.shade400,
      child: TextField(
        controller: contollerSearch,
        onChanged: (value) {
          textSearch.value = value;
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
              textSearch.value = "";
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
        "Contacts",
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
              'asset/images/icon_filter.png',
              color: Colors.white,
              height: 30,
              width: 30,
            ),
            onPressed: () {
              isShowSearch.value = false;

              _showPopupFilter(context);
            },
          ),
        ),
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
                textSearch.value = "";
              }
            },
          ),
        )
      ],
    );
  }
  void _showPopupFilter(BuildContext context) async {
    Constants.db.departmentDao
        .getListDepartmentByEffect(1)
        .listen((event) {
      listFilter = [];
      Department all = Department.none();
      all.id=-1;
      all.title="Đoàn Bay 919";
      all.parentID=1003.0;
      listFilter.add(all);
      listFilter.addAll(event);
      showDialog(
          context: context,
          builder: (_) {
            return Container(
              height: null,
              margin: EdgeInsets.only(top: 55),
              child: Column(
                children: [
                  Flexible(
                    child: ListView.builder(
                        shrinkWrap: true,
                        physics: NeverScrollableScrollPhysics(),
                        itemCount: listFilter.length,
                        itemBuilder: (_, index) {
                          return Material(
                            child: InkResponse(
                              child: ListTile(
                                title: Text("${listFilter[index]!.title}"),
                                leading: Radio(
                                  value: listFilter[index].id,
                                  groupValue: departmentID.value,
                                  onChanged: (value) {
                                    Navigator.of(context).pop();
                                    Future.delayed(Duration(milliseconds: 300),() {
                                      departmentID.value=value!;
                                    },);
                                  },
                                ),
                              ),
                              onTap: () {
                                Navigator.of(context).pop();
                                Future.delayed(Duration(milliseconds: 300),() {
                                  departmentID.value=listFilter[index].id;
                                },);
                              },
                            ),
                          );
                        }),
                  ),
                  Expanded(
                    child: Container(),
                    flex: 1,
                  )
                ],
              ),
            );
          });
    });
  }

}
