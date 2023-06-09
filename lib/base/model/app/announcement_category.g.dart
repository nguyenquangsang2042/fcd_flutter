// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'announcement_category.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AnnouncementCategory _$AnnouncementCategoryFromJson(
        Map<String, dynamic> json) =>
    AnnouncementCategory(
      json['ID'] as int,
      json['Title'] as String,
      json['TitleEN'] as String?,
      json['Description'] as String,
      json['IconPath'] as String,
      json['ImagePath'] as String,
      json['AnnounceTemplateId'] as int?,
      json['NotifyTemplateId'] as int,
      json['ResourceCategoryId'] as int,
      json['UrlDetail'] as String?,
      json['RemindBeforeTime'] as int,
      json['IsCreate'] as bool,
      json['Device'] as int,
      json['Modified'] as String,
      json['Created'] as String,
      json['Orders'] as int,
    );

Map<String, dynamic> _$AnnouncementCategoryToJson(
        AnnouncementCategory instance) =>
    <String, dynamic>{
      'ID': instance.id,
      'Title': instance.title,
      'TitleEN': instance.titleEN,
      'Description': instance.description,
      'IconPath': instance.iconPath,
      'ImagePath': instance.imagePath,
      'AnnounceTemplateId': instance.announceTemplateId,
      'NotifyTemplateId': instance.notifyTemplateId,
      'ResourceCategoryId': instance.resourceCategoryId,
      'UrlDetail': instance.urlDetail,
      'RemindBeforeTime': instance.remindBeforeTime,
      'IsCreate': instance.isCreate,
      'Device': instance.device,
      'Modified': instance.modified,
      'Created': instance.created,
      'Orders': instance.orders,
    };
