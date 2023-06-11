
import 'package:fcd_flutter/base/model/app/app_language.dart';
import 'package:floor/floor.dart';

@dao
abstract class AppLanguageDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertAppLanguage(List<AppLanguage> appLanguages);
}
