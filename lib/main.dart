import 'package:dio/dio.dart';
import 'package:fcd_flutter/base/api/api_client.dart';
import 'package:fcd_flutter/base/api/api_controller.dart';
import 'package:fcd_flutter/base/constans.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/db_variable.dart';
import 'package:fcd_flutter/base/model/app/settings.dart';
import 'package:fcd_flutter/blocs/login/login_cubit.dart';
import 'package:fcd_flutter/blocs/navigation/navigation_cubit.dart';
import 'package:fcd_flutter/screens/navigation_screen/navigation_screen.dart';
import 'package:floor/floor.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import 'base/database/app_database.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  Constanst.db =
      await $FloorAppDatabase.databaseBuilder('fcd_database.db').build();
  Constanst.api = ApiClient(Dio());
  Constanst.apiController= ApiController();
  Constanst.apiController.updateMasterData();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [
        BlocProvider<NavigationCubit>(
          create: (_) => NavigationCubit(),
        ),
        BlocProvider<LoginCubit>(
          create: (_) => LoginCubit(),
        ),
      ],
      child: MaterialApp(
          title: 'Flutter Demo',
          theme: ThemeData(
            colorScheme: ColorScheme.fromSeed(seedColor: Colors.blueAccent),
            useMaterial3: true,
          ),
          home: NavigationScreen()),
    );
  }
}
