import 'package:dio/dio.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';
import 'package:retrofit/retrofit.dart';

import '../model/app/airport.dart';
import '../model/app/user.dart';
part 'api_client.g.dart';
@RestApi(baseUrl: "https://pilot.vuthao.com")
abstract class ApiClient{
  factory ApiClient(Dio dio) = _ApiClient;
  @POST('/API/User.ashx?func=getOtp')
  @FormUrlEncoded()
  Future<Status> getOtp(@Field("data") String data);

  @GET('/API/ApiPublic.ashx?func=get&bname=BeanSettings')
  Future<ApiList<Setting>> getSettings(@Query("Modified") String modified,@Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanUser')
  Future<ApiList<User>> getUsers(@Query("Modified") String modified,@Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanAirport')
  Future<ApiList<Airport>> getAirports(@Query("Modified") String modified,@Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanUserTicketStatus')
  Future<ApiList<UserTicketStatus>> getUserTicketStatuses(@Query("Modified") String modified,@Query("isFirst") String isFirst);
}