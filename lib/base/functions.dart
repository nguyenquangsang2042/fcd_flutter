import 'package:flutter/material.dart';

class Functions {
  static final Functions _singleton = Functions._internal();

  static Functions get instance {
    return _singleton;
  }

  Functions._internal();

  void hideKeyboard() {
    FocusManager.instance.primaryFocus?.unfocus();
  }
}
