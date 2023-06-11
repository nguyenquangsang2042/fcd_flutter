import 'package:fcd_flutter/blocs/navigation/navigation_cubit.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class ReloginSreen extends StatelessWidget {
  const ReloginSreen({super.key});

  @override
  Widget build(BuildContext context) {
    Future.delayed(Duration(seconds: 3),()=> getDynamicData(context));
    return Container(
      decoration: const BoxDecoration(
        image: DecorationImage(
          image: AssetImage('asset/images/Background.png'),
          fit: BoxFit.cover,
        ),
      ),
    );
  }

  Future<void> getDynamicData(BuildContext context) async {
        BlocProvider.of<NavigationCubit>(context).navigateToMainView();
  }
}
