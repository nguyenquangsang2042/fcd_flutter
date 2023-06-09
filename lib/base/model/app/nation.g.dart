// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'nation.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Nation _$NationFromJson(Map<String, dynamic> json) => Nation(
      json['ID'] as int,
      json['Title'] as String,
      json['Rank'] as int,
      json['Modified'] as String,
      json['Created'] as String,
    );

Map<String, dynamic> _$NationToJson(Nation instance) => <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'Rank': instance.rank,
      'Modified': instance.modified,
      'Created': instance.created,
    };
