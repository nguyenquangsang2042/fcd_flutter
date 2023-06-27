import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'helpdesk.g.dart';
@entity
@JsonSerializable()
class Helpdesk {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;

  @JsonKey(name: 'CategoryId')
  late int categoryId;

  @JsonKey(name: 'Title')
  late String title;

  @JsonKey(name: 'Content')
  late String content;

  @JsonKey(name: 'ReplyContent')
  late String? replyContent;

  @JsonKey(name: 'UserId')
  late String userId;

  @JsonKey(name: 'DepartmentId')
  late int departmentId;

  @JsonKey(name: 'Status')
  late int status;

  @JsonKey(name: 'Modified')
  late String? modified;

  @JsonKey(name: 'Created')
  late String? created;

  @JsonKey(name: 'AdminReply')
  late String? adminReply;

  @JsonKey(name: 'DateReply')
  late String? dateReply;


  Helpdesk(
      this.id,
      this.categoryId,
      this.title,
      this.content,
      this.replyContent,
      this.userId,
      this.departmentId,
      this.status,
      this.modified,
      this.created,
      this.adminReply,
      this.dateReply);

  factory Helpdesk.fromJson(Map<String, dynamic> json) => _$HelpdeskFromJson(json);

  Map<String, dynamic> toJson() => _$HelpdeskToJson(this);
}
