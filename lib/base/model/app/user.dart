import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'user.g.dart';
@entity
@JsonSerializable()
class User{
  @primaryKey
  @JsonKey(name: 'ID')
  final String id;
  @JsonKey(name: 'Code')
  final String? code;
  @JsonKey(name: 'Code2')
  final String? code2;
  @JsonKey(name: 'Code3')
  final String? code3;
  @JsonKey(name: 'FullName')
  final String? fullName;
  @JsonKey(name: 'FullNameNoAccent')
  final String? fullNameNoAccent;
  @JsonKey(name: 'Alias')
  final String? alias;
  @JsonKey(name: 'Gender')
  final bool gender;
  @JsonKey(name: 'Birthplace')
  final String? birthplace;
  @JsonKey(name: 'Mobile')
  final String? mobile;
  @JsonKey(name: 'Email')
  final String? email;
  @JsonKey(name: 'Avatar')
  final String? avatar;
  @JsonKey(name: 'EmailNoDomain')
  final String? emailNoDomain;
  @JsonKey(name: 'Department')
  final int department;
  @JsonKey(name: 'DepartmentName')
  final String? departmentName;
  @JsonKey(name: 'Position')
  final int position;
  @JsonKey(name: 'PositionName')
  final String? positionName;
  @JsonKey(name: 'Modified')
  final String? modified;
  @JsonKey(name: 'Nationality')
  final String? nationality;
  @JsonKey(name: 'WorkingPattern')
  final String? workingPattern;
  @JsonKey(name: 'Status')
  final int status;
  @JsonKey(name: 'SpecialContent')
  final String? specialContent;
  @JsonKey(name: 'Birthday')
  final String? birthday;
  @JsonKey(name: 'Address')
  final String? address;
  @JsonKey(name: 'IdentityNum')
  final String? identityNumber;
  @JsonKey(name: 'NgayVaoDang')
  final String? ngayVaoDang;
  @JsonKey(name: 'StartDateWork')
  final String? startDateWork;
  @JsonKey(name: 'Base')
  final String? base;
  @JsonKey(name: 'IDNumber')
  final String? idNumber;
  @JsonKey(name: 'RewardDiscipline')
  final String? rewardDiscipline;
  @JsonKey(name: 'EstimatedFlightTimeInMonth')
  final String? estimatedFlightTimeInMonth;
  late bool isCurrent;

  User(
      this.id,
      this.code,
      this.code2,
      this.code3,
      this.fullName,
      this.fullNameNoAccent,
      this.alias,
      this.gender,
      this.birthplace,
      this.mobile,
      this.email,
      this.avatar,
      this.emailNoDomain,
      this.department,
      this.departmentName,
      this.position,
      this.positionName,
      this.modified,
      this.nationality,
      this.workingPattern,
      this.status,
      this.specialContent,
      this.birthday,
      this.address,
      this.identityNumber,
      this.ngayVaoDang,
      this.startDateWork,
      this.base,
      this.idNumber,
      this.rewardDiscipline,
      this.estimatedFlightTimeInMonth);
  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
  Map<String, dynamic> toJson() => _$UserToJson(this);
}