import 'package:fcd_flutter/base/model/app/settings.dart';
import 'package:floor/floor.dart';

@dao
abstract class SettingsDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertSettings(List<Setting> settings);
}
