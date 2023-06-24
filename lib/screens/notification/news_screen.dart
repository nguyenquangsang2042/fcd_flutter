import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/download_file.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/widgets/connectivity_widget.dart';
import 'package:flutter/material.dart';
import 'package:flutter_inappwebview/flutter_inappwebview.dart';
import 'package:path/path.dart';

class NewsScreen extends StatelessWidget {
  NewsScreen({super.key, required this.notify});

  Notify notify;
  String url = '';

  @override
  Widget build(BuildContext context) {
    return ConnectivityWidget(
        onlineWidget: FutureBuilder(
          future: Constants.db.settingDao
              .findSettingByKey("NOTIFICATION_REQUIE_LOGIN"),
          builder: (context, keyNews) {
            if (keyNews.hasData &&
                keyNews.data != null &&
                notify.announCategoryId != null) {
              return FutureBuilder(
                  future: Constants.api.mobileAutoLoginWeb(
                      Constants.sharedPreferences.get('set-cookie').toString(),
                      Constants.currentUser.id),
                  builder: (context, loginKey) {
                    if (loginKey.hasData &&
                        loginKey.data != null &&
                        loginKey.data!.data != null) {
                      if (notify.announCategoryId == 7) {
                        url =
                            "/frontend/NewDetail.aspx?IID=${notify.announcementId}&UserID=${notify.userId}&IsDlg=0&FlgRead=1&autoid=${loginKey.data!.data}";
                      } else {
                        if (keyNews.data!.VALUE
                            .contains(notify.announCategoryId.toString())) {
                          url =
                              '/FrontEnd/Announcement.aspx?AnnouncementId=${notify.announcementId}&UserID=${notify.userId}&IsDlg=0&FlgRead=1&autoid=${loginKey.data!.data}';
                        } else {
                          url =
                              '/FrontEnd/Announcement.aspx?AnnouncementId=${notify.announcementId}&UserID=${notify.userId}&IsDlg=0&FlgRead=1';
                        }
                      }

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
                      return Container();
                    }
                  });
            } else {
              return Container();
            }
          },
        ),
        offlineWidget: const Scaffold(
          body: Center(
            child: Text("Vui lòng kết nối internet!!"),
          ),
        ));
  }
}
