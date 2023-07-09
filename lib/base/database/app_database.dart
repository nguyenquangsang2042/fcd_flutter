import 'dart:async';
import 'package:fcd_flutter/base/database/dao/airport_dao.dart';
import 'package:fcd_flutter/base/database/dao/banner_dao.dart';
import 'package:fcd_flutter/base/database/dao/db_variable_dao.dart';
import 'package:fcd_flutter/base/database/dao/department_dao.dart';
import 'package:fcd_flutter/base/database/dao/district_dao.dart';
import 'package:fcd_flutter/base/database/dao/faqs_dao.dart';
import 'package:fcd_flutter/base/database/dao/help_desk_category_dao.dart';
import 'package:fcd_flutter/base/database/dao/helpdesk_dao.dart';
import 'package:fcd_flutter/base/database/dao/library_dao.dart';
import 'package:fcd_flutter/base/database/dao/licence_dao.dart';
import 'package:fcd_flutter/base/database/dao/menu_home_dao.dart';
import 'package:fcd_flutter/base/database/dao/menuapp_dao.dart';
import 'package:fcd_flutter/base/database/dao/nation_dao.dart';
import 'package:fcd_flutter/base/database/dao/pilot_schedule_all_dao.dart';
import 'package:fcd_flutter/base/database/dao/settings_dao.dart';
import 'package:fcd_flutter/base/database/dao/student_dao.dart';
import 'package:fcd_flutter/base/database/dao/survey_category_dao.dart';
import 'package:fcd_flutter/base/database/dao/survey_dao.dart';
import 'package:fcd_flutter/base/database/dao/survey_table_dao.dart';
import 'package:fcd_flutter/base/database/dao/user_dao.dart';
import 'package:fcd_flutter/base/database/dao/user_ticket_category_dao.dart';
import 'package:fcd_flutter/base/database/dao/user_ticket_status_dao.dart';
import 'package:fcd_flutter/base/database/dao/ward_dao.dart';
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/app_language.dart';
import 'package:fcd_flutter/base/model/app/bean_banner.dart';
import 'package:fcd_flutter/base/model/app/bean_faqs.dart';
import 'package:fcd_flutter/base/model/app/bean_library.dart';
import 'package:fcd_flutter/base/model/app/db_variable.dart';
import 'package:fcd_flutter/base/model/app/district.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:fcd_flutter/base/model/app/help_desk_category.dart';
import 'package:fcd_flutter/base/model/app/helpdesk.dart';
import 'package:fcd_flutter/base/model/app/helpdesk_linhvuc.dart';
import 'package:fcd_flutter/base/model/app/licence.dart';
import 'package:fcd_flutter/base/model/app/menu_app.dart';
import 'package:fcd_flutter/base/model/app/menu_home.dart';
import 'package:fcd_flutter/base/model/app/nation.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/model/app/province.dart';
import 'package:fcd_flutter/base/model/app/settings.dart';
import 'package:fcd_flutter/base/model/app/student.dart';
import 'package:fcd_flutter/base/model/app/survey.dart';
import 'package:fcd_flutter/base/model/app/survey_category.dart';
import 'package:fcd_flutter/base/model/app/survey_table.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';
import 'package:fcd_flutter/base/model/app/ward.dart';
import 'package:floor/floor.dart';
import 'package:flutter/material.dart';
import 'package:sqflite/sqflite.dart' as sqflite;

import '../model/app/announcement_category.dart';
import '../model/app/department.dart';
import '../model/app/pilot_schedule_all.dart';
import '../model/app/pilot_schedule_pdf.dart';
import '../model/app/user.dart';
import '../model/app/user_ticket_category.dart';
import 'dao/announcement_category_dao.dart';
import 'dao/app_language_dao.dart';
import 'dao/helpdesk_linhvuc_dao.dart';
import 'dao/notify_dao.dart';
import 'dao/pilot_schedule_pdf_dao.dart';
import 'dao/province_dao.dart';

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
  PilotSchedulePdf,
  AnnouncementCategory,
  Nation,
  Province,
  Ward,
  District,
  Notify,
  MenuApp,
  MenuHome,
  BeanBanner,
  License,
  BeanLibrary,
  BeanFAQs,
  Helpdesk,
  Student,
  SurveyTable,
  Survey,
  SurveyCategory
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
  AnnouncementCategoryDao get announcementCategoryDao;
  NationDao get nationDao;
  ProvinceDao get provinceDao;
  DistrictDao get districtDao;
  WardDao get wardDao;
  NotifyDao get notifyDao;
  MenuAppDao get menuAppDao;
  MenuHomeDao get menuHomeDao;
  BannerDao get bannerDao;
  LicenceDao get licenceDao;
  LibraryDao get libraryDao;
  HelpdeskDao get helpdeskDao;
  StudentDao get studentDao;
  SurveyTableDao get surveyTableDao;
  SurveyDao get surveyDao;
  SurveyCategoryDao get surveyCategoryDao;
}
