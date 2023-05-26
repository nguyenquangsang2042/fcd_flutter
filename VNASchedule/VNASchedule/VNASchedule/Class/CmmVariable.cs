using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace VNASchedule.Class
{

    public class CmmVariable
    {
        //public static string M_Domain = "https://pilot.vuthao.com";// site dev new 
        public static string M_Domain = "https://pilotuat.vuthao.com";// site uat new 
        //public static string M_Domain = "https://pilot919dev.vuthao.com";// site dev new 
        //public static string M_Domain = "https://pilot919.com";// site khách hàng
        public static string M_DataPath = "DB_sqlite_XamDocument.db";   // đường dẫn file DB trên thiết bị
        public static string M_DataLangPath = "DB_Lang.db";             // đường dẫn file DB Chứa langue
        public static string M_Avatar = "avatar.jpg";
        public static string M_AvatarCus = "avatarCus.jpg";             // đường dẫn Avatar trên thiết bị 
        public static string M_Folder_Avatar = "Avatars";
        public static string M_Folder_Advertise = "Advertise";          // Folder lưu avatar file trên thiết bị
        public static string M_settingFileName = "config.ini";          // đường dẫn file setting trên thiết bị
        public static string M_DataFolder = "data";                     // đường dẫn lưu file trên thiết bị
        public static string M_Folder_imgServiceCategory = "data/images/servicesCate";      // đường dẫn lưu ảnh Service Category trên thiết bị
        public static string M_Folder_imgService = "data/images/services";      // đường dẫn lưu ảnh service trên thiết bị
        public static string EvaluateUrl = "/SurveyEvaluate/Assessment.aspx";   // đường dẫn bài kiểm tra
        public static string EvaluateResultUrl = "/SurveyEvaluate/Result.aspx"; // đường dẫn kết quả bài kiểm tra
        public readonly static bool M_flgSysDebug = true;
        public static Dictionary<string, string> M_LangData = null;     //Dictionary
        public static ConfigVariable SysConfig = null;                  // Dữ liệu lưu dữ lại ghi xuống file như: site, subsite
        public static HttpClient M_AuthenticatedHttpClient = null;      // httpClient sử dụng chung khi kết nối thành công server
        public static CookieContainer M_AuthenticatedCookie = null;     // httpClient sử dụng chung khi kết nối thành công server
        public static short M_AutoReLoginNum = 0;                       // Số Auto login bị lỗi liên tiếp
        public static short M_AutoReLoginNumMax = 5;                    // Số tối đa được phép Auto login lỗi liên tiếp, nếu > Max sẽ yêu cầu User đăng nhập lại

        public static List<int> M_LstMyGroupId;
        public static string M_ApiPath = "/API";
        public static string M_ApiLogin = "/API/User.ashx";

        //Danh sách Danh mục lấy all data khi login lần đầu
        public static string[] M_MasterCategorys = new string[] { "BeanItemDeleted","BeanSettings","BeanMenuApp", "BeanAirport", "BeanUser"
                                                                , "BeanWard", "BeanNation", "BeanProvince", "BeanDistrict"
                                                                , "BeanRelationshipType", "BeanUserTicketStatus", "BeanUserTicketCategory", "BeanFAQs" 
                                                                , "BeanHelpDeskCategory", "BeanDepartment", "BeanPilotSchedulePdf", "BeanAnnouncementCategory"};

        public static string M_WorkDateFormatDayVN = "dd/MM/yyyy";
        public static string M_WorkDateFormatTimeVN = "HH:mm";
        public static string M_WorkDateFormatVN = M_WorkDateFormatDayVN + " " + M_WorkDateFormatTimeVN;
        public static string M_Default_Helpdesk_Mobile = "0903136969";

        public static string M_WorkDateFormatDay = "MM/dd/yyyy";
        public static string M_WorkDateFormatTime = "HH:mm";
        public static string M_LogPath = "logPilot.txt";
        public static int M_DiffHours = 0;//-14;
        public static string M_WorkDateFormat = M_WorkDateFormatDay + " " + M_WorkDateFormatTime;
        public static int M_DelayGetDataTime = 3;                        // Độ trễ của mạng khi lấy dữ liệu, đơn vị Giây
        public static int M_NewsCount = 0;
        public static int M_NotiCount = 0;
        //thuyngo add
        public static int M_NotiTrainCount = 0;

        public static bool M_RenewDB = false;
        public static bool M_IsDataSyncing = true;
        public static bool M_IsFirstInstallApp;
        public static bool M_IS_SAFETY_QUALIFICATION_DEPARTMENT = true;  // User co

        public static bool M_IsAutoLogin = false;
        public static int M_BackStackFragmentNum
        {
            get => M_IsAutoLogin ? 4 : 6;
        }
    }
}
