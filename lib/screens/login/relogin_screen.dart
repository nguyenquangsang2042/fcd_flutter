import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:fcd_flutter/base/api/login_controller.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../blocs/login/login_cubit.dart';
import '../../blocs/navigation/navigation_cubit.dart';

class ReLoginScreen extends StatelessWidget {
  const ReLoginScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    getReLogin(context);
    return Container(
      decoration: const BoxDecoration(
        image: DecorationImage(
          image: AssetImage('asset/images/Background.png'),
          fit: BoxFit.cover,
        ),
      ),
    );
  }

  Future<void> getReLogin(BuildContext context) async {
    if (await Connectivity().checkConnectivity() == ConnectivityResult.none) {
      BlocProvider.of<NavigationCubit>(context).navigateToMainView();
    } else {
      LoginController.instance
          .getCurrentLoginUser(
              Constants.deviceInfo.toJson().toString(),
              "{'Email':'${Constants.loginName}','VerifyCode':'${Constants.loginPass}'}",
              "1",
              "1")
          .then((value) => updateDataLoginAndCurrentUser(value.data, context))
          .onError((error, stackTrace) => AlertDialogController.instance
                  .showAlert(context, "Vietnam Airlines",
                      "Auth fail, try again!!", "Cancel", () {
                BlocProvider.of<LoginCubit>(context).navigationToLoginMail();
              }));
    }
  }

  updateDataLoginAndCurrentUser(currentUser, BuildContext context) {
    Constants.currentUser = currentUser;
    BlocProvider.of<LoginCubit>(context).navigationToLoginLoaiding();
  }
}
