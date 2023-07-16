import 'package:fcd_flutter/base/model/app/pilot_schedule.dart';
import 'package:floor/floor.dart';

@dao
abstract class PilotScheduleDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertPilotSchedules(List<PilotSchedule> pilotSchedule);
  @Query(
      "SELECT Schedule.*, AP.Code AS ApCodeFrom, AP2.Code AS ApCodeTo FROM PilotSchedule Schedule INNER JOIN Airport AP "
          "ON Schedule.FromId = AP.ID INNER JOIN Airport AP2 ON Schedule.ToId = AP2.ID where Schedule.status <> -1 ORDER BY Schedule.DepartureTime ")
  Stream<List<PilotSchedule>> getScheduleEvents();
}
