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
  ValueNotifier<bool> isAlphaBetSort = ValueNotifier(true);
  ValueNotifier<bool> isShowSearch = ValueNotifier(false);
  ValueNotifier<String> keySearch= ValueNotifier("");
  TextEditingController textEditingController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return WillPopScope(
        child: Scaffold(
            appBar: buildAppBar(context),
            body: Column(
              children: [
                ValueListenableBuilder(
                  valueListenable: isShowSearch,
                  builder: (context, value, child) {
                    return Visibility(
                      visible: value,
                      child: buildTextSearch(),
                    );
                  },
                ),
                ValueListenableBuilder(valueListenable: keySearch, builder: (context, value, child) {
                  return Flexible(
                    child: ConnectivityWidget(
                      offlineWidget: buildOfflineMode(),
                      onlineWidget: buildOnlineList(),
                    ),
                  );
                },)
              ],
            )),
        onWillPop: () => canBack(context));
  }

  AppBar buildAppBar(BuildContext context) {
    return AppBar(
      actions: [
        ValueListenableBuilder(
          valueListenable: isAlphaBetSort,
          builder: (context, value, child) {
            return InkResponse(
              child: Container(
                height: 20,
                margin: EdgeInsets.only(right: 10),
                child: value
                    ? SizedBox(
                        height: 20,
                        width: 20,
                        child: Image.asset('asset/images/ic_sortaz.png'),
                      )
                    : SizedBox(
                        height: 20,
                        width: 20,
                        child: Image.asset('asset/images/ic_sortza.png'),
                      ),
              ),
              onTap: () {
                isAlphaBetSort.value = !value;
              },
            );
          },
        ),
        InkResponse(
          child: Container(
            margin: EdgeInsets.only(right: 10),
            child: Icon(
              Icons.search,
              color: Colors.white,
            ),
          ),
          onTap: () {
            isShowSearch.value = !isShowSearch.value;
          },
        )
      ],
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
            if (currentFolder.value.isNotEmpty &&
                titleAppBar.value.length > 1) {
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
    );
  }

  Widget buildTextSearch() {
    textEditingController = TextEditingController(text: keySearch.value);
    return Container(
      padding: EdgeInsets.all(5.0),
      color: Colors.grey.shade400,
      child: TextField(
        controller: textEditingController,
        onChanged: (value) {
          keySearch.value=value;
        },
        decoration: InputDecoration(
          filled: true,
          fillColor: Colors.white,
          contentPadding: EdgeInsets.symmetric(horizontal: 10.0),
          hintText: "Search",
          prefixIcon: Icon(Icons.search),
          suffixIcon: IconButton(
            icon: Icon(Icons.cancel),
            onPressed: () {
              textEditingController.text="";
              keySearch.value="";
            }, // Replace with delete functionality
          ),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(6.0),
            borderSide: BorderSide.none,
          ),
        ),
      ),
    );
  }

  MultiValueListenableBuilder buildOfflineMode() {
    return MultiValueListenableBuilder(
      valueListenables: [currentFolder],
      builder: (context, values, child) {
        return FutureBuilder(
          future: Constants.db.libraryDao
              .getLibraryByParentFolderCode(currentFolder.value.last),
          builder: (context, snapshot) {
            if (snapshot.connectionState == ConnectionState.done) {
              if (snapshot.data != null && snapshot.data!.isNotEmpty) {
                List<BeanLibrary> temp = snapshot.data!;
                if(keySearch.value.isNotEmpty)
                {
                  temp= filterData(snapshot.data!, keySearch.value);
                }
                if (!isAlphaBetSort.value) {
                  temp.sort((a, b) => b.name.compareTo(a.name));
                }
                return Column(
                  children: [
                    Flexible(
                        child: ListView.builder(
                      itemCount: temp.length,
                      itemBuilder: (context, index) {
                        BeanLibrary item = temp[index];
                        return InkResponse(
                          child: Dismissible(
                            key: Key(item.id.toString()),
                            onDismissed: (direction) {
                              Constants.db.libraryDao
                                  .deleteLibraryByID(item.id);
                            },
                            background: Container(
                              color: Colors.red,
                              child: Icon(
                                Icons.delete,
                                color: Colors.white,
                              ),
                              alignment: Alignment.centerRight,
                            ),
                            child: ListTile(
                              leading:
                                  Functions.instance.getFileIcon(item.fileType),
                              title: Text(item.name),
                              subtitle: item.items != 0
                                  ? Text("${item.items} Items")
                                  : const SizedBox(
                                      height: 0,
                                      width: 0,
                                    ),
                            ),
                          ),
                          onTap: () async {
                            item.parentFolderCode = currentFolder.value.last;
                            if (Functions.instance.isSupportedFileType(
                                '${item.name}${item.fileType}')) {
                              DownloadFile.downloadFile(
                                  context,
                                  '${Constants.baseURL}${item.path}',
                                  '${item.name}${item.fileType}');
                              String dir =
                                  (await getApplicationSupportDirectory()).path;
                              String filePath =
                                  '$dir/${item.name}${item.fileType}';
                              item.localPath = filePath;
                            } else {
                              List<int> newListCurrentFolder = [];
                              newListCurrentFolder.addAll(currentFolder.value);
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
                child: Center(
                  child: CircularProgressIndicator(),
                ),
              );
            }
          },
        );
      },
    );
  }

  MultiValueListenableBuilder buildOnlineList() {
    return MultiValueListenableBuilder(
      valueListenables: [currentFolder, isAlphaBetSort],
      builder: (context, values, child) {
        return FutureBuilder(
          future: Constants.api.getLibraryByID(
              Constants.sharedPreferences.get('set-cookie').toString(),
              currentFolder.value.last),
          builder: (context, snapshot) {
            if (snapshot.connectionState == ConnectionState.done ) {
              if (snapshot.data!.data.isNotEmpty) {
                List<BeanLibrary> temp = snapshot.data!.data;
                if(keySearch.value.isNotEmpty)
                {
                  temp= filterData(snapshot.data!.data, keySearch.value);
                }
                if (!isAlphaBetSort.value) {
                  temp.sort((a, b) => b.name.compareTo(a.name));
                }
                return temp.length>0?Column(
                  children: [
                    Flexible(
                        child: ListView.builder(
                          itemCount: temp.length,
                          itemBuilder: (context, index) {
                            BeanLibrary item = temp[index];
                            return InkResponse(
                              child: ListTile(
                                leading:
                                Functions.instance.getFileIcon(item.fileType),
                                title: Text(item.name),
                                subtitle: item.items != 0
                                    ? Text("${item.items} Items")
                                    : const SizedBox(
                                  height: 0,
                                  width: 0,
                                ),
                              ),
                              onTap: () async {
                                item.parentFolderCode = currentFolder.value.last;
                                if (Functions.instance.isSupportedFileType(
                                    '${item.name}${item.fileType}')) {
                                  DownloadFile.downloadFile(
                                      context,
                                      '${Constants.baseURL}${item.path}',
                                      '${item.name}${item.fileType}');
                                  String dir =
                                      (await getApplicationSupportDirectory()).path;
                                  String filePath =
                                      '$dir/${item.name}${item.fileType}';
                                  item.localPath = filePath;
                                } else {
                                  List<int> newListCurrentFolder = [];
                                  newListCurrentFolder.addAll(currentFolder.value);
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
                          },
                        ))
                  ],
                ):Container(
                  child: Center(
                    child: Text("Không có dữ liệu"),
                  ),
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
                child: Center(
                  child: CircularProgressIndicator(),
                ),
              );
            }
          },
        );
      },
    );
  }

  Future<bool> canBack(context) async {
    if (currentFolder.value.isNotEmpty && titleAppBar.value.length > 1) {
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
List<BeanLibrary> filterData(List<BeanLibrary> dataList, String searchText) {
  if (searchText.isEmpty) {
    return dataList; // return the original list if the search string is empty
  }
  List<BeanLibrary> filteredList = dataList.where((item) =>
      item.name.toLowerCase().contains(Functions.instance.removeDiacritics(searchText.toLowerCase()))).toList();
  return filteredList;
}