import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_inappwebview/flutter_inappwebview.dart';

import '../../base/alert_dialog.dart';
import '../../base/constants.dart';
import '../../base/download_file.dart';
import '../../base/functions.dart';
import 'package:path/path.dart';

import '../notification/news_screen.dart';

class PopupNotifyUnread extends StatefulWidget {
  PopupNotifyUnread({super.key,required this.data});
  List<Notify> data;
  final PageController pageController = PageController(initialPage: 0);
  @override
  State<PopupNotifyUnread> createState() => _PopupNotifyUnreadState();
}

class _PopupNotifyUnreadState extends State<PopupNotifyUnread> {
  PageController _pageController = PageController(initialPage: 0);
  int _currentPage = 0;
  late int _totalPages;
  late String announcementId;
  @override
  Widget build(BuildContext context) {
    _totalPages = widget.data.length;
    return Dialog(
      child: Container(
        width: MediaQuery.of(context).size.width * 0.9,
        height: MediaQuery.of(context).size.height * 0.8,
        child: Stack(
          children: [
            Expanded(
              child: PageView(
                physics: NeverScrollableScrollPhysics(),
                controller: _pageController,
                onPageChanged: (int page) {
                  setState(() {
                    _currentPage = page;
                  });
                },
                children: widget.data.map((e) {
                  String url ="/FrontEnd/Announcement.aspx?AnnouncementId=${e.announcementId}&UserID=${e.userId}&IsDlg=1";
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
                    onLoadStart: (controller, url) {
                      print("url $url");
                      announcementId = url!.queryParameters['AnnouncementId']!;
                    },
                    shouldOverrideUrlLoading:
                        (controller, navigationAction) async {
                      String url = navigationAction.request.url.toString();
                      String? result=navigationAction.request.url?.queryParameters['result'];
                      if(result!=null &&result.isNotEmpty && result.contains("moveToDetail"))
                      {
                        Notify? item = await Constants.db.notifyDao.getNotifyWithAnnouncementId(announcementId);
                        if(item!=null)
                        {
                          Navigator.pop(context);
                          Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) => NewsScreen(notify: item)));
                        }
                        else
                        {
                          AlertDialogController.instance.showAlert(context, "FCD919", "Not found this detail item.", "Cancel", () { });
                        }
                        return NavigationActionPolicy.CANCEL;
                      }
                      else
                      {
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
                      }

                    },
                    initialUrlRequest: URLRequest(
                        url: Uri.parse('${Constants.baseURL}$url')),
                  );
                }).toList(),
              ),
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              mainAxisSize: MainAxisSize.max,
              children: [
                if (_currentPage > 0)
                  Align(alignment: Alignment.centerLeft, child: IconButton(
                    onPressed: () {
                      _pageController.previousPage(
                        duration: Duration(milliseconds: 300),
                        curve: Curves.ease,
                      );
                    },
                    icon: const Icon(Icons.navigate_before,color: Colors.black,),
                  )),
                Spacer(),
                if (_currentPage < _totalPages - 1)
                  Align(alignment: Alignment.centerRight,child: IconButton(
                    onPressed: () {
                      _pageController.nextPage(
                        duration: Duration(milliseconds: 300),
                        curve: Curves.ease,
                      );
                    },
                    icon: const Icon(Icons.navigate_next,color: Colors.black,),
                  ),)
              ],
            ),
          ],
        ),
      ),
    );
  }
}
