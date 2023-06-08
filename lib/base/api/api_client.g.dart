// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'api_client.dart';

// **************************************************************************
// RetrofitGenerator
// **************************************************************************

// ignore_for_file: unnecessary_brace_in_string_interps,no_leading_underscores_for_local_identifiers

class _ApiClient implements ApiClient {
  _ApiClient(
    this._dio, {
    this.baseUrl,
  }) {
    baseUrl ??= 'https://pilot.vuthao.com';
  }

  final Dio _dio;

  String? baseUrl;

  @override
  Future<Status> getOtp(data) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{};
    final _headers = <String, dynamic>{};
    final _data = {'data': data};
    final _result =
        await _dio.fetch<Map<String, dynamic>>(_setStreamType<Status>(Options(
      method: 'POST',
      headers: _headers,
      extra: _extra,
      contentType: 'application/x-www-form-urlencoded',
    )
            .compose(
              _dio.options,
              '/API/User.ashx?func=getOtp',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = Status.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<Setting>> getSettings(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio
        .fetch<Map<String, dynamic>>(_setStreamType<ApiList<Setting>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanSettings',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<Setting>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<User>> getUsers(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio
        .fetch<Map<String, dynamic>>(_setStreamType<ApiList<User>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanUser',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<User>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<Airport>> getAirports(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio
        .fetch<Map<String, dynamic>>(_setStreamType<ApiList<Airport>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanAirport',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<Airport>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<UserTicketStatus>> getUserTicketStatuses(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio.fetch<Map<String, dynamic>>(
        _setStreamType<ApiList<UserTicketStatus>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanUserTicketStatus',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<UserTicketStatus>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<AppLanguage>> getAppLanguages(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio.fetch<Map<String, dynamic>>(
        _setStreamType<ApiList<AppLanguage>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanAppLanguage',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<AppLanguage>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<UserTicketCategory>> getUserTicketCategories(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio.fetch<Map<String, dynamic>>(
        _setStreamType<ApiList<UserTicketCategory>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanUserTicketCategory',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<UserTicketCategory>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<FAQs>> getFAQs(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio
        .fetch<Map<String, dynamic>>(_setStreamType<ApiList<FAQs>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanFAQs',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<FAQs>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<HelpDeskCategory>> getHelpDeskCategories(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio.fetch<Map<String, dynamic>>(
        _setStreamType<ApiList<HelpDeskCategory>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanHelpDeskCategory',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<HelpDeskCategory>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<PilotScheduleAll>> getPilotScheduleAll(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio.fetch<Map<String, dynamic>>(
        _setStreamType<ApiList<PilotScheduleAll>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanPilotScheduleAll',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<PilotScheduleAll>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<HelpDeskLinhVuc>> getHelpDeskLinhVucs(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio.fetch<Map<String, dynamic>>(
        _setStreamType<ApiList<HelpDeskLinhVuc>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanHelpDeskLinhVuc',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<HelpDeskLinhVuc>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<Department>> getDepartments(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio.fetch<Map<String, dynamic>>(
        _setStreamType<ApiList<Department>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanDepartment',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<Department>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<PilotSchedulePdf>> getPilotSchedulePdf(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio.fetch<Map<String, dynamic>>(
        _setStreamType<ApiList<PilotSchedulePdf>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanPilotSchedulePdf',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<PilotSchedulePdf>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<AnnouncementCategory>> getAnnouncementCategory(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio.fetch<Map<String, dynamic>>(
        _setStreamType<ApiList<AnnouncementCategory>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanAnnouncementCategory',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<AnnouncementCategory>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<Nation>> getNation(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio
        .fetch<Map<String, dynamic>>(_setStreamType<ApiList<Nation>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanNation',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<Nation>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<Province>> getProvince(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio
        .fetch<Map<String, dynamic>>(_setStreamType<ApiList<Province>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanProvince',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<Province>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<District>> getDistrict(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio
        .fetch<Map<String, dynamic>>(_setStreamType<ApiList<District>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanDistrict',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<District>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<ApiList<Ward>> getWard(
    modified,
    isFirst,
  ) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{
      r'Modified': modified,
      r'isFirst': isFirst,
    };
    final _headers = <String, dynamic>{};
    final Map<String, dynamic>? _data = null;
    final _result = await _dio
        .fetch<Map<String, dynamic>>(_setStreamType<ApiList<Ward>>(Options(
      method: 'GET',
      headers: _headers,
      extra: _extra,
    )
            .compose(
              _dio.options,
              '/API/ApiPublic.ashx?func=get&bname=BeanWard',
              queryParameters: queryParameters,
              data: _data,
            )
            .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ApiList<Ward>.fromJson(_result.data!);
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
