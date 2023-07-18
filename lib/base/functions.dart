import 'dart:convert';
import 'dart:io';

import 'package:fcd_flutter/base/constants.dart';
import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:intl/intl.dart';
import 'package:path/path.dart' as p;
import 'package:html/parser.dart';

class Functions {
  static final Functions _singleton = Functions._internal();

  static Functions get instance {
    return _singleton;
  }

  Functions._internal();

  void hideKeyboard() {
    FocusManager.instance.primaryFocus?.unfocus();
  }

  Future<void> launchCustomUrl(_type, _url) async {
    final Uri smsLaunchUri = Uri(
      scheme: _type,
      path: _url,
    );
    if (!await launchUrl(smsLaunchUri)) {
      throw Exception('Could not launch $_url');
    }
  }
  Future<void> deleteAllDataAndGetMassterdata()async{
    Constants.sharedPreferences.remove('email');
    Constants.sharedPreferences.remove('pass');
    Constants.sharedPreferences.remove('set-cookie');
    Constants.db.airportDao.deleteAll();
    Constants.db.announcementCategoryDao.deleteAll();
    Constants.db.appLanguageDao.deleteAll();
    Constants.db.departmentDao.deleteAll();
    Constants.db.districtDao.deleteAll();
    Constants.db.faqDao.deleteAll();
    Constants.db.helpDeskCategoryDao.deleteAll();
    Constants.db.helpdeskDao.deleteAll();
    Constants.db.libraryDao.deleteAll();
    Constants.db.licenceDao.deleteAll();
    Constants.db.menuAppDao.deleteAll();
    Constants.db.nationDao.deleteAll();
    Constants.db.dbVariableDao.deleteAll();
    Constants.db.notifyDao.deleteAll();
    Constants.db.pilotScheduleAllDao.deleteAll();
    Constants.db.pilotSchedulePdfDao.deleteAll();
    Constants.db.provinceDao.deleteAll();
    Constants.db.settingDao.deleteAll();
    Constants.db.studentDao.deleteAll();
    Constants.db.surveyCategoryDao.deleteAll();
    Constants.db.surveyDao.deleteAll();
    Constants.db.surveyTableDao.deleteAll();
    Constants.db.userDao.deleteAll();
    Constants.db.userTicketCategoryDao.deleteAll();
    Constants.db.userTicketStatusDao.deleteAll();
    Constants.db.wardDao.deleteAll();
    Constants.apiController.updateMasterData();
  }
  Future<void> launchZalo(String phoneNumber) async {
    final urlScheme = 'zalo://chat?phone=$phoneNumber';
    final appStoreUrl = 'https://apps.apple.com/vn/app/zalo/id579523206';
    final googlePlayUrl = 'https://play.google.com/store/apps/details?id=com.zing.zalo';

    if (await canLaunch(urlScheme)) {
      await launch(urlScheme);
    } else {
      if (Platform.isIOS&& await canLaunch(appStoreUrl)) {
        await launch(appStoreUrl);
      } else if (await canLaunch(googlePlayUrl)) {
        await launch(googlePlayUrl);
      } else {
        throw 'Could not launch Zalo';
      }
    }
  }
  Future<void> launchVibe(String phoneNumber) async {
    final urlScheme = 'vibe://chat?phone=$phoneNumber';
    final appStoreUrl = Platform.isAndroid ? 'https://play.google.com/store/apps/details?id=com.viber.voip' : 'https://apps.apple.com/us/app/vibe-by-hitch-app/id1100814853';

    if (await canLaunch(urlScheme)) {
      await launch(urlScheme);
    } else {
      if (await canLaunch(appStoreUrl)) {
        await launch(appStoreUrl);
      } else {
        throw 'Could not launch Vibe';
      }
    }
  }
  DateTime stringToDate(String dateString, String? format) {
    if (format == null) {
      return DateTime.parse(dateString);
    }
    return DateFormat(format).parseStrict(dateString);
  }

  String formatDateToString(DateTime dateTime) {
    return DateFormat('yyyy-MM-dd HH:mm:ss').format(dateTime);
  }
  String formatDateToStringWithFormat(DateTime dateTime,String format) {
    return DateFormat(format).format(dateTime);
  }

  String formatDateString(String dateString, String formatReturn) {
    DateTime dateTime = DateTime.parse(dateString);
    String formattedDate = DateFormat(formatReturn).format(dateTime);
    return formattedDate;
  }

  Image getFileIcon(String fileType,int type) {
    if(type!=1)
      {
        switch (fileType.toLowerCase()) {
          case '.pdf':
            return Image.asset("asset/images/icon_pdf.png");
          case '.doc':
          case '.docx':
            return Image.asset("asset/images/icon_docx.png");
          case '.xls':
          case '.xlsx':
            return Image.asset("asset/images/icon_xlsx.png");
          case '.png':
          case '.jpg':
          case '.jpeg':
            return Image.asset("asset/images/icon_image.png");
          case '.mp3':
            return Image.asset("asset/images/icon_mp3.png");
          case '.mp4':
            return Image.asset("asset/images/icon_mp4.png");
          default:
            return Image.asset("asset/images/icon_file_blank.png");
        }
      }
    else
      {
        return Image.asset("asset/images/icon_folder.png");
      }
  }

  bool isWordDocument(String fileName) {
    return p.extension(fileName) == '.doc' || p.extension(fileName) == '.docx';
  }

  bool isExcelSpreadsheet(String fileName) {
    return p.extension(fileName) == '.xls' || p.extension(fileName) == '.xlsx';
  }

  bool isPowerPointPresentation(String fileName) {
    return p.extension(fileName) == '.ppt' || p.extension(fileName) == '.pptx';
  }

  bool isPDF(String fileName) {
    return p.extension(fileName) == '.pdf';
  }

  bool isSupportedFileType(String fileName) {
    final extension = p.extension(fileName);
    return extension == '.doc' ||
        extension == '.docx' ||
        extension == '.xls' ||
        extension == '.xlsx' ||
        extension == '.ppt' ||
        extension == '.pptx' ||
        extension == '.pdf' ||
        extension == '.png' ||
        extension == '.jpg' ||
        extension == '.jpeg' ||
        extension == '.mp3' ||
        extension == '.mp4' ||
        extension == '.gif';
  }

  String removeDiacritics(String str) {
    var withDia =
        'ÀÁÂÃÄÅàáâãäåÒÓÔÕÕÖØòóôõöøÈÉÊËèéêëðÇçÐÌÍÎÏìíîïÙÚÛÜùúûüÑñŠšŸÿýŽž';
    var withoutDia =
        'AAAAAAaaaaaaOOOOOOOooooooEEEEeeeeeCcDIIIIiiiiUUUUuuuuNnSsYyyZz';

    for (int i = 0; i < withDia.length; i++) {
      str = str.replaceAll(withDia[i], withoutDia[i]);
    }

    return str;
  }

  bool isUnicode(String input) {
    var asciiBytesCount = utf8.encode(input).where((byte) => byte < 128).length;
    var unicodeBytesCount = utf8.encode(input).length;
    return asciiBytesCount != unicodeBytesCount;
  }

  String getFileNameFromURL(String url)
  {
    return p.basename(url);
  }

  String htmlToPlainText(String htmlString) {
    String plainText = parse(htmlString).documentElement!.text;
    return plainText;
  }
}
