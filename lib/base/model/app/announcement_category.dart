import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'announcement_category.g.dart';
@entity
@JsonSerializable()
class AnnouncementCategory {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;

  @JsonKey(name: 'Title')
  late String title;

  @JsonKey(name: 'TitleEN')
  late String? titleEN;

  @JsonKey(name: 'Description')
  late String description;

  @JsonKey(name: 'IconPath')
  late String iconPath;

  @JsonKey(name: 'ImagePath')
  late String imagePath;

  @JsonKey(name: 'AnnounceTemplateId')
  late int? announceTemplateId;

  @JsonKey(name: 'NotifyTemplateId')
  late int notifyTemplateId;

  @JsonKey(name: 'ResourceCategoryId')
  late int resourceCategoryId;

  @JsonKey(name: 'UrlDetail')
  late String? urlDetail;

  @JsonKey(name: 'RemindBeforeTime')
  late int remindBeforeTime;

  @JsonKey(name: 'IsCreate')
  late bool isCreate;

  @JsonKey(name: 'Device')
  late int device;

  @JsonKey(name: 'Modified')
  late String modified;

  @JsonKey(name: 'Created')
  late String created;

  @JsonKey(name: 'Orders')
  late int orders;

  AnnouncementCategory(
      this.id,
      this.title,
      this.titleEN,
      this.description,
      this.iconPath,
      this.imagePath,
      this.announceTemplateId,
      this.notifyTemplateId,
      this.resourceCategoryId,
      this.urlDetail,
      this.remindBeforeTime,
      this.isCreate,
      this.device,
      this.modified,
      this.created,
      this.orders);

  factory AnnouncementCategory.fromJson(Map<String, dynamic> json) => _$AnnouncementCategoryFromJson(json);

  Map<String, dynamic> toJson() => _$AnnouncementCategoryToJson(this);
}
