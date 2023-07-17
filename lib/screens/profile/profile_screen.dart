import 'package:auto_size_text/auto_size_text.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/widgets/image_selection_screen.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
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
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          "Address".toUpperCase(),
                          style: const TextStyle(
                              color: Color(0xFF295989),
                              fontWeight: FontWeight.bold,
                              fontSize: 16),
                        ),
                        Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(
                                "Nation".toUpperCase(),
                                style: const TextStyle(
                                    color: Color(0xFF006784),
                                    fontWeight: FontWeight.bold,
                                    fontSize: 14),
                              ),
                              SizedBox(
                                height: 40,
                                child: Row(
                                  children: [
                                    TextField(
                                      enabled: false,
                                      style: TextStyle(color: Colors.black),
                                    ),
                                    Icon(Icons.expand_more_sharp)
                                  ],
                                ),
                              )
                            ]),
                        buildElementEdit(
                            "Street".toUpperCase(),
                            streetController,
                            TextInputType.text,
                            [],
                            widget.enableEdit.value),
                      ],
                    )
                  ]),
            )
          ],
        ),
      ),
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

  Column buildElementEdit(
      String header,
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
          const Opacity(
            opacity: 0,
            child: Text("Sign out"),
          ),
          Container(
            margin: EdgeInsets.only(right: 10),
            height: 120,
            width: 120,
            child: QrImageView(
              data: 'pilot:contact:${Constants.currentUser.email}',
              version: QrVersions.auto,
              size: 200.0,
            ),
          ),
          Container(
            height: 120,
            width: 120,
            color: Colors.black,
            child: ImageSelectionScreen(
              urlPath:
                  '${Constants.baseURL}/Data/Users/${Constants.currentUser.id}/avatar.jpg?ver=${Functions.instance.formatDateToStringWithFormat(DateTime.now(), 'yyyyMMddHHmmss')}',
              errPath: 'asset/images/icon_avatar64.png',
            ),
            margin: EdgeInsets.only(right: 10, left: 10),
          ),
          Flexible(
              child: InkWell(
            child: const AutoSizeText(
              "Log out",
              style: TextStyle(color: Color(0xFF006784)),
            ),
            onTap: () {},
          ))
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
