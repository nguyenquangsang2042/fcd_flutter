// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'airport.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Airport _$AirportFromJson(Map<String, dynamic> json) => Airport(
      json['ID'] as int,
      json['Title'] as String,
      json['Code'] as String,
      json['Description'] as String,
      json['Status'] as int,
      json['Modified'] as String,
      json['Created'] as String,
    );

Map<String, dynamic> _$AirportToJson(Airport instance) => <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'Code': instance.code,
      'Description': instance.description,
      'Status': instance.status,
      'Modified': instance.modified,
      'Created': instance.created,
    };
