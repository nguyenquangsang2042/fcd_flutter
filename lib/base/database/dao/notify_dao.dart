import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:floor/floor.dart';

@dao
abstract class NotifyDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertNotifies(List<Notify> notify);

  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId in (:beanAnnounceID) AND AnnounCategoryId <> :keyNews ORDER BY Created DESC')
  Stream<List<Notify>> getListNotifyWithAnnounceCategory(
      List<String> beanAnnounceID, String keyNews);
}
