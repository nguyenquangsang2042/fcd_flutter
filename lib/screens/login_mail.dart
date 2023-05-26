import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

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
                    TextButton(onPressed: ()=>_launchUrl('tel','0966443324'),child: Text("0966443324",style: TextStyle(color: Colors.white,fontSize: 14),),),
                    Text("/"),
                    TextButton(onPressed: ()=>_launchUrl('tel','0966443324'),child: Text("0966443324",style: TextStyle(color: Colors.white,fontSize: 14),),),
                  ],
                ),
              ),

            ],
          )),
    );
  }
  Future<void> _launchUrl(_type,_url) async {
    final Uri smsLaunchUri = Uri(
      scheme: _type,
      path: _url,
    );
    if (!await launchUrl(smsLaunchUri)) {
      throw Exception('Could not launch $_url');
    }
  }
}
