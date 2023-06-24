import 'package:fcd_flutter/base/constants.dart';
import 'package:flutter/material.dart';

class CircleImageCookie extends StatelessWidget {
  final String imageUrl;
  final String errImage;
  final double height;
  final double width;
  const CircleImageCookie({super.key, required this.imageUrl,required this.errImage,required this.width,required this.height});

  @override
  Widget build(BuildContext context) {
    return
      ClipOval(
        child: Container(
          width: width,
          height: height,
          decoration: const BoxDecoration(
              color: Colors.white),
          child: Image.network(
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
              'Cookie': Constants.sharedPreferences.get('set-cookie').toString()
            },
            fit: BoxFit.cover,
          )
        ),
      );

  }
}
