import 'package:fcd_flutter/base/model/status.dart';

class ApiObject<T> extends Status {
  late T data;

  ApiObject();

  ApiObject.fromJson(Map<String, dynamic> json) {
    data = json['data'] as T;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['data'] = this.data;
    return data;
  }
}
