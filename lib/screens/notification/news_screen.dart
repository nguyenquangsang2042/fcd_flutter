import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:flutter/material.dart';
import 'package:flutter_inappwebview/flutter_inappwebview.dart';

class NewsScreen extends StatelessWidget {
  NewsScreen({super.key,required this.notify});
  Notify notify;

  @override
  Widget build(BuildContext context) {
    return InAppWebView(initialUrlRequest: URLRequest(url: Uri.parse(Constanst.baseURL)),);
  }
}
