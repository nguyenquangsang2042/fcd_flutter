import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class NavigationGridScreen extends StatelessWidget {
  NavigationGridScreen({super.key,required this.childView});
  Widget childView;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: childView,
    );
  }
}
