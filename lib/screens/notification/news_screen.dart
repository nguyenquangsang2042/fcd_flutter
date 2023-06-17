import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/download_file.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:flutter/material.dart';
import 'package:flutter_inappwebview/flutter_inappwebview.dart';
import 'package:path/path.dart';

class NewsScreen extends StatelessWidget {
  NewsScreen(
      {super.key,
      required this.notify,
      required this.safetyID,
      required this.qualificationID});

  Notify notify;
  String safetyID;
  String qualificationID;
  String url = '';
  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
      future:
          Constanst.db.settingDao.findSettingByKey("NOTIFICATION_REQUIE_LOGIN"),
      builder: (context, keynews) {
        if (keynews.hasData &&
            keynews.data != null &&
            notify.announCategoryId != null) {
          return FutureBuilder(
              future: Constanst.api.mobileAutoLoginWeb(
                  Constanst.sharedPreferences.get('set-cookie').toString(),
                  Constanst.currentUser.id),
              builder: (context, loginKey) {
                if (loginKey.hasData &&
                    loginKey.data != null &&
                    loginKey.data!.data != null) {
                  if (notify.announCategoryId == 7) {
                    url =
                        "/frontend/NewDetail.aspx?IID=${notify.announcementId}&UserID=${notify.userId}&IsDlg=0&FlgRead=1&autoid=${loginKey.data!.data}";
                  } else {
                    if (keynews.data!.VALUE
                        .contains(notify.announCategoryId.toString())) {
                      url =
                          '/FrontEnd/Announcement.aspx?AnnouncementId=${notify.announcementId}&UserID=${notify.userId}&IsDlg=0&FlgRead=1&autoid=${loginKey.data!.data}';
                    } else {
                      url =
                          '/FrontEnd/Announcement.aspx?AnnouncementId=${notify.announcementId}&UserID=${notify.userId}&IsDlg=0&FlgRead=1';
                    }
                  }
                  return InAppWebView(
                    shouldOverrideUrlLoading: (controler,navigationAction)async
                    {
                      if (url.toString().contains("tel")) {
                        Functions.instance.launchCustomUrl(
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
                         DownloadFile.downloadFile(
                            context, url.toString(), basename(url.toString()));
                         return NavigationActionPolicy.CANCEL;
                      }
                      return NavigationActionPolicy.ALLOW;
                    },
                    onLoadStart: (controll, url) async {
                      await controll.goBack();
                      if (url.toString().contains("tel")) {
                        await Functions.instance.launchCustomUrl(
                            url.toString().split(":")[0],
                            url.toString().split(":")[1]);

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
                        await DownloadFile.downloadFile(
                            context, url.toString(), basename(url.toString()));
                      }
                    },
                    initialUrlRequest:
                        URLRequest(url: Uri.parse('${Constanst.baseURL}$url')),
                  );
                } else {
                  return Container();
                }
              });
        } else {
          return Container();
        }
      },
    );
  }
}
