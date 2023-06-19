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
      'SELECT * FROM Notify WHERE AnnounCategoryId <> :keyNews AND SearCol  LIKE "%:keyWord%" AND AnnounCategoryId IN (:beanAnnounceID) AND ANStatus <> -1 ORDER BY FlgRead,Created DESC ')
  Stream<List<Notify>> getListHaveKeywordFilterType01ORDER_BY_FlgRead_Created_DESC (String keyNews,
      String keyWord, List<String> beanAnnounceID);
  @Query(
      'SELECT * FROM Notify WHERE AnnounCategoryId <> :keyNews AND SearCol  LIKE "%:keyWord%" AND AnnounCategoryId IN (:beanAnnounceID) AND ANStatus <> -1 ORDER BY flgImmediately DESC ,Created DESC  ')
  Stream<List<Notify>> getListHaveKeywordFilterType01ORDER_BY_flgImmediately_DESC_Created_DESC  (String keyNews,
      String keyWord, List<String> beanAnnounceID);
  @Query(
      'SELECT * FROM Notify WHERE AnnounCategoryId <> :keyNews AND SearCol  LIKE "%:keyWord%" AND AnnounCategoryId IN (:beanAnnounceID) AND ANStatus <> -1 ORDER BY flgConfirm DESC, flgConfirmed   ,Created DESC   ')
  Stream<List<Notify>> getListHaveKeywordFilterType01ORDER_BY_flgConfirm_DESC_flgConfirmed_Created_DESC(String keyNews,
      String keyWord, List<String> beanAnnounceID);

  @Query(
      'SELECT * FROM Notify WHERE AnnounCategoryId <> :keyNews AND SearCol  LIKE "%:keyWord%" AND AnnounCategoryId IN (:beanAnnounceID) AND ANStatus <> -1 ORDER BY Created DESC ')
  Stream<List<Notify>> getListHaveKeywordFilterType01ORDER_BY_Created_DESC(String keyNews,
      String keyWord, List<String> beanAnnounceID);

  @Query(
      'SELECT * FROM Notify WHERE AnnounCategoryId <> :keyNews AND SearCol  LIKE "%:keyWord%" AND   ANStatus <> -1 AND AnnounCategoryId IN (:beanAnnounceID)  ORDER BY FlgRead,Created DESC  ')
  Stream<List<Notify>> getListHaveKeywordFilterTypeOrder01ORDER_BY_FlgRead_Created_DESC(String keyNews,
      String keyWord, List<String> beanAnnounceID);
  @Query(
      'SELECT * FROM Notify WHERE AnnounCategoryId <> :keyNews AND SearCol  LIKE "%:keyWord%" AND   ANStatus <> -1 AND AnnounCategoryId IN (:beanAnnounceID)  ORDER BY flgImmediately DESC ,Created DESC  ')
  Stream<List<Notify>> getListHaveKeywordFilterTypeOrder01ORDER_BY_flgImmediately_DESC_Created_DESC(String keyNews,
      String keyWord, List<String> beanAnnounceID);
  @Query(
      'SELECT * FROM Notify WHERE AnnounCategoryId <> :keyNews AND SearCol  LIKE "%:keyWord%" AND   ANStatus <> -1 AND AnnounCategoryId IN (:beanAnnounceID)  ORDER BY flgConfirm DESC, flgConfirmed   ,Created DESC   ')
  Stream<List<Notify>> getListHaveKeywordFilterTypeOrder01ORDER_BY_flgConfirm_DESC_flgConfirmed_Created_DESC (String keyNews,
      String keyWord, List<String> beanAnnounceID);
  @Query(
      'SELECT * FROM Notify WHERE AnnounCategoryId <> :keyNews AND SearCol  LIKE "%:keyWord%" AND   ANStatus <> -1 AND AnnounCategoryId IN (:beanAnnounceID)  ORDER BY Created DESC ')
  Stream<List<Notify>> getListHaveKeywordFilterTypeOrder01ORDER_BY_Created_DESC (String keyNews,
      String keyWord, List<String> beanAnnounceID);



  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId <> :keyNews AND AnnounCategoryId IN (:beanAnnounceID)  ORDER BY FlgRead,Created DESC ')
  Stream<List<Notify>> getListNotHaveKeywordFilterType01ORDER_BY_FlgRead_Created_DESC(
      String keyNews, List<String> beanAnnounceID);
  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId <> :keyNews AND AnnounCategoryId IN (:beanAnnounceID)  ORDER BY flgImmediately DESC ,Created DESC  ')
  Stream<List<Notify>> getListNotHaveKeywordFilterType01ORDER_BY_flgImmediately_DESC_Created_DESC(
      String keyNews, List<String> beanAnnounceID);
  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId <> :keyNews AND AnnounCategoryId IN (:beanAnnounceID)  ORDER BY flgConfirm DESC, flgConfirmed   ,Created DESC   ')
  Stream<List<Notify>> getListNotHaveKeywordFilterType01ORDER_BY_flgConfirm_DESC_flgConfirmed_Created_DESC (
      String keyNews, List<String> beanAnnounceID);
  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId <> :keyNews AND AnnounCategoryId IN (:beanAnnounceID)  ORDER BY Created DESC ')
  Stream<List<Notify>> getListNotHaveKeywordFilterType01ORDER_BY_Created_DESC(
      String keyNews, List<String> beanAnnounceID);




  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId IN (:beanAnnounceID)  AND AnnounCategoryId <> :keyNews  ORDER BY FlgRead,Created DESC ')
  Stream<List<Notify>> getListNotHaveKeywordFilterTypeOrder01ORDER_BY_FlgRead_Created_DESC (
      List<String> beanAnnounceID, String keyNews);
  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId IN (:beanAnnounceID)  AND AnnounCategoryId <> :keyNews  ORDER BY flgImmediately DESC ,Created DESC ')
  Stream<List<Notify>> getListNotHaveKeywordFilterTypeOrder01ORDER_BY_flgImmediately_DESC_Created_DESC(
      List<String> beanAnnounceID, String keyNews);
  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId IN (:beanAnnounceID)  AND AnnounCategoryId <> :keyNews  ORDER BY flgConfirm DESC, flgConfirmed   ,Created DESC  ')
  Stream<List<Notify>> getListNotHaveKeywordFilterTypeOrder01ORDER_BY_flgConfirm_DESC_flgConfirmed_Created_DESC(
      List<String> beanAnnounceID, String keyNews);
  @Query(
      'SELECT * FROM Notify WHERE ANStatus <> -1 AND AnnounCategoryId IN (:beanAnnounceID)  AND AnnounCategoryId <> :keyNews  ORDER BY Created DESC ')
  Stream<List<Notify>> getListNotHaveKeywordFilterTypeOrder01ORDER_BY_Created_DESC(
      List<String> beanAnnounceID, String keyNews);
}
