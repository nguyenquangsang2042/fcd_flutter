import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:flutter/material.dart';
import 'package:flutter_inappwebview/flutter_inappwebview.dart';

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
    CookieManager _cookieManager = CookieManager.instance();
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
                  _cookieManager.setCookie(
                    url: Uri.parse(Constanst.baseURL),
                    name: Constanst.sharedPreferences
                        .get('set-cookie')
                        .toString()
                        .substring(0, 17),
                    value: Constanst.sharedPreferences
                        .get('set-cookie')
                        .toString()
                        .split(";")[0]
                        .substring(18),
                    domain: Constanst.baseDomain,
                    isSecure: true,
                  );
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
                    shouldOverrideUrlLoading: (controller,navigationAction)async{
                      return NavigationActionPolicy.ALLOW;
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
