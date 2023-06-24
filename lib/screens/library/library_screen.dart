import 'dart:io';

import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/download_file.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/bean_library.dart';
import 'package:flutter/material.dart';
import 'package:flutter_overlay_loader/flutter_overlay_loader.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';
import 'package:open_filex/open_filex.dart';
import 'package:path_provider/path_provider.dart';

import '../../base/widgets/connectivity_widget.dart';

class LibraryScreen extends StatelessWidget {
  LibraryScreen({Key? key}) : super(key: key);
  ValueNotifier<List<String>> titleAppBar = ValueNotifier(["Library"]);
  ValueNotifier<List<int>> currentFolder = ValueNotifier([0]);

  @override
  Widget build(BuildContext context) {
    return WillPopScope(
        child: Scaffold(
          appBar: AppBar(
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
                  if (currentFolder.value.isNotEmpty &&titleAppBar.value.length>1) {
                    List<int> newListCurrentFolder = [];
                    newListCurrentFolder.addAll(currentFolder.value);
                    newListCurrentFolder.removeLast();
                    currentFolder.value = newListCurrentFolder;
                    List<String> newList = [];
                    newList.addAll(titleAppBar.value);
                    newList.removeLast();
                    titleAppBar.value = newList;
                  } else {
                    Navigator.pop(context);
                  }
                },
              ),
            ),
            title: ValueListenableBuilder(
              valueListenable: titleAppBar,
              builder: (context, value, child) {
                return Text(
                  value.last,
                  style: TextStyle(color: Colors.white, fontSize: 18),
                );
              },
            ),
            backgroundColor: const Color(0xFF006784),
            centerTitle: true,
          ),
          body: ConnectivityWidget(offlineWidget:buildOfflineMode() , onlineWidget: buildOnlineList(),),
        ),
        onWillPop: () => canBack(context));
  }

  MultiValueListenableBuilder buildOfflineMode() {
    return MultiValueListenableBuilder(
                valueListenables: [currentFolder],
                builder: (context, values, child) {
                  return FutureBuilder(
                    future:Constants.db.libraryDao.getLibraryByParentFolderCode(currentFolder.value.last),
                    builder: (context, snapshot) {
                      if (snapshot.connectionState == ConnectionState.done) {
                        if (snapshot.data != null && snapshot.data!.isNotEmpty) {
                          return Column(
                            children: [
                              Flexible(
                                  child: ListView.builder(
                                    itemCount: snapshot.data!.length,
                                    itemBuilder: (context, index) {
                                      BeanLibrary item = snapshot.data![index];
                                      return buildItemLibrary(item, context);
                                    },
                                  ))
                            ],
                          );
                        } else {
                          return Container(
                            child: Center(
                              child: Text("Không có dữ liệu"),
                            ),
                          );
                        }
                      } else {
                        return Container(
                          child: Center(child: CircularProgressIndicator(),),
                        );
                      }
                    },
                  );
                },
              );
  }

  MultiValueListenableBuilder buildOnlineList() {
    return MultiValueListenableBuilder(
                      valueListenables: [currentFolder],
                      builder: (context, values, child) {
                        return FutureBuilder(
                          future: Constants.api.getLibraryByID(
                              Constants.sharedPreferences.get('set-cookie').toString(),
                              currentFolder.value.last),
                          builder: (context, snapshot) {
                            if (snapshot.connectionState == ConnectionState.done) {
                              if (snapshot.data!.data.length>0) {
                                return Column(
                                  children: [
                                    Flexible(
                                        child: ListView.builder(
                                          itemCount: snapshot.data!.data.length,
                                          itemBuilder: (context, index) {
                                            BeanLibrary item = snapshot.data!.data[index];
                                            return buildItemLibrary(item, context);
                                          },
                                        ))
                                  ],
                                );
                              } else {
                                return Container(
                                  child: Center(
                                    child: Text("Không có dữ liệu"),
                                  ),
                                );
                              }
                            } else {
                              return Container(
                                child: Center(child: CircularProgressIndicator(),),
                              );
                            }
                          },
                        );
                      },
                    );
  }

  InkResponse buildItemLibrary(BeanLibrary item, BuildContext context) {
    return InkResponse(
                                            child: ListTile(
                                              leading: Functions.instance
                                                  .getFileIcon(item.fileType),
                                              title: Text(item.name),
                                              subtitle: item.items != 0
                                                  ? Text("${item.items} Items")
                                                  : const SizedBox(
                                                height: 0,
                                                width: 0,
                                              ),
                                            ),
                                            onTap: () async{
                                              item.parentFolderCode=currentFolder.value.last;
                                              if (Functions.instance.isSupportedFileType(
                                                  '${item.name}${item.fileType}')) {
                                                DownloadFile.downloadFile(
                                                    context,
                                                    '${Constants.baseURL}${item.path}',
                                                    '${item.name}${item.fileType}');
                                                String dir = (await getApplicationSupportDirectory()).path;
                                                String filePath = '$dir/${item.name}${item.fileType}';
                                                item.localPath=filePath;
                                              } else {
                                                List<int> newListCurrentFolder = [];
                                                newListCurrentFolder
                                                    .addAll(currentFolder.value);
                                                newListCurrentFolder.add(item.id);
                                                currentFolder.value = newListCurrentFolder;
                                                List<String> newList = [];
                                                newList.addAll(titleAppBar.value);
                                                newList.add(item.name);
                                                titleAppBar.value = newList;
                                              }
                                              Constants.db.libraryDao.insertLibrary(item);

                                            },
                                          );
  }

  Future<bool> canBack(context) async {
    if (currentFolder.value.isNotEmpty && titleAppBar.value.length>1) {
      List<int> newListCurrentFolder = [];
      newListCurrentFolder.addAll(currentFolder.value);
      newListCurrentFolder.removeLast();
      currentFolder.value = newListCurrentFolder;
      List<String> newList = [];
      newList.addAll(titleAppBar.value);
      newList.removeLast();
      titleAppBar.value = newList;
      return false;
    } else {
      return true;
    }
  }
}
