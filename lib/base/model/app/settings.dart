import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'settings.g.dart';
@entity
@JsonSerializable()
class Setting
{
  @primaryKey
  late String KEY;
  late String VALUE;
  @ColumnInfo(name:"DESC" )
  late String? DESC;
  late int  DEVICE ;
  late String  Modified ;

  Setting();
  factory Setting.fromJson(Map<String, dynamic> json) => _$SettingFromJson(json);
  Map<String, dynamic> toJson() => _$SettingToJson(this);
}