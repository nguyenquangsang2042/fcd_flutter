import 'package:fcd_flutter/base/model/app/ward.dart';
import 'package:floor/floor.dart';

import '../../model/app/user.dart';

@dao
abstract class WardDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertWard(List<Ward> ward);
  @Query('Delete From Ward')
  Future<void> deleteAll();
}