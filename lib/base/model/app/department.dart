import 'dart:convert';

import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'department.g.dart';
@entity
@JsonSerializable()
class Department
{
  @primaryKey
  @JsonKey(name: "ID")
  late double id;
  @JsonKey(name:"Title" )
  late String title;
  @JsonKey(name: "Code")
  late String code;
  @JsonKey(name:"ParentID" )
  late double? parentID;
  @JsonKey(name: "ParentName")
  late String parentName;
  @JsonKey(name: "GroupID")
  late double groupID;
  @JsonKey(name: "Modified")
  late String modified;
  @JsonKey(name: "Effect")
  late bool effect;

  Department(this.id, this.title, this.code, this.parentID, this.parentName,
      this.groupID, this.modified, this.effect);
  factory Department.fromJson(Map<String, dynamic> json) =>
      _$DepartmentFromJson(json);

  Map<String, dynamic> toJson() => _$DepartmentToJson(this);
}