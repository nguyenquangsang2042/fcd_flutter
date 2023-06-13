import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/menu_app.dart';
import 'package:fcd_flutter/base/model/app/menu_home.dart';
import 'package:floor/floor.dart';

@dao
abstract class MenuHomeDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertMenuHome(List<MenuHome> menuHomes);
  @Query('Delete From MenuHome')
  Future<void> deleteAll();
}
