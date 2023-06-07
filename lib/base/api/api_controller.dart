import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:fcd_flutter/base/model/app/help_desk_category.dart';
import 'package:fcd_flutter/base/model/app/helpdesk_linhvuc.dart';
import 'package:fcd_flutter/base/model/app/user.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_category.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';

import '../constans.dart';
import '../model/api_list.dart';
import '../model/app/app_language.dart';
import '../model/app/db_variable.dart';
import '../model/app/pilot_schedule_all.dart';
import '../model/app/settings.dart';

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
  }

  void updateSetting()async {
    DBVariable? dbVariable= await Constanst.db.dbVariableDao.findDBVariableById("Setting");
    if(dbVariable!=null) {
      Constanst.api.getSettings(dbVariable.Value, "0").then((value) {
        ApiList<Setting> data = value;
        Constanst.db.settingDao.insertSettings(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Setting", data.dateNow));
      });
    }
    else
      {
        Constanst.api.getSettings("", "1").then((value) {
          ApiList<Setting> data = value;
          Constanst.db.settingDao.insertSettings(data.data);
          Constanst.db.dbVariableDao
              .insertDBVariable(DBVariable.haveParams("Setting", data.dateNow));
        });
      }
  }
  void updateUser()async {
    DBVariable? dbVariable= await Constanst.db.dbVariableDao.findDBVariableById("User");
    if(dbVariable!=null) {
      Constanst.api.getUsers(dbVariable.Value, "0").then((value) {
        ApiList<User> data = value;
        Constanst.db.userDao.insertUsers(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("User", data.dateNow));
      });
    }
    else
      {
        Constanst.api.getUsers("", "1").then((value) {
          ApiList<User> data = value;
          Constanst.db.userDao.insertUsers(data.data);
          Constanst.db.dbVariableDao
              .insertDBVariable(DBVariable.haveParams("User", data.dateNow));
        });
      }
  }
  void updateAirport()async {
    DBVariable? dbVariable= await Constanst.db.dbVariableDao.findDBVariableById("Airport");
    if(dbVariable!=null) {
      Constanst.api.getAirports(dbVariable.Value, "0").then((value) {
        ApiList<Airport> data = value;
        Constanst.db.airportDao.insertAirport(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("Airport", data.dateNow));
      });
    }
    else
      {
        Constanst.api.getAirports("", "1").then((value) {
          ApiList<Airport> data = value;
          Constanst.db.airportDao.insertAirport(data.data);
          Constanst.db.dbVariableDao
              .insertDBVariable(DBVariable.haveParams("Airport", data.dateNow));
        });
      }
  }

  void updateUserTicketStatuses()async {
    DBVariable? dbVariable= await Constanst.db.dbVariableDao.findDBVariableById("UserTicketStatus");
    if(dbVariable!=null) {
      Constanst.api.getUserTicketStatuses(dbVariable.Value, "0").then((value) {
        ApiList<UserTicketStatus> data = value;
        Constanst.db.userTicketStatusDao.insertUserTicketStatuses(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("UserTicketStatus", data.dateNow));
      });
    }
    else
    {
      Constanst.api.getUserTicketStatuses("", "1").then((value) {
        ApiList<UserTicketStatus> data = value;
        Constanst.db.userTicketStatusDao.insertUserTicketStatuses(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("UserTicketStatus", data.dateNow));
      });
    }
  }
  void updateAppLanguage()async {
    DBVariable? dbVariable= await Constanst.db.dbVariableDao.findDBVariableById("AppLanguage");
    if(dbVariable!=null) {
      Constanst.api.getAppLanguages(dbVariable.Value, "0").then((value) {
        ApiList<AppLanguage> data = value;
        Constanst.db.appLanguageDao.insertAppLanguage(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("AppLanguage", data.dateNow));
      });
    }
    else
    {
      Constanst.api.getAppLanguages("", "1").then((value) {
        ApiList<AppLanguage> data = value;
        Constanst.db.appLanguageDao.insertAppLanguage(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("AppLanguage", data.dateNow));
      });
    }
  }
  void updateUserTicketCategory()async {
    DBVariable? dbVariable= await Constanst.db.dbVariableDao.findDBVariableById("UserTicketCategory");
    if(dbVariable!=null) {
      Constanst.api.getUserTicketCategories(dbVariable.Value, "0").then((value) {
        ApiList<UserTicketCategory> data = value;
        Constanst.db.userTicketCategoryDao.insertUserTicketCategories(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("UserTicketCategory", data.dateNow));
      });
    }
    else
    {
      Constanst.api.getUserTicketCategories("", "1").then((value) {
        ApiList<UserTicketCategory> data = value;
        Constanst.db.userTicketCategoryDao.insertUserTicketCategories(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("UserTicketCategory", data.dateNow));
      });
    }
  }
  void updatePilotScheduleAll()async {
    DBVariable? dbVariable= await Constanst.db.dbVariableDao.findDBVariableById("PilotScheduleAll");
    if(dbVariable!=null) {
      Constanst.api.getPilotScheduleAll(dbVariable.Value, "0").then((value) {
        ApiList<PilotScheduleAll> data = value;
        Constanst.db.pilotScheduleAllDao.insertPilotScheduleAll(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("PilotScheduleAll", data.dateNow));
      });
    }
    else
    {
      Constanst.api.getPilotScheduleAll("", "1").then((value) {
        ApiList<PilotScheduleAll> data = value;
        Constanst.db.pilotScheduleAllDao.insertPilotScheduleAll(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("PilotScheduleAll", data.dateNow));
      });
    }
  }

  void updateFAQs()async {
    DBVariable? dbVariable= await Constanst.db.dbVariableDao.findDBVariableById("FAQs");
    if(dbVariable!=null) {
      Constanst.api.getFAQs(dbVariable.Value, "0").then((value) {
        ApiList<FAQs> data = value;
        Constanst.db.faqDao.insertFAQs(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("FAQs", data.dateNow));
      });
    }
    else
    {
      Constanst.api.getFAQs("", "1").then((value) {
        ApiList<FAQs> data = value;
        Constanst.db.faqDao.insertFAQs(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("FAQs", data.dateNow));
      });
    }
  }
void updateHelpDeskCategories()async {
    DBVariable? dbVariable= await Constanst.db.dbVariableDao.findDBVariableById("HelpDeskCategory");
    if(dbVariable!=null) {
      Constanst.api.getHelpDeskCategories(dbVariable.Value, "0").then((value) {
        ApiList<HelpDeskCategory> data = value;
        Constanst.db.helpDeskCategoryDao.insertHelpDeskCategory(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("HelpDeskCategory", data.dateNow));
      });
    }
    else
    {
      Constanst.api.getHelpDeskCategories("", "1").then((value) {
        ApiList<HelpDeskCategory> data = value;
        Constanst.db.helpDeskCategoryDao.insertHelpDeskCategory(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("HelpDeskCategory", data.dateNow));
      });
    }
  }

  void updateHelpDeskLinhVuc()async {
    DBVariable? dbVariable= await Constanst.db.dbVariableDao.findDBVariableById("HelpDeskLinhVuc");
    if(dbVariable!=null) {
      Constanst.api.getHelpDeskLinhVucs(dbVariable.Value, "0").then((value) {
        ApiList<HelpDeskLinhVuc> data = value;
        Constanst.db.helpDeskLinhVucDao.insertHelpDeskLinhVucs(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("HelpDeskLinhVuc", data.dateNow));
      });
    }
    else
    {
      Constanst.api.getHelpDeskLinhVucs("", "1").then((value) {
        ApiList<HelpDeskLinhVuc> data = value;
        Constanst.db.helpDeskLinhVucDao.insertHelpDeskLinhVucs(data.data);
        Constanst.db.dbVariableDao
            .insertDBVariable(DBVariable.haveParams("HelpDeskLinhVuc", data.dateNow));
      });
    }
  }

}
