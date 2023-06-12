// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'menu_home.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

MenuHome _$MenuHomeFromJson(Map<String, dynamic> json) => MenuHome(
      json['ID'] as int,
      json['Title'] as String,
      json['Status'] as int,
      json['Key'] as String,
      json['Url'] as String,
      json['index'] as int,
      json['IndexIpad'] as int,
    );

Map<String, dynamic> _$MenuHomeToJson(MenuHome instance) => <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'Status': instance.status,
      'Key': instance.key,
      'Url': instance.url,
      'index': instance.index,
      'IndexIpad': instance.indexIpad,
    };
