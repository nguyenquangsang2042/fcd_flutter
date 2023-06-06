class Status {
  late String status;
  late Mess mess;
  late String dateNow;

  Status();

  Status.fromJson(Map<String, dynamic> json) {
    status = json['status'];
    mess = (json['mess'] != null ? Mess.fromJson(json['mess']) : null)!;
    dateNow = json['dateNow'];
  }
}

class Mess {
  late String? key;
  late String? value;


  Mess();

  Mess.fromJson(Map<String, dynamic> json) {
    key = json['Key'];
    value = json['Value'];
  }
}
