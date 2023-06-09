// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'province.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Province _$ProvinceFromJson(Map<String, dynamic> json) => Province(
      json['ID'] as int,
      json['Title'] as String,
      json['NationID'] as int,
      json['Modified'] as String,
    );

Map<String, dynamic> _$ProvinceToJson(Province instance) => <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'NationID': instance.nationId,
      'Modified': instance.modified,
    };
