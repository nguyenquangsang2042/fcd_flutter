
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:floor/floor.dart';

import '../../model/app/announcement_category.dart';

@dao
abstract class AnnouncementCategoryDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertAnnouncementCategories(List<AnnouncementCategory> announcementCategories);
}