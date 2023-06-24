import 'package:dio/dio.dart';
import 'package:fcd_flutter/base/constants.dart';

class DioController {
  static final DioController _singleton = DioController._internal();

  factory DioController() {
    return _singleton;
  }

  late Dio _dio;

  Dio get dio {
    return _dio;
  }

  DioController._internal() {
    _dio = Dio();
  }
//    Constanst.sharedPreferences.setString("set-cookie", _result.headers.value("set-cookie").toString()) => them dong nay tron login va relogin
}
