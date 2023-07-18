
import 'package:floor/floor.dart';

import '../../model/app/user_ticket_status.dart';

@dao
abstract class UserTicketStatusDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertUserTicketStatuses(List< UserTicketStatus> userTicketStatuses);
  @Query('Delete From UserTicketStatus')
  Future<void> deleteAll();
}