using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using Android.Util;
using static Android.Provider.Settings;
using Newtonsoft.Json;
using VNASchedule.Class;
using VNASchedule.Bean;

namespace VNASchedule.Droid.Code.Class
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
            SaveRegistrationToken(refreshedToken);
        }

        //server okiaf
        public void SaveRegistrationToken(string token)
        {
            try
            {
                DeviceInfo objDevice = new DeviceInfo();
                objDevice.DeviceId = Secure.GetString(ContentResolver,Secure.AndroidId); ;
                objDevice.DeviceOS = 1;
                token = FirebaseInstanceId.Instance.Token;
                if (!string.IsNullOrEmpty(token))
                    objDevice.DevicePushToken = token;
                objDevice.DeviceOSVersion = Build.VERSION.Release.ToString();
                var appVersion = Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;
                objDevice.AppVersion = appVersion;
                objDevice.DeviceModel = Build.Model;
                string device = JsonConvert.SerializeObject(objDevice);

                CmmVariable.SysConfig.DeviceInfo = device;
                CmmFunction.WriteSettingToFile();
            }
            catch (Exception ex)
            {

            }

        }
        void SendRegistrationToServer(string token)
        {

        }
        public static class QuickstartPreferences
        {
            public const string SENT_TOKEN_TO_SERVER = "sentTokenToServer";
            public const string REGISTRATION_COMPLETE = "registrationComplete";
            public const string PUSH_APP = "PushApp";
            public const string PUSH_REMAIND_LATE = "RemindLate";
            public const string PUSH_AUTO_LOGOUT = "AutoLogout";
            public const string WAKE_UPAPP = "WakeUpApp";
        }
    }
}