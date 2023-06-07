import 'package:fcd_flutter/base/model/app/pilot_schedule_all.dart';
import 'package:floor/floor.dart';

@dao
abstract class PilotScheduleAllDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertPilotScheduleAll(List<PilotScheduleAll> pilotScheduleAll);
}
