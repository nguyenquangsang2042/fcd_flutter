import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/user.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';

import '../constans.dart';
import '../model/api_list.dart';
import '../model/app/db_variable.dart';
import '../model/app/settings.dart';

class ApiController {
  void updateMasterData() async {
    updateSetting();
    updateUser();
    updateAirport();
    updateUserTicketStatuses();
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

}
