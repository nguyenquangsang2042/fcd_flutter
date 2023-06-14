// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'settings.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Setting _$SettingFromJson(Map<String, dynamic> json) => Setting(
      json['KEY'] as String,
      json['VALUE'] as String,
      json['DESC'] as String?,
      json['DEVICE'] as int,
      json['Modified'] as String,
    );

Map<String, dynamic> _$SettingToJson(Setting instance) => <String, dynamic>{
      'KEY': instance.KEY,
      'VALUE': instance.VALUE,
      'DESC': instance.DESC,
      'DEVICE': instance.DEVICE,
      'Modified': instance.Modified,
    };
