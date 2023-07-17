import 'dart:io';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/widgets/image_with_cookie.dart';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';

class ImageSelectionScreen extends StatefulWidget {
  ImageSelectionScreen({required this.urlPath,required this.errPath});
  String urlPath;
  String errPath;
  @override
  _ImageSelectionScreenState createState() => _ImageSelectionScreenState();
}

class _ImageSelectionScreenState extends State<ImageSelectionScreen> {
  final ImagePicker _picker = ImagePicker();
  File? _imageFile;

  Future<void> _pickImage(ImageSource source) async {
    final pickedFile = await _picker.pickImage(source: source);
    if (pickedFile != null) {
      setState(() {
        _imageFile = File(pickedFile.path);
      });
    }
  }

  void _showSelectionDialog() {
    showModalBottomSheet(
      context: context,
      builder: (BuildContext context) {
        return SafeArea(
          child: Wrap(
            children: <Widget>[
              ListTile(
                leading: Icon(Icons.photo_library),
                title: Text('Gallery'),
                onTap: () {
                  Navigator.of(context).pop();
                  _pickImage(ImageSource.gallery);
                },
              ),
              ListTile(
                leading: Icon(Icons.photo_camera),
                title: Text('Camera'),
                onTap: () {
                  Navigator.of(context).pop();
                  _pickImage(ImageSource.camera);
                },
              ),
            ],
          ),
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Material(child: InkResponse(onTap: _showSelectionDialog,child:  _imageFile != null
        ? Image.file(
      _imageFile!,
      fit: BoxFit.cover,
    )
        : ImageWithCookie(
      imageUrl:
      '${widget.urlPath}',
      errImage: widget.errPath,
    ),),);
  }
}
