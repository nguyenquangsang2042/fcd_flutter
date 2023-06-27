import 'package:fcd_flutter/base/model/app/helpdesk.dart';
import 'package:floor/floor.dart';

@dao
abstract class HelpdeskDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertHelpdesk(List<Helpdesk> helpdesk);
}