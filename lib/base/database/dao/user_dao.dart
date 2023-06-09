import 'package:floor/floor.dart';

import '../../model/app/user.dart';

@dao
abstract class UserDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertUsers(List<User> users);

}