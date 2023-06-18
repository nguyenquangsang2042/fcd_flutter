import 'dart:io';

import 'package:dio/dio.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_overlay_loader/flutter_overlay_loader.dart';
import 'package:open_filex/open_filex.dart';
import 'package:path_provider/path_provider.dart';

class DownloadFile {
  DownloadFile._();

  static void showDownloadProgress( received, total) {
    if (total != -1) {
      print((received / total * 100).toStringAsFixed(0) + "%");
    }
  }

  static Future<String> downloadFile(
      BuildContext context, String url, String fileName) async {
    String dir = (await getApplicationSupportDirectory()).path;
    String filePath = '$dir/$fileName';
    File file = File(filePath);
    try {
      Loader.show(context,progressIndicator:CircularProgressIndicator(),);
      Dio dio = Dio();
      Response response = await dio.get(
        url,
        onReceiveProgress: showDownloadProgress,
        options: Options(
            responseType: ResponseType.bytes,
            followRedirects: false,
            validateStatus: (status) {
              return status! < 500;
            }),
      );
      var raf = file.openSync(mode: FileMode.write);
      // response.data is List<int> type
      raf.writeFromSync(response.data);
      await raf.close();
    } catch (e) {
      print(e);
    }

    if (await file.exists()) {
      if(Loader.isShown) Loader.hide();
      try {
        OpenResult result = await OpenFilex.open(file.path);
        if (result.message.contains("No APP found to open this file")) {
          return "Không có dụng tương thích để mở file";
        }
      } catch (e) {
        return "Không tải được file";
      }
    } else {
      return "Không tải được file";
    }
    return "";
  }
}