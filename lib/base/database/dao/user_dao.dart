import 'package:floor/floor.dart';

import '../../model/app/user.dart';

@dao
abstract class UserDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertUsers(List<User> users);
  @Query("Select * from User WHERE IFNULL(Status,1) = 1  ORDER BY FullNameNoAccent")
  Stream<List<User>> findAll();
  @Query("Select * from User WHERE IFNULL(Status,1) = 1 AND Department = :departmentID ORDER BY FullNameNoAccent")
  Stream<List<User>> findWithDepartmentID(double departmentID);
  
  @Query("SELECT * FROM User WHERE (FullNameNoAccent LIKE :keySearch OR Mobile LIKE :keySearch)  AND IFNULL(Status,1) = 1 ORDER BY FullNameNoAccent")
  Stream<List<User>> findByFullnameOrMobile(String keySearch);
  @Query("SELECT * FROM User WHERE (FullNameNoAccent LIKE :keySearch OR Mobile LIKE :keySearch) AND Department = :departmentID AND IFNULL(Status,1) = 1 ORDER BY FullNameNoAccent")
  Stream<List<User>> findByFullnameOrMobileAndDepartment(String keySearch,double departmentID);

  @Query("SELECT * FROM User WHERE Email like :email ")
  Future<User?> findUserByEmail(String email);
  @Query('Delete From User')
  Future<void> deleteAll();

}