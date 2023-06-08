import 'package:fcd_flutter/base/model/app/settings.dart';
import 'package:floor/floor.dart';

import '../../model/app/pilot_schedule_pdf.dart';

@dao
abstract class PilotSchedulePdfDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertPilotSchedulePdf(List<PilotSchedulePdf> pilotSchedulePdf);
}
