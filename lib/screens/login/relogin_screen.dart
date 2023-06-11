import 'package:flutter/material.dart';

class ReloginSreen extends StatelessWidget {
  const ReloginSreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: const BoxDecoration(
        image: DecorationImage(
          image: AssetImage('asset/images/Background.png'),
          fit: BoxFit.cover,
        ),
      ),
    );
  }
}
