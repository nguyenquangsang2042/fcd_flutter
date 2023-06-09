// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'district.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

District _$DistrictFromJson(Map<String, dynamic> json) => District(
      json['ID'] as int,
      json['Title'] as String,
      json['ProvinceID'] as int,
      json['Modified'] as String,
    );

Map<String, dynamic> _$DistrictToJson(District instance) => <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'ProvinceID': instance.provinceId,
      'Modified': instance.modified,
    };
