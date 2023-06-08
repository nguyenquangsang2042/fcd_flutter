// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_ticket_category.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserTicketCategory _$UserTicketCategoryFromJson(Map<String, dynamic> json) =>
    UserTicketCategory(
      json['ID'] as int,
      json['Title'] as String,
      json['Created'] as String,
      json['Modified'] as String,
    );

Map<String, dynamic> _$UserTicketCategoryToJson(UserTicketCategory instance) =>
    <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'Created': instance.created,
      'Modified': instance.modified,
    };
