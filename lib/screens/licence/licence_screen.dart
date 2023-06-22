import 'package:flutter/material.dart';

import '../../base/constanst.dart';

class LicenceScreen extends StatelessWidget {
  const LicenceScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
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
              Navigator.pop(context);
            },
          ),
        ),
        title: const Text(
          'Licence',
          style: TextStyle(color: Colors.white, fontSize: 18),
        ),
        backgroundColor: const Color(0xFF006784),
        centerTitle: true,
      ),
      body: FutureBuilder(
        future: Constanst.apiController.updateLicence(),
        builder: (context, snapshot) {
          if(snapshot.connectionState== ConnectionState.done)
            {
              return StreamBuilder(
                  stream: Constanst.db.licenceDao.findAll(),
                  builder: (context, snapshot) {
                    if(snapshot.hasData)
                      {
                        return Column(children: snapshot.data!.map((e) => Text("data")).toList(),);
                      }
                    else
                      {
                        return Container();
                      }
                  },);
            }
          else
            {
              return Container(child: Center(child: CircularProgressIndicator(),));
            }
        },
      ),
    );
  }
}
