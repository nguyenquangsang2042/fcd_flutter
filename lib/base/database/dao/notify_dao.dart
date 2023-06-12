
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:fcd_flutter/base/model/app/nation.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:floor/floor.dart';

@dao
abstract class NotifyDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertNotifies(List<Notify> notify);
}
