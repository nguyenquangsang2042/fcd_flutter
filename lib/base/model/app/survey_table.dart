import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'survey_table.g.dart';

@JsonSerializable()
@entity
class SurveyTable {
  @primaryKey
  @JsonKey(name: 'ID')
  String id;

  @JsonKey(name: 'SID')
  int sid;

  @JsonKey(name: 'Title')
  String title;

  @JsonKey(name: 'Status')
  int? status;

  @JsonKey(name: 'StartDate')
  String? startDate;

  @JsonKey(name: 'DueDate')
  String? dueDate;

  @JsonKey(name: 'SurveyCategoryId')
  int? surveyCategoryId;

  @JsonKey(name: 'Permission')
  String? permission;

  @JsonKey(name: 'PermissionName')
  String? permissionName;

  @JsonKey(name: 'Description')
  String? description;

  @JsonKey(name: 'AllowMultipleResponses')
  bool allowMultipleResponses;

  @JsonKey(name: 'LoginRequired')
  bool loginRequired;

  @JsonKey(name: 'Index')
  int index;

  @JsonKey(name: 'Modified')
  String? modified;

  @JsonKey(name: 'ModifiedBy')
  String modifiedBy;

  @JsonKey(name: 'DesignModified')
  String designModified;

  @JsonKey(name: 'Created')
  String? created;

  @JsonKey(name: 'CreatedBy')
  String createdBy;

  @JsonKey(name: 'Options')
  String? options;

  @JsonKey(name: 'AllowBranchPageBreak')
  bool allowBranchPageBreak;

  @JsonKey(name: 'ActionStatus')
  int actionStatus;


  SurveyTable(
      this.id,
      this.sid,
      this.title,
      this.status,
      this.startDate,
      this.dueDate,
      this.surveyCategoryId,
      this.permission,
      this.permissionName,
      this.description,
      this.allowMultipleResponses,
      this.loginRequired,
      this.index,
      this.modified,
      this.modifiedBy,
      this.designModified,
      this.created,
      this.createdBy,
      this.options,
      this.allowBranchPageBreak,
      this.actionStatus);

  factory SurveyTable.fromJson(Map<String, dynamic> json) =>
      _$SurveyTableFromJson(json);

  Map<String, dynamic> toJson() => _$SurveyTableToJson(this);
}
