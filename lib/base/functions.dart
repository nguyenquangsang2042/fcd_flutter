import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

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
}
