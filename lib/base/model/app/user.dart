import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'user.g.dart';
@entity
@JsonSerializable()
class User{
  @primaryKey
  @JsonKey(name: 'ID')
  late String id;
  @JsonKey(name: 'Code')
  late String? code;
  @JsonKey(name: 'Code2')
  late String? code2;
  @JsonKey(name: 'Code3')
  late String? code3;
  @JsonKey(name: 'FullName')
  late String? fullName;
  @JsonKey(name: 'FullNameNoAccent')
  late String? fullNameNoAccent;
  @JsonKey(name: 'Alias')
  late String? alias;
  @JsonKey(name: 'Gender')
  late bool gender;
  @JsonKey(name: 'Birthplace')
  late String? birthplace;
  @JsonKey(name: 'Mobile')
  late String? mobile;
  @JsonKey(name: 'Email')
  late String? email;
  @JsonKey(name: 'Avatar')
  late String? avatar;
  @JsonKey(name: 'EmailNoDomain')
  late String? emailNoDomain;
  @JsonKey(name: 'Department')
  late int department;
  @JsonKey(name: 'DepartmentName')
  late String? departmentName;
  @JsonKey(name: 'Position')
  late int position;
  @JsonKey(name: 'PositionName')
  late String? positionName;
  @JsonKey(name: 'Modified')
  late String? modified;
  @JsonKey(name: 'Nationality')
  late String? nationality;
  @JsonKey(name: 'WorkingPattern')
  late String? workingPattern;
  @JsonKey(name: 'Status')
  late int status;
  @JsonKey(name: 'SpecialContent')
  late String? specialContent;
  @JsonKey(name: 'Birthday')
  late String? birthday;
  @JsonKey(name: 'Address')
  late String? address;
  @JsonKey(name: 'IdentityNum')
  late String? identityNumber;
  @JsonKey(name: 'NgayVaoDang')
  late String? ngayVaoDang;
  @JsonKey(name: 'StartDateWork')
  late String? startDateWork;
  @JsonKey(name: 'Base')
  late String? base;
  @JsonKey(name: 'IDNumber')
  late String? idNumber;
  @JsonKey(name: 'RewardDiscipline')
  late String? rewardDiscipline;
  @JsonKey(name: 'EstimatedFlightTimeInMonth')
  late String? estimatedFlightTimeInMonth;
  @JsonKey(name: 'UserType')
  late int userType;

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
      this.estimatedFlightTimeInMonth,
      this.userType);

  User.none();

  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
  Map<String, dynamic> toJson() => _$UserToJson(this);
}