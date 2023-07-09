import 'package:fcd_flutter/base/model/app/student.dart';
import 'package:fcd_flutter/base/widgets/connectivity_widget.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_inappwebview/flutter_inappwebview.dart';

import '../../base/constants.dart';
import '../../base/download_file.dart';
import '../../base/functions.dart';
import 'package:path/path.dart';

class CourseWebview extends StatelessWidget {
  CourseWebview({super.key, required this.data});
  Student data;
  @override
  Widget build(BuildContext context) {
    return ConnectivityWidget(
        onlineWidget: buildDetail(),
        offlineWidget: Container(
          child: Center(
            child: Text("Please Connect to Intenet"),
          ),
        ));
  }

  Scaffold buildDetail() {
    return Scaffold(
        body: StreamBuilder(
          stream: Constants.db.notifyDao.getNotifyWithID(data.notifyId!),
          builder: (context, snapshot) {
            if (snapshot.connectionState == ConnectionState.active) {
              if (snapshot.hasData && snapshot.data != null) {
                String url =
                    "/FrontEnd/Announcement.aspx?AnnouncementId=${snapshot.data!.announcementId}&UserID=${snapshot.data!.userId}&IsDlg=0";
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
                      await DownloadFile.downloadFile(
                          context, url.toString(), basename(url.toString()));
                      return NavigationActionPolicy.CANCEL;
                    }
                    return NavigationActionPolicy.ALLOW;
                  },
                  initialUrlRequest:
                      URLRequest(url: Uri.parse('${Constants.baseURL}$url')),
                );
              } else {
                return Container(
                  child: Center(
                    child: Text("Nodata"),
                  ),
                );
              }
            } else {
              return Container(
                child: Center(
                  child: CircularProgressIndicator(),
                ),
              );
            }
          },
        ),
      );
  }
}
