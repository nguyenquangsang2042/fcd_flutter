// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'settings.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Setting _$SettingFromJson(Map<String, dynamic> json) => Setting()
  ..KEY = json['KEY'] as String
  ..VALUE = json['VALUE'] as String
  ..DESC = json['DESC'] as String?
  ..DEVICE = json['DEVICE'] as int
  ..Modified = json['Modified'] as String;

Map<String, dynamic> _$SettingToJson(Setting instance) => <String, dynamic>{
      'KEY': instance.KEY,
      'VALUE': instance.VALUE,
      'DESC': instance.DESC,
      'DEVICE': instance.DEVICE,
      'Modified': instance.Modified,
    };
