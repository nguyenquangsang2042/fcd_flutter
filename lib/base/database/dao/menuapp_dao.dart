
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/menu_app.dart';
import 'package:floor/floor.dart';

@dao
abstract class MenuAppDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertMenuApp(List<MenuApp> menuapps);
  @Query("SELECT * FROM MenuApp WHERE status = :intStatus AND languageId = :languageId")
  Stream<List<MenuApp>> getMenuAppByStatusAndLanguageID(int intStatus,int languageId);
  @Query('Delete From MenuApp')
  Future<void> deleteAll();
}
