import 'package:flutter/material.dart';
import 'package:flutter_html/flutter_html.dart';
import 'package:url_launcher/url_launcher.dart';

class HtmlContentWidget extends StatelessWidget {
  final String htmlContent;

  HtmlContentWidget({required this.htmlContent});

  @override
  Widget build(BuildContext context) {
    return Html(
      data: htmlContent,
      onLinkTap: (url, attributes, element) async {
        if (await canLaunchUrl(Uri.parse(url!))) {
          await launch(url!);
        }
      },
      style: {
        "a": Style(color: Colors.blue.shade800), // Set the color of links here
      },
    );
  }
}
