import 'package:dio/dio.dart';
import 'package:retrofit/retrofit.dart';

import '../model/status.dart';
part 'api_client.g.dart';
@RestApi(baseUrl: "https://pilot.vuthao.com")
abstract class ApiClient{
  factory ApiClient(Dio dio) = _ApiClient;
  @POST('/API/User.ashx?func=getOtp')
  @FormUrlEncoded()
  Future<Status> getOtp(@Field("data") String data);
}