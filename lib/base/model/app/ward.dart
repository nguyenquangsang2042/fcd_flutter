import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'ward.g.dart';
@entity
@JsonSerializable()
class Ward {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;

  @JsonKey(name: 'Title')
  late String title;

  @JsonKey(name: 'DistrictID')
  late int districtId;

  @JsonKey(name: 'Modified')
  late String modified;


  Ward(this.id, this.title, this.districtId, this.modified);

  factory Ward.fromJson(Map<String, dynamic> json) => _$WardFromJson(json);

  Map<String, dynamic> toJson() => _$WardToJson(this);
}
