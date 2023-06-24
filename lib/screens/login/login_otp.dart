import 'package:fcd_flutter/base/alert_dialog.dart';
import 'package:fcd_flutter/base/api/api_client.dart';
import 'package:fcd_flutter/base/api/api_controller.dart';
import 'package:fcd_flutter/base/api/dio_controller.dart';
import 'package:fcd_flutter/base/api/login_controller.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/crypto_controller.dart';
import 'package:fcd_flutter/blocs/login/login_cubit.dart';
import 'package:fcd_flutter/blocs/navigation/navigation_cubit.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:pinput/pinput.dart';

class LoginOTPScreen extends StatelessWidget {
  const LoginOTPScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    String email = (context.read<LoginCubit>().state as LoginOTPState).email;
    final defaultPinTheme = PinTheme(
      width: 56,
      height: 56,
      textStyle: const TextStyle(
          fontSize: 20,
          color: Color.fromRGBO(30, 60, 87, 1),
          fontWeight: FontWeight.w600),
      decoration: BoxDecoration(
        border: Border.all(color: const Color.fromRGBO(234, 239, 243, 1)),
        borderRadius: BorderRadius.circular(20),
      ),
    );
    final submittedPinTheme = defaultPinTheme.copyWith(
      decoration: defaultPinTheme.decoration?.copyWith(
        color: const Color.fromRGBO(234, 239, 243, 1),
      ),
    );
    return WillPopScope(
        child: Container(
            height: double.infinity,
            width: double.infinity,
            decoration: const BoxDecoration(
              image: DecorationImage(
                image: AssetImage('asset/images/Background.png'),
                fit: BoxFit.cover,
              ),
            ),
            child: SafeArea(
              child: Column(
                mainAxisSize: MainAxisSize.min,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Align(
                    alignment: Alignment.center,
                    child: Image.asset('asset/images/vna_logo.png'),
                  ),
                  const Padding(
                    padding: EdgeInsets.only(
                        top: 15, bottom: 15, left: 17, right: 15),
                    child: Text(
                      "Please enter OTP code from  email content",
                      style: TextStyle(color: Colors.white, fontSize: 14),
                      textAlign: TextAlign.left,
                    ),
                  ),
                  Align(
                    alignment: Alignment.center,
                    child: Pinput(
                      length: 6,
                      inputFormatters: [FilteringTextInputFormatter.digitsOnly],
                      obscureText: true,
                      keyboardType: TextInputType.number,
                      defaultPinTheme: submittedPinTheme,
                      focusedPinTheme: submittedPinTheme,
                      submittedPinTheme: submittedPinTheme,
                      pinputAutovalidateMode: PinputAutovalidateMode.onSubmit,
                      showCursor: true,
                      onCompleted: (pin) async {
                        LoginController.instance
                            .getCurrentLoginUser(
                                Constants.deviceInfo.toJson().toString(),
                                "{'Email':'$email','VerifyCode':'$pin'}",
                                "1",
                                "1")
                            .then((value) =>
                            updateDataLoginAndCurrentUser(
                                value.data, context, pin))
                            .onError((error, stackTrace) =>
                                AlertDialogController.instance.showAlert(
                                    context,
                                    "Vietnam Airlines",
                                    "Auth fail, try again!!",
                                    "Cancel",
                                    () {}));
                      },
                    ),
                  ),
                  Align(
                    alignment: Alignment.center,
                    child: TextButton(
                        onPressed: () {
                          Constants.api.getOtp(email).then((value) {
                            AlertDialogController.instance.showAlert(
                                context,
                                "Vietnam Airlines",
                                "We have sent OTP to your email address, please check email to get OTP code. Thank you.",
                                "Cancel",
                                () {});
                          }).onError((error, stackTrace) {
                            AlertDialogController.instance.showAlert(
                                context,
                                "Vietnam Airlines",
                                "Send fail, try again!!",
                                "Cancel",
                                () {});
                          });
                        },
                        child: const Row(
                          mainAxisSize: MainAxisSize.min,
                          children: [
                            Text(
                              "Not have OTP code ?  ",
                              style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.normal),
                            ),
                            Text(
                              "Resend",
                              style: TextStyle(color: Color(0xFF00C6C7)),
                            ),
                          ],
                        )),
                  )
                ],
              ),
            )),
        onWillPop: () => handlePopBack(context));
  }

  Future<bool> handlePopBack(context) async {
    BlocProvider.of<LoginCubit>(context).navigationToLoginMail();
    return false;
  }

  updateDataLoginAndCurrentUser(currentUser, BuildContext context, String pin) {
    Constants.currentUser = currentUser;
    Constants.sharedPreferences.setString(
        "email", (context.read<LoginCubit>().state as LoginOTPState).email);
    Constants.sharedPreferences.setString(
        "pass", pin);
    BlocProvider.of<LoginCubit>(context).navigationToLoginLoaiding();
  }
}
