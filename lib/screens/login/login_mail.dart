import 'package:dio/dio.dart';
import 'package:fcd_flutter/base/alert_dialog.dart';
import 'package:fcd_flutter/base/api/api_client.dart';
import 'package:fcd_flutter/base/model/status.dart';
import 'package:fcd_flutter/screens/login/login_otp.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_progress_hud/flutter_progress_hud.dart';
import 'package:logger/logger.dart';
import 'package:url_launcher/url_launcher.dart';

import '../../base/exports_base.dart';
import '../../blocs/login/login_cubit.dart';

class LoginMailScreen extends StatelessWidget {
  const LoginMailScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    TextEditingController emailController =
    TextEditingController(text: "thainv@vuthao.com");
    return Scaffold(
      body: Container(
          decoration: const BoxDecoration(
            image: DecorationImage(
              image: AssetImage('asset/images/Background.png'),
              fit: BoxFit.cover,
            ),
          ),
          child: ProgressHUD(
            child: Builder(
              builder: (context) {
                return Column(
                  children: <Widget>[
                    const SizedBox(
                      height: 30,
                    ),
                    Column(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        Image.asset('asset/images/vna_logo.png'),
                        TextField(
                          controller: emailController,
                          style: const TextStyle(color: Colors.white),
                          decoration: const InputDecoration(
                            hintText: "Input your email of VietnamAirlines",
                            hintStyle: TextStyle(
                                color: Colors.white,
                                fontStyle: FontStyle.normal,
                                fontWeight: FontWeight.normal),
                            // add any other decoration properties you want
                          ),
                        )
                      ],
                    ),
                    Container(
                      margin: const EdgeInsets.only(top: 10),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.end,
                        children: [
                          const SizedBox(width: 10),
                          RawMaterialButton(
                            onPressed: () async {
                              Functions.instance.hideKeyboard();
                              ProgressHUD.of(context)?.show();
                              if (RegExp(
                                  r'^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$')
                                  .hasMatch(emailController.text)) {
                                Dio dio = Dio();
                                Status status = await ApiClient(dio).getOtp(
                                    "{'Email':'${emailController.text}'}");
                                if (!status.status
                                    .toLowerCase()
                                    .contains('err')) {
                                  AlertDialogController.instance.showAlert(
                                      context, "Vietnam Airlines",
                                      "We have sent OTP to your email address, please check email to get OTP code. Thank you.",
                                      "Close",(){
                                    BlocProvider.of<LoginCubit>(context).navigationToLoginOTP(emailController.text);
                                  });
                                }
                                else {
                                  AlertDialogController.instance.showAlert(
                                      context, "Vietnam Airlines",
                                      "Authent fail, please try again or contact to admin for more information, thank you!",
                                      "Close",null);
                                }
                                ProgressHUD.of(context)?.dismiss();
                              } else {
                                ProgressHUD.of(context)?.dismiss();
                                AlertDialogController.instance.showAlert(
                                    context, "Vietnam Airlines",
                                    "Please enter valid email, thank you!",
                                    "Close",null);
                              }
                            },
                            elevation: 2.0,
                            fillColor: Colors.white,
                            padding: const EdgeInsets.all(15.0),
                            shape: const CircleBorder(),
                            child: const Text("Next"),
                          ),
                        ],
                      ),
                    ), // Center empty container
                    Expanded(
                      child: Container(),
                    ),
                    Container(
                      padding: EdgeInsets.all(20),
                      child: Row(
                        children: [
                          Image.asset(
                            'asset/images/icon_helpdesk.png',
                            color: Colors.white,
                          ),
                          TextButton(
                            onPressed: () => _launchUrl('tel', '0966443324'),
                            child: const Text(
                              "0966443324",
                              style:
                              TextStyle(color: Colors.white, fontSize: 14),
                            ),
                          ),
                          const Text("/"),
                          TextButton(
                            onPressed: () => _launchUrl('tel', '0966443324'),
                            child: const Text(
                              "0966443324",
                              style:
                              TextStyle(color: Colors.white, fontSize: 14),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ],
                );
              },
            ),
          )),
    );
  }

  Future<void> _launchUrl(_type, _url) async {
    final Uri smsLaunchUri = Uri(
      scheme: _type,
      path: _url,
    );
    if (!await launchUrl(smsLaunchUri)) {
      throw Exception('Could not launch $_url');
    }
  }
}
