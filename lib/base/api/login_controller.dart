import 'package:dio/dio.dart';
import 'package:fcd_flutter/base/api/dio_controller.dart';

import '../constants.dart';
import '../model/api_object.dart';
import '../model/app/user.dart';

class LoginController
{
  static final LoginController _singleton = LoginController._internal();

  static LoginController get instance {
    return _singleton;
  }

  LoginController._internal();
  Future<ApiObject<User>> getCurrentLoginUser(
      deviceInfo,
      data,
      loginType,
      userTypeLogin,
      ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{};
    final _headers = <String, dynamic>{};
    final _data = {
      'deviceInfo': deviceInfo,
      'data': data,
      'loginType': loginType,
      'userTypeLogin': userTypeLogin,
    };
    final _result = await DioController().dio
        .fetch<Map<String, dynamic>>(_setStreamType<ApiObject<User>>(Options(
      method: 'POST',
      headers: _headers,
      extra: _extra,
      contentType: 'application/x-www-form-urlencoded',
    )
        .compose(
      DioController().dio.options,
      '/API/User.ashx?func=otpConfirm',
      queryParameters: queryParameters,
      data: _data,
    )
        .copyWith(baseUrl: Constants.baseURL)));
    final value = ApiObject<User>.fromJson(_result.data!);
    Constants.sharedPreferences.setString("set-cookie", _result.headers.value("set-cookie").toString());
    return value;
  }
  Future<ApiObject<User>> reLogin(
      reLogin,
      data,
      ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{r'reLogin': reLogin};
    final _headers = <String, dynamic>{};
    final _data = {'data': data};
    final _result = await  DioController().dio
        .fetch<Map<String, dynamic>>(_setStreamType<ApiObject<User>>(Options(
      method: 'POST',
      headers: _headers,
      extra: _extra,
      contentType: 'application/x-www-form-urlencoded',
    )
        .compose(
      DioController().dio.options,
      '/API/User.ashx?func=login',
      queryParameters: queryParameters,
      data: _data,
    )
        .copyWith(baseUrl: Constants.baseURL)));
    final value = ApiObject<User>.fromJson(_result.data!);
    Constants.sharedPreferences.setString("set-cookie", _result.headers.value("set-cookie").toString());
    return value;
  }

  RequestOptions _setStreamType<T>(RequestOptions requestOptions) {
    if (T != dynamic &&
        !(requestOptions.responseType == ResponseType.bytes ||
            requestOptions.responseType == ResponseType.stream)) {
      if (T == String) {
        requestOptions.responseType = ResponseType.plain;
      } else {
        requestOptions.responseType = ResponseType.json;
      }
    }
    return requestOptions;
  }
}