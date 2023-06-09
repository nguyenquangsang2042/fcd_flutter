import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'bean_faqs.g.dart';

@JsonSerializable()
@entity
class BeanFAQs {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;

  @JsonKey(name: 'DepartmentId')
  late int departmentId;

  @JsonKey(name: 'Question')
  late String question;

  @JsonKey(name: 'Answer')
  late String answer;

  @JsonKey(name: 'Status')
  late int status;

  @JsonKey(name: 'Created')
  late String created;

  @JsonKey(name: 'Modified')
  late String modified;

  @JsonKey(name: 'Language')
  late String language;


  BeanFAQs(this.id, this.departmentId, this.question, this.answer, this.status,
      this.created, this.modified, this.language);

  factory BeanFAQs.fromJson(Map<String, dynamic> json) => _$BeanFAQsFromJson(json);

  Map<String, dynamic> toJson() => _$BeanFAQsToJson(this);
}
