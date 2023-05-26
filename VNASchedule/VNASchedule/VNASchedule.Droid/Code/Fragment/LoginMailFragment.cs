using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Text.RegularExpressions;
using VNASchedule.Bean;
using static Android.Provider.Settings;
using Firebase.Iid;
using VNASchedule.Class;
using System.Net.Http;
using Newtonsoft.Json;
using VNASchedule.DataProvider;
using System.Threading.Tasks;
using System.IO;
using VNASchedule.Droid.Code.Class;
using Android.Views.InputMethods;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using Firebase;
using Android.Text;
using Android.Text.Style;
using static Android.Resource;
using Android.Views.Animations;
using Android.Provider;

namespace VNASchedule.Droid.Code.Fragment
{
    public class LoginMailFragment : Android.App.Fragment
    {
        private MainActivity mainAct;
        private View _rootView;
        private LayoutInflater _inflater;
        private EditText edt_mail;
        private TextView tv_next;
        private TextView tv_helpdesk_mobile, tv_helpdesk_mobile2;
        private string token = "";
        private Android.Views.Animations.Animation click_animation;

        public LoginMailFragment() { }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            mainAct = (MainActivity)this.Activity;
            _inflater = inflater;
            _rootView = inflater.Inflate(Resource.Layout.LoginMail, null);
            edt_mail = _rootView.FindViewById<EditText>(Resource.Id.edt_LoginMail_Mail);
            tv_helpdesk_mobile2 = _rootView.FindViewById<TextView>(Resource.Id.tv_helpdesk_phone_number2);

            tv_next = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginMail_Next);
            click_animation = AnimationUtils.LoadAnimation(_rootView.Context, Resource.Animation.alpha);

            tv_helpdesk_mobile = _rootView.FindViewById<TextView>(Resource.Id.tv_helpdesk_phone_number);
            requestRead();
            GetHelpDeskMobile();
            RegisDeviceInfo();

            tv_helpdesk_mobile.Click += tv_helpdesk_mobile_click;
            tv_helpdesk_mobile2.Click += tv_helpdesk_mobile2_click;
            //#if DEBUG
            //            edt_mail.Text = "hungnv@vuthao.com";
            //#endif      
            tv_next.Click += Next;
            _rootView.SetOnTouchListener(mainAct);
            return _rootView;
        }

        #region Event
        private void tv_helpdesk_mobile2_click(object sender, EventArgs e)
        {

            try
            {
                tv_helpdesk_mobile2.StartAnimation(click_animation);
                if (!string.IsNullOrEmpty(tv_helpdesk_mobile2.Text))
                {
                    var uri = Android.Net.Uri.Parse("tel:" + tv_helpdesk_mobile2.Text);
                    var intent = new Intent(Intent.ActionDial, uri);
                    StartActivity(intent);

                }

            }
            catch (Exception ex)
            { }
        }

        private void tv_helpdesk_mobile_click(object sender, EventArgs e)
        {
            try
            {
                tv_helpdesk_mobile.StartAnimation(click_animation);
                if (!string.IsNullOrEmpty(tv_helpdesk_mobile.Text))
                {
                    var uri = Android.Net.Uri.Parse("tel:" + tv_helpdesk_mobile.Text);
                    var intent = new Intent(Intent.ActionDial, uri);
                    StartActivity(intent);

                }

            }
            catch (Exception ex)
            { }
        }
        private async void Next(object sender, EventArgs e)
        {
            try
            {
                tv_next.Enabled = false;
                ProviderBase provider = new ProviderBase();
                bool res = false;
                if (!string.IsNullOrEmpty(edt_mail.Text) && Regex.Match(edt_mail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                {
                    HideKeyB();
                    string email = string.Empty;
                    email = edt_mail.Text;
                    string APIresult = "";
                    BeanUser reg_user = new BeanUser();
                    reg_user.Email = edt_mail.Text;

                    ProviderUser p_user = new ProviderUser();
                    CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                    await Task.Run(() =>
                    {
                        //if (File.Exists(CmmVariable.M_DataPath))
                        //{
                        //    res = provider.UpdateAllMasterData(true, 30, false);//update
                        //}
                        //else
                        //{
                        //    CmmFunction.instanceDB(CmmVariable.M_DataPath);
                        //    res = provider.UpdateAllMasterData(false, 30, true);//get all
                        //}
                        CmmVariable.M_AuthenticatedCookie = null;
                        HttpClient client = CmmFunction.InstanceHttpClient();
                        APIresult = p_user.RegistUser(email);

                        if (!string.IsNullOrEmpty(APIresult) && APIresult != "Err")
                        {
                            mainAct.RunOnUiThread(() =>
                            {
                                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                                alert.SetTitle("Vietnam Airlines");
                                alert.SetMessage("We have sent OTP to your email address, please check email to get OTP code. Thank you.");
                                alert.SetNegativeButton("Close", (senderAlert, args) =>
                                {
                                    alert.Dispose();
                                    LoginOTPFragment LoginOTP = new LoginOTPFragment(APIresult, reg_user);
                                    mainAct.ShowFragment(FragmentManager, LoginOTP, "LoginOTP");
                                });

                                Dialog dialog = alert.Create();
                                dialog.SetCanceledOnTouchOutside(false);
                                dialog.Show();
                            });
                        }
                        else
                        {
                            mainAct.RunOnUiThread(() =>
                            {
                                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                                alert.SetTitle("Vietnam Airlines");
                                alert.SetMessage("Authent fail, please try again or contact to admin for more information, thank you!");
                                alert.SetNegativeButton("Close", (senderAlert, args) =>
                                {
                                    alert.Dispose();
                                });
                                Dialog dialog = alert.Create();
                                dialog.SetCanceledOnTouchOutside(false);
                                dialog.Show();
                            });
                        }
                    });
                }
                else
                {
                    Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                    alert.SetTitle("Vietnam Airlines");
                    alert.SetMessage("Please enter valid email, thank you!");
                    alert.SetNegativeButton("Close", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });
                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                tv_next.Enabled = true;
                CmmDroidFunction.HideProcessingDialog();
            }
        }
        private void HideKeyB()
        {
            InputMethodManager inputManager = (InputMethodManager)_rootView.Context.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(_rootView.WindowToken, HideSoftInputFlags.NotAlways);
            inputManager.HideSoftInputFromInputMethod(_rootView.WindowToken, HideSoftInputFlags.ImplicitOnly);
        }
        #endregion

        #region Data
        private async void GetHelpDeskMobile()
        {
            GetMasterDataLogin();
            SetHelpDeskMobile(tv_helpdesk_mobile, "Helpdesk_Mobile_ID");
            SetHelpDeskMobile(tv_helpdesk_mobile2, "Helpdesk_Mobile_ID_2");
        }

        private void SetHelpDeskMobile(TextView tv, string keyAppSetting)
        {
            try
            {
                if (!string.IsNullOrEmpty(CmmFunction.GetAppSetting(keyAppSetting)))
                {
                    var mobileHelpdeskId = Int32.Parse(CmmFunction.GetAppSetting("Helpdesk_Mobile_ID"));

                    string query = string.Format(@"SELECT * FROM BeanFAQs WHERE  ID =  {0}", mobileHelpdeskId);
                    var beanFAQs = SQLiteHelper.GetList<BeanFAQs>(query).ListData;

                    var mobileConverted = (Html.FromHtml(Html.FromHtml(beanFAQs[0].Answer).ToString())).ToString();

                    tv.SetText(underlineText((beanFAQs != null && beanFAQs.Count > 0) ? mobileConverted : CmmVariable.M_Default_Helpdesk_Mobile)
                        , TextView.BufferType.Spannable);
                }
                else
                    tv.SetText(underlineText(CmmVariable.M_Default_Helpdesk_Mobile), TextView.BufferType.Spannable);
            }
            catch (Exception ex)
            {
                tv.SetText(underlineText(CmmVariable.M_Default_Helpdesk_Mobile), TextView.BufferType.Spannable);
            }
        }

        //private async void GetHelpDeskMobile()
        //{
        //    try
        //    {
        //        var task2 = GetMasterDataLogin();
        //        var result = await Task.WhenAll(task2);
        //        if (!string.IsNullOrEmpty(CmmFunction.GetAppSetting("Helpdesk_Mobile_ID")))
        //        {
        //            var mobileHelpdeskId = Int32.Parse(CmmFunction.GetAppSetting("Helpdesk_Mobile_ID"));
        //            var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
        //            string query = string.Format(@"SELECT * FROM BeanFAQs WHERE  ID =  {0}", mobileHelpdeskId);
        //            var beanFAQs = conn.Query<BeanFAQs>(query);

        //            var mobileConverted = (Html.FromHtml(Html.FromHtml(beanFAQs[0].Answer).ToString())).ToString();

        //            if (beanFAQs != null && beanFAQs.Count > 0)
        //            {
        //                tv_helpdesk_mobile.SetText(underlineText(mobileConverted), TextView.BufferType.Spannable);
        //            }
        //            else
        //            {
        //                tv_helpdesk_mobile.SetText(underlineText(CmmVariable.M_Default_Helpdesk_Mobile), TextView.BufferType.Spannable);
        //            }
        //            conn.Close();
        //        }
        //        else
        //        {
        //            tv_helpdesk_mobile.SetText(underlineText(CmmVariable.M_Default_Helpdesk_Mobile), TextView.BufferType.Spannable);
        //        }
        //        if (!string.IsNullOrEmpty(CmmFunction.GetAppSetting("Helpdesk_Mobile_ID_2")))
        //        {
        //            var mobileHelpdeskId = Int32.Parse(CmmFunction.GetAppSetting("Helpdesk_Mobile_ID_2"));
        //            var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
        //            string query = string.Format(@"SELECT * FROM BeanFAQs WHERE  ID =  {0}", mobileHelpdeskId);
        //            var beanFAQs = conn.Query<BeanFAQs>(query);
        //            var mobileConverted = (Html.FromHtml(Html.FromHtml(beanFAQs[0].Answer).ToString())).ToString();
        //            if (beanFAQs != null && beanFAQs.Count > 0)
        //            {
        //                tv_helpdesk_mobile2.SetText(underlineText(mobileConverted), TextView.BufferType.Spannable);
        //            }
        //            else
        //            {
        //                tv_helpdesk_mobile2.SetText(underlineText(CmmVariable.M_Default_Helpdesk_Mobile), TextView.BufferType.Spannable);
        //            }
        //            conn.Close();
        //        }
        //        else
        //        {
        //            tv_helpdesk_mobile2.SetText(underlineText(CmmVariable.M_Default_Helpdesk_Mobile), TextView.BufferType.Spannable);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tv_helpdesk_mobile.SetText(underlineText(CmmVariable.M_Default_Helpdesk_Mobile), TextView.BufferType.Spannable);
        //        tv_helpdesk_mobile2.SetText(underlineText(CmmVariable.M_Default_Helpdesk_Mobile), TextView.BufferType.Spannable);
        //    }
        //}

        private void GetMasterDataLogin()
        {
            try
            {
                bool isDbExist = File.Exists(CmmVariable.M_DataPath);
                if (isDbExist)
                    File.Delete(CmmVariable.M_DataPath);

                CmmFunction.InstantiateDB(CmmVariable.M_DataPath);
                if (!isDbExist)
                    this.ProviderLang().UpdateLangData(CmmVariable.SysConfig.LangCode, false, true);

                //Get All User trước khi vào app
                this.ProviderBase().SetMasterDataAsync<BeanUser>(false, CmmVariable.SysConfig.DataLimitDay, true);

                //this.ProviderBase().UpdateAllMasterData(false, CmmVariable.SysConfig.DataLimitDay, true);
                this.ProviderBase().SetAllMasterDataAsync(false, CmmVariable.SysConfig.DataLimitDay, true);
            }
            catch (Exception ex)
            {

            }
        }

        //private async Task<int> GetMasterDataLogin()
        //{
        //    try
        //    {
        //        await Task.Run(() =>
        //        {
        //            ProviderBase provider = new ProviderBase();
        //            ProviderAppLang prolang = new ProviderAppLang();
        //            if (File.Exists(CmmVariable.M_DataPath))
        //            {
        //                if (File.Exists(CmmVariable.M_DataPath))
        //                    File.Delete(CmmVariable.M_DataPath);
        //                CmmFunction.InstantiateDB(CmmVariable.M_DataPath);
        //                provider.UpdateAllMasterData(false, CmmVariable.SysConfig.DataLimitDay, true);//update
        //            }
        //            else
        //            {
        //                CmmFunction.InstantiateDB(CmmVariable.M_DataPath);
        //                provider.UpdateAllMasterData(false, CmmVariable.SysConfig.DataLimitDay, true);//get all
        //                prolang.UpdateLangData(CmmVariable.SysConfig.LangCode, false, true);
        //            }
        //        });

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return 1;
        //}
        private SpannableString underlineText(string inputText)
        {
            SpannableString content = new SpannableString(inputText);
            content.SetSpan(new UnderlineSpan(), 0, inputText.Length, 0);
            return content;
        }
        private void requestRead()
        {
            if (ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted ||
                ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.WriteExternalStorage) != Android.Content.PM.Permission.Granted ||
                ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.ReadExternalStorage) != Android.Content.PM.Permission.Granted ||
                ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.SetAlarm) != Android.Content.PM.Permission.Granted ||
                ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.WriteCalendar) != Android.Content.PM.Permission.Granted ||
                ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.ReadCalendar) != Android.Content.PM.Permission.Granted ||
                ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.AccessNotificationPolicy) != Android.Content.PM.Permission.Granted
                )
            {
                ActivityCompat.RequestPermissions(mainAct,
                            new string[] { Manifest.Permission.Camera, Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage, Manifest.Permission.SetAlarm, Manifest.Permission.WriteCalendar, Manifest.Permission.ReadCalendar, Manifest.Permission.AccessNotificationPolicy }, MainActivity.MY_PERMISSIONS_REQUEST_CAMERA_EXTERNAL_STORAGE);

                NotificationChannel channel = new NotificationChannel("temp", "My Channel",
            NotificationImportance.High);
                NotificationManager notificationManager = (NotificationManager)mainAct.Application.ApplicationContext.GetSystemService(Context.NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }





        }
        private void RegisDeviceInfo()
        {

            try
            {
                DeviceInfo objDevice = new DeviceInfo();
                objDevice.DeviceId = Secure.GetString(_rootView.Context.ContentResolver, Secure.AndroidId);
                objDevice.DeviceOS = 1;
                try
                {
                    token = FirebaseInstanceId.Instance.Token;
                    if (string.IsNullOrEmpty(token))
                    {
                        FirebaseInstanceId.Instance.DeleteInstanceId();
                    }
                }
                catch (Exception ex)
                { }
                if (!string.IsNullOrEmpty(token))
                    objDevice.DevicePushToken = token;
                var appVersion = Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;
                objDevice.AppVersion = appVersion;
                objDevice.DeviceOSVersion = Build.VERSION.Release.ToString();
                objDevice.DeviceModel = Build.Model;
                string device = JsonConvert.SerializeObject(objDevice);

                CmmVariable.SysConfig.DeviceInfo = device;
                CmmFunction.WriteSettingToFile();
                if (CmmVariable.M_AuthenticatedHttpClient == null)
                {
                    HttpClient client = CmmFunction.InstanceHttpClient();
                    client.BaseAddress = new Uri(CmmVariable.M_Domain);
                    CmmVariable.M_AuthenticatedHttpClient = client;
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion
    }
}