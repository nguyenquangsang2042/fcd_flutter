import 'package:fcd_flutter/base/model/app/bean_salary.dart';
import 'package:fcd_flutter/base/model/app/survey_category.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class ExamScreen extends StatelessWidget {
  ExamScreen({super.key});
  ValueNotifier<bool> isShowUngrade= ValueNotifier(false);
  ValueNotifier<SurveyCategory> type= ValueNotifier(SurveyCategory.all(0, "All"));
  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisSize: MainAxisSize.max,
      children: [
        Container(

          margin: const EdgeInsets.all(10),child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          mainAxisSize: MainAxisSize.max,
          children: [
            Row(
              mainAxisSize: MainAxisSize.max,
              children: [
              const Text("Type"),
              Container(
                constraints: const BoxConstraints(
                  minWidth: 100,
                  maxWidth: 100
                ),
                margin: EdgeInsets.only(left: 5,right: 5),
                decoration: BoxDecoration(
                  border: Border.all(
                    color: Colors.black,
                    width: 1,
                  ),
                ),
                child: Padding(child: Text(type.value.title!),padding: EdgeInsets.all(2),),
              ),
            ],),

            Row(children: [
              const Text("Show ungraded only"),
              ValueListenableBuilder(valueListenable: isShowUngrade, builder: (context, value, child) {
                return SizedBox(height: 30,child: Transform.scale(scale: 0.7,child: Switch(
                  value: value,
                  onChanged: (bool value) {
                    isShowUngrade.value=value;
                  },
                ),),);
              },)
            ],)

          ],
        ),),
        Container(width: MediaQuery.of(context).size.width,height: 1,color: Colors.grey.shade200,),

      ],);
  }
}
