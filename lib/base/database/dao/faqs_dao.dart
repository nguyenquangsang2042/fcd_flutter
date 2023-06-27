
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:floor/floor.dart';

@dao
abstract class FAQsDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertFAQs(List<FAQs> faqs);
  @Query('SELECT * FROM FAQs WHERE Status = 1  and ID not in (:lstID) and Language = :langCode ORDER BY Created DESC')
  Stream<List<FAQs>> getListFaqsDifLstIDAndLang(List<int>lstID,String langCode);
}
