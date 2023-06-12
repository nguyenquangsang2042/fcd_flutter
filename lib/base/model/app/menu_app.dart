import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'menu_app.g.dart';

@JsonSerializable()
@entity
class MenuApp {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;
  @JsonKey(name: 'Title')
  late String title;
  @JsonKey(name: 'LanguageId')
  late int languageId;
  @JsonKey(name: 'Range')
  late int range;
  @JsonKey(name: 'Created')
  late String created;
  @JsonKey(name: 'Status')
  late int status;
  @JsonKey(name: 'Url')
  late String url;

  MenuApp(this.id, this.title, this.languageId, this.range, this.created,
      this.status, this.url);

  factory MenuApp.fromJson(Map<String, dynamic> json) =>
      _$MenuAppFromJson(json);

  Map<String, dynamic> toJson() => _$MenuAppToJson(this);
}
