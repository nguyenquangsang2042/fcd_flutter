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
    );
  }
}
