import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'user_ticket_category.g.dart';
@entity
@JsonSerializable()
class UserTicketCategory
{
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;
  @JsonKey(name: 'Title')
  late String title;
  @JsonKey(name: 'Created')
  late String created;
  @JsonKey(name: 'Modified')
  late String modified;

  UserTicketCategory(this.id, this.title, this.created, this.modified);

  factory UserTicketCategory.fromJson(Map<String, dynamic> json) => _$UserTicketCategoryFromJson(json);
  Map<String, dynamic> toJson() => _$UserTicketCategoryToJson(this);
}