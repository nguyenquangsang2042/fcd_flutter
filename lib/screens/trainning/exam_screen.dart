import 'package:declarative_refresh_indicator/declarative_refresh_indicator.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/bean_salary.dart';
import 'package:fcd_flutter/base/model/app/survey.dart';
import 'package:fcd_flutter/base/model/app/survey_category.dart';
import 'package:fcd_flutter/base/widgets/nodata.dart';
import 'package:fcd_flutter/screens/trainning/detail_exam.dart';
import 'package:fcd_flutter/screens/trainning/survey_list_pilot.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

class ExamScreen extends StatelessWidget {
  ExamScreen({super.key, required this.keySearch});

  String keySearch;
  ValueNotifier<bool> isShowUngrade = ValueNotifier(false);
  ValueNotifier<SurveyCategory> type =
      ValueNotifier(SurveyCategory.all(0, "All"));
  ValueNotifier<bool> isRefresh = ValueNotifier(false);

  @override
  Widget build(BuildContext context) {
    Constants.apiController.updateSurvey();
    return ValueListenableBuilder(
      valueListenable: isRefresh,
      builder: (context, value, child) {
        return DeclarativeRefreshIndicator(
          refreshing: value,
          onRefresh: () async {
            isRefresh.value = true;
            await Constants.apiController.updateSurvey();
            Constants.apiController.updateStudent();
            Constants.apiController.updateNotify();
            Constants.apiController.updateSurveyTable();
            Constants.apiController.updateSurveyCategory();
            isRefresh.value = false;
          },
          child: Column(
            mainAxisSize: MainAxisSize.max,
            children: [
              Container(
                margin: const EdgeInsets.all(10),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  mainAxisSize: MainAxisSize.max,
                  children: [
                    Row(
                      mainAxisSize: MainAxisSize.max,
                      children: [
                        const Text("Type"),
                        Container(
                          constraints: const BoxConstraints(
                              minWidth: 100, maxWidth: 100),
                          margin: const EdgeInsets.only(left: 5, right: 5),
                          decoration: BoxDecoration(
                            border: Border.all(
                              color: Colors.black,
                              width: 1,
                            ),
                          ),
                          child: Padding(
                              padding: EdgeInsets.all(2),
                              child: StreamBuilder(
                                stream: Constants.db.surveyCategoryDao.getAll(),
                                builder: (context, snapshot) {
                                  if (snapshot.connectionState ==
                                      ConnectionState.active) {
                                    if (snapshot.hasData &&
                                        snapshot.data!.isNotEmpty) {
                                      return PopupMenuButton(
                                        offset: Offset(0, 35),
                                        child: ValueListenableBuilder(
                                          valueListenable: type,
                                          builder: (context, value, child) {
                                            return Text(type.value.title!);
                                          },
                                        ),
                                        itemBuilder: (context) {
                                          List<SurveyCategory> data = [
                                            SurveyCategory.all(0, "All")
                                          ];
                                          data.addAll(snapshot.data!);
                                          return data
                                              .map((e) => PopupMenuItem(
                                                    child: Text(e.title!),
                                                    onTap: () {
                                                      type.value = e;
                                                    },
                                                  ))
                                              .toList();
                                        },
                                      );
                                    }
                                    return Text("All");
                                  } else {
                                    return Text("All");
                                  }
                                },
                              )),
                        ),
                      ],
                    ),
                    Row(
                      children: [
                        const Text("Show ungraded only"),
                        ValueListenableBuilder(
                          valueListenable: isShowUngrade,
                          builder: (context, value, child) {
                            return SizedBox(
                              height: 30,
                              child: Transform.scale(
                                scale: 0.7,
                                child: Switch(
                                  value: value,
                                  onChanged: (bool value) {
                                    isShowUngrade.value = value;
                                  },
                                ),
                              ),
                            );
                          },
                        )
                      ],
                    )
                  ],
                ),
              ),
              Container(
                width: MediaQuery.of(context).size.width,
                height: 1,
                color: Colors.grey.shade200,
              ),
              Flexible(
                  child: StreamBuilder(
                stream: Constants.db.surveyDao.getAll(),
                builder: (context, snapshot) {
                  if (snapshot.connectionState == ConnectionState.active) {
                    return MultiValueListenableBuilder(
                      valueListenables: [type, isShowUngrade],
                      builder: (context, values, child) {
                        if (snapshot.hasData && snapshot.data!.isNotEmpty) {
                          List<Survey> data = snapshot.data!;
                          if (keySearch.isNotEmpty) {
                            data = data
                                .where((element) => element.title
                                    .toLowerCase()
                                    .contains(keySearch.toLowerCase()))
                                .toList();
                          }
                          if (isShowUngrade.value) {
                            data = data
                                .where((element) =>
                                    element.point == null ||
                                    element.point!.isEmpty)
                                .toList();
                          }
                          if (type.value.id! != 0) {
                            data = data
                                .where((element) =>
                                    element.surveyCategoryId == type.value.id)
                                .toList();
                          }
                          if (data.isNotEmpty) {
                            return ListView.builder(
                              itemCount: data.length,
                              itemBuilder: (context, index) {
                                return buildItemList(data, index, context);
                              },
                            );
                          }
                          return const NoData();
                        } else {
                          return const NoData();
                        }
                      },
                    );
                  } else {
                    return Container(
                      child: Center(
                        child: CircularProgressIndicator(),
                      ),
                    );
                  }
                },
              ))
            ],
          ),
        );
      },
    );
  }

  InkResponse buildItemList(
      List<Survey> data, int index, BuildContext context) {
    return InkResponse(
      child: ListTile(
        subtitle: StreamBuilder(
          stream: Constants.db.surveyCategoryDao
              .getSurveyCategoryWithID(data[index].surveyCategoryId),
          builder: (context, snapshot) {
            if (snapshot.connectionState == ConnectionState.active) {
              if (snapshot.hasData && snapshot.data != null) {
                return Text("Type: ${snapshot.data!.title}");
              }
              return SizedBox(
                height: 0,
                width: 0,
              );
            }
            return SizedBox(
              height: 0,
              width: 0,
            );
          },
        ),
        tileColor: index % 2 != 0 ? Colors.white : Colors.grey.shade50,
        trailing: Column(
            crossAxisAlignment: CrossAxisAlignment.end,
            mainAxisSize: MainAxisSize.min,
            children: [
              Container(
                margin: EdgeInsets.only(top: 5),
                child: SizedBox(
                  height: 18,
                  width: 18,
                  child: Image.asset(
                      'asset/images/${getImageType(data[index])}.png'),
                ),
              ),
              Spacer(),
              Text(data[index].created == null
                  ? ""
                  : Functions.instance
                      .formatDateString(data[index].created!, "dd MMM yyy"))
            ]),
        title: Text(
          data[index].title,
          style: TextStyle(color: Color(0xFF006784)),
        ),
      ),
      onTap: () {
        Navigator.push(
            context,
            MaterialPageRoute(
                builder: (context) => SurveyListPilot(
                      data: data[index],
                    )));
      },
    );
  }

  String getImageType(Survey survey) {
    if (survey.permissionType == true) // Giao vien
    {
      if (survey.actionStatus == 0 || survey.actionStatus == null) {
        return 'icon_exam_active';
      } else if (survey.actionStatus == 1) {
        return 'icon_view';
      } else {
        return 'icon_exam_complete';
      }
    } else // Hoc vien
    {
      if (survey.actionStatus == 3) {
        return 'icon_exam_complete';
      } else {
        return 'icon_view';
      }
    }
  }
}
