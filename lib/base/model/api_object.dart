import 'package:fcd_flutter/base/model/status.dart';

import 'app/airport.dart';
import 'app/announcement_category.dart';
import 'app/app_language.dart';
import 'app/department.dart';
import 'app/district.dart';
import 'app/faqs.dart';
import 'app/help_desk_category.dart';
import 'app/helpdesk_linhvuc.dart';
import 'app/nation.dart';
import 'app/pilot_schedule_all.dart';
import 'app/pilot_schedule_pdf.dart';
import 'app/province.dart';
import 'app/settings.dart';
import 'app/user.dart';
import 'app/user_ticket_category.dart';
import 'app/user_ticket_status.dart';
import 'app/ward.dart';

class ApiObject<T> extends Status {
  late T data;

  ApiObject();

  ApiObject.fromJson(Map<String, dynamic> json) {
    data = redirectFormat(json['data']) as T;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['data'] = this.data;
    return data;
  }
  Object? redirectFormat(Map<String, dynamic> data) {
    if (T == Setting) {
      return Setting.fromJson(data);
    } else if (T == User) {
      return User.fromJson(data);
    } else if (T == Airport) {
      return Airport.fromJson(data);
    } else if (T == UserTicketStatus) {
      return UserTicketStatus.fromJson(data);
    } else if (T == AppLanguage) {
      return AppLanguage.fromJson(data);
    } else if (T == UserTicketCategory) {
      return UserTicketCategory.fromJson(data);
    } else if (T == FAQs) {
      return FAQs.fromJson(data);
    } else if (T == HelpDeskCategory) {
      return HelpDeskCategory.fromJson(data);
    } else if (T == PilotScheduleAll) {
      return PilotScheduleAll.fromJson(data);
    } else if (T == HelpDeskLinhVuc) {
      return HelpDeskLinhVuc.fromJson(data);
    } else if (T == Department) {
      return Department.fromJson(data);
    } else if (T == AnnouncementCategory) {
      return AnnouncementCategory.fromJson(data);
    } else if (T == PilotSchedulePdf) {
      return PilotSchedulePdf.fromJson(data);
    } else if (T == Nation) {
      return Nation.fromJson(data);
    } else if (T == Province) {
      return Province.fromJson(data);
    }else if (T == District) {
      return District.fromJson(data);
    }else if (T == Ward) {
      return Ward.fromJson(data);
    } else {
      return null;
    }
  }
}
