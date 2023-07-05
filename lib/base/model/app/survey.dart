import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'survey.g.dart';
@entity
@JsonSerializable()
class Survey {
  @primaryKey
  @JsonKey(name: 'ID')
  String id;

  @JsonKey(name: 'Title')
  String title;

  @JsonKey(name: 'StartDate')
  String? startDate;

  @JsonKey(name: 'SurveyCategoryId')
  int surveyCategoryId;

  @JsonKey(name: 'TestForm')
  String testForm;

  @JsonKey(name: 'Description')
  String? description;

  @JsonKey(name: 'ActionStatus')
  int? actionStatus;

  @JsonKey(name: 'Status')
  int status;

  @JsonKey(name: 'Modified')
  String? modified;

  @JsonKey(name: 'Created')
  String? created;

  @JsonKey(name: 'UrlReview')
  String urlReview;

  @JsonKey(name: 'Point')
  String? point;

  @JsonKey(name: 'PerId')
  String perId;

  @JsonKey(name: 'PermissionType')
  int permissionType;

  @JsonKey(name: 'DepartmentID')
  double departmentId;

  @JsonKey(name: 'ListDepartmentID')
  String listDepartmentId;

  @JsonKey(name: 'ParentId')
  String? parentId;

  @JsonKey(name: 'FileExport')
  String? fileExport;

  bool? isExpand = true;
  bool? isSelected = false;


  Survey(
      this.id,
      this.title,
      this.startDate,
      this.surveyCategoryId,
      this.testForm,
      this.description,
      this.actionStatus,
      this.status,
      this.modified,
      this.created,
      this.urlReview,
      this.point,
      this.perId,
      this.permissionType,
      this.departmentId,
      this.listDepartmentId,
      this.parentId,
      this.fileExport,
      this.isExpand,
      this.isSelected);

  factory Survey.fromJson(Map<String, dynamic> json) => _$SurveyFromJson(json);

  Map<String, dynamic> toJson() => _$SurveyToJson(this);
}
