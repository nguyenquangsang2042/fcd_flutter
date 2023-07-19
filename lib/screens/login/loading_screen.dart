import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/blocs/navigation/navigation_cubit.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class LoadingScreen extends StatelessWidget {
  const LoadingScreen({super.key});

  @override
  Widget build(BuildContext context) {
    getDynamicData(context);
    return Container(
      decoration: const BoxDecoration(
        image: DecorationImage(
          image: AssetImage('asset/images/Background.png'),
          fit: BoxFit.fill,
        ),
      ),
    );
  }

  Future<void> getDynamicData(BuildContext context) async {
    Constants.apiController.updateAllDynamicData();

    BlocProvider.of<NavigationCubit>(context).navigateToMainView();
  }
}
