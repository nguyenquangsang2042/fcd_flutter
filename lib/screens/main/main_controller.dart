import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';

class MainController {
  static final MainController _singleton = MainController._internal();

  static MainController get instance {
    return _singleton;
  }

  MainController._internal();

  final ScrollController _scrollController = ScrollController();
  late ValueNotifier<int> _heightBanner=ValueNotifier(0);
  late ValueNotifier<int> _wightBanner=ValueNotifier(0);

  ValueNotifier<int> get heightBanner => _heightBanner;
  late int wightScreen;
  ScrollController get scrollController {
    _scrollController.addListener(() {
      if (_scrollController.position.userScrollDirection ==
          ScrollDirection.reverse) {
        if(_heightBanner!=null && _heightBanner.value>70)
        {
          _heightBanner.value =_heightBanner.value-5;
        }
        if(_wightBanner!=null && _wightBanner.value>150)
        {
          _wightBanner.value =_wightBanner.value-5;
        }
      } else {
        if(_heightBanner!=null && _heightBanner.value<200)
        {
          _heightBanner.value =_heightBanner.value+5;
        }
        if(_wightBanner!=null && _wightBanner.value<wightScreen)
        {
          _wightBanner.value =_wightBanner.value+5;
        }

      }
    });
    return _scrollController;
  }

  ValueNotifier<int> get wightBanner => _wightBanner;
}
