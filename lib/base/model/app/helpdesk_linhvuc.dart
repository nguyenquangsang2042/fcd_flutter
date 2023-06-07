import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'helpdesk_linhvuc.g.dart';
@entity
@JsonSerializable()
class HelpDeskLinhVuc
{
  @primaryKey
  @JsonKey(name: 'ID')
  late double id;
  @JsonKey(name: 'Title_EN')
  late String titleEn;
  @JsonKey(name: 'Title_VN')
  late String titleVn;
  @JsonKey(name: 'Status')
  late int status;
  @JsonKey(name: 'Order')
  late int order;
  @JsonKey(name: 'Modified')
  late String modified;
  @JsonKey(name: 'IDGroupMail')
  late String idGroupMail;

  HelpDeskLinhVuc(this.id, this.titleEn, this.titleVn, this.status, this.order,
      this.modified, this.idGroupMail);
  factory HelpDeskLinhVuc.fromJson(Map<String, dynamic> json) => _$HelpDeskLinhVucFromJson(json);
  Map<String, dynamic> toJson() => _$HelpDeskLinhVucToJson(this);
}