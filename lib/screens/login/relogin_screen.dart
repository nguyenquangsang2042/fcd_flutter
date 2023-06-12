import 'package:fcd_flutter/base/api/login_controller.dart';
import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../blocs/login/login_cubit.dart';

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
    LoginController.instance
        .getCurrentLoginUser(
            Constanst.deviceInfo.toJson().toString(),
            "{'Email':'${Constanst.loginName}','VerifyCode':'${Constanst.loginPass}'}",
            "1",
            "1")
        .then((value) => updateDataLoginAndCurrentUser(value.data, context))
        .onError((error, stackTrace) => AlertDialogController.instance
            .showAlert(context, "Vietnam Airlines", "Auth fail, try again!!",
                "Cancel", () {}));
  }

  updateDataLoginAndCurrentUser(currentUser, BuildContext context) {
    Constanst.currentUser = currentUser;
    BlocProvider.of<LoginCubit>(context).navigationToLoginLoaiding();
  }
}
