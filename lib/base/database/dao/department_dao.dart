
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/app_language.dart';
import 'package:fcd_flutter/base/model/app/department.dart';
import 'package:floor/floor.dart';

@dao
abstract class DepartmentDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertDepartment(List<Department> departments);
}
