import 'package:json_annotation/json_annotation.dart';

part 'survey_list_pilot.g.dart';

@JsonSerializable()
class SurveyListPilot {
  @JsonKey(name: 'SurveyTableId')
  String surveyTableId;

  @JsonKey(name: 'UserId1')
  String userId1;

  @JsonKey(name: 'FullName1')
  String fullName1;

  @JsonKey(name: 'Mobile1')
  String mobile1;

  @JsonKey(name: 'Avatar1')
  String avatar1;

  @JsonKey(name: 'PerId1')
  String perId1;

  @JsonKey(name: 'DepartmentName1')
  String departmentName1;

  @JsonKey(name: 'Step1')
  String step1;

  @JsonKey(name: 'UserId2')
  String userId2;

  @JsonKey(name: 'FullName2')
  String fullName2;

  @JsonKey(name: 'PerId2')
  String perId2;

  @JsonKey(name: 'Mobile2')
  String mobile2;

  @JsonKey(name: 'Avatar2')
  String avatar2;

  @JsonKey(name: 'DepartmentName2')
  String departmentName2;

  @JsonKey(name: 'Step2')
  String step2;

  @JsonKey(name: 'UrlReview1')
  String urlReview1;

  @JsonKey(name: 'UrlReview2')
  String urlReview2;

  @JsonKey(name: 'User1Code2')
  String user1Code2;

  @JsonKey(name: 'User2Code2')
  String user2Code2;

  @JsonKey(name: 'PilotPair')
  String pilotPair;

  @JsonKey(name: 'Supporter')
  bool? supporter;

  @JsonKey(name: 'Supporter1')
  bool? supporter1;

  @JsonKey(name: 'Supporter2')
  bool? supporter2;

  @JsonKey(name: 'isSelected')
  bool isSelected;

  @JsonKey(name: 'isSelected1a')
  bool isSelected1a;

  @JsonKey(name: 'isSelected1b')
  bool isSelected1b;

  @JsonKey(name: 'FileExport')
  String fileExport;

  @JsonKey(name: 'Date')
  String? date;

  @JsonKey(name: 'Status1')
  int? status1;

  @JsonKey(name: 'Status2')
  int? status2;


  SurveyListPilot(
      this.surveyTableId,
      this.userId1,
      this.fullName1,
      this.mobile1,
      this.avatar1,
      this.perId1,
      this.departmentName1,
      this.step1,
      this.userId2,
      this.fullName2,
      this.perId2,
      this.mobile2,
      this.avatar2,
      this.departmentName2,
      this.step2,
      this.urlReview1,
      this.urlReview2,
      this.user1Code2,
      this.user2Code2,
      this.pilotPair,
      this.supporter,
      this.supporter1,
      this.supporter2,
      this.isSelected,
      this.isSelected1a,
      this.isSelected1b,
      this.fileExport,
      this.date,
      this.status1,
      this.status2);

  factory SurveyListPilot.fromJson(Map<String, dynamic> json) =>
      _$SurveyListPilotFromJson(json);

  Map<String, dynamic> toJson() => _$SurveyListPilotToJson(this);
}
