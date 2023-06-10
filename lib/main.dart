import 'dart:io';

import 'package:device_info_plus/device_info_plus.dart';
import 'package:fcd_flutter/base/api/api_client.dart';
import 'package:fcd_flutter/base/api/api_controller.dart';
import 'package:fcd_flutter/base/constans.dart';
import 'package:fcd_flutter/base/model/device_info.dart';
import 'package:fcd_flutter/blocs/login/login_cubit.dart';
import 'package:fcd_flutter/blocs/navigation/navigation_cubit.dart';
import 'package:fcd_flutter/screens/navigation_screen/navigation_screen.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:package_info_plus/package_info_plus.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'base/api/dio_controller.dart';
import 'base/database/app_database.dart';
import 'package:firebase_core/firebase_core.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp();
  Constanst.db =
  await $FloorAppDatabase.databaseBuilder('fcd_database.db').build();
  Constanst.api = ApiClient(DioController().dio);
  Constanst.apiController = ApiController();
  Constanst.apiController.updateMasterData();
  Constanst.sharedPreferences= await SharedPreferences.getInstance();
  getDeviceInfo();
  runApp(const MyApp());
}

Future<void> getDeviceInfo() async {
  final DeviceInfoPlugin deviceInfo = DeviceInfoPlugin();
  final PackageInfo packageInfo = await PackageInfo.fromPlatform();
  FirebaseMessaging messaging = FirebaseMessaging.instance;
  if (Platform.isAndroid) {
    AndroidDeviceInfo androidInfo = await deviceInfo.androidInfo;
    Constanst.deviceInfo = DeviceInfo.required(DeviceId: "'${androidInfo.id}'",
        DevicePushToken: "'${await messaging.getToken()}'",
        DeviceOS: 1,
        AppVersion:"'${packageInfo.version}'",
        DeviceOSVersion: "'${androidInfo.version.release}'",
        DeviceModel: "'${androidInfo.model}'");
  } else if (Platform.isIOS) {}
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
