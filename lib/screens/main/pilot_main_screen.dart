import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:declarative_refresh_indicator/declarative_refresh_indicator.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/model/app/bean_banner.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:fcd_flutter/screens/main/banner_screen.dart';
import 'package:fcd_flutter/screens/main/main_controller.dart';
import 'package:fcd_flutter/screens/main/recycle_grid_screen.dart';
import 'package:fcd_flutter/screens/qr_scan/qr_scan_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class PilotMainScreen extends StatelessWidget {
  PilotMainScreen({Key? key}) : super(key: key);
  final ValueNotifier<bool> isRefresh = ValueNotifier(false);
  bool isNeedShowAds = true;
  @override
  Widget build(BuildContext context) {
    MainController.instance.wightBanner.value =
        MediaQuery.of(context).size.width.toInt();
    MainController.instance.heightBanner.value = 200;
    MainController.instance.wightScreen =
        MediaQuery.of(context).size.width.toInt();
    if (Connectivity().checkConnectivity() != ConnectivityResult.none) {
      Constants.api
          .getMyUserInfo(
              Constants.sharedPreferences.get('set-cookie').toString())
          .then((value) =>
      Constants.currentUser = value.data);
    }
    return Scaffold(
      backgroundColor: Color(0xFF00485C),
      appBar: AppBar(
          backgroundColor: Color(0xFF00485C),
          title: Flex(
            direction: Axis.horizontal,
            children: [
              Expanded(
                flex: 3,
                child: Image.asset('asset/images/ic_title_vnairlines.png'),
              ),
              buildIconScanQR(context)
            ],
          )),
      body: Container(
        decoration: const BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.only(
            topLeft: Radius.circular(30),
            topRight: Radius.circular(30),
          ),
        ),
        child: Column(
          children: [
            ValueListenableBuilder<int>(
                valueListenable: MainController.instance.wightBanner,
                builder: (context, value, child) {
                  if (value <= 150) {
                    return Row(
                      children: [
                        Flexible(
                          child: Container(
                            width: MainController.instance.wightScreen / 2,
                            decoration: const BoxDecoration(
                              color: Colors.white,
                              borderRadius: BorderRadius.only(
                                topLeft: Radius.circular(30),
                                topRight: Radius.circular(30),
                              ),
                            ),
                            height: 50,
                            child: Align(
                                alignment: Alignment.centerLeft,
                                child: Padding(
                                  padding: const EdgeInsets.only(left: 15),
                                  child: ClipOval(
                                    child: Container(
                                      width: 40.0,
                                      height: 40.0,
                                      decoration: const BoxDecoration(
                                          color: Colors.white),
                                      child: Image.asset(
                                          'asset/images/icon_avatar64.png'),
                                    ),
                                  ),
                                )),
                          ),
                        ),
                        Flexible(
                          child: Container(
                            margin: EdgeInsets.only(right: 20),
                            width: MainController.instance.wightScreen / 2,
                            decoration: const BoxDecoration(
                              color: Colors.white,
                              borderRadius: BorderRadius.only(
                                topLeft: Radius.circular(30),
                                topRight: Radius.circular(30),
                              ),
                            ),
                            height: 50,
                            child: const Align(
                                alignment: Alignment.centerRight,
                                child: Padding(
                                  padding: EdgeInsets.all(4),
                                  child: SizedBox(
                                    width: 80,
                                    height: 40,
                                    child: Align(
                                      alignment: Alignment.centerRight,
                                      child: BannerScreen(),
                                    ),
                                  ),
                                )),
                          ),
                        ),
                      ],
                    );
                  } else {
                    return Wrap(
                      children: [
                        Container(
                          width: MainController.instance.wightScreen / 2,
                          decoration: const BoxDecoration(
                            color: Colors.white,
                            borderRadius: BorderRadius.only(
                              topLeft: Radius.circular(30),
                              topRight: Radius.circular(30),
                            ),
                          ),
                          height: 50,
                          child: Align(
                              alignment: Alignment.centerLeft,
                              child: Padding(
                                padding: const EdgeInsets.only(left: 15),
                                child: ClipOval(
                                  child: Container(
                                    width: 40.0,
                                    height: 40.0,
                                    decoration: const BoxDecoration(
                                        color: Colors.white),
                                    child: Image.asset(
                                        'asset/images/icon_avatar64.png'),
                                  ),
                                ),
                              )),
                        ),
                        Align(
                          alignment: Alignment.topRight,
                          child: SizedBox(
                            width: value.toDouble(),
                            height: MainController.instance.heightBanner.value
                                .toDouble(),
                            child: const BannerScreen(),
                          ),
                        )
                      ],
                    );
                  }
                }),
            Flexible(
                flex: 1,
                child: ValueListenableBuilder<bool>(
                  valueListenable: isRefresh,
                  builder: (_, value, __) {
                    if(isNeedShowAds)
                      {
                        showNotifyAlert(context);
                        isNeedShowAds=false;
                      }
                    return DeclarativeRefreshIndicator(
                        child: RecycleGridScreen(),
                        refreshing: value,
                        onRefresh: () {
                          isRefresh.value = true;
                          Constants.apiController.updateMasterData();
                          Future.delayed(Duration(seconds: 3))
                              .then((value) => isRefresh.value = false);
                        });
                  },
                ))
          ],
        ),
      ),
    );
  }

  void showNotifyAlert(BuildContext context) {
    Constants.db.notifyDao.checkPopAnnouncement().then((value) {
      if(value.isNotEmpty)
        {
          // show dialog => kiem ta lai thong tin;
        }
    });
  }

  Expanded buildIconScanQR(BuildContext context) {
    return Expanded(
              flex: 1,
              child: Align(
                  alignment: Alignment.centerRight,
                  child: InkResponse(child: SizedBox(
                      child: Image.asset(
                        'asset/images/icon_qrcode.png',
                      ),
                      height: 35,
                      width: 35),onTap: () {
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => QRScannerScreen()));
                      },)),
            );
  }
}
