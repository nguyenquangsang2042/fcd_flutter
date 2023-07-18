
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:fcd_flutter/base/model/app/nation.dart';
import 'package:floor/floor.dart';

@dao
abstract class NationDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertNations(List<Nation> nation);
  
  @Query("Select * from Nation")
  Future<List<Nation>> getAllNation();
  @Query('Delete From Nation')
  Future<void> deleteAll();
}
