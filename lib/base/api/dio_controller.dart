import 'package:alice/alice.dart';
import 'package:dio/dio.dart';

class DioController {
  static final DioController _singleton = DioController._internal();

  factory DioController() {
    return _singleton;
  }

  late Dio _dio;

  Dio get dio => _dio;

  DioController._internal() {
    _dio = Dio();
  }
}
