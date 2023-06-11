import 'package:crypto/crypto.dart';
// ignore: depend_on_referenced_packages
import 'package:convert/convert.dart';

class CryptoController {
  static final CryptoController _singleton = CryptoController._internal();

  static CryptoController get instance {
    return _singleton;
  }

  CryptoController._internal();

  String getMd5Hash(String input) {
    String retValue = "";
    final md5Hash = md5.convert(input.codeUnits);
    retValue = hex.encode(md5Hash.bytes);
    return retValue;
  }
}
