import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_inappwebview/flutter_inappwebview.dart';

import '../../base/constants.dart';
import '../../base/download_file.dart';
import '../../base/functions.dart';
import '../../base/widgets/connectivity_widget.dart';
import 'package:path/path.dart';

class ReportScreen extends StatelessWidget {
  const ReportScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: ConnectivityWidget(onlineWidget: FutureBuilder(
          future: Constants.api.mobileAutoLoginWeb(
              Constants.sharedPreferences.get('set-cookie').toString(),
              Constants.currentUser.id),
          builder: (context, loginKey) {
            if (loginKey.hasData &&
                loginKey.data != null &&
                loginKey.data!.data != null) {
              String url="/Admin/ReportFlightAndPilot.aspx?AutoId=${loginKey.data!.data}";
              return InAppWebView(
                onReceivedServerTrustAuthRequest:
                    (controller, challenge) async {
                  return ServerTrustAuthResponse(
                      action: ServerTrustAuthResponseAction.PROCEED);
                },
                initialOptions: InAppWebViewGroupOptions(
                  crossPlatform: InAppWebViewOptions(
                    // Set JavaScriptEnabled true
                      javaScriptEnabled: true,
                      useShouldOverrideUrlLoading: true),
                ),
                shouldOverrideUrlLoading:
                    (controller, navigationAction) async {
                  String url = navigationAction.request.url.toString();
                  if (url.toString().contains("tel")) {
                    await controller.goBack();
                    await Functions.instance.launchCustomUrl(
                        url.toString().split(":")[0],
                        url.toString().split(":")[1]);
                    return NavigationActionPolicy.CANCEL;
                  } else if (url
                      .toString()
                      .toLowerCase()
                      .endsWith('.doc') ||
                      url.toString().toLowerCase().endsWith('.docx') ||
                      url.toString().toLowerCase().endsWith('.pdf') ||
                      url.toString().toLowerCase().endsWith('.xls') ||
                      url.toString().toLowerCase().endsWith('.xlsx') ||
                      url.toString().toLowerCase().endsWith('.ppt') ||
                      url.toString().toLowerCase().endsWith('.pptx') ||
                      url.toString().toLowerCase().endsWith('.jpg') ||
                      url.toString().toLowerCase().endsWith('.png') ||
                      url.toString().toLowerCase().endsWith('.gif') ||
                      url.toString().toLowerCase().endsWith('.txt')) {
                    await controller.goBack();
                    await DownloadFile.downloadFile(context,
                        url.toString(), basename(url.toString()));
                    return NavigationActionPolicy.CANCEL;
                  }
                  return NavigationActionPolicy.ALLOW;
                },
                initialUrlRequest: URLRequest(
                    url: Uri.parse('${Constants.baseURL}$url')),
              );
            } else {
              return Container(color: Colors.white,);
            }
          }),offlineWidget: Container(child: Center(child: Text("Vui lòng kết nối mạng"),),),
      ),
    );
  }
  AppBar buildAppBar(BuildContext context) {
    return AppBar(
      leading: SizedBox(
        width: 50,
        height: 50,
        child: IconButton(
          icon: Image.asset(
            'asset/images/icon_back30.png',
            color: Colors.white,
            height: 20,
            width: 40,
          ),
          onPressed: () {
            Navigator.pop(context);
          },
        ),
      ),
      title: const Text(
        "FLIGHTS - PILOTS REPORT",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold,fontSize: 18),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
    );
  }

}
