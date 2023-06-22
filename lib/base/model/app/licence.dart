import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'licence.g.dart';

@JsonSerializable()
@entity
class License {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;
  @JsonKey(name: 'UserId')
  late String userId;
  @JsonKey(name: 'LicenseTypeID')
  late int licenseTypeId;
  @JsonKey(name: 'Num')
  late String number;
  @JsonKey(name: 'Date')
  late String date;
  @JsonKey(name: 'ExpireDate')
  late String expireDate;
  @JsonKey(name: 'Status')
  late int status;
  @JsonKey(name: 'Note')
  late String note;
  @JsonKey(name: 'Created')
  late String created;
  @JsonKey(name: 'Modified')
  late String modified;
  @JsonKey(name: 'AlertDate')
  late String? alertDate;
  @JsonKey(name: 'LicenseType')
  late String licenseType;
  @JsonKey(name: 'IsImportant')
  late bool isImportant;

  License(
      this.id,
      this.userId,
      this.licenseTypeId,
      this.number,
      this.date,
      this.expireDate,
      this.status,
      this.note,
      this.created,
      this.modified,
      this.alertDate,
      this.licenseType,
      this.isImportant);

  factory License.fromJson(Map<String, dynamic> json) =>
      _$LicenseFromJson(json);

  Map<String, dynamic> toJson() => _$LicenseToJson(this);
}
