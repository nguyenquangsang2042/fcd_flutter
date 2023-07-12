import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/model/app/bean_salary.dart';
import 'package:fcd_flutter/base/model/app/survey.dart';
import 'package:fcd_flutter/base/model/app/survey_category.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

class ExamScreen extends StatelessWidget {
  ExamScreen({super.key});
  ValueNotifier<bool> isShowUngrade = ValueNotifier(false);
  ValueNotifier<SurveyCategory> type =
      ValueNotifier(SurveyCategory.all(0, "All"));
  @override
  Widget build(BuildContext context) {
    return Column(
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
                    constraints:
                        const BoxConstraints(minWidth: 100, maxWidth: 100),
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
                          if(snapshot.connectionState==ConnectionState.active)
                            {
                              if(snapshot.hasData&&snapshot.data!.isNotEmpty)
                                {
                                  return PopupMenuButton(
                                    offset: Offset(0, 35),
                                    child: ValueListenableBuilder(valueListenable: type, builder: (context, value, child) {
                                      return Text(type.value.title!);
                                    },),itemBuilder: (context) {
                                      List<SurveyCategory> data = [SurveyCategory.all(0, "All")];
                                      data.addAll(snapshot.data!);
                                    return data.map((e) =>  PopupMenuItem(child: Text(e.title!),onTap: () {
                                      type.value=e;
                                    },)).toList();
                                  },);
                                }
                              return Text("All");
                            }
                          else
                            {
                              return Text("All");
                            }
                        },
                      )
                    ),
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
        Flexible(child: StreamBuilder(stream: Constants.db.surveyDao.getAll(),builder: (context, snapshot) {
          if(snapshot.connectionState==ConnectionState.active)
            {
                return MultiValueListenableBuilder(valueListenables: [type,isShowUngrade], builder: (context, values, child) {
                  if(snapshot.hasData&& snapshot.data!.isNotEmpty)
                  {
                    List<Survey> data = snapshot.data!;
                    if(isShowUngrade.value)
                    {
                      data=data.where((element) => element.point==null|| element.point!.isEmpty).toList();
                    }
                    if(type.value.id!!=0)
                      {
                        data = data.where((element) => element.surveyCategoryId==type.value.id).toList();
                      }
                    if(data.isNotEmpty)
                      {
                        return ListView.builder(itemCount: data.length,itemBuilder: (context, index) {
                          return ListTile(title: Text(data[index].title),);
                        },);
                      }
                    return Container(child: Center(child: Text("No data"),),);
                  }
                  else{
                    return Container(child: Center(child: Text("No data"),),);
                  }
                },);
            }
          else
            {
              return Container(child: Center(child: CircularProgressIndicator(),),);
            }
        },))
      ],
    );
  }
}
