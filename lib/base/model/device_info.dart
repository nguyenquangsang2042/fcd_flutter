import 'package:json_annotation/json_annotation.dart';

part 'device_info.g.dart';

@JsonSerializable()
class DeviceInfo {
  late String DeviceId;
  late String? DevicePushToken;
  late int DeviceOS;
  late String AppVersion;
  late String DeviceOSVersion;
  late String DeviceModel;

  DeviceInfo(this.DeviceId, this.DevicePushToken, this.DeviceOS,
      this.AppVersion, this.DeviceOSVersion, this.DeviceModel);

  DeviceInfo.required(
      {required this.DeviceId,
      required this.DevicePushToken,
      required this.DeviceOS,
      required this.AppVersion,
      required this.DeviceOSVersion,
      required this.DeviceModel});

  factory DeviceInfo.fromJson(Map<String, dynamic> json) =>
      _$DeviceInfoFromJson(json);

  Map<String, dynamic> toJson() => _$DeviceInfoToJson(this);
}
