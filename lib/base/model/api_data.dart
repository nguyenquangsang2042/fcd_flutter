import 'package:fcd_flutter/base/model/status.dart';

class ApiData extends Status {
  late String data;

  ApiData();

  ApiData.fromJson(Map<String, dynamic> json) {
    data = json['data'];
    status = json['status'];
    mess = (json['mess'] != null ? Mess.fromJson(json['mess']) : null)!;
    dateNow = json['dateNow'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['data'] = this.data;
    return data;
  }


}
