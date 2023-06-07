
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:floor/floor.dart';

@dao
abstract class FAQsDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertFAQs(List<FAQs> faqs);
}
