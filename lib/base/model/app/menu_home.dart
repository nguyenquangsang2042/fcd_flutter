import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'menu_home.g.dart';

@JsonSerializable()
@entity
class MenuHome {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;

  @JsonKey(name: 'Title')
  late String title;

  @JsonKey(name: 'Status')
  late int status;

  @JsonKey(name: 'Key')
  late String key;

  @JsonKey(name: 'Url')
  late String url;

  @JsonKey(name: 'index')
  late int index;

  @JsonKey(name: 'IndexIpad')
  late int indexIpad;

  MenuHome( this.id,
     this.title,
     this.status,
     this.key,
     this.url,
     this.index,
     this.indexIpad);

  factory MenuHome.fromJson(Map<String, dynamic> json) =>
      _$MenuHomeFromJson(json);

  Map<String, dynamic> toJson() => _$MenuHomeToJson(this);
}
