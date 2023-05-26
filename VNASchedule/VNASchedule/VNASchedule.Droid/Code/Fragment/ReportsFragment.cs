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
using Android.Webkit;
using VNASchedule.Bean;
using SQLite;
using VNASchedule.Class;
using VNASchedule.Droid.Code.Class;
using Android.Graphics;
using Android.Content.Res;
using Android.Support.V4.Content;
using VNASchedule.DataProvider;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Android.Support.Design.Widget;
using Android.Support.Design.Internal;
using Android.Views.InputMethods;
using static VNASchedule.Droid.Code.Fragment.PilotMainFragment;
using System.Web;
using System.Net;
using Android.Views.Animations;
using System.Reflection;
using System.ComponentModel;
using static Java.Util.ResourceBundle;
using Javax.Security.Auth;
using Android.Media;
using Android.Net;
using System.Security.Policy;

namespace VNASchedule.Droid.Code.Fragment
{
    public class ReportsFragment : Android.App.Fragment
    {
        private ImageView img_home, img_notification, img_news, img_schedule, img_extend;
        private TextView tv_count_notification;
        private TextView tv_count_news;
        private TextView tv_news, tv_notifications, tv_newsNum, tv_notificationsNum, tv_traning, tv_license, tv_library, tv_payroll, tv_contact, tv_faq, tv_report;
        private LinearLayout lnReport;
        private TextView tv_cancel;
        private TextView tv_request;
        public static MainActivity mainAct;
        private View _rootView;
        private LayoutInflater _inflater;
        private ImageView img_back;
        public ImageView img_flag;
        private WebView web;
        private TextView tv_bottom_safety;
        private TextView tv_bottom_news;
        private TextView tv_title;
        public bool isFromDialog = false;
        private BeanNotify beanNotify = new BeanNotify();
        private LinearLayout ln_pro;
        private LinearLayout lnBottomMenu;
        private Animation click_animation;
        private Dialog dialog;
        public ProgressBar progressBar;
        private string backFragment = "";
        public bool click_attach_file = true;
        private NewsFragment newsFragment;
        private NotificationFragment notificationFragment;
        private bool checksearch;
        public bool newsType = true;
        //private NotificationFragment2 notificationFragment2;
        private Java.IO.File file;
        private bool isAlowPopupMenuNavigation = true;
        private RelativeLayout rl_bottom_home;
        private RelativeLayout rl_bottom_safety;
        private RelativeLayout rl_bottom_news;
        private RelativeLayout rl_bottom_schedule;
        private RelativeLayout rl_bottom_extent;
        private bool isFlagVisible;
        public int SafetyID { get; private set; }
        public int QualificationID { get; private set; }
        private TrainingFragment _trainingFragment;
        private string url = "";
        private string AutoID = "";

        #region public - private method
        public void updateViewCount()
        {
            if (CmmVariable.M_NewsCount != 0)
            {
                if (CmmVariable.M_NewsCount >= 100)
                {
                    //  txtCountNews.SetWidth(50);
                    tv_count_news.SetTextSize(ComplexUnitType.Sp, 8);

                }
                tv_count_news.Text = CmmVariable.M_NewsCount.ToString();
            }
            else tv_count_news.Visibility = ViewStates.Gone;
            if (CmmVariable.M_NotiCount != 0)
            {
                tv_count_notification.Text = CmmVariable.M_NotiCount.ToString();

            }
            else tv_count_notification.Visibility = ViewStates.Gone;
        }
        #endregion
        /// <summary>
        /// newsType: true news , false notification
        /// </summary>
        /// <param name="beanNotify"></param>
        /// <param name="newsType"></param>
        public ReportsFragment(NotificationFragment notificationFragment, BeanNotify beanNotify, bool newsType)
        {
            this.beanNotify = beanNotify;
            this.newsType = newsType;
            this.notificationFragment = notificationFragment;
        }
        public ReportsFragment(NewsFragment newsFragment, BeanNotify beanNotify)

        {
            this.beanNotify = beanNotify;
            this.newsFragment = newsFragment;

        }
        public ReportsFragment(BeanNotify beanNotify, bool newsType, string backfragment)
        {
            this.beanNotify = beanNotify;
            this.newsType = newsType;
            this.backFragment = backfragment;
        }
        public ReportsFragment(string url)
        {
            this.url = url;
        }
        public ReportsFragment(BeanNotify beanNotify)
        {
            this.beanNotify = beanNotify;
        }
        public ReportsFragment(BeanNotify beanNotify, bool isFromDialog)
        {
            this.isFromDialog = isFromDialog;
            this.beanNotify = beanNotify;
        }

        public ReportsFragment(TrainingFragment _trainingFragment, BeanNotify beanNotify)
        {
            this._trainingFragment = _trainingFragment;
            this.beanNotify = beanNotify;
        }

        public ReportsFragment() { }
        public override void OnCreate(Bundle savedInstanceState)
        {
            MainActivity.checkViewDetailNew = true;
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            mainAct = (MainActivity)this.Activity;
            _inflater = inflater;
            _rootView = inflater.Inflate(Resource.Layout.Reportslayout, null);
            img_back = _rootView.FindViewById<ImageView>(Resource.Id.img_WebNews_Back);
            web = _rootView.FindViewById<WebView>(Resource.Id.web_WebNews);
            tv_bottom_safety = _rootView.FindViewById<TextView>(Resource.Id.tv_bottom_safety);
            tv_bottom_news = _rootView.FindViewById<TextView>(Resource.Id.tv_bottom_news);
            tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_WebNews_Title);
            img_flag = _rootView.FindViewById<ImageView>(Resource.Id.img_WebNews_Flag);
            ln_pro = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_WebNews_Pro);
            img_home = _rootView.FindViewById<ImageView>(Resource.Id.img_home_bottom);
            img_notification = _rootView.FindViewById<ImageView>(Resource.Id.img_notification_bottom);
            img_news = _rootView.FindViewById<ImageView>(Resource.Id.img_news_bottom);
            tv_count_notification = _rootView.FindViewById<TextView>(Resource.Id.txt_count_notification);
            tv_count_news = _rootView.FindViewById<TextView>(Resource.Id.txt_count_news);
            img_schedule = _rootView.FindViewById<ImageView>(Resource.Id.img_schedule_bottom);
            img_extend = _rootView.FindViewById<ImageView>(Resource.Id.img_extent_bottom);
            lnBottomMenu = _rootView.FindViewById<LinearLayout>(Resource.Id.bottom_ln_news);
            progressBar = _rootView.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            rl_bottom_home = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_home);
            rl_bottom_safety = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_safety);
            rl_bottom_news = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_news);
            rl_bottom_schedule = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_schedule);
            rl_bottom_extent = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_extent);
            click_animation = AnimationUtils.LoadAnimation(_rootView.Context, Resource.Animation.alpha);
            ViewConfiguration();
            tv_title.Text = "FLIGHTS - PILOTS REPORT";
            url = CmmVariable.M_Domain + "/Admin/ReportFlightAndPilot.aspx?AutoId=";
            LoadData();
            //CreateDirectoryForPictures();
            CmmEvent.UserTicketRequest += CmmEvent_UpdateCount;
            mainAct.Window.SetSoftInputMode(SoftInput.AdjustResize);
            MainActivity.tammao = "";

            MEven.DetailNew += ReLoadView;
            img_back.Click += Back;
            _rootView.SetOnTouchListener(mainAct);
            rl_bottom_home.Click += HomePage;
            rl_bottom_news.Click += NewsPage;
            rl_bottom_schedule.Click += SchedulePage;
            //rl_bottom_extent.Click += PopupNavigationMenu;
            new MoreMenu(new MoreMenuProperties()
            {
                Fragment = this,
                RelativeLayoutExtent = rl_bottom_extent,
                HideControlIds = new int[] { Resource.Id.ln_report }
            });
            rl_bottom_safety.Click += NotificationPage;
            MEven.OnBackPress -= BackPress;
            MEven.OnBackPress += BackPress;
            if (CmmVariable.M_NewsCount != 0)
            {
                if (CmmVariable.M_NewsCount >= 100)
                {
                    //  txtCountNews.SetWidth(50);
                    tv_count_news.SetTextSize(ComplexUnitType.Sp, 8);

                }
                tv_count_news.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NewsCount);
            }
            else tv_count_news.Visibility = ViewStates.Gone;
            if (CmmVariable.M_NotiCount != 0)
            {
                tv_count_notification.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NotiCount);
            }
            else tv_count_notification.Visibility = ViewStates.Gone;
            return _rootView;
        }


        private void ViewConfiguration()
        {
            if (!string.IsNullOrEmpty(backFragment) || isFromDialog)
            {
                lnBottomMenu.Visibility = ViewStates.Gone;
            }
            if (CmmVariable.M_IS_SAFETY_QUALIFICATION_DEPARTMENT)
            {
                lnBottomMenu.WeightSum = 5;
            }
            tv_title.Text = beanNotify.Title;
            web.Visibility = ViewStates.Gone;
            HybridWebViewClient web1 = new HybridWebViewClient(mainAct, beanNotify, ln_pro, web, file, this);
            HybridWebViewClient1 web2 = new HybridWebViewClient1(mainAct);
            if (newsType == true)
            {

                img_news.SetColorFilter(new Color(ContextCompat.GetColor(_rootView.Context, Resource.Color.clVNAText)));
                img_news.Enabled = false;
                img_news.Focusable = false;
                tv_bottom_news.SetTextColor(new Color(ContextCompat.GetColor(_rootView.Context, Resource.Color.clVNAText)));
            }
            else
            {
                img_notification.SetColorFilter(new Color(ContextCompat.GetColor(_rootView.Context, Resource.Color.clVNAText)));
                img_notification.Enabled = false;
                img_notification.Focusable = false;
                tv_bottom_safety.SetTextColor(new Color(ContextCompat.GetColor(_rootView.Context, Resource.Color.clVNAText)));
            }
            ProviderUser provider = new ProviderUser();
            AutoID = provider.MobileAutoLoginWeb(CmmVariable.SysConfig.UserId);
            web.SetWebChromeClient(web2);
            web.SetWebViewClient(web1);
            web.SetWebChromeClient(web2);
            web.Settings.JavaScriptEnabled = true;
            web.SetWebViewClient(web1);
            web.Settings.JavaScriptEnabled = true;
            web.VerticalScrollBarEnabled = true;
            web.HorizontalScrollBarEnabled = true;
            web.Settings.SetSupportZoom(false);
            web.Settings.BuiltInZoomControls = true;
            web.Settings.DisplayZoomControls = false;
            web.Settings.LoadWithOverviewMode = true;//WebView hoàn toàn thu nhỏ
            web.Settings.UseWideViewPort = true;//tải webview xem bình thường
        }

        private async void CmmEvent_UpdateCount(object sender, EventArgs e)
        {
            try
            {
                ProviderBase p_base = new ProviderBase();
                //CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                await Task.Run(() =>
                {
                    p_base.UpdateMasterData<BeanNotify>(true);
                    mainAct.RunOnUiThread(() =>
                    {

                        string querytempNotifi = string.Format(@"SELECT COUNT(*) AS CountUnReadNews FROM BeanNotify NOLOCK WHERE  ANStatus <> -1 AND AnnounCategoryId = {0} OR AnnounCategoryId = {1} AND FlgRead = 0  ORDER BY Created DESC", SafetyID, QualificationID);
                        var tempNoti = SQLiteHelper.GetList<CountNum>(querytempNotifi).ListData;

                        if (tempNoti != null && tempNoti.Count > 0)
                        {
                            CmmVariable.M_NotiCount = tempNoti[0].CountUnReadNews;
                            tv_count_notification.Visibility = ViewStates.Visible;
                            tv_count_notification.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NotiCount);
                        }
                        else tv_count_notification.Visibility = ViewStates.Gone;

                    });
                });
            }
            catch (Exception ex)
            { }
            finally
            {
                CmmDroidFunction.HideProcessingDialog();
            }
        }
        private void PopupNavigationMenu(object sender, EventArgs e)
        {
            if (isAlowPopupMenuNavigation)
            {
                isAlowPopupMenuNavigation = false;
                View ScheduleDetail = _inflater.Inflate(Resource.Layout.ExtendCustomDialog, null);
                tv_traning = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_training);
                tv_request = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_request_ticket);
                tv_license = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_license);
                tv_library = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_library);
                tv_payroll = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_payroll);
                tv_cancel = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_cancel);
                tv_contact = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_contact);
                tv_faq = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_faqs);
                lnReport = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.ln_report);
                tv_report = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_report);
                tv_report.Click += ReportClick;
                lnReport.Visibility = ViewStates.Gone;
                tv_traning.Click += TraningClick;
                tv_license.Click += LicenseClick;
                tv_library.Click += LibraryClick;
                tv_payroll.Click += PayrollClick;
                tv_contact.Click += ContactClick;
                tv_request.Click += RequestClick;
                tv_cancel.Click += DismissMenuDialog;
                tv_faq.Click += FaqsClick;
                dialog = new Dialog(_rootView.Context, Resource.Style.Dialog);
                Window window = dialog.Window;
                dialog.RequestWindowFeature(1);
                window.SetGravity(GravityFlags.Bottom);
                Android.Util.DisplayMetrics dm = Resources.DisplayMetrics;
                ViewGroup.LayoutParams pa = ScheduleDetail.LayoutParameters;
                dialog.SetContentView(ScheduleDetail);
                dialog.Show();
                dialog.SetCancelable(false);
                WindowManagerLayoutParams s = window.Attributes;
                s.Width = dm.WidthPixels - 5;
                s.Width = dm.WidthPixels - 70;
                s.Y = 50;
                window.Attributes = s;
            }
        }
        private void ReportClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new ReportsFragment(), "ReportsFragment", 0);
        }
        private void DismissMenuDialog(object sender, EventArgs e)
        {
            isAlowPopupMenuNavigation = true;
            dialog?.Dismiss();
        }

        private void SchedulePage(object sender, EventArgs e)
        {
            this.BackToHome();
            if (CmmVariable.SysConfig.UserType == 1)//1 là phi công 
                mainAct.ShowFragmentAnim(FragmentManager, new FlightScheduleFragment(), "FlightScheduleFragment", 0);
            else if (CmmVariable.SysConfig.UserType == 2)//2 là mặt đất
                mainAct.ShowFragmentAnim(FragmentManager, new FlightScheduleMDFragment(), "FlightScheduleMDFragment", 0);
        }

        private void NewsPage(object sender, EventArgs e)
        {
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new NewsFragment(true), null, 0);
        }

        private void NotificationPage(object sender, EventArgs e)
        {
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new NotificationFragment(), null, 0);
        }

        private void HomePage(object sender, EventArgs e)
        {
            this.BackToHome();
        }

        private void RequestClick(object sender, EventArgs e)
        {
            var supportCountTry = CmmFunction.GetAppSetting("TICKET_APP_SUPPORT_COUNTRY");
            string[] supportCountTryArr = supportCountTry.Split(",");
            var registUrl = CmmFunction.GetAppSetting("TICKET_REIGIST_URL");

            try
            {
                var keyNews = CmmFunction.GetAppSetting("NotifyRequestTicketAlert");
                foreach (string x in supportCountTryArr)
                {

                    if (CmmVariable.SysConfig.Nationality.ToLower().Contains(x.ToLower()))
                    {
                        dialog?.Dismiss();
                        this.BackToHome();
                        mainAct.ShowFragment(FragmentManager, new TicketRequestFragment(), "TicketRequest");
                        return;
                    }
                    else
                    {
                        var uri = Android.Net.Uri.Parse(registUrl);
                        var intent = new Intent(Intent.ActionView, uri);
                        StartActivity(intent);
                        tv_request.Focusable = false;
                        tv_request.Enabled = false;

                        Handler h = new Handler();
                        Action myAction = () =>
                        {
                            tv_request.Focusable = true;
                            tv_request.Enabled = true;

                        };
                        h.PostDelayed(myAction, 1500);
                        return;
                    }
                }

            }
            catch (Exception ex)
            { }
        }
        private void FaqsClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new SupportFragment(), "SupportFragment", 0);
        }

        private void ContactClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new ContactsFragment(), "ContactsFragment", 0);
        }

        private void PayrollClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new PayrollFragment(), "PayrollFragment", 0);
        }

        private void LibraryClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new LibraryFragment(), "LibraryFragment", 0);
        }

        private void LicenseClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new LicenceFragment(), "LicenceFragment", 0);
        }
        private void BackPress(object sender, EventArgs e)
        {
            if (!isFlagVisible)
                img_flag.Visibility = ViewStates.Invisible;
            try
            {
                if (web.CanGoBack())
                {
                    web.GoBack();
                }
                else if (isFromDialog)
                {
                    if (beanNotify.AnnounCategoryId == SafetyID || beanNotify.AnnounCategoryId == QualificationID)
                    {
                        NotificationFragment notificationFragment = new NotificationFragment();
                        mainAct.BackFragment();
                        mainAct.ShowFragment(FragmentManager, notificationFragment, "Notification");

                    }
                    else
                    {
                        NewsFragment News = new NewsFragment(true);
                        mainAct.BackFragment();
                        mainAct.ShowFragment(FragmentManager, News, "News");
                    }
                }
                else
                {
                    if (_trainingFragment != null)
                        _trainingFragment.setData();
                    mainAct.BackFragment();
                }
            }
            catch (Exception)
            {


            }
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            MEven.OnBackPress -= BackPress;

        }
        private void TraningClick(object sender, EventArgs e)
        {
            dialog.Dismiss();

            mainAct.ShowFragmentAnim(FragmentManager, new TrainingFragment(), "Faqs", 0);
        }
        public void confirmSuccess(BeanNotify noti, string confirmType)
        {
            SQLiteConnection conn = new SQLiteConnection(CmmVariable.M_DataPath);

            switch (confirmType)
            {
                case "confirmSuccess":
                    noti.flgConfirmed = true;
                    break;
                case "replySuccess":
                    noti.flgConfirmed = true;
                    noti.flgReplied = true;
                    break;
                case "surveySuccess":
                    noti.flgConfirmed = true;
                    noti.flgReplied = true;
                    break;
            }
            conn.Update(noti);
            conn.Close();
            if (isFromDialog)
            {
                if (beanNotify.AnnounCategoryId == SafetyID || beanNotify.AnnounCategoryId == QualificationID)
                {
                    NotificationFragment notificationFragment = new NotificationFragment();
                    mainAct.BackFragment();
                    mainAct.ShowFragment(FragmentManager, notificationFragment, "Notification");

                }
                else
                {
                    NewsFragment News = new NewsFragment(true);
                    mainAct.BackFragment();
                    mainAct.ShowFragment(FragmentManager, News, "News");
                }
            }
            else
            {
                if (_trainingFragment != null)
                    _trainingFragment.setData();
                mainAct.BackFragment();
            }
        }
        private void LoadData()
        {
            string urlLoad = url + AutoID;
            web.LoadUrl(urlLoad);
            if (!string.IsNullOrEmpty(urlLoad))
            {
                web.Visibility = ViewStates.Visible;
            }
        }
        public void CreateDirectoryForPictures()
        {
            try
            {
                file = new Java.IO.File(
                   System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath), "Vietnam Airlines");
                if (!file.Exists())
                {
                    file.Mkdirs();

                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ReLoadView(object sender, MEven.ChangeViewEventArgs e)
        {
            beanNotify = e.IsSuccess;
            HybridWebViewClient web1 = new HybridWebViewClient(mainAct, beanNotify, ln_pro, web, file, this);
            HybridWebViewClient1 web2 = new HybridWebViewClient1(mainAct);
            web.SetWebChromeClient(web2);
            web.SetWebViewClient(web1);
            LoadData();
        }

        private void Back(object sender, EventArgs e)
        {
            img_back.StartAnimation(click_animation);
            img_flag.SetImageResource(Resource.Drawable.icon_flaghigh);
            if (!isFlagVisible)
                img_flag.Visibility = ViewStates.Invisible;
            if (web.CanGoBack())
            {
                web.GoBack();
            }
            else if (isFromDialog)
            {
                if (beanNotify.AnnounCategoryId == SafetyID || beanNotify.AnnounCategoryId == QualificationID)
                {
                    NotificationFragment notificationFragment = new NotificationFragment();
                    mainAct.BackFragment();
                    mainAct.ShowFragment(FragmentManager, notificationFragment, "Notification");

                }
                else
                {
                    NewsFragment News = new NewsFragment(true);
                    mainAct.BackFragment();
                    mainAct.ShowFragment(FragmentManager, News, "News");
                }
            }
            else
            {
                if (_trainingFragment != null)
                {
                    _trainingFragment.setData();
                }
                mainAct.BackFragment();
            }
        }

        public static void destroyFragment()
        {

        }
        public override void OnDestroyView()
        {
            CmmEvent.UserTicketRequest -= CmmEvent_UpdateCount;
            //MainActivity.checkViewListNew = false;
            mainAct.Window.SetSoftInputMode(SoftInput.AdjustPan);
            MainActivity.checkViewDetailNew = false;
            MEven.DetailNew -= ReLoadView;
            if (newsFragment != null)
            {
                newsFragment.isAllowItemClick = true;
            }
            if (notificationFragment != null)
            {
                notificationFragment.isAllowItemClick = true;
            }

            base.OnDestroyView();
        }

        //private void ImageShare_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string shareDataString = "";
        //        //if (web != null && !string.IsNullOrEmpty(web.Url))
        //        //{
        //        //    curentWebviewURL = web.Url;
        //        //}
        //        if (web1.beanFavoriteNews != null)
        //        {
        //            shareDataString = web1.beanFavoriteNews.UrlFriend;
        //        }
        //        else if (!string.IsNullOrEmpty(shareURL))
        //        {
        //            shareDataString = shareURL;
        //        }
        //        shareDataString = CmmVariable.M_Domain + shareDataString;

        //        Intent shareIntent = new Intent(Intent.ActionSend);
        //        shareIntent.SetType("text/plain");
        //        shareIntent.PutExtra(Android.Content.Intent.ExtraSubject, new string[] { "Hi!" });
        //        string shareMessage = shareDataString;               
        //        shareIntent.PutExtra(Intent.ExtraText, shareMessage);
        //        StartActivity(Intent.CreateChooser(shareIntent, "choose one"));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //e.toString();
        //    }
        //}

        public class HybridWebViewClient : WebViewClient
        {
            private Activity mActivity;
            private BeanNotify noti;
            private LinearLayout ln_pro;
            private WebView web;
            private Java.IO.File file;
            private ReportsFragment webNewsFragment;
            public HybridWebViewClient(Activity _mActivity, BeanNotify _noti, LinearLayout _ln_pro, WebView _web, Java.IO.File file, ReportsFragment webNewsFragment)
            {
                this.mActivity = _mActivity;
                this.noti = _noti;
                this.ln_pro = _ln_pro;
                this.web = _web;
                this.file = file;
                this.webNewsFragment = webNewsFragment;
            }
            private string Base64Decode(string base64EncodedData)
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                if (url.Contains(CmmVariable.M_Domain))
                {
                    string exten = System.IO.Path.GetExtension(url);
                    if (!string.IsNullOrEmpty(exten) && exten.Length <= 5)
                    {
                        if (exten.ToLower() == ".jpg" || exten.ToLower() == ".jpeg" || exten.ToLower() == ".png" || exten.ToLower() == ".bmp")
                        {
                            //CmmDroidFunction.DownloadAndOpenFile(mainAct, mainAct, url);
                            //CmmDroidFunction.OpenFile(mainAct, mainAct, url);
                            webNewsFragment.img_flag.Visibility = ViewStates.Visible;
                            webNewsFragment.img_flag.SetImageResource(Resource.Drawable.icon_share_file);
                            webNewsFragment.img_flag.Click += (o, sender) =>
                            {
                                AndroidDownloader downloader = new AndroidDownloader();
                                downloader.DownloadFile(url);
                            };

                            //SavePicture(url.Split('/')[url.Split('/').Length - 1], "", global::Android.OS.Environment.DirectoryDownloads);
                        }
                        else if (exten.ToLower() == ".doc" || exten.ToLower() == ".docx" || exten.ToLower() == ".xls" || exten.ToLower() == ".xlsx"
                            || exten.ToLower() == ".pdf" || exten.ToLower() == ".ppt" || exten.ToLower() == ".pptx" || exten.ToLower() == ".txt ")
                        {
                            //string supportGoogle = "https://docs.google.com/viewerng/viewer?embedded=true&url=";
                            //if (!url.Contains(supportGoogle))
                            //{
                            //    //view.StopLoading();
                            //    url = supportGoogle + url;
                            //    view.LoadUrl(url);
                            //}
                            DownloadAndOpenFile(url);
                        }
                        return false;
                    }

                    else
                    {
                        System.Uri uri = new System.Uri(url);
                        string result = HttpUtility.ParseQueryString(uri.Query).Get("result");
                        if (!string.IsNullOrEmpty(result) && result.Equals("confirmSuccess"))
                        {
                            //view.StopLoading();
                            webNewsFragment.confirmSuccess(noti, "confirmSuccess");
                            return true;

                        }
                        else if (!string.IsNullOrEmpty(result) && result.Equals("replySuccess"))
                        {
                            //view.StopLoading();
                            webNewsFragment.confirmSuccess(noti, "replySuccess");
                            return true;

                        }
                        else if (!string.IsNullOrEmpty(result) && result.Equals("surveySuccess"))
                        {
                            webNewsFragment.confirmSuccess(noti, "surveySuccess");
                            return true;

                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    OpenPage(mainAct, url);
                    return true;
                }


            }
            public static void OpenPage(Activity context, String url)
            {
                var uri = Android.Net.Uri.Parse(url);
                var intent = new Intent(Intent.ActionView, uri);
                context.StartActivity(intent);
            }
            public override void OnPageStarted(WebView view, string url, Bitmap favicon)
            {

                try
                {

                    ln_pro.Visibility = ViewStates.Visible;
                    //web.Visibility = ViewStates.Gone;
                    string exten = System.IO.Path.GetExtension(url);
                    if (url.Contains("objRedirect"))
                    {
                        view.StopLoading();

                        if (url.Contains("Source"))
                        {
                            view.StopLoading();

                            string urlDecode = WebUtility.UrlDecode(url + string.Empty);
                            System.Uri uriSource = new System.Uri(urlDecode, UriKind.RelativeOrAbsolute);
                            string Source = HttpUtility.ParseQueryString(uriSource.Query).Get("Source");

                            System.Uri uriObjRedirect = new System.Uri(Source, UriKind.RelativeOrAbsolute);
                            string objRedirect = HttpUtility.ParseQueryString(uriObjRedirect.Query).Get("objRedirect");

                            if (!string.IsNullOrEmpty(objRedirect))
                            {
                                var temp = CmmFunction.Base64Decode(objRedirect);
                                var result = JsonConvert.DeserializeObject<ObjRedirect>(temp);
                                if (result != null)
                                {
                                    //var tam=Base64Decode(tokens[1]);
                                    mainAct.ShowFlightSchedule(result);

                                }
                                //if (notifyView != null)
                                //{
                                //    this.NavigationController.PopViewController(false);
                                //    notifyView.NavigationToSchedule(result);
                                //}
                                //else if (mainView != null)
                                //{
                                //    this.NavigationController.PopViewController(false);
                                //    mainView.NavigationToSchedule(result);
                                //}f
                            }
                        }
                        else
                        {
                            System.Uri aaa = new System.Uri(url, UriKind.RelativeOrAbsolute);
                            string param1 = HttpUtility.ParseQueryString(aaa.Query).Get("objRedirect");


                            if (!string.IsNullOrEmpty(param1))
                            {
                                //var tam=Base64Decode(tokens[1]);
                                var tam = Base64Decode(param1);
                                var result = JsonConvert.DeserializeObject<ObjRedirect>(tam);
                                //destroyFragment();
                                mainAct.ShowFlightSchedule(result);

                            }
                        }

                    }

                    else if (url.StartsWith("tel:"))
                    {
                        view.StopLoading();
                        Intent intent = new Intent(Intent.ActionDial,
                                Android.Net.Uri.Parse(url));
                        mActivity.StartActivity(intent);
                    }



                }
                catch (Exception)
                {

                }
                base.OnPageStarted(view, url, favicon);
            }
            private async void DownloadAndOpenFile(string url)
            {
                try
                {
                    if (webNewsFragment.click_attach_file == true)
                    {
                        webNewsFragment.click_attach_file = false;
                        CmmDroidFunction.showProcessingDialog("Loading...", mActivity, false);
                        ProviderUser p_user = new ProviderUser();
                        //string extension = System.IO.Path.GetFileName(url.Replace("/", "_"))
                        string extension = System.IO.Path.GetFileName(url);

                        string localPath = "";

                        if (file == null)
                        {
                            file = new Java.IO.File(System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath), "Vietnam Airlines");
                            if (!file.Exists())
                            {
                                file.Mkdirs();
                            }
                        }
                        localPath = System.IO.Path.Combine(file.ToString(), extension);

                        //string newfilepathurl = CmmVariable.sysConfig.Domain + url;
                        bool result = false;
                        if (!File.Exists(localPath))
                        {
                            await Task.Run(() =>
                            {
                                result = p_user.DownloadFile(url, localPath, CmmVariable.M_AuthenticatedHttpClient);
                                if (result)
                                {
                                    loadImage(localPath);
                                }
                                else
                                {
                                    mActivity.RunOnUiThread(() =>
                                    {
                                        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mActivity);
                                        alert.SetTitle("Vietnam Airlines");
                                        alert.SetMessage("Download Failed. Try again?");
                                        alert.SetNegativeButton("Confirm", (senderAlert, args) =>
                                        {
                                            DownloadAndOpenFile(url);
                                            alert.Dispose();
                                        });
                                        alert.SetPositiveButton("Cancel", (senderAlert, args) =>
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
                            loadImage(localPath);
                        }

                    }
                }
                catch (Exception ex)
                { }
                finally
                {
                    CmmDroidFunction.HideProcessingDialog();
                    webNewsFragment.click_attach_file = true;
                }
            }
            private void loadImage(string localpath)
            {
                try
                {
                    Android.Net.Uri pdfPath = Android.Net.Uri.FromFile(new Java.IO.File(localpath));
                    Java.IO.File file = new Java.IO.File(localpath);
                    file.SetReadable(true);
                    Android.Net.Uri uri = FileProvider.GetUriForFile(mActivity, "com.Vuthao.VNASchedule", file);
                    //Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
                    string extension = System.IO.Path.GetExtension(localpath);
                    string application = "";
                    switch (extension.ToLower())
                    {
                        case ".doc":
                        case ".docx":
                            application = "application/msword";
                            break;
                        case ".pdf":
                            application = "application/pdf";
                            break;
                        case ".xls":
                        case ".xlsx":
                            application = "application/vnd.ms-excel";
                            break;
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                            application = "image/jpeg";
                            break;
                        default:
                            application = "*/*";
                            break;
                    }
                    Intent intent = new Intent(Intent.ActionView);
                    intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
                    intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                    intent.SetDataAndType(uri, application);
                    mActivity.StartActivity(intent);
                }
                catch (ActivityNotFoundException ex)
                {
                    CmmDroidFunction.logErr(ex, "ChuaPhanCongFragment - loadImage - Er: " + ex);
                    mActivity.RunOnUiThread(() =>
                    {
                        Toast.MakeText(mActivity, "You do not have an app that can open this type of file.", ToastLength.Long).Show();
                    });
                }
            }
            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);
                try
                {
                    webNewsFragment.progressBar.Visibility = ViewStates.Gone;

                    if (!noti.FlgRead)
                    {
                        SQLiteConnection conn = new SQLiteConnection(CmmVariable.M_DataPath);
                        noti.FlgRead = true;
                        conn.Update(noti);

                        conn.Close();
                        int SafetyID = Int32.Parse(CmmFunction.GetAppSetting("SAFETY_CATEGORY_ID"));
                        int QualificationID = Int32.Parse(CmmFunction.GetAppSetting("QUALIFICATION_CATEGORY_ID"));
                        if (noti.AnnounCategoryId.Value == SafetyID || noti.AnnounCategoryId.Value == QualificationID)
                            CmmVariable.M_NotiCount = CmmVariable.M_NotiCount - 1;
                        else
                            CmmVariable.M_NewsCount = CmmVariable.M_NewsCount - 1;
                        if (string.IsNullOrEmpty(webNewsFragment.backFragment))
                        {
                            webNewsFragment.updateViewCount();
                        }

                        //CmmVariable.M_NewsCount = CmmVariable.M_NewsCount - 1;
                        //if (CmmVariable.M_NotiCount != 0)
                        //{
                        //    tv_count_notification.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NotiCount.ToString();

                        //}
                    }
                    if (!string.IsNullOrEmpty(CmmFunction.GetAppSetting("ListTempAnnounceID")))
                    {
                        string tempAnnouncement = CmmFunction.GetAppSetting("ListTempAnnounceID");
                        var tempAnnouncementList = tempAnnouncement.Split(";");
                        if (tempAnnouncementList.Contains(noti.AnnounCategoryId.ToString()))
                        {

                            //SQLiteConnection conn = new SQLiteConnection(CmmVariable.M_DataPath);
                            //conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                            string query_survey = @"Delete  FROM BeanNotify Where ID = ? ";
                            //conn.Execute(query_survey, noti.ID);
                            //conn.Close();
                            SQLiteHelper.NonQuery(query_survey, noti.ID);
                        }
                    }


                    ln_pro.Visibility = ViewStates.Gone;
                    web.Visibility = ViewStates.Visible;
                }
                catch (Exception)
                {


                }
                //if (url.Contains("result=confirmSuccess"))
                //{                   
                //    confirmSuccess(noti);
                //}
            }

            public override void OnReceivedHttpError(WebView view, IWebResourceRequest request, WebResourceResponse errorResponse)
            {
                base.OnReceivedHttpError(view, request, errorResponse);
            }
            public static string GetUrlEncodedKey(string urlEncoded, string key)
            {
                urlEncoded = "&" + urlEncoded + "&";
                int Index = urlEncoded.IndexOf("&" + key + "=", StringComparison.OrdinalIgnoreCase);
                if (Index < 0)
                    return "";
                int lnStart = Index + 2 + key.Length;
                int Index2 = urlEncoded.IndexOf("&", lnStart);
                if (Index2 < 0)
                    return "";

                return UrlDecode(urlEncoded.Substring(lnStart, Index2 - lnStart));
            }
            public static string UrlDecode(string text)
            {
                // pre-process for + sign space formatting since System.Uri doesn't handle it
                // plus literals are encoded as %2b normally so this should be safe
                text = text.Replace("+", " ");
                return System.Uri.UnescapeDataString(text);
            }

            public void SavePicture(string name, System.IO.Stream data, string location = "temp")
            {
                try
                {
                    var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    documentsPath = System.IO.Path.Combine(documentsPath, "Orders", location);
                    Directory.CreateDirectory(documentsPath);

                    string filePath = System.IO.Path.Combine(documentsPath, name);

                    byte[] bArray = new byte[data.Length];
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        using (data)
                        {
                            data.Read(bArray, 0, (int)data.Length);
                        }
                        int length = bArray.Length;
                        fs.Write(bArray, 0, length);
                    }
                    Toast.MakeText(mainAct, "Tải ảnh thành công", ToastLength.Long).Show();
                }
                catch
                {
                    Toast.MakeText(mainAct, "Tải ảnh thất bại", ToastLength.Long).Show();
                }

            }
            private void DownloadHandle(DialogInterface dialog, int which)
            {
            }
        }

        public class AndroidDownloader
        {
            private string[] items;
            private string newFileName;
            public bool DownloadFile(string url)
            {
                string pathToNewFolder = System.IO.Path.Combine((string)Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "VNA");

                if (!File.Exists(pathToNewFolder))
                {
                    Directory.CreateDirectory(pathToNewFolder);
                }
                newFileName = "";
                try
                {
                    Toast.MakeText(mainAct, "Đang tải ảnh", ToastLength.Short).Show();
                    string[] arr = System.IO.Path.GetFileName(url).Split('.');
                    string pathToNewFile = System.IO.Path.Combine(pathToNewFolder, arr[0] + DateTime.Now.ToString("_ddMMyyyy_hh_mm") + "." + arr[arr.Length - 1]);
                    ProviderBase providerBase = new ProviderBase();
                    Task.Run(() =>
                    {
                        var result = providerBase.DownloadFile(url, pathToNewFile, CmmVariable.M_AuthenticatedHttpClient);
                        AddPicToGallery(pathToNewFile);
                        mainAct.RunOnUiThread(() => { Toast.MakeText(mainAct, "Tải ảnh thành công", ToastLength.Long).Show(); });
                    });
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            /// <summary>
            /// Báo cho Gallery của máy biết có hình ảnh mới.
            /// </summary>
            /// <param name="filePath">Đường dẫn đến thư mục</param>
            private void AddPicToGallery(string filePath)
            {
                Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                Java.IO.File file = new Java.IO.File(filePath);
                Android.Net.Uri contentUri = Android.Net.Uri.FromFile(file);
                mediaScanIntent.SetData(contentUri);
                mainAct.SendBroadcast(mediaScanIntent);
            }
        }

        public class DownloadEventArgs : EventArgs
        {
            public bool FileSaved = false;
            public DownloadEventArgs(bool fileSaved)
            {
                FileSaved = fileSaved;
            }
        }
    }
}