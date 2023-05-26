using System;
using VNASchedule.Bean;

namespace VNASchedule.Class
{
    //
    public static class CmmEvent
    {
        public class UpdateEventArgs : EventArgs
        {
            public bool IsSuccess { get; set; }
            public string LangSelected { get; set; }
            public string ErrMess { get; set; }

            public UpdateEventArgs()
            {
                LangSelected = "";
                ErrMess = "";
            }
            public UpdateEventArgs(bool isSuccess, string langSelected = "", string errMess = "")
            {
                IsSuccess = isSuccess;
                LangSelected = langSelected;
                ErrMess = errMess;
            }
        }

        public class LoginEventArgs : EventArgs
        {
            public bool IsSuccess { get; set; }
            public string UserName { get; set; }
            public string ErrCode { get; set; }
            public string Pass { get; set; }
            public BeanUser UserInfo { get; set; }

            public LoginEventArgs()
            {
                UserName = "";
                Pass = "";
            }
            public LoginEventArgs(bool isSuccess, string userName = "", string pass = "", BeanUser userInfo = null, string errCode = "")
            {
                IsSuccess = isSuccess;
                UserName = userName;
                Pass = pass;
                UserInfo = userInfo;
                ErrCode = errCode;
            }
        }

        public class AnnouncementEventArgs : EventArgs
        {
            public BeanNotify nofitiocation { get; set; }

            public AnnouncementEventArgs()
            {
            }
            public AnnouncementEventArgs(BeanNotify _noti)
            {
                nofitiocation = _noti;
            }
        }
        public class OnConnecInternetEventArgs : EventArgs
        {
            public bool IsSuccess { get; set; }
            public bool isConnectInternet { get; set; }
            public string ErrMess { get; set; }

            public OnConnecInternetEventArgs()
            {
                isConnectInternet = false;
                ErrMess = "";
            }
            public OnConnecInternetEventArgs(bool isSuccess, string errMess = "")
            {
                IsSuccess = isSuccess;
                ErrMess = errMess;
            }
        }

        public class OnUpdateAPIEventArgs : EventArgs
        {
            public bool IsSuccess { get; set; }
            public string ErrMess { get; set; }

            public OnUpdateAPIEventArgs()
            {
                ErrMess = "";
            }
            public OnUpdateAPIEventArgs(bool isSuccess, string errMess = "")
            {
                IsSuccess = isSuccess;
                ErrMess = errMess;
            }
        }

        public static event EventHandler UpdateCount;
        public static event EventHandler<UpdateEventArgs> UpdateLangComplete;
        public static event EventHandler<LoginEventArgs> ReloginRequest;
        public static event EventHandler SyncDataRequest;
        public static event EventHandler JobNotifyRequest;
        public static event EventHandler UserRequest;
        public static event EventHandler<AnnouncementEventArgs> UpdateAnnouncement;
        public static event EventHandler ScheduleRequest;
        public static event EventHandler TrainingRequest;
        public static event EventHandler UserTicketRequest;
        public static event EventHandler Logout;
        public static event EventHandler UpdateNumNotify;
        public static event EventHandler PushNotify;
        public static event EventHandler OnStartInsertDB;
        public static event EventHandler OnFinishInsertDB;
        public static event EventHandler OnFAQsSearchClick;
        public static event EventHandler OnHelPDeskSearchClick;
        public static event EventHandler OnFAQsTabSelected;
        public static event EventHandler OnHelpDeskTabSelected;
        public static event EventHandler OnUrlChange;
        public static event EventHandler<OnConnecInternetEventArgs> OnConnecInternet;
        public static event EventHandler<OnUpdateAPIEventArgs> OnUpdateAPI;
        public static event EventHandler OnDownloadSchedule;
        public static event EventHandler OnFinishLoadParentLibrary;


        public static void FinishLoadParentLibrary(object sender, EventArgs e)
        {
            if (OnFinishLoadParentLibrary != null)
            {
                OnFinishLoadParentLibrary(sender, e);
            }
        }
        public static void DownloadSchedule(object sender, EventArgs e)
        {
            if (OnDownloadSchedule != null)
            {
                OnDownloadSchedule(sender, e);
            }
        }
        public static void UpdateAPI(object sender, OnUpdateAPIEventArgs e)
        {
            if (OnUpdateAPI != null)
            {
                OnUpdateAPI(sender, e);
            }
        }
        public static void User_Logout(object sender, EventArgs e)
        {
            if (Logout != null)
            {
                Logout(sender, e);
            }
        }
        public static void URLChange(object sender, EventArgs e)
        {
            if (OnUrlChange != null)
            {
                OnUrlChange(sender, e);
            }
        }
        public static void ConnecInternet(object sender, OnConnecInternetEventArgs e)
        {
            if (OnConnecInternet != null)
            {
                OnConnecInternet(sender, e);
            }
        }
        public static void UpdateCount_Performence(object sender, EventArgs e)
        {
            if (UpdateCount == null)
            {
                UpdateCount(sender, e);
            }
        }

        public static void UpdateLangComplete_Performence(object sender, UpdateEventArgs e)
        {

            if (UpdateLangComplete != null)
            {
                UpdateLangComplete(sender, e);
            }
        }

        public static void ReloginRequest_Performence(object sender, LoginEventArgs e)
        {

            if (ReloginRequest != null)
            {
                ReloginRequest(sender, e);
            }
        }
        public static void FAQsSeachClick(object sender, EventArgs e)
        {
            if (OnFAQsSearchClick != null)
            {
                OnFAQsSearchClick(sender, e);
            }
        }
        public static void FAQsTabSelectedClick(object sender, EventArgs e)
        {
            if (OnFAQsTabSelected != null)
            {
                OnFAQsTabSelected(sender, e);
            }
        }
        public static void HelpdeskTabSelectedClick(object sender, EventArgs e)
        {
            if (OnHelpDeskTabSelected != null)
            {
                OnHelpDeskTabSelected(sender, e);
            }
        }

        public static void HelpDeskSearchClick(object sender, EventArgs e)
        {
            if (OnHelPDeskSearchClick != null)
            {
                OnHelPDeskSearchClick(sender, e);
            }
        }

        public static void SyncDataRequest_Performence(object sender, EventArgs e)
        {
            if (SyncDataRequest != null)
            {
                SyncDataRequest(sender, e);
            }
        }

        public static void JobNotifyRequest_Performence(object sender, EventArgs e)
        {

            if (JobNotifyRequest != null)
            {
                JobNotifyRequest(sender, e);
            }
        }

        public static void UserRequest_Performence(object sender, EventArgs e)
        {

            if (UserRequest != null)
            {
                UserRequest(sender, e);
            }
        }

        public static void UpdateAnnouncement_Performence(object sender, AnnouncementEventArgs e)
        {

            if (UpdateAnnouncement != null)
            {
                UpdateAnnouncement(sender, e);
            }
        }

        public static void ScheduleRequest_Performence(object sender, EventArgs e)
        {

            if (ScheduleRequest != null)
            {
                ScheduleRequest(sender, e);
            }
        }

        public static void TrainingRequest_Performence(object sender, EventArgs e)
        {

            if (TrainingRequest != null)
            {
                TrainingRequest(sender, e);
            }
        }

        public static void UserTicketRequest_Performence(object sender, EventArgs e)
        {

            if (UserTicketRequest != null)
            {
                UserTicketRequest(sender, e);
            }
        }

        public static void UpdateNumNotify_Performence(object sender, EventArgs e)
        {

            if (UpdateNumNotify != null)
            {
                UpdateNumNotify(sender, e);
            }
        }
        public static void StartInsertDB(object sender, EventArgs e)
        {
            if (OnStartInsertDB != null)
            {
                OnStartInsertDB(sender, e);
            }
        }
        public static void FinishInsertDB(object sender, EventArgs e)
        {
            if (OnFinishInsertDB != null)
            {
                OnFinishInsertDB(sender, e);
            }
        }
    }
}
