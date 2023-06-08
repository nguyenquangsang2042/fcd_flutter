import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'province.g.dart';
@entity
@JsonSerializable()
class Province
{
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;

  @JsonKey(name: 'Title')
  late String title;

  @JsonKey(name: 'NationID')
  late int nationId;

  @JsonKey(name: 'Modified')
  late String modified;

  Province(this.id, this.title, this.nationId, this.modified);

  factory Province.fromJson(Map<String, dynamic> json) => _$ProvinceFromJson(json);

  Map<String, dynamic> toJson() => _$ProvinceToJson(this);
}