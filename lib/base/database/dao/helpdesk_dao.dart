import 'package:fcd_flutter/base/model/app/helpdesk.dart';
import 'package:floor/floor.dart';

@dao
abstract class HelpdeskDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertHelpdesk(List<Helpdesk> helpdesk);
  
  @Query("SELECT * FROM Helpdesk WHERE Status = 1  ORDER BY Created DESC")
  Stream<List<Helpdesk>> getAllHelpDeskStatusEquals1();
}