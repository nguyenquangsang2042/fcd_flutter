
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:floor/floor.dart';

import '../../model/app/district.dart';

@dao
abstract class DistrictDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertDistrict(List<District> districts);
  @Query('Delete From District')
  Future<void> deleteAll();
}
