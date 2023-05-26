import 'package:flutter/material.dart';

class LoginMailScreen extends StatelessWidget {
  const LoginMailScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
          decoration: BoxDecoration(
            image: DecorationImage(
              image: AssetImage('asset/images/Background.png'),
              fit: BoxFit.cover,
            ),
          ),
          child: Column(
            children: <Widget>[
              SizedBox(
                height: 30,
              ),
              Container(
                  child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  Image.asset('asset/images/vna_logo.png'),
                  const TextField(
                    style: TextStyle(color: Colors.white),
                    decoration: InputDecoration(
                      hintText: "Input your email of VietnamAirlines",
                      hintStyle: TextStyle(
                          color: Colors.white,
                          fontStyle: FontStyle.normal,
                          fontWeight: FontWeight.normal),
                      // add any other decoration properties you want
                    ),
                  )
                ],
              )),
              Container(margin: EdgeInsets.only(top: 10),child:  Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  SizedBox(width: 10),
                  RawMaterialButton(
                    onPressed: () {
                      // do something onTap
                    },
                    elevation: 2.0,
                    fillColor: Colors.white,
                    child: Text("Next"),
                    padding: EdgeInsets.all(15.0),
                    shape: CircleBorder(),
                  ),
                ],
              ),)
,              // Center empty container
              Expanded(
                child: Container(),
              ),
              Container(
                padding: EdgeInsets.all(20),
                child: Row(
                  children: [
                    Image.asset('asset/images/icon_helpdesk.png',color: Colors.white,),
                    Text("data"),
                    Text(" / "),
                    Text("data"),
                  ],
                ),
              ),

            ],
          )),
    );
  }
}
