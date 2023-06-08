
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:floor/floor.dart';

import '../../model/app/province.dart';

@dao
abstract class ProvinceDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertProvince(List<Province> provinces);
}
