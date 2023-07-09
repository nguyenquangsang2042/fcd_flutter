import 'package:fcd_flutter/base/exports_base.dart';
import 'package:fcd_flutter/base/model/app/airport.dart';
import 'package:fcd_flutter/base/model/app/announcement_category.dart';
import 'package:fcd_flutter/base/model/app/app_language.dart';
import 'package:fcd_flutter/base/model/app/bean_banner.dart';
import 'package:fcd_flutter/base/model/app/bean_library.dart';
import 'package:fcd_flutter/base/model/app/bean_salary.dart';
import 'package:fcd_flutter/base/model/app/department.dart';
import 'package:fcd_flutter/base/model/app/district.dart';
import 'package:fcd_flutter/base/model/app/help_desk_category.dart';
import 'package:fcd_flutter/base/model/app/helpdesk.dart';
import 'package:fcd_flutter/base/model/app/helpdesk_linhvuc.dart';
import 'package:fcd_flutter/base/model/app/licence.dart';
import 'package:fcd_flutter/base/model/app/menu_app.dart';
import 'package:fcd_flutter/base/model/app/menu_home.dart';
import 'package:fcd_flutter/base/model/app/nation.dart';
import 'package:fcd_flutter/base/model/app/notify.dart';
import 'package:fcd_flutter/base/model/app/pilot_schedule_all.dart';
import 'package:fcd_flutter/base/model/app/pilot_schedule_pdf.dart';
import 'package:fcd_flutter/base/model/app/province.dart';
import 'package:fcd_flutter/base/model/app/student.dart';
import 'package:fcd_flutter/base/model/app/survey.dart';
import 'package:fcd_flutter/base/model/app/survey_category.dart';
import 'package:fcd_flutter/base/model/app/survey_table.dart';
import 'package:fcd_flutter/base/model/app/user.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_category.dart';
import 'package:fcd_flutter/base/model/app/user_ticket_status.dart';
import 'package:fcd_flutter/base/model/app/ward.dart';
import 'package:fcd_flutter/base/model/status.dart';

import 'app/faqs.dart';

class ApiList<T> extends Status {
  late List<T> data;

  ApiList();

  ApiList.fromJson(Map<String, dynamic> json) {
    if (json['data'] != null) {
      data = List<T>.empty(growable: true);
      json['data'].forEach((v) {
        data.add(redirectFormat(v) as T);
      });
    }
    status = json['status'];
    mess = (json['mess'] != null ? Mess.fromJson(json['mess']) : null)!;
    dateNow = json['dateNow'];
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
    } else if (T == District) {
      return District.fromJson(data);
    } else if (T == Ward) {
      return Ward.fromJson(data);
    } else if (T == Notify) {
      return Notify.fromJson(data);
    } else if (T == MenuApp) {
      return MenuApp.fromJson(data);
    } else if (T == MenuHome) {
      return MenuHome.fromJson(data);
    } else if (T == BeanBanner) {
      return BeanBanner.fromJson(data);
    }else if (T == License) {
      return License.fromJson(data);
    }else if (T == BeanLibrary) {
      return BeanLibrary.fromJson(data);
    }else if (T == BeanSalary) {
      return BeanSalary.fromJson(data);
    }else if (T == Helpdesk) {
      return Helpdesk.fromJson(data);
    }else if (T == Student) {
      return Student.fromJson(data);
    }else if (T == Student) {
      return Student.fromJson(data);
    }else if (T == SurveyTable) {
      return SurveyTable.fromJson(data);
    }else if (T == Survey) {
      return Survey.fromJson(data);
    } else if (T == SurveyCategory) {
      return SurveyCategory.fromJson(data);
    } else {
      return null;
    }
  }
}
