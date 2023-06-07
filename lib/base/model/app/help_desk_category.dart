import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'help_desk_category.g.dart';
@entity
@JsonSerializable()
class HelpDeskCategory
{
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;
  @JsonKey(name: 'Title')
  late String title;
  @JsonKey(name: 'Description')
  late String? description;
  @JsonKey(name: 'Modified')
  late String modified;
  @JsonKey(name: 'Created')
  late String created;

  HelpDeskCategory(
      this.id, this.title, this.description, this.modified, this.created);
  factory HelpDeskCategory.fromJson(Map<String, dynamic> json) => _$HelpDeskCategoryFromJson(json);
  Map<String, dynamic> toJson() => _$HelpDeskCategoryToJson(this);
}