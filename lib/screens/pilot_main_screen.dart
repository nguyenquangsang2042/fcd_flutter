import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/model/app/bean_banner.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class PilotMainScreen extends StatelessWidget {
  const PilotMainScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
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
                        child: Image.asset('asset/images/icon_qrcode.png'),
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
            Container(
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
                    padding: EdgeInsets.only(left: 15),
                    child: ClipOval(
                      child: Container(
                        width: 40.0,
                        height: 40.0,
                        decoration: const BoxDecoration(color: Colors.white),
                        child: Image.asset('asset/images/icon_avatar64.png'),
                      ),
                    ),
                  )),
            ),
            SizedBox(
              height: 200,
              child: StreamBuilder(
                stream: Constanst.db.bannerDao.findAll(),
                builder: (context, snapshot) {
                  if (snapshot.hasData) {
                    return PageView.builder(
                      itemCount: snapshot.data!.length,
                      itemBuilder: (BuildContext context, int index) {
                        return ImageWithCookie(
                          imageUrl:
                              '${Constanst.baseURL}${snapshot.data![index].filePath}',
                        );
                      },
                    );
                  } else {
                    return const CircularProgressIndicator();
                  }
                },
              ),
            )
          ],
        ),
      ),
    );
  }
}
