import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:intl/intl.dart';
import 'package:path/path.dart' as p;

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
}
