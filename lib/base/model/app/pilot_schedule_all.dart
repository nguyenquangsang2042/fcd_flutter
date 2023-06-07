import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';
part 'pilot_schedule_all.g.dart';
@entity
@JsonSerializable()
class PilotScheduleAll
{
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
  late String? userModified;
  @JsonKey(name: 'Modified')
  late String modified;
  @JsonKey(name: 'Created')
  late String created;

  PilotScheduleAll(this.id, this.title, this.filePath, this.scheduleDate,
      this.creator, this.userModified, this.modified, this.created);
  factory PilotScheduleAll.fromJson(Map<String, dynamic> json) => _$PilotScheduleAllFromJson(json);
  Map<String, dynamic> toJson() => _$PilotScheduleAllToJson(this);
}