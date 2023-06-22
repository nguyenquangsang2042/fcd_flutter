import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/bean_banner.dart';
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
import 'package:fcd_flutter/base/model/app/user.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_category.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';
import 'package:flutter/material.dart';

import '../constanst.dart';
import '../model/api_list.dart';
import '../model/app/announcement_category.dart';
import '../model/app/app_language.dart';
import '../model/app/db_variable.dart';
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
        await Constanst.db.dbVariableDao.findDBVariableById("Setting");
    if (dbVariable != null) {
      Constanst.api.getSettings(dbVariable.Value, "0").then((value) {
        ApiList<Setting> data = value;
        Constanst.db.settingDao.insertSettings(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Setting", data.dateNow));
      });
    } else {
      Constanst.api.getSettings("", "1").then((value) {
        ApiList<Setting> data = value;
        Constanst.db.settingDao.insertSettings(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Setting", data.dateNow));
      });
    }
  }

  void updateUser() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("User");
    if (dbVariable != null) {
      Constanst.api.getUsers(dbVariable.Value, "0").then((value) {
        ApiList<User> data = value;
        Constanst.db.userDao.insertUsers(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("User", data.dateNow));
      });
    } else {
      Constanst.api.getUsers("", "1").then((value) {
        ApiList<User> data = value;
        Constanst.db.userDao.insertUsers(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("User", data.dateNow));
      });
    }
  }

  void updateAirport() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("Airport");
    if (dbVariable != null) {
      Constanst.api.getAirports(dbVariable.Value, "0").then((value) {
        ApiList<Airport> data = value;
        Constanst.db.airportDao.insertAirport(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Airport", data.dateNow));
      });
    } else {
      Constanst.api.getAirports("", "1").then((value) {
        ApiList<Airport> data = value;
        Constanst.db.airportDao.insertAirport(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Airport", data.dateNow));
      });
    }
  }

  void updateUserTicketStatuses() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("UserTicketStatus");
    if (dbVariable != null) {
      Constanst.api.getUserTicketStatuses(dbVariable.Value, "0").then((value) {
        ApiList<UserTicketStatus> data = value;
        Constanst.db.userTicketStatusDao.insertUserTicketStatuses(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("UserTicketStatus", data.dateNow));
      });
    } else {
      Constanst.api.getUserTicketStatuses("", "1").then((value) {
        ApiList<UserTicketStatus> data = value;
        Constanst.db.userTicketStatusDao.insertUserTicketStatuses(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("UserTicketStatus", data.dateNow));
      });
    }
  }

  void updateAppLanguage() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("AppLanguage");
    if (dbVariable != null) {
      Constanst.api.getAppLanguages(dbVariable.Value, "0").then((value) {
        ApiList<AppLanguage> data = value;
        Constanst.db.appLanguageDao.insertAppLanguage(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("AppLanguage", data.dateNow));
      });
    } else {
      Constanst.api.getAppLanguages("", "1").then((value) {
        ApiList<AppLanguage> data = value;
        Constanst.db.appLanguageDao.insertAppLanguage(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("AppLanguage", data.dateNow));
      });
    }
  }

  void updateUserTicketCategory() async {
    DBVariable? dbVariable = await Constanst.db.dbVariableDao
        .findDBVariableById("UserTicketCategory");
    if (dbVariable != null) {
      Constanst.api
          .getUserTicketCategories(dbVariable.Value, "0")
          .then((value) {
        ApiList<UserTicketCategory> data = value;
        Constanst.db.userTicketCategoryDao
            .insertUserTicketCategories(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("UserTicketCategory", data.dateNow));
      });
    } else {
      Constanst.api.getUserTicketCategories("", "1").then((value) {
        ApiList<UserTicketCategory> data = value;
        Constanst.db.userTicketCategoryDao
            .insertUserTicketCategories(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("UserTicketCategory", data.dateNow));
      });
    }
  }

  void updatePilotScheduleAll() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("PilotScheduleAll");
    if (dbVariable != null) {
      Constanst.api.getPilotScheduleAll(dbVariable.Value, "0").then((value) {
        ApiList<PilotScheduleAll> data = value;
        Constanst.db.pilotScheduleAllDao.insertPilotScheduleAll(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("PilotScheduleAll", data.dateNow));
      });
    } else {
      Constanst.api.getPilotScheduleAll("", "1").then((value) {
        ApiList<PilotScheduleAll> data = value;
        Constanst.db.pilotScheduleAllDao.insertPilotScheduleAll(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("PilotScheduleAll", data.dateNow));
      });
    }
  }

  void updateFAQs() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("FAQs");
    if (dbVariable != null) {
      Constanst.api.getFAQs(dbVariable.Value, "0").then((value) {
        ApiList<FAQs> data = value;
        Constanst.db.faqDao.insertFAQs(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("FAQs", data.dateNow));
      });
    } else {
      Constanst.api.getFAQs("", "1").then((value) {
        ApiList<FAQs> data = value;
        Constanst.db.faqDao.insertFAQs(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("FAQs", data.dateNow));
      });
    }
  }

  void updateHelpDeskCategories() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("HelpDeskCategory");
    if (dbVariable != null) {
      Constanst.api.getHelpDeskCategories(dbVariable.Value, "0").then((value) {
        ApiList<HelpDeskCategory> data = value;
        Constanst.db.helpDeskCategoryDao.insertHelpDeskCategory(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("HelpDeskCategory", data.dateNow));
      });
    } else {
      Constanst.api.getHelpDeskCategories("", "1").then((value) {
        ApiList<HelpDeskCategory> data = value;
        Constanst.db.helpDeskCategoryDao.insertHelpDeskCategory(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("HelpDeskCategory", data.dateNow));
      });
    }
  }

  void updateHelpDeskLinhVuc() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("HelpDeskLinhVuc");
    if (dbVariable != null) {
      Constanst.api.getHelpDeskLinhVucs(dbVariable.Value, "0").then((value) {
        ApiList<HelpDeskLinhVuc> data = value;
        Constanst.db.helpDeskLinhVucDao.insertHelpDeskLinhVucs(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("HelpDeskLinhVuc", data.dateNow));
      });
    } else {
      Constanst.api.getHelpDeskLinhVucs("", "1").then((value) {
        ApiList<HelpDeskLinhVuc> data = value;
        Constanst.db.helpDeskLinhVucDao.insertHelpDeskLinhVucs(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("HelpDeskLinhVuc", data.dateNow));
      });
    }
  }

  void updateDepartments() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("Department");
    if (dbVariable != null) {
      Constanst.api.getDepartments(dbVariable.Value, "0").then((value) {
        ApiList<Department> data = value;
        Constanst.db.departmentDao.insertDepartment(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("Department", data.dateNow));
      });
    } else {
      Constanst.api.getDepartments("", "1").then((value) {
        ApiList<Department> data = value;
        Constanst.db.departmentDao.insertDepartment(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("Department", data.dateNow));
      });
    }
  }

  void updatePilotSchedulePdf() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("PilotSchedulePdf");
    if (dbVariable != null) {
      Constanst.api.getPilotSchedulePdf(dbVariable.Value, "0").then((value) {
        ApiList<PilotSchedulePdf> data = value;
        Constanst.db.pilotSchedulePdfDao.insertPilotSchedulePdf(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("PilotSchedulePdf", data.dateNow));
      });
    } else {
      Constanst.api.getPilotSchedulePdf("", "1").then((value) {
        ApiList<PilotSchedulePdf> data = value;
        Constanst.db.pilotSchedulePdfDao.insertPilotSchedulePdf(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("PilotSchedulePdf", data.dateNow));
      });
    }
  }

  void updateAnnouncementCategory() async {
    DBVariable? dbVariable = await Constanst.db.dbVariableDao
        .findDBVariableById("AnnouncementCategory");
    if (dbVariable != null) {
      Constanst.api
          .getAnnouncementCategory(dbVariable.Value, "0")
          .then((value) {
        ApiList<AnnouncementCategory> data = value;
        Constanst.db.announcementCategoryDao
            .insertAnnouncementCategories(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("AnnouncementCategory", data.dateNow));
      });
    } else {
      Constanst.api.getAnnouncementCategory("", "1").then((value) {
        ApiList<AnnouncementCategory> data = value;
        Constanst.db.announcementCategoryDao
            .insertAnnouncementCategories(data.data);
        Constanst.db.dbVariableDao.insertDBVariable(
            DBVariable.haveParams("AnnouncementCategory", data.dateNow));
      });
    }
  }

  void updateNation() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("Nation");
    if (dbVariable != null) {
      Constanst.api.getNation(dbVariable.Value, "0").then((value) {
        ApiList<Nation> data = value;
        Constanst.db.nationDao.insertNations(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Nation", data.dateNow));
      });
    } else {
      Constanst.api.getNation("", "1").then((value) {
        ApiList<Nation> data = value;
        Constanst.db.nationDao.insertNations(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Nation", data.dateNow));
      });
    }
  }

  void updateProvince() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("Province");
    if (dbVariable != null) {
      Constanst.api.getProvince(dbVariable.Value, "0").then((value) {
        ApiList<Province> data = value;
        Constanst.db.provinceDao.insertProvince(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Province", data.dateNow));
      });
    } else {
      Constanst.api.getProvince("", "1").then((value) {
        ApiList<Province> data = value;
        Constanst.db.provinceDao.insertProvince(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Province", data.dateNow));
      });
    }
  }

  void updateDistrict() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("District");
    if (dbVariable != null) {
      Constanst.api.getDistrict(dbVariable.Value, "0").then((value) {
        ApiList<District> data = value;
        Constanst.db.districtDao.insertDistrict(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("District", data.dateNow));
      });
    } else {
      Constanst.api.getDistrict("", "1").then((value) {
        ApiList<District> data = value;
        Constanst.db.districtDao.insertDistrict(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("District", data.dateNow));
      });
    }
  }

  void updateWard() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("Ward");
    if (dbVariable != null) {
      Constanst.api.getWard(dbVariable.Value, "0").then((value) {
        ApiList<Ward> data = value;
        Constanst.db.wardDao.insertWard(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Ward", data.dateNow));
      });
    } else {
      Constanst.api.getWard("", "1").then((value) {
        ApiList<Ward> data = value;
        Constanst.db.wardDao.insertWard(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Ward", data.dateNow));
      });
    }
  }

  void updateMenuApp() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("MenuApp");
    if (dbVariable != null) {
      Constanst.api.getMenuApp(dbVariable.Value, "0").then((value) {
        ApiList<MenuApp> data = value;
        Constanst.db.menuAppDao.insertMenuApp(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("MenuApp", data.dateNow));
      });
    } else {
      Constanst.api.getMenuApp("", "1").then((value) {
        ApiList<MenuApp> data = value;
        Constanst.db.menuAppDao.insertMenuApp(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("MenuApp", data.dateNow));
      });
    }
  }

  Future<void> updateMenuHome() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("MenuHome");
    if (dbVariable != null) {
      await Constanst.db.menuHomeDao.deleteAll();
    }
    Constanst.api
        .getMenuHome({'UserId': "'${Constanst.currentUser.id}'"}.toString())
        .then((value) {
      ApiList<MenuHome> data = value;
      Constanst.db.menuHomeDao.insertMenuHome(data.data);
      Constanst.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("MenuHome", data.dateNow));
    });
  }

  Future<void> updateBanner() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("Banner");
    if (dbVariable != null) {
      await Constanst.db.bannerDao.deleteAll();
    }
    ApiList<BeanBanner> data = await Constanst.api
        .getBanner(Constanst.sharedPreferences.get('set-cookie').toString());
    await Constanst.db.bannerDao.insertBanners(data.data);
    Constanst.db.dbVariableDao
        .insertDBVariable(DBVariable.haveParams("Banner", data.dateNow));
  }
  Future<void> updateNotify() async {
    DBVariable? dbVariable =
        await Constanst.db.dbVariableDao.findDBVariableById("Notify");
    if (dbVariable != null) {
      ApiList<Notify> data = await Constanst.api.getNotify(
          Constanst.sharedPreferences.get('set-cookie').toString(),
          dbVariable.Value,
          "0");
      await Constanst.db.notifyDao.insertNotifies(data.data);
      Constanst.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("Notify", data.dateNow));
    } else {
      ApiList<Notify> data = await Constanst.api.getNotify(
          Constanst.sharedPreferences.get('set-cookie').toString(), "", "1");
      await Constanst.db.notifyDao.insertNotifies(data.data);
      Constanst.db.dbVariableDao
          .insertDBVariable(DBVariable.haveParams("Notify", data.dateNow));
    }
  }

  Future<void> updateLicence() async {
    DBVariable? dbVariable =
    await Constanst.db.dbVariableDao.findDBVariableById("Licence");
    if (dbVariable != null) {
      await Constanst.db.licenceDao.deleteAll();
    }
    ApiList<License> data = await Constanst.api
        .getUserLicense(Constanst.sharedPreferences.get('set-cookie').toString(),Constanst.currentUser.id);
    await Constanst.db.licenceDao.insertLicenses(data.data);
    Constanst.db.dbVariableDao
        .insertDBVariable(DBVariable.haveParams("Licence", data.dateNow));
  }

}
