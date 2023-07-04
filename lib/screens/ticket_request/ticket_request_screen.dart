import 'package:fcd_flutter/screens/faqs/helpdesk_screen.dart';
import 'package:flutter/material.dart';

import '../main/navigation_grid.dart';

class TicketRequestScreen extends StatelessWidget {
  const TicketRequestScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(context),
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
        "Request Ticket",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold,fontSize: 18),
      ),
      backgroundColor: const Color(0xFF006784),
      centerTitle: true,
      actions: [
        Container(
          margin: EdgeInsets.only(right: 10),
          height: 25,
          width: 25,
          child: InkResponse(child: Image.asset('asset/images/icon_helpdesk.png',color: Colors.white,),onTap: () {
            Navigator.push(
                context,
                MaterialPageRoute(
                    builder: (context) => NavigationGridScreen(childView: HelpdeskScreen(isShowAppBar: true,keySearch: "",))));
          },)
        )
      ],
    );
  }

}
