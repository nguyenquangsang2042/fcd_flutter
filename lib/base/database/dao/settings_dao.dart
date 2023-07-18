import 'package:fcd_flutter/base/model/app/settings.dart';
import 'package:floor/floor.dart';

@dao
abstract class SettingsDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertSettings(List<Setting> settings);
  @Query('SELECT * FROM Setting WHERE [KEY] = :key')
  Future<Setting?>findSettingByKey(String key);
  @Query('Select * from Setting Where [Key] in (:lstKey)')
  Future<List<Setting>> getListSettingInLstKey(List<String> lstKey);
  @Query('Delete From Setting')
  Future<void> deleteAll();
}
