import 'package:fcd_flutter/base/model/app/survey.dart';
import 'package:floor/floor.dart';

@dao
abstract class SurveyDao
{
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertSurvey(List<Survey> survey);
}