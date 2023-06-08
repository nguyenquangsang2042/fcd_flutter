// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'helpdesk_linhvuc.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

HelpDeskLinhVuc _$HelpDeskLinhVucFromJson(Map<String, dynamic> json) =>
    HelpDeskLinhVuc(
      (json['ID'] as num).toDouble(),
      json['Title_EN'] as String,
      json['Title_VN'] as String,
      json['Status'] as int,
      json['Order'] as int,
      json['Modified'] as String,
      json['IDGroupMail'] as String,
    );

Map<String, dynamic> _$HelpDeskLinhVucToJson(HelpDeskLinhVuc instance) =>
    <String, dynamic>{
      'ID': instance.id,
      'Title_EN': instance.titleEn,
      'Title_VN': instance.titleVn,
      'Status': instance.status,
      'Order': instance.order,
      'Modified': instance.modified,
      'IDGroupMail': instance.idGroupMail,
    };
