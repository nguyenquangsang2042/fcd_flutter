// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'bean_banner.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

BeanBanner _$BeanBannerFromJson(Map<String, dynamic> json) => BeanBanner(
      json['ID'] as int,
      json['Title'] as String,
      json['FileName'] as String,
      json['FilePath'] as String,
      json['Created'] as String,
      json['Status'] as int,
      json['SortOrder'] as int,
    );

Map<String, dynamic> _$BeanBannerToJson(BeanBanner instance) =>
    <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'FileName': instance.fileName,
      'FilePath': instance.filePath,
      'Created': instance.created,
      'Status': instance.status,
      'SortOrder': instance.sortOrder,
    };
