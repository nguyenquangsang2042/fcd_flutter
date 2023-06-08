// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'help_desk_category.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

HelpDeskCategory _$HelpDeskCategoryFromJson(Map<String, dynamic> json) =>
    HelpDeskCategory(
      json['ID'] as int,
      json['Title'] as String,
      json['Description'] as String?,
      json['Modified'] as String,
      json['Created'] as String,
    );

Map<String, dynamic> _$HelpDeskCategoryToJson(HelpDeskCategory instance) =>
    <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'Description': instance.description,
      'Modified': instance.modified,
      'Created': instance.created,
    };
