import 'package:fcd_flutter/base/model/app/db_variable.dart';
import 'package:floor/floor.dart';

@dao
abstract class DBVariableDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertDBVariable(DBVariable dbVariable);
  @Query('Select * from DBVariable where Id like :id')
  Future<DBVariable? >findDBVariableById(String id);
}