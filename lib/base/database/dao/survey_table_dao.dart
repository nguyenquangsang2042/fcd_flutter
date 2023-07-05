import 'package:fcd_flutter/base/model/app/survey_table.dart';
import 'package:floor/floor.dart';

@dao
abstract class SurveyTableDao
{
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertSurveyTable(List<SurveyTable> surveyTable);
}