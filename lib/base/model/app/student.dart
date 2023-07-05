import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'student.g.dart';

@JsonSerializable()
@entity
class Student {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;

  @JsonKey(name: 'Title')
  late String title;

  @JsonKey(name: 'Description')
  late String description;

  @JsonKey(name: 'LicenseTypeID')
  late int licenseTypeId;

  @JsonKey(name: 'LicenseName')
  late String? licenseName;

  @JsonKey(name: 'NotifyID')
  late String? notifyId;

  @JsonKey(name: 'AlertDate')
  late String? alertDate;

  @JsonKey(name: 'Modified')
  late String modified;
  Student(this.id, this.title, this.description, this.licenseTypeId,
      this.licenseName, this.notifyId, this.alertDate, this.modified);

  factory Student.fromJson(Map<String, dynamic> json) => _$StudentFromJson(json);

  Map<String, dynamic> toJson() => _$StudentToJson(this);
}
