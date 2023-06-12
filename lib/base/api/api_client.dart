import 'package:dio/dio.dart';
import 'package:fcd_flutter/base/constanst.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/department.dart';
import 'package:fcd_flutter/base/model/app/district.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:fcd_flutter/base/model/app/help_desk_category.dart';
import 'package:fcd_flutter/base/model/app/menu_app.dart';
import 'package:fcd_flutter/base/model/app/nation.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_category.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';
import 'package:retrofit/retrofit.dart';

import '../model/app/airport.dart';
import '../model/app/announcement_category.dart';
import '../model/app/app_language.dart';
import '../model/app/helpdesk_linhvuc.dart';
import '../model/app/pilot_schedule_all.dart';
import '../model/app/pilot_schedule_pdf.dart';
import '../model/app/province.dart';
import '../model/app/user.dart';
import '../model/app/ward.dart';
part 'api_client.g.dart';

@RestApi(baseUrl: "https://pilot.vuthao.com")
abstract class ApiClient {
  factory ApiClient(Dio dio) = _ApiClient;
  @POST('/API/User.ashx?func=getOtp')
  @FormUrlEncoded()
  Future<Status> getOtp(@Field("data") String data);

  @GET('/API/ApiPublic.ashx?func=get&bname=BeanSettings')
  Future<ApiList<Setting>> getSettings(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanUser')
  Future<ApiList<User>> getUsers(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanAirport')
  Future<ApiList<Airport>> getAirports(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanUserTicketStatus')
  Future<ApiList<UserTicketStatus>> getUserTicketStatuses(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanAppLanguage')
  Future<ApiList<AppLanguage>> getAppLanguages(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanUserTicketCategory')
  Future<ApiList<UserTicketCategory>> getUserTicketCategories(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanFAQs')
  Future<ApiList<FAQs>> getFAQs(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanHelpDeskCategory')
  Future<ApiList<HelpDeskCategory>> getHelpDeskCategories(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanPilotScheduleAll')
  Future<ApiList<PilotScheduleAll>> getPilotScheduleAll(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanHelpDeskLinhVuc')
  Future<ApiList<HelpDeskLinhVuc>> getHelpDeskLinhVucs(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanDepartment')
  Future<ApiList<Department>> getDepartments(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);

  @GET('/API/ApiPublic.ashx?func=get&bname=BeanPilotSchedulePdf')
  Future<ApiList<PilotSchedulePdf>> getPilotSchedulePdf(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanAnnouncementCategory')
  Future<ApiList<AnnouncementCategory>> getAnnouncementCategory(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanNation')
  Future<ApiList<Nation>> getNation(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanProvince')
  Future<ApiList<Province>> getProvince(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanDistrict')
  Future<ApiList<District>> getDistrict(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanWard')
  Future<ApiList<Ward>> getWard(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiHandler.ashx?func=GetMenuApplication&LanguageId=0')
  Future<ApiList<MenuApp>> getMenuApp(
      @Query("Modified") String modified, @Query("isFirst") String isFirst);

  @GET('/API/ApiHandler.ashx?func=GetInfoListMenuHomeScreen')
  Future<ApiList<MenuApp>> getMenuHome(@Query('data') String data);

  @GET('/API/ApiPublic.ashx?func=get&bname=BeanNotify')
  Future<ApiList<Notify>> getNotify(@Header('Cookie') String cookieValue,
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
}
