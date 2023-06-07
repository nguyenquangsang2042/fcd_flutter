
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_category.dart';
import 'package:floor/floor.dart';

@dao
abstract class UserTicketCategoryDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertUserTicketCategories(List<UserTicketCategory> userTicketCategories);
}
