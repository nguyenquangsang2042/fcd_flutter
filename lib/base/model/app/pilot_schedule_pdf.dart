import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'pilot_schedule_pdf.g.dart';
@entity
@JsonSerializable()
class PilotSchedulePdf {
  @primaryKey
  @JsonKey(name: 'ID')
  late int id;

  @JsonKey(name: 'Title')
  late String title;

  @JsonKey(name: 'FilePath')
  late String filePath;

  @JsonKey(name: 'ScheduleDate')
  late String scheduleDate;

  @JsonKey(name: 'Creator')
  late String creator;

  @JsonKey(name: 'UserModified')
  late String userModified;

  @JsonKey(name: 'Created')
  late String created;

  @JsonKey(name: 'Modified')
  late String modified;

  PilotSchedulePdf(this.id, this.title, this.filePath, this.scheduleDate,
      this.creator, this.userModified, this.created, this.modified);

  factory PilotSchedulePdf.fromJson(Map<String, dynamic> json) =>
      _$PilotSchedulePdfFromJson(json);

  Map<String, dynamic> toJson() => _$PilotSchedulePdfToJson(this);
}
