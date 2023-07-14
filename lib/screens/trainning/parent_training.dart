import 'package:declarative_refresh_indicator/declarative_refresh_indicator.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/announcement_category.dart';
import 'package:fcd_flutter/screens/trainning/course_scrren.dart';
import 'package:fcd_flutter/screens/trainning/exam_screen.dart';
import 'package:fcd_flutter/screens/trainning/notice_training.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

class ParentTrainingScreen extends StatelessWidget {
  ParentTrainingScreen({super.key});

  ValueNotifier<bool> isShowSearch = ValueNotifier(false);
  ValueNotifier<String> keyWord = ValueNotifier("");
  ValueNotifier<int> currentPage = ValueNotifier(0);
  List<String> trainingNotify = ["1000", "1010", "5"];
  late List<AnnouncementCategory> listFilter;
  ValueNotifier<List<String>> defaultSafety = ValueNotifier([]);
  int _groupValue = 0;
  ValueNotifier<String> sortType = ValueNotifier("");
  String _groupValueSort = "Date";
  TextEditingController contollerSearch = TextEditingController(text: '');
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: DefaultTabController(
        length: 3,
        child: Column(
          children: [
            TabBar(
              tabs: [
                Tab(icon: Text("Notice")),
                Tab(icon: Text("Course")),
                Tab(icon: Text("Exam")),
              ],
              onTap: (value) {
                currentPage.value = value;
              },
            ),
            ValueListenableBuilder(
              valueListenable: isShowSearch,
              builder: (context, value, child) {
                return Visibility(visible: value, child: buildTextSearch());
              },
            ),
            Flexible(
              child: MultiValueListenableBuilder(valueListenables: [keyWord,currentPage],builder: (context, values, child) {
                return PageView.builder(
                  itemCount: 3,
                  itemBuilder: (context, index) {
                    return [
                      NoticeTrainingScreen(
                          lstAnnounCategoryId: trainingNotify,keyWord: keyWord.value),
                      CourseScreen(keySearch: keyWord.value,),
                      ExamScreen(keySearch: keyWord.value),
                    ][currentPage.value];
                  },
                );
              },),
            )
          ],
        ),
      ),
    );
  }
  Widget buildTextSearch() {
    contollerSearch = TextEditingController(text: keyWord.value);
    return Container(
      padding: EdgeInsets.all(5.0),
      color: Colors.grey.shade400,
      child: TextField(
        controller: contollerSearch,
        onChanged: (value) {
          keyWord.value = value;
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
              keyWord.value = "";
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

  void _showPopupSort(BuildContext context) async {
    List<String> lstSortType = ["Date", "Unread", "Emergency", "Confirm"];
    showDialog(
        context: context,
        builder: (_) {
          return Container(
            height: null,
            margin: EdgeInsets.only(top: 55),
            child: Column(
              children: [
                ListView.builder(
                    shrinkWrap: true,
                    physics: NeverScrollableScrollPhysics(),
                    itemCount: lstSortType.length,
                    itemBuilder: (_, index) {
                      return Material(
                          child: InkResponse(
                        onTap: () {
                          Navigator.of(context).pop();
                          sortType.value = lstSortType[index];
                          //streamList.value = setStreamGetData();
                          _groupValueSort = lstSortType[index];
                        },
                        child: ListTile(
                          title: Text(lstSortType[index]),
                          leading: Radio(
                            value: lstSortType[index],
                            groupValue: _groupValueSort,
                            onChanged: (value) {
                              Navigator.of(context).pop();
                              sortType.value = lstSortType[index];
                              //streamList.value = setStreamGetData();
                              _groupValueSort = lstSortType[index];
                            },
                          ),
                        ),
                      ));
                    }),
                Expanded(
                  child: Container(),
                  flex: 1,
                )
              ],
            ),
          );
        });
  }

  void _showPopupFilter(BuildContext context) async {
    Constants.db.announcementCategoryDao
        .getAnnouncementCategoryInListID(trainingNotify)
        .listen((event) {
      listFilter = [];
      listFilter.add(AnnouncementCategory.none(0, "All"));
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
                                  groupValue: _groupValue,
                                  onChanged: (value) {
                                    Navigator.of(context).pop();
                                    if (value == 0) {
                                      defaultSafety.value
                                          .addAll(trainingNotify);
                                    } else {
                                      defaultSafety.value = [value.toString()];
                                    }
                                    //streamList.value = setStreamGetData();
                                    _groupValue = value!;
                                  },
                                ),
                              ),
                              onTap: () {
                                Navigator.of(context).pop();
                                if (listFilter[index].id == 0) {
                                  defaultSafety.value.addAll(trainingNotify);
                                } else {
                                  defaultSafety.value = [
                                    listFilter[index].id.toString()
                                  ];
                                }
                                //streamList.value = setStreamGetData();
                                _groupValue = listFilter[index].id;
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
        'Training',
        style: TextStyle(color: Colors.white, fontSize: 18),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
      actions: [
        ValueListenableBuilder(
          valueListenable: currentPage,
          builder: (context, value, child) {
            return Visibility(
                visible: value == 0,
                child: Container(
                  width: 40,
                  height: 40,
                  child: IconButton(
                    icon: Image.asset(
                      'asset/images/icon_filter.png',
                      color: Colors.white,
                      height: 20,
                      width: 30,
                    ),
                    onPressed: () {
                      isShowSearch.value = false;
                      _showPopupFilter(context);
                    },
                  ),
                ));
          },
        ),
        ValueListenableBuilder(
          valueListenable: currentPage,
          builder: (context, value, child) {
            return Visibility(
                visible: value == 0 ,
                child: Container(
                  width: 40,
                  height: 40,
                  child: IconButton(
                    icon: Image.asset(
                      'asset/images/icon_sort_descending.png',
                      color: Colors.white,
                      height: 20,
                      width: 30,
                    ),
                    onPressed: () {
                      isShowSearch.value = false;
                      _showPopupSort(context);
                    },
                  ),
                ));
          },
        ),
        Container(
          width: 40,
          height: 40,
          child: IconButton(
            icon: Image.asset(
              'asset/images/icon_search26.png',
              color: Colors.white,
              height: 20,
              width: 30,
            ),
            onPressed: () {
              isShowSearch.value = !isShowSearch.value;
              if (!isShowSearch.value) {
                keyWord.value = "";
                //streamList.value = setStreamGetData();
              }
            },
          ),
        ),
      ],
    );
  }
}
