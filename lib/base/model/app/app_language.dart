import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'app_language.g.dart';
@entity
@JsonSerializable()
class AppLanguage
{
  @primaryKey
  @JsonKey(name: 'Key')
  late String key;
  @JsonKey(name: 'Value')
  late String value;

  AppLanguage(this.key, this.value);

  factory AppLanguage.fromJson(Map<String, dynamic> json) => _$AppLanguageFromJson(json);

  Map<String, dynamic> toJson() => _$AppLanguageToJson(this);
}