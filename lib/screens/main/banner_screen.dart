import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:flutter/material.dart';

class BannerScreen extends StatelessWidget {
  const BannerScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return StreamBuilder(
                stream: Constants.db.bannerDao.findAll(),
                builder: (context, snapshot) {
                  if (snapshot.hasData) {
                    return PageView.builder(
                      itemCount: snapshot.data!.length,
                      itemBuilder: (BuildContext context, int index) {
                        return ImageWithCookie(
                          imageUrl:
                              '${Constants.baseURL}${snapshot.data![index].filePath}',
                          errImage: 'asset/images/main_background.png',
                        );
                      },
                    );
                  } else {
                     return const Center(child: CircularProgressIndicator(),);
                  }
                },
              );
  }
}