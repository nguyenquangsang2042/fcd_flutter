
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:fcd_flutter/base/model/app/help_desk_category.dart';
import 'package:floor/floor.dart';

@dao
abstract class HelpDeskCategoryDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertHelpDeskCategory(List<HelpDeskCategory> helpDeskCategories);
  @Query('Delete From HelpDeskCategory')
  Future<void> deleteAll();
}
