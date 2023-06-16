import 'package:fcd_flutter/blocs/navigation/navigation_cubit.dart';
import 'package:fcd_flutter/screens/login/login_provider.dart';
import 'package:fcd_flutter/screens/main/pilot_main_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class NavigationScreen extends StatelessWidget {
  const NavigationScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: BlocBuilder(
        bloc: BlocProvider.of<NavigationCubit>(context),
        builder: (BuildContext context, state) {
          if (state == NavigationView.login) {
            return const LoginProvider();
          } else if (state == NavigationView.main) {
            return PilotMainScreen();
          }
          return const SizedBox.shrink();
        },
      ),
    );
  }
}
