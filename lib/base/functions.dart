import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:intl/intl.dart';
class Functions {
  static final Functions _singleton = Functions._internal();

  static Functions get instance {
    return _singleton;
  }

  Functions._internal();

  void hideKeyboard() {
    FocusManager.instance.primaryFocus?.unfocus();
  }
  Future<void> launchCustomUrl(_type, _url) async {
    final Uri smsLaunchUri = Uri(
      scheme: _type,
      path: _url,
    );
    if (!await launchUrl(smsLaunchUri)) {
      throw Exception('Could not launch $_url');
    }
  }
  DateTime stringToDate(String dateString, String? format) {
    if(format==null) {
      return DateTime.parse(dateString);
    }
    return DateFormat(format).parseStrict(dateString);
  }
  String formatDateToString(DateTime dateTime) {
    return DateFormat('yyyy-MM-dd HH:mm:ss').format(dateTime);
  }
  String formatDateString(String dateString,String formatReturn) {
    DateTime dateTime = DateTime.parse(dateString);
    String formattedDate = DateFormat(formatReturn).format(dateTime);
    return formattedDate;
  }

}
