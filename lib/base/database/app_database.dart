import 'dart:async';
import 'package:fcd_flutter/base/database/dao/airport_dao.dart';
import 'package:fcd_flutter/base/database/dao/db_variable_dao.dart';
import 'package:fcd_flutter/base/database/dao/settings_dao.dart';
import 'package:fcd_flutter/base/database/dao/user_dao.dart';
import 'package:fcd_flutter/base/database/dao/user_ticket_status_dao.dart';
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/db_variable.dart';
import 'package:fcd_flutter/base/model/app/settings.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';
import 'package:floor/floor.dart';
import 'package:sqflite/sqflite.dart' as sqflite;

import '../model/app/user.dart';

part 'app_database.g.dart'; // the generated code will be there

@Database(version: 1, entities: [DBVariable, Setting,User,Airport,UserTicketStatus])
abstract class AppDatabase extends FloorDatabase {
  SettingsDao get settingDao;

  DBVariableDao get dbVariableDao;
  UserDao get userDao;
  AirportDao get airportDao;
  UserTicketStatusDao get userTicketStatusDao;
}
