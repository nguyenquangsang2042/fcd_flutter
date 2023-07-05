import 'package:fcd_flutter/base/model/app/student.dart';
import 'package:floor/floor.dart';

@dao
abstract class StudentDao{
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertStudent(List<Student> student);
}