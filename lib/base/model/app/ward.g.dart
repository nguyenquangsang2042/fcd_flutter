// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'ward.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Ward _$WardFromJson(Map<String, dynamic> json) => Ward(
      json['ID'] as int,
      json['Title'] as String,
      json['DistrictID'] as int,
      json['Modified'] as String,
    );

Map<String, dynamic> _$WardToJson(Ward instance) => <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'DistrictID': instance.districtId,
      'Modified': instance.modified,
    };
