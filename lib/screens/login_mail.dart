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
          SizedBox(height: 30,),
          Container(
              child: Image.asset('asset/images/vna_logo.png')
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
