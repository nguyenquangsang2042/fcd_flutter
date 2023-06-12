// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'menu_app.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

MenuApp _$MenuAppFromJson(Map<String, dynamic> json) => MenuApp(
      json['ID'] as int,
      json['Title'] as String,
      json['LanguageId'] as int,
      json['Range'] as int,
      json['Created'] as String,
      json['Status'] as int,
      json['Url'] as String,
    );

Map<String, dynamic> _$MenuAppToJson(MenuApp instance) => <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'LanguageId': instance.languageId,
      'Range': instance.range,
      'Created': instance.created,
      'Status': instance.status,
      'Url': instance.url,
    };
