import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../blocs/login/login_cubit.dart';
import '../api/login_controller.dart';

class ConnectivityWidget extends StatelessWidget {
  ConnectivityWidget(
      {super.key, required this.onlineWidget, required this.offlineWidget});
  Widget onlineWidget;
  Widget offlineWidget;
  @override
  Widget build(BuildContext context) {
    return StreamBuilder(
      stream: Connectivity().onConnectivityChanged,
      builder: (context, snapshot) {
        if (snapshot.connectionState== ConnectionState.waiting|| snapshot.data == ConnectivityResult.none || snapshot.data==null) {
          return offlineWidget;

        } else {
          return FutureBuilder(
            future: LoginController.instance.getCurrentLoginUser(
                Constants.deviceInfo.toJson().toString(),
                "{'Email':'${Constants.loginName}','VerifyCode':'${Constants.loginPass}'}",
                "1",
                "1"),
            builder: (context, snapshot) {
              if (snapshot.connectionState == ConnectionState.done) {
                if (snapshot.data == null || snapshot.data!.data == null) {
                  BlocProvider.of<LoginCubit>(context).navigationToLoginMail();
                } else {
                  Constants.currentUser = snapshot.data!.data;
                }
                return onlineWidget;
              } else {
                return Container(child: Center(
                  child: CircularProgressIndicator(),
                ),);
              }
            },
          );
        }

      },
    );
  }
}
