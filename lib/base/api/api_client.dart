import 'package:dio/dio.dart';
import 'package:fcd_flutter/base/constants.dart';
import 'package:fcd_flutter/base/database/dao/menu_home_dao.dart';
import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/bean_faqs.dart';
import 'package:fcd_flutter/base/model/app/bean_library.dart';
import 'package:fcd_flutter/base/model/app/bean_banner.dart';
import 'package:fcd_flutter/base/model/app/bean_salary.dart';
import 'package:fcd_flutter/base/model/app/department.dart';
import 'package:fcd_flutter/base/model/app/district.dart';
import 'package:fcd_flutter/base/model/app/faqs.dart';
import 'package:fcd_flutter/base/model/app/help_desk_category.dart';
import 'package:fcd_flutter/base/model/app/helpdesk.dart';
import 'package:fcd_flutter/base/model/app/licence.dart';
import 'package:fcd_flutter/base/model/app/menu_app.dart';
import 'package:fcd_flutter/base/model/app/menu_home.dart';
import 'package:fcd_flutter/base/model/app/nation.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/model/app/pilot_schedule.dart';
import 'package:fcd_flutter/base/model/app/student.dart';
import 'package:fcd_flutter/base/model/app/survey.dart';
import 'package:fcd_flutter/base/model/app/survey_category.dart';
import 'package:fcd_flutter/base/model/app/survey_list_pilot.dart';
import 'package:fcd_flutter/base/model/app/survey_table.dart';
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

@RestApi(baseUrl: "https://pilotuat.vuthao.com")
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

  @GET('/API/User.ashx?func=GetListUserLicense')
  Future<ApiList<License>> getUserLicense(
      @Header('Cookie') String cookieValue, @Query("uid") String uid);

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
  Future<ApiList<MenuHome>> getMenuHome(@Query('data') String data);

  @GET('/API/ApiLibrary.ashx?func=GetBanner')
  Future<ApiList<BeanBanner>> getBanner(@Header('Cookie') String cookieValue);

  @GET('/API/ApiPublic.ashx?func=get&bname=BeanNotify')
  Future<ApiList<Notify>> getNotify(@Header('Cookie') String cookieValue,
      @Query("Modified") String modified, @Query("isFirst") String isFirst);
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanPilotSchedule')
  Future<ApiList<PilotSchedule>> getPilotSchedule(@Header('Cookie') String cookieValue,
      @Query("Modified") String modified, @Query("isFirst") String isFirst);

  @GET('/API/ApiPublic.ashx?func=get&bname=BeanStudent')
  Future<ApiList<Student>> getStudent(@Header('Cookie') String cookieValue,
      @Query("Modified") String modified, @Query("isFirst") String isFirst);

  @GET('/API/ApiPublic.ashx?func=get&bname=BeanSurveyTable')
  Future<ApiList<SurveyTable>> getSurveyTable(@Header('Cookie') String cookieValue,
      @Query("Modified") String modified, @Query("isFirst") String isFirst);

  @GET('/API/ApiPublic.ashx?func=get&bname=BeanSurvey')
  Future<ApiList<Survey>> getSurvey(@Header('Cookie') String cookieValue,
      @Query("Modified") String modified, @Query("isFirst") String isFirst);

  @GET('/api/ApiSurvey.ashx?func=GetAllSurveyCategory')
  Future<ApiList<SurveyCategory>> getSurveyCategory(@Header('Cookie') String cookieValue);


  @GET('/API/ApiPublic.ashx?func=get&bname=BeanHelpdesk')
  Future<ApiList<Helpdesk>> getHelpdesk(@Header('Cookie') String cookieValue,
      @Query("Modified") String modified, @Query("isFirst") String isFirst);

  @POST('/API/User.ashx?func=mobileAutoLoginWeb')
  Future<ApiObject<String?>> mobileAutoLoginWeb(
      @Header('Cookie') String cookieValue, @Field('uid') String uid);

  @GET('/API/ApiLibrary.ashx?func=GetFolderItem')
  Future<ApiList<BeanLibrary>> getLibraryByID(
      @Header('Cookie') String cookieValue, @Query('fid') int fid);
  
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanSalary')
  Future<ApiList<BeanSalary>> getSalaryBetweenTwoDate(
      @Header('Cookie') String cookieValue,
      @Query('FromDate') String fromDate,
      @Query('ToDate') String toDate
      );
  @GET('/API/User.ashx?func=getMyUserInfo')
  Future<ApiObject<User>> getMyUserInfo( @Header('Cookie') String cookieValue);

  @POST('/API/HelpDesk.ashx?func=add')
  @FormUrlEncoded()
  Future<Status> sendQuestionToHelpDesk( @Header('Cookie') String cookieValue,@Field("data") String data);
  
  @GET('/API/ApiPublic.ashx?func=get&bname=BeanSurveyListPilot&obj=false')
  Future<ApiList<SurveyListPilot>> getSurveyListPilot(
      @Header('Cookie') String cookieValue,
      @Query('rid') String rid );
}
