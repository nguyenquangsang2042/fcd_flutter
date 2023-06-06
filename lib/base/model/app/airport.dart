import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'airport.g.dart';
@entity
@JsonSerializable()
class Airport
{
  @primaryKey
  @JsonKey(name: 'ID')
  final int id;
  @JsonKey(name: 'Title')
  final String title;
  @JsonKey(name: 'Code')
  final String code;
  @JsonKey(name: 'Description')
  final String description;
  @JsonKey(name: 'Status')
  final int status;
  @JsonKey(name: 'Modified')
  final String modified;
  @JsonKey(name: 'Created')
  final String created;

  Airport(this.id, this.title, this.code, this.description,
      this.status, this.modified, this.created);
  factory Airport.fromJson(Map<String, dynamic> json) => _$AirportFromJson(json);

  Map<String, dynamic> toJson() => _$AirportToJson(this);
}