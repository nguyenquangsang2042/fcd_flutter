import 'package:flutter/material.dart';

class LoginMailScreen extends StatelessWidget {
  const LoginMailScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        image: DecorationImage(
          image: AssetImage('asset/images/Background.png'),
          fit: BoxFit.cover,
        ),
      ),
      child: Column(
        children: <Widget>[
          Expanded(
            child: Container(
              child: Text("a"),
            ),
          ),
          // Center empty container
          Expanded(
            child: Container(),
          ),
          Container(
           child: Text("B"),
          ),
        ],
      )
    );
  }
}
