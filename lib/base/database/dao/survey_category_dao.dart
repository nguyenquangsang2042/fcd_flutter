import 'package:fcd_flutter/base/model/app/survey_category.dart';
import 'package:floor/floor.dart';

@dao
abstract class SurveyCategoryDao{
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertSurveyCategory(List<SurveyCategory> survey_category);
  @Query('Delete From SurveyCategory')
  Future<void> deleteAll();
}