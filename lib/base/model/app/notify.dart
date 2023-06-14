import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'notify.g.dart';

@JsonSerializable()
@entity
class Notify {
  @primaryKey
  @JsonKey(name: 'ID')
  late String id;

  @JsonKey(name: 'UserId')
  late String userId;

  @JsonKey(name: 'Content')
  late String content;

  @JsonKey(name: 'ResourceCategoryId')
  late int resourceCategoryId;

  @JsonKey(name: 'ResourceSubCategoryId')
  late int resourceSubCategoryId;

  @JsonKey(name: 'AnnouncementId')
  late int announcementId;

  @JsonKey(name: 'Link')
  late String? link;

  @JsonKey(name: 'Icon')
  late String? icon;

  @JsonKey(name: 'FlgRead')
  late bool flgRead;

  @JsonKey(name: 'SendTime')
  late String? sendTime;

  @JsonKey(name: 'ReadTime')
  late String? readTime;

  @JsonKey(name: 'flgConfirm')
  late bool flgConfirm;

  @JsonKey(name: 'flgConfirmed')
  late int flgConfirmed;

  @JsonKey(name: 'flgReply')
  late bool flgReply;

  @JsonKey(name: 'flgReplied')
  late bool flgReplied;

  @JsonKey(name: 'ReplyTime')
  late String? replyTime;

  @JsonKey(name: 'ReplyContent')
  late String? replyContent;

  @JsonKey(name: 'flgImmediately')
  late bool flgImmediately;

  @JsonKey(name: 'ShowPopup')
  late bool showPopup;

  @JsonKey(name: 'ActionTime')
  late String? actionTime;

  @JsonKey(name: 'Modified')
  late String modified;

  @JsonKey(name: 'Created')
  late String created;

  @JsonKey(name: 'FlgSurvey')
  late bool flgSurvey;

  @JsonKey(name: 'Title')
  late  String? title;

  @JsonKey(name: 'ANStatus')
  late int anStatus;

  @JsonKey(name: 'AnnounCategoryId')
  late int announCategoryId;

  @JsonKey(name: 'IconPath')
  late String? iconPath;

  @JsonKey(name: 'IsSurveyPoll')
  late int isSurveyPoll;

  @JsonKey(name: 'SearCol')
  late String searCol;
  

  Notify(
      this.id,
      this.userId,
      this.content,
      this.resourceCategoryId,
      this.resourceSubCategoryId,
      this.announcementId,
      this.link,
      this.icon,
      this.flgRead,
      this.sendTime,
      this.readTime,
      this.flgConfirm,
      this.flgConfirmed,
      this.flgReply,
      this.flgReplied,
      this.replyTime,
      this.replyContent,
      this.flgImmediately,
      this.showPopup,
      this.actionTime,
      this.modified,
      this.created,
      this.flgSurvey,
      this.title,
      this.anStatus,
      this.announCategoryId,
      this.iconPath,
      this.isSurveyPoll,
      this.searCol);
  factory Notify.fromJson(Map<String, dynamic> json) => _$NotifyFromJson(json);

  Map<String, dynamic> toJson() => _$NotifyToJson(this);
}


