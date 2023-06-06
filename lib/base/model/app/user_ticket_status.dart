import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'user_ticket_status.g.dart';

@entity
@JsonSerializable()
class UserTicketStatus {
  @JsonKey(name: "ID")
  @primaryKey
  int id;
  @JsonKey(name: 'Title')
  String? title;
  @JsonKey(name: "TitleEN")
  String? titleEn;
  @JsonKey(name: 'Modified')
  String? modified;
  @JsonKey(name: 'Created')
  String? created;

  UserTicketStatus(
      this.id, this.title, this.titleEn, this.modified, this.created);

  factory UserTicketStatus.fromJson(Map<String, dynamic> json) =>
      _$UserTicketStatusFromJson(json);

  Map<String, dynamic> toJson() => _$UserTicketStatusToJson(this);
}
