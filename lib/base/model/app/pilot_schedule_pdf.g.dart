// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'pilot_schedule_pdf.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PilotSchedulePdf _$PilotSchedulePdfFromJson(Map<String, dynamic> json) =>
    PilotSchedulePdf(
      json['ID'] as int,
      json['Title'] as String,
      json['FilePath'] as String,
      json['ScheduleDate'] as String,
      json['Creator'] as String,
      json['UserModified'] as String,
      json['Created'] as String,
      json['Modified'] as String,
    );

Map<String, dynamic> _$PilotSchedulePdfToJson(PilotSchedulePdf instance) =>
    <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'FilePath': instance.filePath,
      'ScheduleDate': instance.scheduleDate,
      'Creator': instance.creator,
      'UserModified': instance.userModified,
      'Created': instance.created,
      'Modified': instance.modified,
    };
