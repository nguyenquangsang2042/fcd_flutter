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

  @Query(
      'SELECT * FROM Notify WHERE AnnounCategoryId <> :keyNews AND SearCol  LIKE "%:keyWord%" AND AnnounCategoryId IN (:beanAnnounceID) AND ANStatus <> -1 ORDER BY :orderBy')
  Stream<List<Notify>> getListHaveKeywordFilterType01(String keyNews,
      String keyWord, List<String> beanAnnounceID, String orderBy);

  @Query(
      'SELECT * FROM Notify WHERE AnnounCategoryId <> :keyNews AND SearCol  LIKE "%:keyWord%" AND   ANStatus <> -1 AND AnnounCategoryId IN (:beanAnnounceID)  ORDER BY :orderBy')
  Stream<List<Notify>> getListHaveKeywordFilterTypeOrder01(String keyNews,
      String keyWord, List<String> beanAnnounceID, String orderBy);

  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId <> :keyNews AND AnnounCategoryId IN (:beanAnnounceID)  ORDER BY :orderBy')
  Stream<List<Notify>> getListNotHaveKeywordFilterType01(
      String keyNews, List<String> beanAnnounceID, String orderBy);

  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId IN (:beanAnnounceID)  AND AnnounCategoryId <> :keyNews  ORDER BY :orderBy')
  Stream<List<Notify>> getListNotHaveKeywordFilterTypeOrder01(
      List<String> beanAnnounceID, String keyNews, String orderBy);
}
