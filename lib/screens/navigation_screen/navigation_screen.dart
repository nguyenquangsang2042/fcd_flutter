import 'package:fcd_flutter/blocs/navigation/navigation_cubit.dart';
import 'package:fcd_flutter/screens/login/login_mail.dart';
import 'package:fcd_flutter/screens/pilot_main_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class NavigationScreen extends StatelessWidget {
  NavigationScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: BlocBuilder(
        bloc: BlocProvider.of<NavigationCubit>(context),
        builder: (BuildContext context, state) {
          if (state == NavigationView.login) {
            return LoginMailScreen();
          } else if (state == NavigationView.main) {
            return PilotMainScreen();
          }
          return SizedBox.shrink();
        },
      ),
    );
  }
}
