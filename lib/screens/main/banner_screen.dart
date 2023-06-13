import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:flutter/material.dart';

class BannerScreen extends StatelessWidget {
  const BannerScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return StreamBuilder(
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
                     return const Center(child: CircularProgressIndicator(),);
                  }
                },
              );
  }
}