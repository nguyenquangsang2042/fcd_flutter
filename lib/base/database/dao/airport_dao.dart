
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:floor/floor.dart';

@dao
abstract class AirportDao {
  @Insert(onConflict: OnConflictStrategy.replace)
  Future<void> insertAirport(List<Airport> airports);
  @Query('Delete From Airport')
  Future<void> deleteAll();
}
