import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/model/app/bean_banner.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:fcd_flutter/screens/main/banner_screen.dart';
import 'package:fcd_flutter/screens/main/main_controller.dart';
import 'package:fcd_flutter/screens/main/recycle_grid_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class PilotMainScreen extends StatelessWidget {
  const PilotMainScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    MainController.instance.wightBanner.value =
        MediaQuery.of(context).size.width.toInt();
    MainController.instance.heightBanner.value = 200;
    MainController.instance.wightScreen =
        MediaQuery.of(context).size.width.toInt();
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
              Expanded(
                flex: 1,
                child: Align(
                    alignment: Alignment.centerRight,
                    child: SizedBox(
                        child: Image.asset('asset/images/icon_qrcode.png',),
                        height: 35,
                        width: 35)),
              )
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
                        SizedBox(
                          width: value.toDouble(),
                          height: MainController.instance.heightBanner.value
                              .toDouble(),
                          child: const BannerScreen(),
                        )
                      ],
                    );
                  }
                }),
            const Flexible(flex: 1, child: RecycleGridScreen())
          ],
        ),
      ),
    );
  }
}
