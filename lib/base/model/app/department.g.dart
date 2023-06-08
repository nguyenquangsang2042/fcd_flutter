// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'department.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Department _$DepartmentFromJson(Map<String, dynamic> json) => Department(
      (json['ID'] as num).toDouble(),
      json['Title'] as String,
      json['Code'] as String,
      (json['ParentID'] as num?)?.toDouble(),
      json['ParentName'] as String,
      (json['GroupID'] as num).toDouble(),
      json['Modified'] as String,
      json['Effect'] as bool,
    );

Map<String, dynamic> _$DepartmentToJson(Department instance) =>
    <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'Code': instance.code,
      'ParentID': instance.parentID,
      'ParentName': instance.parentName,
      'GroupID': instance.groupID,
      'Modified': instance.modified,
      'Effect': instance.effect,
    };
