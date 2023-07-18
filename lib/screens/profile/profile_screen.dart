import 'package:auto_size_text/auto_size_text.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/nation.dart';
import 'package:fcd_flutter/base/widgets/image_selection_screen.dart';
import 'package:fcd_flutter/blocs/login/login_cubit.dart';
import 'package:fcd_flutter/blocs/navigation/navigation_cubit.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:qr_flutter/qr_flutter.dart';

class ProfileScreen extends StatefulWidget {
  ProfileScreen({Key? key}) : super(key: key);
  ValueNotifier<bool> enableEdit = ValueNotifier(true);

  @override
  State<ProfileScreen> createState() => _ProfileScreenState();
}

class _ProfileScreenState extends State<ProfileScreen> {
  TextEditingController mobileController =
  TextEditingController(text: Constants.currentUser.mobile);
  TextEditingController emailVNaController =
  TextEditingController(text: Constants.currentUser.email);
  TextEditingController emailUserController =
  TextEditingController(text: Constants.currentUser.email2);
  TextEditingController birthdayController = TextEditingController(
      text: Functions.instance
          .formatDateString(Constants.currentUser.birthday!, "dd/MM/yyyy"));
  TextEditingController nationalController =
  TextEditingController(text: Constants.currentUser.nationality);
  TextEditingController sexController = TextEditingController(
      text: Constants.currentUser.gender ? "Male" : "Female");
  TextEditingController crewCodeController =
  TextEditingController(text: Constants.currentUser.code2);
  TextEditingController myIDTravelAccountController =
  TextEditingController(text: Constants.currentUser.code3);
  TextEditingController hrCodeController =
  TextEditingController(text: Constants.currentUser.code);
  TextEditingController departmentFleetController =
  TextEditingController(text: Constants.currentUser.departmentName);
  TextEditingController rankController =
  TextEditingController(text: Constants.currentUser.positionName);
  TextEditingController specialContentController =
  TextEditingController(text: Constants.currentUser.specialContent);
  TextEditingController streetController =
  TextEditingController(text: Constants.currentUser.address);
  TextEditingController nationTitleController =
  TextEditingController(text: "");
  ValueNotifier<Nation?> nationSelected = ValueNotifier(null);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
      body: SingleChildScrollView(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            buildAvartarQrCodeLogout(),
            name(),
            Container(
              margin: const EdgeInsets.all(10),
              child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    //buildGeneral(),
                    buildAdress(context)
                  ]),
            )
          ],
        ),
      ),
    );
  }

  Column buildAdress(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          "Address".toUpperCase(),
          style: const TextStyle(
              color: Color(0xFF295989),
              fontWeight: FontWeight.bold,
              fontSize: 16),
        ),
        buildNation(context),
        buildElementEdit("Street".toUpperCase(), streetController,
            TextInputType.text, [], widget.enableEdit.value),
      ],
    );
  }

  Column buildNation(BuildContext context) {
    return Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
      Text(
        "Nation".toUpperCase(),
        style: const TextStyle(
            color: Color(0xFF006784),
            fontWeight: FontWeight.bold,
            fontSize: 14),
      ),
      SizedBox(
        height: 40,
        child: InkWell(
          child: ValueListenableBuilder(
            valueListenable: nationSelected, builder: (context, value, child) {
            if (value != null) {
              nationTitleController.text = value!.title;
            }
            return TextField(
              controller: nationTitleController,
              decoration: InputDecoration(
                labelText: value == null ? 'Please choose your nation' : "",
                suffixIcon:
                Icon(Icons.expand_more_sharp), // Add your desired icon here
              ),
              enabled: false,
              style: TextStyle(color: Colors.black),
            );
          },),
          onTap: () {
            _showPopupChoiseNation(context);
          },
        ),
      )
    ]);
  }

  _showPopupChoiseNation(BuildContext context) {
    ValueNotifier<String> keySearchNation = ValueNotifier("");
    TextEditingController nationSearchController = TextEditingController(
        text: "");
    showDialog(
      context: context,
      builder: (context) {
        return Dialog(
          backgroundColor: Colors.white,
          child: Container(
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(12.0),
              color: Colors.white, // Replace with your desired background color
            ),
            padding: const EdgeInsets.all(16),
            child: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                TextField(
                  controller: nationSearchController,
                  decoration: const InputDecoration(
                    hintText: 'Search...',
                  ),
                  onChanged: (value) {
                    keySearchNation.value = value;
                  },
                ),
                const SizedBox(height: 16.0),
                Expanded(
                  child: FutureBuilder(
                    future: Constants.db.nationDao.getAllNation(),
                    builder: (context, snapshot) {
                      if (snapshot.connectionState == ConnectionState.done) {
                        return ValueListenableBuilder(
                          valueListenable: keySearchNation, builder: (context,
                            value, child) {
                          List<Nation> data = snapshot.data!;

                          if (value.isNotEmpty) {
                            data = data.where((element) =>
                                Functions.instance.removeDiacritics(
                                    element.title)
                                    .toLowerCase()
                                    .contains(Functions.instance.removeDiacritics(value).toLowerCase())).toList();
                          }
                          return ListView.builder(
                          itemCount: data.length, // Replace with your actual list length
                          itemBuilder: (context, index) {
                          return ListTile(
                          title: Text(data[index].title),
                          onTap: () {
                          nationSelected.value=data[index];
                          Navigator.pop(context);

                          },
                          );
                          },
                          );
                        },);
                      }
                      else {
                        return Center(child: CircularProgressIndicator(),);
                      }
                    },
                  ),
                ),
              ],
            ),
          ),
        );
      },
    );
  }

  Column buildGeneral() {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const Text(
          "GENERAL",
          style: TextStyle(
              color: Color(0xFF295989),
              fontWeight: FontWeight.bold,
              fontSize: 16),
        ),
        buildElementEdit(
            "Mobile".toUpperCase(),
            mobileController,
            TextInputType.number,
            [FilteringTextInputFormatter.digitsOnly],
            widget.enableEdit.value),
        buildElementEdit("Email VNA".toUpperCase(), emailVNaController,
            TextInputType.emailAddress, [], widget.enableEdit.value),
        buildElementEdit("Email User".toUpperCase(), emailVNaController,
            TextInputType.emailAddress, [], widget.enableEdit.value),
        buildElementEdit("Birthday".toUpperCase(), birthdayController,
            TextInputType.datetime, [], widget.enableEdit.value),
        buildElementEdit("NATIONALITY".toUpperCase(), nationalController,
            TextInputType.text, [], widget.enableEdit.value),
        buildElementEdit("SEX".toUpperCase(), sexController, TextInputType.text,
            [], widget.enableEdit.value),
        buildElementEdit("CREW CODE".toUpperCase(), crewCodeController,
            TextInputType.text, [], widget.enableEdit.value),
        buildElementEdit(
            "my id travel Account".toUpperCase(),
            myIDTravelAccountController,
            TextInputType.text,
            [],
            widget.enableEdit.value),
        buildElementEdit("HR CODE".toUpperCase(), myIDTravelAccountController,
            TextInputType.text, [], widget.enableEdit.value),
        buildElementEdit(
            "DEPARTMENT/FLEET".toUpperCase(),
            departmentFleetController,
            TextInputType.text,
            [],
            widget.enableEdit.value),
        buildElementEdit("RANK".toUpperCase(), rankController,
            TextInputType.text, [], widget.enableEdit.value),
        buildElementEdit(
            "SPECIAL CONTENT".toUpperCase(),
            specialContentController,
            TextInputType.text,
            [],
            widget.enableEdit.value),
      ],
    );
  }

  Column buildElementEdit(String header,
      TextEditingController controller,
      TextInputType? keyboardType,
      List<TextInputFormatter>? inputFormatters,
      bool enableEdit) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        SizedBox(
          height: 10,
        ),
        Text(
          header,
          style: const TextStyle(
              color: Color(0xFF006784),
              fontWeight: FontWeight.bold,
              fontSize: 14),
        ),
        SizedBox(
          height: 40,
          child: TextField(
            inputFormatters: inputFormatters,
            keyboardType: keyboardType,
            enabled: enableEdit,
            controller: controller,
            style: TextStyle(color: Colors.black),
          ),
        )
      ],
    );
  }

  Row name() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        Text(
          "${Constants.currentUser.fullName}",
          style: TextStyle(
              fontSize: 18, fontWeight: FontWeight.bold, color: Colors.orange),
        ),
      ],
    );
  }

  Container buildAvartarQrCodeLogout() {
    return Container(
      margin: const EdgeInsets.all(10),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Expanded(child: const Opacity(
            opacity: 0,
            child: Text("Sign out"),
          ),flex: 1,),
          Expanded(child: Container(
            margin: EdgeInsets.only(right: 10),
            height: 120,
            width: 120,
            child: QrImageView(
              data: 'pilot:contact:${Constants.currentUser.email}',
              version: QrVersions.auto,
              size: 200.0,
            ),
          ),flex: 3,),
          Expanded(child: Container(child: SizedBox(
            height: 120,
            width: 120,
            child: ImageSelectionScreen(
              urlPath:
              '${Constants.baseURL}/Data/Users/${Constants.currentUser
                  .id}/avatar.jpg?ver=${Functions.instance
                  .formatDateToStringWithFormat(
                  DateTime.now(), 'yyyyMMddHHmmss')}',
              errPath: 'asset/images/icon_avatar64.png',
            ),
          ),padding: EdgeInsets.all(5)),flex: 3,),
          Expanded(child: InkWell(
            child: Container(child: Icon(Icons.logout), margin: EdgeInsets.only(top: 2),),
            onTap: () {
              Functions.instance.deleteAllDataAndGetMassterdata();
              context.read<LoginCubit>().emit(LoginMailState());
              context.read<NavigationCubit>().navigateToLoginView();
              Navigator.pop(context);
            },
          ),flex: 1,)
        ],
      ),
    );
  }

  AppBar buildAppBar(BuildContext context) {
    return AppBar(
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
            Navigator.pop(context);
          },
        ),
      ),
      title: const Text(
        "Profile",
        style: TextStyle(
            color: Colors.white, fontWeight: FontWeight.bold, fontSize: 18),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
    );
  }
}
