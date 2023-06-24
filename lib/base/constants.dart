import 'package:fcd_flutter/base/api/api_client.dart';
import 'package:fcd_flutter/base/api/api_controller.dart';
import 'package:fcd_flutter/base/database/app_database.dart';
import 'package:fcd_flutter/base/model/app/user.dart';
import 'package:fcd_flutter/base/model/device_info.dart';
import 'package:shared_preferences/shared_preferences.dart';

class Constants {
  static late AppDatabase db;
  static late ApiClient api;
  static late ApiController apiController;
  static late DeviceInfo deviceInfo;
  static late User currentUser;
  static late SharedPreferences sharedPreferences;
  static late String loginName;
  static late String loginPass;
   //static String baseURL='https://pilot.vuthao.com';
   //static String baseDomain='pilot.vuthao.com';
  static String baseURL='https://pilotuat.vuthao.com';
  static String baseDomain='pilotuat.vuthao.com';
  static String formatDateddmmyyy='dd/MM/yyyy';
}
