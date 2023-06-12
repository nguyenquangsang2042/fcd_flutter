import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'bean_banner.g.dart';

@JsonSerializable()
@entity
class BeanBanner {
  @primaryKey
  @JsonKey(name: 'ID')
  int id;
  @JsonKey(name: 'Title')
  String title;
  @JsonKey(name: 'FileName')
  String fileName;
  @JsonKey(name: 'FilePath')
  String filePath;
  @JsonKey(name: 'Created')
  String created;
  @JsonKey(name: 'Status')
  int status;
  @JsonKey(name: 'SortOrder')
  int sortOrder;

  BeanBanner(this.id, this.title, this.fileName, this.filePath, this.created,
      this.status, this.sortOrder);

  factory BeanBanner.fromJson(Map<String, dynamic> json) => _$BeanBannerFromJson(json);

  Map<String, dynamic> toJson() => _$BeanBannerToJson(this);
}
