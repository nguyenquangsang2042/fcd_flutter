import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'nation.g.dart';
@entity
@JsonSerializable()
class Nation
{
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;

  @JsonKey(name: 'Title')
  late String title;

  @JsonKey(name: 'Rank')
  late int rank;

  @JsonKey(name: 'Modified')
  late String modified;

  @JsonKey(name: 'Created')
  late String created;

  Nation(this.id, this.title, this.rank, this.modified, this.created);

  factory Nation.fromJson(Map<String, dynamic> json) => _$NationFromJson(json);

  Map<String, dynamic> toJson() => _$NationToJson(this);
}