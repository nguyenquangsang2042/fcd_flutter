import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'district.g.dart';
@entity
@JsonSerializable()
class District {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;

  @JsonKey(name: 'Title')
  late String title;

  @JsonKey(name: 'ProvinceID')
  late int provinceId;

  @JsonKey(name: 'Modified')
  late String modified;


  District(this.id, this.title, this.provinceId, this.modified);

  factory District.fromJson(Map<String, dynamic> json) => _$DistrictFromJson(json);

  Map<String, dynamic> toJson() => _$DistrictToJson(this);
}
