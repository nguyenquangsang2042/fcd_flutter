// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'notify.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Notify _$NotifyFromJson(Map<String, dynamic> json) => Notify(
      json['ID'] as String,
      json['UserId'] as String,
      json['Content'] as String,
      json['ResourceCategoryId'] as int,
      json['ResourceSubCategoryId'] as int,
      json['AnnouncementId'] as int,
      json['Link'] as String?,
      json['Icon'] as String?,
      json['FlgRead'] as bool,
      json['SendTime'] as String?,
      json['ReadTime'] as String?,
      json['flgConfirm'] as bool,
      json['flgConfirmed'] as int,
      json['flgReply'] as bool,
      json['flgReplied'] as bool,
      json['ReplyTime'] as String?,
      json['ReplyContent'] as String?,
      json['flgImmediately'] as bool,
      json['ShowPopup'] as bool,
      json['ActionTime'] as String?,
      json['Modified'] as String,
      json['Created'] as String,
      json['FlgSurvey'] as bool,
      json['Title'] as String?,
      json['ANStatus'] as int,
      json['AnnounCategoryId'] as int,
      json['IconPath'] as String?,
      json['IsSurveyPoll'] as int,
      json['SearCol'] as String,
    );

Map<String, dynamic> _$NotifyToJson(Notify instance) => <String, dynamic>{
      'ID': instance.id,
      'UserId': instance.userId,
      'Content': instance.content,
      'ResourceCategoryId': instance.resourceCategoryId,
      'ResourceSubCategoryId': instance.resourceSubCategoryId,
      'AnnouncementId': instance.announcementId,
      'Link': instance.link,
      'Icon': instance.icon,
      'FlgRead': instance.flgRead,
      'SendTime': instance.sendTime,
      'ReadTime': instance.readTime,
      'flgConfirm': instance.flgConfirm,
      'flgConfirmed': instance.flgConfirmed,
      'flgReply': instance.flgReply,
      'flgReplied': instance.flgReplied,
      'ReplyTime': instance.replyTime,
      'ReplyContent': instance.replyContent,
      'flgImmediately': instance.flgImmediately,
      'ShowPopup': instance.showPopup,
      'ActionTime': instance.actionTime,
      'Modified': instance.modified,
      'Created': instance.created,
      'FlgSurvey': instance.flgSurvey,
      'Title': instance.title,
      'ANStatus': instance.anStatus,
      'AnnounCategoryId': instance.announCategoryId,
      'IconPath': instance.iconPath,
      'IsSurveyPoll': instance.isSurveyPoll,
      'SearCol': instance.searCol,
    };
