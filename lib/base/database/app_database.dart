import 'dart:async';
import 'package:fcd_flutter/base/database/dao/airport_dao.dart';
import 'package:fcd_flutter/base/database/dao/db_variable_dao.dart';
import 'package:fcd_flutter/base/database/dao/department_dao.dart';
import 'package:fcd_flutter/base/database/dao/faqs_dao.dart';
import 'package:fcd_flutter/base/database/dao/help_desk_category_dao.dart';
import 'package:fcd_flutter/base/database/dao/pilot_schedule_all_dao.dart';
import 'package:fcd_flutter/base/database/dao/settings_dao.dart';
import 'package:fcd_flutter/base/database/dao/user_dao.dart';
import 'package:fcd_flutter/base/database/dao/user_ticket_category_dao.dart';
import 'package:fcd_flutter/base/database/dao/user_ticket_status_dao.dart';
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/app_language.dart';
import 'package:fcd_flutter/base/model/app/db_variable.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:fcd_flutter/base/model/app/help_desk_category.dart';
import 'package:fcd_flutter/base/model/app/helpdesk_linhvuc.dart';
import 'package:fcd_flutter/base/model/app/settings.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';
import 'package:floor/floor.dart';
import 'package:sqflite/sqflite.dart' as sqflite;

import '../model/app/department.dart';
import '../model/app/pilot_schedule_all.dart';
import '../model/app/pilot_schedule_pdf.dart';
import '../model/app/user.dart';
import '../model/app/user_ticket_category.dart';
import 'dao/app_language_dao.dart';
import 'dao/helpdesk_linhvuc_dao.dart';
import 'dao/pilot_schedule_pdf_dao.dart';

part 'app_database.g.dart'; // the generated code will be there

@Database(version: 1, entities: [
  DBVariable,
  Setting,
  User,
  Airport,
  UserTicketStatus,
  AppLanguage,
  UserTicketCategory,
  FAQs,
  HelpDeskCategory,
  PilotScheduleAll,
  HelpDeskLinhVuc,
  Department,
  PilotSchedulePdf
])
abstract class AppDatabase extends FloorDatabase {
  SettingsDao get settingDao;

  DBVariableDao get dbVariableDao;
  UserDao get userDao;
  AirportDao get airportDao;
  UserTicketStatusDao get userTicketStatusDao;
  AppLanguageDao get appLanguageDao;
  UserTicketCategoryDao get userTicketCategoryDao;
  FAQsDao get faqDao;
  HelpDeskCategoryDao get helpDeskCategoryDao;
  PilotScheduleAllDao get pilotScheduleAllDao;
  HelpDeskLinhVucDao get helpDeskLinhVucDao;
  DepartmentDao get departmentDao;
  PilotSchedulePdfDao get pilotSchedulePdfDao;
}
