import 'package:fcd_flutter/base/constanst.dart';
import 'package:flutter/material.dart';

class ImageWithCookie extends StatelessWidget {
  final String imageUrl;
  const ImageWithCookie({super.key, required this.imageUrl});

  @override
  Widget build(BuildContext context) {
    return Image.network(
      imageUrl,
      loadingBuilder: (context, child, loadingProcess) {
        if (loadingProcess == null) return child;
        return Image.asset('asset/images/main_background.png');
      },
      headers: {
        'Cookie': Constanst.sharedPreferences.get('set-cookie').toString()
      },
      fit: BoxFit.cover,
    );
  }
}
