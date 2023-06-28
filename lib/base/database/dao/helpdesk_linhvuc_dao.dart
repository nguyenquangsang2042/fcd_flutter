import 'package:fcd_flutter/base/model/app/helpdesk_linhvuc.dart';
import 'package:fcd_flutter/base/model/app/pilot_schedule_all.dart';
import 'package:floor/floor.dart';

@dao
abstract class HelpDeskLinhVucDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertHelpDeskLinhVucs(List<HelpDeskLinhVuc> helpDeskLinhVucs);
  @Query('Select * HelpDeskLinhVuc Order by [Order]')
  Stream<List<HelpDeskLinhVuc>> getAllHelpdeskLinhVuc();
}
