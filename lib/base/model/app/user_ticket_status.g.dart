// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_ticket_status.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserTicketStatus _$UserTicketStatusFromJson(Map<String, dynamic> json) =>
    UserTicketStatus(
      json['ID'] as int,
      json['Title'] as String?,
      json['TitleEN'] as String?,
      json['Modified'] as String?,
      json['Created'] as String?,
    );

Map<String, dynamic> _$UserTicketStatusToJson(UserTicketStatus instance) =>
    <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'TitleEN': instance.titleEn,
      'Modified': instance.modified,
      'Created': instance.created,
    };
