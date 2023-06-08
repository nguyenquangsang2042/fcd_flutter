// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'faqs.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

FAQs _$FAQsFromJson(Map<String, dynamic> json) => FAQs(
      json['ID'] as int,
      json['DepartmentId'] as int?,
      json['Question'] as String,
      json['Answer'] as String,
      json['Status'] as int,
      json['Created'] as String,
      json['Modified'] as String,
      json['Language'] as String,
    );

Map<String, dynamic> _$FAQsToJson(FAQs instance) => <String, dynamic>{
      'ID': instance.id,
      'DepartmentId': instance.departmentId,
      'Question': instance.question,
      'Answer': instance.answer,
      'Status': instance.status,
      'Created': instance.created,
      'Modified': instance.modified,
      'Language': instance.language,
    };
