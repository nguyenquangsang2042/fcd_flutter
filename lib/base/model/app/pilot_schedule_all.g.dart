// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'pilot_schedule_all.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PilotScheduleAll _$PilotScheduleAllFromJson(Map<String, dynamic> json) =>
    PilotScheduleAll(
      json['ID'] as int,
      json['Title'] as String,
      json['FilePath'] as String,
      json['ScheduleDate'] as String,
      json['Creator'] as String,
      json['UserModified'] as String?,
      json['Modified'] as String,
      json['Created'] as String,
    );

Map<String, dynamic> _$PilotScheduleAllToJson(PilotScheduleAll instance) =>
    <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'FilePath': instance.filePath,
      'ScheduleDate': instance.scheduleDate,
      'Creator': instance.creator,
      'UserModified': instance.userModified,
      'Modified': instance.modified,
      'Created': instance.created,
    };
