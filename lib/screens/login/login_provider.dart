import 'package:fcd_flutter/blocs/login/login_cubit.dart';
import 'package:fcd_flutter/screens/login/login_mail.dart';
import 'package:fcd_flutter/screens/login/login_otp.dart';
import 'package:fcd_flutter/screens/login/relogin_screen.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class LoginProvider extends StatelessWidget {
  const LoginProvider({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BlocBuilder(
        bloc: BlocProvider.of<LoginCubit>(context),
        builder: (context, state) {
          if (state is LoginMailState) {
            return const LoginMailScreen();
          } else if (state is LoginOTPState) {
            return const LoginOTPScreen();
          } else {
            return ReloginSreen();
          }
        });
  }
}
