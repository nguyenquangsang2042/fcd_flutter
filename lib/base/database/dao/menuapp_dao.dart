
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/menu_app.dart';
import 'package:floor/floor.dart';

@dao
abstract class MenuAppDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertMenuApp(List<MenuApp> menuapps);
}
