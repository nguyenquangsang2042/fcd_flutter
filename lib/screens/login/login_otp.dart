import 'package:fcd_flutter/blocs/navigation/navigation_cubit.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class LoginOTPScreen extends StatelessWidget {
  LoginOTPScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(
      child: ElevatedButton(
        onPressed: () {
          BlocProvider.of<NavigationCubit>(context).navigateToMainView();
        },
        child: Text("Click"),
      ),
    );
  }
}
