import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'survey_category.g.dart';
@entity
@JsonSerializable()
class SurveyCategory {
  @primaryKey
  @JsonKey(name: 'ID')
  int? id;
  @JsonKey(name: 'Title')
  String? title;
  @JsonKey(name: 'TitleEN')
  String? titleEN;
  @JsonKey(name: 'Type')
  bool? type;
  @JsonKey(name: 'Code')
  String? code;
  @JsonKey(name: 'Status')
  int? status;
  @JsonKey(name: 'Index')
  int? index;
  @JsonKey(name: 'ParentId')
  int? parentId;
  @JsonKey(name: 'Modified')
  String? modified;
  @JsonKey(name: 'Created')
  String? created;
  @JsonKey(name: 'IsTheory')
  bool? isTheory;
  @JsonKey(name: 'IsTheoryTest')
  bool? isTheoryTest;
  @JsonKey(name: 'TestForm')
  String? testForm;
  @JsonKey(name: 'SessionNo')
  String? sessionNo;
  @JsonKey(name: 'Parent')
  String? parent;

  SurveyCategory(
      this.id,
      this.title,
      this.titleEN,
      this.type,
      this.code,
      this.status,
      this.index,
      this.parentId,
      this.modified,
      this.created,
      this.isTheory,
      this.isTheoryTest,
      this.testForm,
      this.sessionNo,
      this.parent);

  SurveyCategory.none();


  SurveyCategory.all(this.id, this.title);

  factory SurveyCategory.fromJson(Map<String, dynamic> json) =>
      _$SurveyCategoryFromJson(json);

  Map<String, dynamic> toJson() => _$SurveyCategoryToJson(this);
}
