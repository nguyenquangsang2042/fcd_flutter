import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/bean_banner.dart';
import 'package:fcd_flutter/base/model/app/bean_faqs.dart';
import 'package:fcd_flutter/base/model/app/department.dart';
import 'package:fcd_flutter/base/model/app/district.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:fcd_flutter/base/model/app/help_desk_category.dart';
import 'package:fcd_flutter/base/model/app/helpdesk_linhvuc.dart';
import 'package:fcd_flutter/base/model/app/licence.dart';
import 'package:fcd_flutter/base/model/app/menu_app.dart';
import 'package:fcd_flutter/base/model/app/menu_home.dart';
import 'package:fcd_flutter/base/model/app/nation.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/model/app/student.dart';
import 'package:fcd_flutter/base/model/app/survey.dart';
import 'package:fcd_flutter/base/model/app/survey_table.dart';
import 'package:fcd_flutter/base/model/app/user.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_category.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';
import '../constants.dart';
import '../model/api_list.dart';
import '../model/app/announcement_category.dart';
import '../model/app/app_language.dart';
import '../model/app/db_variable.dart';
import '../model/app/helpdesk.dart';
import '../model/app/pilot_schedule_all.dart';
import '../model/app/pilot_schedule_pdf.dart';
import '../model/app/province.dart';
import '../model/app/settings.dart';
import '../model/app/ward.dart';

class ApiController {
  void updateMasterData() async {
    updateSetting();
    updateUser();
    updateAirport();
    updateUserTicketStatuses();
    updateAppLanguage();
    updateUserTicketCategory();
    updateFAQs();
    updateHelpDeskCategories();
    updatePilotScheduleAll();
    updateHelpDeskLinhVuc();
    updateDepartments();
    updatePilotSchedulePdf();
    updateAnnouncementCategory();
    updateNation();
    updateProvince();
    updateDistrict();
    updateWard();
    updateMenuApp();
  }

  void updateSetting() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("Setting");
    if (dbVariable != null) {
      Constants.api.getSettings(dbVariable.Value, "0").then((value) {
        ApiList<Setting> data = value;
        Constants.db.settingDao.insertSettings(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Setting", data.dateNow));
      });
    } else {
      Constants.api.getSettings("", "1").then((value) {
        ApiList<Setting> data = value;
        Constants.db.settingDao.insertSettings(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Setting", data.dateNow));
      });
    }
  }

  void updateUser() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("User");
    if (dbVariable != null) {
      Constants.api.getUsers(dbVariable.Value, "0").then((value) {
        ApiList<User> data = value;
        Constants.db.userDao.insertUsers(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("User", data.dateNow));
      });
    } else {
      Constants.api.getUsers("", "1").then((value) {
        ApiList<User> data = value;
        Constants.db.userDao.insertUsers(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("User", data.dateNow));
      });
    }
  }

  void updateAirport() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("Airport");
    if (dbVariable != null) {
      Constants.api.getAirports(dbVariable.Value, "0").then((value) {
        ApiList<Airport> data = value;
        Constants.db.airportDao.insertAirport(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Airport", data.dateNow));
      });
    } else {
      Constants.api.getAirports("", "1").then((value) {
        ApiList<Airport> data = value;
        Constants.db.airportDao.insertAirport(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Airport", data.dateNow));
      });
    }
  }

  void updateUserTicketStatuses() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("UserTicketStatus");
    if (dbVariable != null) {
      Constants.api.getUserTicketStatuses(dbVariable.Value, "0").then((value) {
        ApiList<UserTicketStatus> data = value;
        Constants.db.userTicketStatusDao.insertUserTicketStatuses(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("UserTicketStatus", data.dateNow));
      });
    } else {
      Constants.api.getUserTicketStatuses("", "1").then((value) {
        ApiList<UserTicketStatus> data = value;
        Constants.db.userTicketStatusDao.insertUserTicketStatuses(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("UserTicketStatus", data.dateNow));
      });
    }
  }

  void updateAppLanguage() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("AppLanguage");
    if (dbVariable != null) {
      Constants.api.getAppLanguages(dbVariable.Value, "0").then((value) {
        ApiList<AppLanguage> data = value;
        Constants.db.appLanguageDao.insertAppLanguage(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("AppLanguage", data.dateNow));
      });
    } else {
      Constants.api.getAppLanguages("", "1").then((value) {
        ApiList<AppLanguage> data = value;
        Constants.db.appLanguageDao.insertAppLanguage(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("AppLanguage", data.dateNow));
      });
    }
  }

  void updateUserTicketCategory() async {
    DBVariable? dbVariable = await Constants.db.dbVariableDao
        .findDBVariableById("UserTicketCategory");
    if (dbVariable != null) {
      Constants.api
          .getUserTicketCategories(dbVariable.Value, "0")
          .then((value) {
        ApiList<UserTicketCategory> data = value;
        Constants.db.userTicketCategoryDao
            .insertUserTicketCategories(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("UserTicketCategory", data.dateNow));
      });
    } else {
      Constants.api.getUserTicketCategories("", "1").then((value) {
        ApiList<UserTicketCategory> data = value;
        Constants.db.userTicketCategoryDao
            .insertUserTicketCategories(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("UserTicketCategory", data.dateNow));
      });
    }
  }

  void updatePilotScheduleAll() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("PilotScheduleAll");
    if (dbVariable != null) {
      Constants.api.getPilotScheduleAll(dbVariable.Value, "0").then((value) {
        ApiList<PilotScheduleAll> data = value;
        Constants.db.pilotScheduleAllDao.insertPilotScheduleAll(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("PilotScheduleAll", data.dateNow));
      });
    } else {
      Constants.api.getPilotScheduleAll("", "1").then((value) {
        ApiList<PilotScheduleAll> data = value;
        Constants.db.pilotScheduleAllDao.insertPilotScheduleAll(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("PilotScheduleAll", data.dateNow));
      });
    }
  }

  void updateFAQs() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("FAQs");
    if (dbVariable != null) {
      Constants.api.getFAQs(dbVariable.Value, "0").then((value) {
        ApiList<FAQs> data = value;
        Constants.db.faqDao.insertFAQs(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("FAQs", data.dateNow));
      });
    } else {
      Constants.api.getFAQs("", "1").then((value) {
        ApiList<FAQs> data = value;
        Constants.db.faqDao.insertFAQs(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("FAQs", data.dateNow));
      });
    }
  }

  void updateHelpDeskCategories() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("HelpDeskCategory");
    if (dbVariable != null) {
      Constants.api.getHelpDeskCategories(dbVariable.Value, "0").then((value) {
        ApiList<HelpDeskCategory> data = value;
        Constants.db.helpDeskCategoryDao.insertHelpDeskCategory(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("HelpDeskCategory", data.dateNow));
      });
    } else {
      Constants.api.getHelpDeskCategories("", "1").then((value) {
        ApiList<HelpDeskCategory> data = value;
        Constants.db.helpDeskCategoryDao.insertHelpDeskCategory(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("HelpDeskCategory", data.dateNow));
      });
    }
  }

  void updateHelpDeskLinhVuc() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("HelpDeskLinhVuc");
    if (dbVariable != null) {
      Constants.api.getHelpDeskLinhVucs(dbVariable.Value, "0").then((value) {
        ApiList<HelpDeskLinhVuc> data = value;
        Constants.db.helpDeskLinhVucDao.insertHelpDeskLinhVucs(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("HelpDeskLinhVuc", data.dateNow));
      });
    } else {
      Constants.api.getHelpDeskLinhVucs("", "1").then((value) {
        ApiList<HelpDeskLinhVuc> data = value;
        Constants.db.helpDeskLinhVucDao.insertHelpDeskLinhVucs(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("HelpDeskLinhVuc", data.dateNow));
      });
    }
  }

  void updateDepartments() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("Department");
    if (dbVariable != null) {
      Constants.api.getDepartments(dbVariable.Value, "0").then((value) {
        ApiList<Department> data = value;
        Constants.db.departmentDao.insertDepartment(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("Department", data.dateNow));
      });
    } else {
      Constants.api.getDepartments("", "1").then((value) {
        ApiList<Department> data = value;
        Constants.db.departmentDao.insertDepartment(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("Department", data.dateNow));
      });
    }
  }

  void updatePilotSchedulePdf() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("PilotSchedulePdf");
    if (dbVariable != null) {
      Constants.api.getPilotSchedulePdf(dbVariable.Value, "0").then((value) {
        ApiList<PilotSchedulePdf> data = value;
        Constants.db.pilotSchedulePdfDao.insertPilotSchedulePdf(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("PilotSchedulePdf", data.dateNow));
      });
    } else {
      Constants.api.getPilotSchedulePdf("", "1").then((value) {
        ApiList<PilotSchedulePdf> data = value;
        Constants.db.pilotSchedulePdfDao.insertPilotSchedulePdf(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("PilotSchedulePdf", data.dateNow));
      });
    }
  }

  void updateAnnouncementCategory() async {
    DBVariable? dbVariable = await Constants.db.dbVariableDao
        .findDBVariableById("AnnouncementCategory");
    if (dbVariable != null) {
      Constants.api
          .getAnnouncementCategory(dbVariable.Value, "0")
          .then((value) {
        ApiList<AnnouncementCategory> data = value;
        Constants.db.announcementCategoryDao
            .insertAnnouncementCategories(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("AnnouncementCategory", data.dateNow));
      });
    } else {
      Constants.api.getAnnouncementCategory("", "1").then((value) {
        ApiList<AnnouncementCategory> data = value;
        Constants.db.announcementCategoryDao
            .insertAnnouncementCategories(data.data);
        Constants.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("AnnouncementCategory", data.dateNow));
      });
    }
  }

  void updateNation() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("Nation");
    if (dbVariable != null) {
      Constants.api.getNation(dbVariable.Value, "0").then((value) {
        ApiList<Nation> data = value;
        Constants.db.nationDao.insertNations(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Nation", data.dateNow));
      });
    } else {
      Constants.api.getNation("", "1").then((value) {
        ApiList<Nation> data = value;
        Constants.db.nationDao.insertNations(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Nation", data.dateNow));
      });
    }
  }

  void updateProvince() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("Province");
    if (dbVariable != null) {
      Constants.api.getProvince(dbVariable.Value, "0").then((value) {
        ApiList<Province> data = value;
        Constants.db.provinceDao.insertProvince(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Province", data.dateNow));
      });
    } else {
      Constants.api.getProvince("", "1").then((value) {
        ApiList<Province> data = value;
        Constants.db.provinceDao.insertProvince(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Province", data.dateNow));
      });
    }
  }

  void updateDistrict() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("District");
    if (dbVariable != null) {
      Constants.api.getDistrict(dbVariable.Value, "0").then((value) {
        ApiList<District> data = value;
        Constants.db.districtDao.insertDistrict(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("District", data.dateNow));
      });
    } else {
      Constants.api.getDistrict("", "1").then((value) {
        ApiList<District> data = value;
        Constants.db.districtDao.insertDistrict(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("District", data.dateNow));
      });
    }
  }

  void updateWard() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("Ward");
    if (dbVariable != null) {
      Constants.api.getWard(dbVariable.Value, "0").then((value) {
        ApiList<Ward> data = value;
        Constants.db.wardDao.insertWard(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Ward", data.dateNow));
      });
    } else {
      Constants.api.getWard("", "1").then((value) {
        ApiList<Ward> data = value;
        Constants.db.wardDao.insertWard(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Ward", data.dateNow));
      });
    }
  }

  void updateMenuApp() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("MenuApp");
    if (dbVariable != null) {
      Constants.api.getMenuApp(dbVariable.Value, "0").then((value) {
        ApiList<MenuApp> data = value;
        Constants.db.menuAppDao.insertMenuApp(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("MenuApp", data.dateNow));
      });
    } else {
      Constants.api.getMenuApp("", "1").then((value) {
        ApiList<MenuApp> data = value;
        Constants.db.menuAppDao.insertMenuApp(data.data);
        Constants.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("MenuApp", data.dateNow));
      });
    }
  }




  Future<void> updateAllDynamicData()async {
    await updateMenuHome();
    await updateBanner();
    updateNotify();
    updateLicence();
    updateHelpdesk();
    updateStudent();
    updateSurveyTable();
    updateSurvey();


  }

  Future<void> updateMenuHome() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("MenuHome");
    if (dbVariable != null) {
      await Constants.db.menuHomeDao.deleteAll();
    }
    Constants.api
        .getMenuHome({'UserId': "'${Constants.currentUser.id}'"}.toString())
        .then((value) {
      ApiList<MenuHome> data = value;
      Constants.db.menuHomeDao.insertMenuHome(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("MenuHome", data.dateNow));
    });
  }

  Future<void> updateBanner() async {
    ApiList<BeanBanner> data = await Constants.api
        .getBanner(Constants.sharedPreferences.get('set-cookie').toString());
    await Constants.db.bannerDao.insertBanners(data.data);
    Constants.db.dbVariableDao
        .insertDBVariable(DBVariable.haveParams("Banner", data.dateNow));
  }
  Future<void> updateNotify() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("Notify");
    if (dbVariable != null) {
      ApiList<Notify> data = await Constants.api.getNotify(
          Constants.sharedPreferences.get('set-cookie').toString(),
          dbVariable.Value,
          "0");
      await Constants.db.notifyDao.insertNotifies(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("Notify", data.dateNow));
    } else {
      ApiList<Notify> data = await Constants.api.getNotify(
          Constants.sharedPreferences.get('set-cookie').toString(), "", "1");
      await Constants.db.notifyDao.insertNotifies(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("Notify", data.dateNow));
    }
  }

  Future<void> updateStudent() async {
    DBVariable? dbVariable =
    await Constants.db.dbVariableDao.findDBVariableById("Student");
    if (dbVariable != null) {
      ApiList<Student> data = await Constants.api.getStudent(
          Constants.sharedPreferences.get('set-cookie').toString(),
          dbVariable.Value,
          "0");
      await Constants.db.studentDao.insertStudent(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("Student", data.dateNow));
    } else {
      ApiList<Student> data = await Constants.api.getStudent(
          Constants.sharedPreferences.get('set-cookie').toString(), "", "1");
      await Constants.db.studentDao.insertStudent(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("Student", data.dateNow));
    }
  }

  Future<void> updateSurveyTable() async {
    DBVariable? dbVariable =
    await Constants.db.dbVariableDao.findDBVariableById("SurveyTable");
    if (dbVariable != null) {
      ApiList<SurveyTable> data = await Constants.api.getSurveyTable(
          Constants.sharedPreferences.get('set-cookie').toString(),
          dbVariable.Value,
          "0");
      await Constants.db.surveyTableDao.insertSurveyTable(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("SurveyTable", data.dateNow));
    } else {
      ApiList<SurveyTable> data = await Constants.api.getSurveyTable(
          Constants.sharedPreferences.get('set-cookie').toString(), "", "1");
      await Constants.db.surveyTableDao.insertSurveyTable(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("SurveyTable", data.dateNow));
    }
  }

  Future<void> updateSurvey() async {
    DBVariable? dbVariable =
    await Constants.db.dbVariableDao.findDBVariableById("Survey");
    if (dbVariable != null) {
      ApiList<Survey> data = await Constants.api.getSurvey(
          Constants.sharedPreferences.get('set-cookie').toString(),
          dbVariable.Value,
          "0");
      await Constants.db.surveyDao.insertSurvey(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("Survey", data.dateNow));
    } else {
      ApiList<Survey> data = await Constants.api.getSurvey(
          Constants.sharedPreferences.get('set-cookie').toString(), "", "1");
      await Constants.db.surveyDao.insertSurvey(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("Survey", data.dateNow));
    }
  }

  Future<void> updateHelpdesk() async {
    DBVariable? dbVariable =
        await Constants.db.dbVariableDao.findDBVariableById("Helpdesk");
    if (dbVariable != null) {
      ApiList<Helpdesk> data = await Constants.api.getHelpdesk(
          Constants.sharedPreferences.get('set-cookie').toString(),
          dbVariable.Value,
          "0");
      await Constants.db.helpdeskDao.insertHelpdesk(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("Helpdesk", data.dateNow));
    } else {
      ApiList<Helpdesk> data = await Constants.api.getHelpdesk(
          Constants.sharedPreferences.get('set-cookie').toString(), "", "1");
      await Constants.db.helpdeskDao.insertHelpdesk(data.data);
      Constants.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("Helpdesk", data.dateNow));
    }
  }
  Future<void> updateLicence() async {
    ApiList<License> data = await Constants.api
        .getUserLicense(Constants.sharedPreferences.get('set-cookie').toString(),Constants.currentUser.id);
   if(data!=null && data.data!=null)
     {
       await Constants.db.licenceDao.insertLicenses(data.data);
       Constants.db.dbVariableDao
           .insertDBVariable(DBVariable.haveParams("Licence", data.dateNow));
     }
  }

}
