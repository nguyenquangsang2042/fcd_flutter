import 'package:firebase_messaging/firebase_messaging.dart';

class FirebaseMessagingService {
  final FirebaseMessaging _firebaseMessaging = FirebaseMessaging.instance;

  void initialize() async {
    _firebaseMessaging.requestPermission();
    await _firebaseMessaging.getToken();
    FirebaseMessaging.onMessage.listen((RemoteMessage message) {
      print('onMessage: $message');
      // Xử lý thông báo đẩy khi ứng dụng đang mở
    });
    FirebaseMessaging.onBackgroundMessage(_firebaseMessagingBackgroundHandler);
    FirebaseMessaging.onMessageOpenedApp.listen((RemoteMessage message) {
      print('onMessageOpenedApp: $message');
      // Xử lý thông báo đẩy khi ứng dụng được khởi động từ trạng thái tắt
    });
  }

  static Future<void> _firebaseMessagingBackgroundHandler(RemoteMessage message) async {
    print('onBackgroundMessage: $message');
    // Xử lý thông báo đẩy khi ứng dụng chạy trong nền
  }

}