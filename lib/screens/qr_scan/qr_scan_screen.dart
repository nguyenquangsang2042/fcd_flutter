import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/screens/contacts/detail_contact_screen.dart';
import 'package:flutter/material.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';

class QRScannerScreen extends StatefulWidget {
  @override
  _QRScannerScreenState createState() => _QRScannerScreenState();
}

class _QRScannerScreenState extends State<QRScannerScreen> {
  final GlobalKey qrKey = GlobalKey(debugLabel: 'QR');
  QRViewController? controller;

  @override
  void dispose() {
    controller?.dispose();
    super.dispose();
  }

  void onQRViewCreated(QRViewController controller) {
    setState(() {
      this.controller = controller;
    });
    controller.scannedDataStream.listen((scanData) {
      controller.pauseCamera();//=> dừng quét
      if(scanData.code!=null)
      {
        RegExp emailRegex = RegExp(r':(\w+@\w+\.\w+)');
        String email = emailRegex.firstMatch(scanData.code!)?.group(1) ?? '';
        Constants.db.userDao.findUserByEmail("%${email}%").then((value) {
          if(value==null)
            {
              controller.resumeCamera();
            }
          else
            {
              Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) => DetailContactScreen(info: value))).then((value) => controller.resumeCamera());
            }
        });
      }
      else
        {
          controller.resumeCamera();
        }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Scan QR Code'),
      ),
      body: Column(
        children: [
          Expanded(
            child: QRView(
              key: qrKey,
              onQRViewCreated: onQRViewCreated,
            ),
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              ElevatedButton(
                onPressed: () {
                  controller?.flipCamera();
                },
                child: const Icon(Icons.flip_camera_ios),
              ),
              const SizedBox(width: 20),
              ElevatedButton(
                onPressed: () {
                  controller?.toggleFlash();
                },
                child: const Icon(Icons.flash_on),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
