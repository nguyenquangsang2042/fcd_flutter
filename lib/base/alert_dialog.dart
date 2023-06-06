import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class AlertDialogController {
  static final AlertDialogController _singleton =
      AlertDialogController._internal();

  static AlertDialogController get instance {
    return _singleton;
  }

  AlertDialogController._internal();

  void showAlert(BuildContext context, String title, String content,
      String textCancel, VoidCallback? onPress) {
    Widget alertDialog = CupertinoAlertDialog(
      title: Text(title),
      content: Text(content),
      actions: [
        CupertinoDialogAction(
          onPressed: () {
            if (onPress == null) {
              Navigator.of(context).pop();
            } else {
              onPress();
            }
          },
          isDefaultAction: true,
          child: Text(textCancel),
        ),
      ],
    );

    // Show the alert dialog
    showDialog(
      context: context,
      builder: (BuildContext context) {
        if (Theme.of(context).platform == TargetPlatform.iOS) {
          return alertDialog;
        } else {
          return AlertDialog(
            title: Text(title),
            content: Text(content),
            actions: [
              TextButton(
                child: Text(textCancel),
                onPressed: () {
                  if (onPress == null) {
                    Navigator.of(context).pop();
                  } else {
                    onPress();
                  }
                },
              ),
            ],
          ); // Return the Material-style dialog for Android and others
        }
      },
    );
  }
}
