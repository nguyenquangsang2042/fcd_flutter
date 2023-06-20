
import 'package:floor/floor.dart';

import '../../model/app/announcement_category.dart';

@dao
abstract class AnnouncementCategoryDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertAnnouncementCategories(List<AnnouncementCategory> announcementCategories);
  @Query('SELECT * FROM AnnouncementCategory WHERE ID IN (:lstId) ')
  Stream<List<AnnouncementCategory>> getAnnouncementCategoryInListID(List<String> lstId);
  @Query('SELECT * FROM AnnouncementCategory WHERE ID NOT IN (:lstId) ORDER BY Orders')
  Stream<List<AnnouncementCategory>> getAnnouncementCategoryNotInListID(List<String> lstId);
}
