import 'package:fcd_flutter/base/constanst.dart';
import 'package:flutter/material.dart';

class ImageWithCookie extends StatelessWidget {
  final String imageUrl;
  final String errImage;
  const ImageWithCookie({super.key, required this.imageUrl,required this.errImage});

  @override
  Widget build(BuildContext context) {
    return Image.network(
      imageUrl,
      errorBuilder: (BuildContext context, Object exception, StackTrace? stackTrace) {
        // Return a default image or error widget here
        return Image.asset(errImage);
      },
      loadingBuilder: (context, child, loadingProcess) {
        if (loadingProcess == null) return child;
        return Image.asset(errImage);
      },
      headers: {
        'Cookie': Constanst.sharedPreferences.get('set-cookie').toString()
      },
      fit: BoxFit.cover,
    );
  }
}
