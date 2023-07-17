import 'package:floor/floor.dart';
import 'package:json_annotation/json_annotation.dart';

part 'pilot_schedule.g.dart';
@entity
@JsonSerializable()
class PilotSchedule {
  @primaryKey
  @JsonKey(name: 'ID')
  int id;

  @JsonKey(name: 'RoutingId')
  int routingId;

  @JsonKey(name: 'FromId')
  int? fromId;

  @JsonKey(name: 'ToId')
  int? toId;

  @JsonKey(name: 'DepartureTime')
  String? departureTime;

  @JsonKey(name: 'BoardingTime')
  String? boardingTime;

  @JsonKey(name: 'ArrivalTime')
  String? arrivalTime;

  @JsonKey(name: 'FlightNo')
  String flightNo;

  @JsonKey(name: 'FlightNo2')
  String flightNo2;

  @JsonKey(name: 'Apl')
  String apl;

  @JsonKey(name: 'Personal')
  String personal;

  @JsonKey(name: 'TaskPositionName')
  String taskPositionName;

  @JsonKey(name: 'Notes')
  String notes;

  @JsonKey(name: 'UserId')
  String userId;

  @JsonKey(name: 'Status')
  int? status;

  @JsonKey(name: 'Modified')
  String? modified;

  @JsonKey(name: 'Created')
  String? created;

  @JsonKey(name: 'UserModified')
  String userModified;

  @JsonKey(name: 'Creator')
  String creator;

  @JsonKey(name: 'AlertDate')
  String? alertDate;

  @JsonKey(name: 'LstAllPersonal')
  String lstAllPersonal;

  String? apCodeFrom;
  String? apFromTitle;
  String? apCodeTo;
  String? apToTitle;


  PilotSchedule(
      this.id,
      this.routingId,
      this.fromId,
      this.toId,
      this.departureTime,
      this.boardingTime,
      this.arrivalTime,
      this.flightNo,
      this.flightNo2,
      this.apl,
      this.personal,
      this.taskPositionName,
      this.notes,
      this.userId,
      this.status,
      this.modified,
      this.created,
      this.userModified,
      this.creator,
      this.alertDate,
      this.lstAllPersonal,
      this.apCodeFrom,
      this.apFromTitle,
      this.apCodeTo,
      this.apToTitle);

  factory PilotSchedule.fromJson(Map<String, dynamic> json) =>
      _$PilotScheduleFromJson(json);

  Map<String, dynamic> toJson() => _$PilotScheduleToJson(this);
}
