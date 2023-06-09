// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'device_info.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

DeviceInfo _$DeviceInfoFromJson(Map<String, dynamic> json) => DeviceInfo(
      json['DeviceId'] as String,
      json['DevicePushToken'] as String?,
      json['DeviceOS'] as int,
      json['AppVersion'] as String,
      json['DeviceOSVersion'] as String,
      json['DeviceModel'] as String,
    );

Map<String, dynamic> _$DeviceInfoToJson(DeviceInfo instance) =>
    <String, dynamic>{
      'DeviceId': instance.DeviceId,
      'DevicePushToken': instance.DevicePushToken,
      'DeviceOS': instance.DeviceOS,
      'AppVersion': instance.AppVersion,
      'DeviceOSVersion': instance.DeviceOSVersion,
      'DeviceModel': instance.DeviceModel,
    };
