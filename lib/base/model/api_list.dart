import 'package:fcd_flutter/base/model/status.dart';

class ApiList<T> extends Status {
  late List<T> data;

  ApiList();

  ApiList.fromJson(Map<String, dynamic> json) {
    if (json['data'] != null) {
      data = List<T>.empty(growable: true);
      json['data'].forEach((v) {
        data.add(v as T);
      });
    }
  }
}
