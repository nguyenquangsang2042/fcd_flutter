import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/user.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';
import 'package:fcd_flutter/base/model/status.dart';

class ApiList<T> extends Status {
  late List<T> data;

  ApiList();

  ApiList.fromJson(Map<String, dynamic> json) {
    if (json['data'] != null) {
      data = List<T>.empty(growable: true);
      json['data'].forEach((v) {
        data.add(redirectFormat(v) as T);
      });
    }
    status = json['status'];
    mess = (json['mess'] != null ? Mess.fromJson(json['mess']) : null)!;
    dateNow = json['dateNow'];
  }

  Object? redirectFormat(Map<String, dynamic> data) {
    if (T == Setting) {
      return Setting.fromJson(data);
    } else if (T == User) {
      return User.fromJson(data);
    } else if (T == Airport) {
      return Airport.fromJson(data);
    } else if (T == UserTicketStatus) {
      return UserTicketStatus.fromJson(data);
    } else {
      return null;
    }
  }
}
