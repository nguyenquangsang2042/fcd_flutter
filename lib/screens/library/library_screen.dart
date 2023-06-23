import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/download_file.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/bean_library.dart';
import 'package:flutter/material.dart';
import 'package:multi_value_listenable_builder/multi_value_listenable_builder.dart';

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
                  if (currentFolder.value.isNotEmpty) {
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
          body: MultiValueListenableBuilder(
            valueListenables: [currentFolder],
            builder: (context, values, child) {
              return FutureBuilder(
                future: Constanst.api.getLibraryByID(
                    Constanst.sharedPreferences.get('set-cookie').toString(),
                    currentFolder.value.last),
                builder: (context, snapshot) {
                  if (snapshot.connectionState == ConnectionState.done) {
                    if (snapshot.data!.data != null) {
                      return Column(
                        children: [
                          Flexible(
                              child: ListView.builder(
                            itemCount: snapshot.data!.data.length,
                            itemBuilder: (context, index) {
                              BeanLibrary item = snapshot.data!.data[index];
                              return InkResponse(
                                child: ListTile(
                                  leading: Functions.instance
                                      .getFileIcon(item.fileType),
                                  title: Text(item.name),
                                  subtitle: item.items != 0
                                      ? Text("${item.items} Items")
                                      : SizedBox(
                                          height: 0,
                                          width: 0,
                                        ),
                                ),
                                onTap: () {
                                  if (Functions.instance.isSupportedFileType(
                                      '${item.name}${item.fileType}')) {
                                    DownloadFile.downloadFile(
                                        context,
                                        '${Constanst.baseURL}${item.path}',
                                        '${item.name}${item.fileType}');
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
                                },
                              );
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
          ),
        ),
        onWillPop: () => canBack(context));
  }

  Future<bool> canBack(context) async {
    if (currentFolder.value.isNotEmpty) {
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
