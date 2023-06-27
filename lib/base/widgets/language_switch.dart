import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class LanguageSwitcher extends StatefulWidget {
  @override
  _LanguageSwitcherState createState() => _LanguageSwitcherState();
}

class _LanguageSwitcherState extends State<LanguageSwitcher> {
  bool _isVnSelected = true;

  @override
  Widget build(BuildContext context) {
    return Stack(
      alignment: _isVnSelected ? Alignment.centerLeft : Alignment.centerRight,
      children: [
        Container(
          width: 90.0,
          height: 30.0,
          decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(20.0),
            color: _isVnSelected ? Colors.green : Colors.grey[300],
          ),
        ),
        Positioned(
          left: 8.0,
          child: Text(
            'VN',
            style: TextStyle(
              fontSize: 16.0,
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),
        ),
        Positioned(
          right: 8.0,
          child: Text(
            'EN',
            style: TextStyle(
              fontSize: 16.0,
              fontWeight: FontWeight.bold,
              color: _isVnSelected ? Colors.white : Colors.grey[700],
            ),
          ),
        ),
        Switch(
          value: _isVnSelected,
          onChanged: (bool newValue) {
            setState(() {
              _isVnSelected = newValue;
            });
          },
          activeColor: Colors.green,
          inactiveTrackColor: Colors.grey[300],
          inactiveThumbColor: Colors.grey[300],
        ),
      ],
    );
  }
}
