import 'package:fcd_flutter/base/model/app/survey.dart';
import 'package:floor/floor.dart';

@dao
abstract class SurveyDao
{
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertSurvey(List<Survey> survey);
  @Query('SELECT * FROM Survey where (Status <> -1 or Status <> -2)  ORDER BY Created DESC')
  Stream<List<Survey>> getAll();
}