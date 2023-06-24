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
    if(format==null) {
      return DateTime.parse(dateString);
    }
    return DateFormat(format).parseStrict(dateString);
  }
  String formatDateToString(DateTime dateTime) {
    return DateFormat('yyyy-MM-dd HH:mm:ss').format(dateTime);
  }
  String formatDateString(String dateString,String formatReturn) {
    DateTime dateTime = DateTime.parse(dateString);
    String formattedDate = DateFormat(formatReturn).format(dateTime);
    return formattedDate;
  }
  Icon getFileIcon(String fileType) {
    switch (fileType.toLowerCase()) {
      case '.pdf':
        return Icon(Icons.picture_as_pdf);
      case '.doc':
      case '.docx':
        return Icon(Icons.description);
      case '.xls':
      case '.xlsx':
        return Icon(Icons.grid_on);
      case '.png':
      case '.jpg':
      case '.jpeg':
        return Icon(Icons.image);
      default:
        return Icon(Icons.folder);
    }
  }
  bool isWordDocument(String fileName) {
    return p.extension(fileName) == '.doc' ||
        p.extension(fileName) == '.docx';
  }

  bool isExcelSpreadsheet(String fileName) {
    return p.extension(fileName) == '.xls' ||
        p.extension(fileName) == '.xlsx';
  }

  bool isPowerPointPresentation(String fileName) {
    return p.extension(fileName) == '.ppt' ||
        p.extension(fileName) == '.pptx';
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
        extension == '.gif';
  }
  String removeDiacritics(String str) {

    var withDia = 'ÀÁÂÃÄÅàáâãäåÒÓÔÕÕÖØòóôõöøÈÉÊËèéêëðÇçÐÌÍÎÏìíîïÙÚÛÜùúûüÑñŠšŸÿýŽž';
    var withoutDia = 'AAAAAAaaaaaaOOOOOOOooooooEEEEeeeeeCcDIIIIiiiiUUUUuuuuNnSsYyyZz';

    for (int i = 0; i < withDia.length; i++) {
      str = str.replaceAll(withDia[i], withoutDia[i]);
    }

    return str;

  }

}
