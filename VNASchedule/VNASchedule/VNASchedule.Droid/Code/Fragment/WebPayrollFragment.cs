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
using VNASchedule.Droid.Code.Class;
using VNASchedule.Class;
using Android.Graphics;
using Android.Views.Animations;
using System.Threading.Tasks;
using VNASchedule.DataProvider;

namespace VNASchedule.Droid.Code.Fragment
{
    public class WebPayrollFragment : Android.App.Fragment
    {
        private MainActivity mainAct;
        private View _rootView;
        private LayoutInflater _inflater;
        private ImageView img_back;
        private WebView web;
        private BeanSalary beanSalary;
        private LinearLayout ln_pro;
        private ImageView img_schedule;
        private ImageView img_extend;
        private TextView tv_count_notification;
        private TextView tv_count_news;
        private Animation click_animation;
        private ImageView img_home;
        private ImageView img_notification;
        private ImageView img_news;
        private TextView tv_request;
        private TextView tv_traning;
        private TextView tv_license;
        private TextView tv_library;
        private TextView tv_payroll;
        private TextView tv_contact;
        private TextView tv_cancel;
        private TextView tv_faq, tv_report;
        private Dialog dialog;
        private bool isAlowPopupMenuNavigation = true;
        private RelativeLayout rl_bottom_home;
        private RelativeLayout rl_bottom_safety;
        private RelativeLayout rl_bottom_news;
        private RelativeLayout rl_bottom_schedule;
        private RelativeLayout rl_bottom_extent;
        private string startdate, enddate;
        public WebPayrollFragment(BeanSalary beanSalary)
        {
            this.beanSalary = beanSalary;
        }
        public WebPayrollFragment() { }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            mainAct = (MainActivity)this.Activity;
            _inflater = inflater;
            _rootView = inflater.Inflate(Resource.Layout.WebPayroll, null);
            if (Arguments != null)
            {
                startdate = Arguments.GetString("startdate");
                enddate = Arguments.GetString("enddate");
            }
            img_back = _rootView.FindViewById<ImageView>(Resource.Id.img_WebPayroll_Back);
            web = _rootView.FindViewById<WebView>(Resource.Id.web_WebPayroll);
            mainAct.Window.SetSoftInputMode(SoftInput.AdjustResize);
            web.Visibility = ViewStates.Gone;
            ln_pro = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_WebPayroll_Pro);
            HybridWebViewClient web1 = new HybridWebViewClient(mainAct, ln_pro, web);
            HybridWebViewClient1 web2 = new HybridWebViewClient1(mainAct);
            img_home = _rootView.FindViewById<ImageView>(Resource.Id.img_home_bottom);
            img_notification = _rootView.FindViewById<ImageView>(Resource.Id.img_notification_bottom);
            img_news = _rootView.FindViewById<ImageView>(Resource.Id.img_news_bottom);
            img_schedule = _rootView.FindViewById<ImageView>(Resource.Id.img_schedule_bottom);
            img_extend = _rootView.FindViewById<ImageView>(Resource.Id.img_extent_bottom);
            tv_count_notification = _rootView.FindViewById<TextView>(Resource.Id.txt_count_notification);
            tv_count_news = _rootView.FindViewById<TextView>(Resource.Id.txt_count_news);
            click_animation = AnimationUtils.LoadAnimation(_rootView.Context, Resource.Animation.alpha);
            rl_bottom_home = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_home);
            rl_bottom_safety = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_safety);
            rl_bottom_news = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_news);
            rl_bottom_schedule = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_schedule);
            rl_bottom_extent = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_extent);

            web.SetWebChromeClient(web2);
            web.SetWebViewClient(web1);
            loadDetailURL();
            string url = CmmVariable.M_Domain + "/frontend/SalaryDetail.aspx?IID=" + beanSalary.ID;
            web.Settings.JavaScriptEnabled = true;
            web.VerticalScrollBarEnabled = true;
            web.HorizontalScrollBarEnabled = true;
            web.Settings.SetSupportZoom(true);
            web.Settings.BuiltInZoomControls = true;
            web.Settings.DisplayZoomControls = false;
            web.Settings.LoadWithOverviewMode = true;
            web.Settings.UseWideViewPort = true;
            web.ScrollBarStyle = ScrollbarStyles.InsideOverlay;
            web.ScrollbarFadingEnabled = true;
            //web.Reload();
            //web.LoadUrl(url);

            img_back.Click += Back;

            rl_bottom_home.Click += HomePage;
            rl_bottom_news.Click += NewsPage;
            rl_bottom_schedule.Click += SchedulePage;
            //rl_bottom_extent.Click += PopupNavigationMenu;
            new MoreMenu(new MoreMenuProperties()
            {
                Fragment = this,
                RelativeLayoutExtent = rl_bottom_extent,
                HideControlIds = new int[] { Resource.Id.ln_payroll }
            });
            rl_bottom_safety.Click += NotificationPage;
            _rootView.SetOnTouchListener(mainAct);
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
                tv_count_notification.Text = CmmVariable.M_NotiCount.ToString();

            else tv_count_notification.Visibility = ViewStates.Gone;
            return _rootView;
        }

        private async void loadDetailURL()
        {
            await Task.Run(() =>
            {
                ProviderUser provider = new ProviderUser();
                var loginKey = provider.MobileAutoLoginWeb(CmmVariable.SysConfig.UserId);
                if (!string.IsNullOrEmpty(loginKey))
                {
                    mainAct.RunOnUiThread(() =>
                    {
                        var urlService = CmmVariable.M_Domain + "/frontend/SalaryDetail.aspx?IID=" + beanSalary.ID + "&autoid=" + loginKey;

                        web.LoadUrl(urlService);
                    });
                }
            });
        }

        private void PopupNavigationMenu(object sender, EventArgs e)
        {
            if (isAlowPopupMenuNavigation)
            {
                isAlowPopupMenuNavigation = false;

                View ScheduleDetail = _inflater.Inflate(Resource.Layout.ExtendCustomDialog, null);
                tv_request = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_request_ticket);
                tv_traning = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_training);
                tv_license = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_license);
                tv_library = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_library);
                tv_payroll = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_payroll);
                tv_contact = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_contact);
                tv_cancel = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_cancel);
                tv_faq = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_faqs);
                tv_report = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_report);
                tv_report.Click += ReportClick;
                LinearLayout ln_payroll = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.ln_payroll);

                ln_payroll.Visibility = ViewStates.Gone;
                tv_traning.Click += TraningClick;
                tv_license.Click += LicenseClick;
                tv_library.Click += LibraryClick;
                tv_payroll.Click += PayrollClick;
                tv_contact.Click += ContactClick;
                tv_faq.Click += FaqsClick;
                tv_request.Click += RequestClick;
                tv_cancel.Click += DismissMenuDialog;
                dialog = new Dialog(_rootView.Context, Resource.Style.Dialog);
                Window window = dialog.Window;
                dialog.RequestWindowFeature(1);
                dialog.SetCancelable(false);
                window.SetGravity(GravityFlags.Bottom);
                Android.Util.DisplayMetrics dm = Resources.DisplayMetrics;
                ViewGroup.LayoutParams pa = ScheduleDetail.LayoutParameters;

                dialog.SetContentView(ScheduleDetail);
                dialog.Show();
                WindowManagerLayoutParams s = window.Attributes;
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
        private void ContactClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new ContactsFragment(true), "ContactsFragment", 0);
        }

        private void DismissMenuDialog(object sender, EventArgs e)
        {
            isAlowPopupMenuNavigation = true;
            dialog?.Dismiss();
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
                        mainAct.ShowFragment(FragmentManager, new TicketRequestFragment(), "TicketRequestFragment");
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

        private void PayrollClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new PayrollFragment(true), "PayrollFragment", 0);
        }

        private void LibraryClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new LibraryFragment(true), "LibraryFragment", 0);
        }

        private void LicenseClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new LicenceFragment(true), "LicenceFragment", 0);
        }

        private void TraningClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new TrainingFragment(true), "TrainingFragment", 0);
        }

        private void SchedulePage(object sender, EventArgs e)
        {
            this.BackToHome();
            if (CmmVariable.SysConfig.UserType == 1)//1 là phi công 
                mainAct.ShowFragmentAnim(FragmentManager, new FlightScheduleFragment(false), "FlightScheduleFragment", 0);
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

        public override void OnDestroyView()
        {
            mainAct.Window.SetSoftInputMode(SoftInput.AdjustPan);
            base.OnDestroyView();
        }
        private void Back(object sender, EventArgs e)
        {


            img_back.StartAnimation(click_animation);
            if (web.CanGoBack())
            {
                web.GoBack();
            }
            else
            {

                //mainAct.FragmentManager.PopBackStack();
                mainAct.FragmentManager.PopBackStack();
                //PayrollFragment Payroll = new PayrollFragment();
                //Bundle args = new Bundle();
                //args.PutString("startdate", startdate);
                //args.PutString("enddate", enddate);


                //Payroll.Arguments = args;
                //mainAct.ShowFragment(FragmentManager, Payroll, "Payroll");

                //mainAct.BackFragment();

            }
        }
        public class HybridWebViewClient : WebViewClient
        {
            private LinearLayout ln_pro;
            private MainActivity mainAct;
            private WebView web;

            public HybridWebViewClient(MainActivity mainAct, LinearLayout ln_pro, WebView web)
            {
                this.mainAct = mainAct;
                this.ln_pro = ln_pro;
                this.web = web;
            }
            public override void OnPageStarted(WebView view, string url, Bitmap favicon)
            {
                string exten = System.IO.Path.GetExtension(url);
                if (!string.IsNullOrEmpty(exten))
                {
                    if (exten.ToLower() == ".jpg" || exten.ToLower() == ".jpeg" || exten.ToLower() == ".png")
                    {

                    }
                    else if (exten.ToLower() == ".doc" || exten.ToLower() == ".docx" || exten.ToLower() == ".xls" || exten.ToLower() == ".xlsx" || exten.ToLower() == ".pdf")
                    {
                        string supportGoogle = "https://docs.google.com/viewerng/viewer?embedded=true&url=";
                        if (!url.Contains(supportGoogle))
                        {
                            //googleSup = true;
                            url = supportGoogle + url;
                            view.LoadUrl(url);
                        }
                    }
                }
                //if ()
                base.OnPageStarted(view, url, favicon);
            }
            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);
                web.Visibility = ViewStates.Visible;
                ln_pro.Visibility = ViewStates.Gone;
                if (url.Contains("confirmSuccess"))
                {
                    mainAct.BackFragment();
                }
            }
            private void openFile(string localpath)
            {
                try
                {
                    Android.Net.Uri pdfPath = Android.Net.Uri.FromFile(new Java.IO.File(localpath));
                    Java.IO.File file = new Java.IO.File(localpath);
                    file.SetReadable(true);
                    Android.Net.Uri uri = Android.Support.V4.Content.FileProvider.GetUriForFile(mainAct, "com.Vuthao.VNASchedule", file);
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
                        case ".mp4":
                            application = "video/*";
                            break;
                        default:
                            application = "*/*";
                            break;
                    }
                    Intent intent = new Intent(Intent.ActionView);
                    intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
                    intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                    intent.SetDataAndType(uri, application);
                    mainAct.StartActivity(intent);
                }
                catch (ActivityNotFoundException ex)
                {
                    CmmDroidFunction.logErr(ex, "ChuaPhanCongFragment - loadImage - Er: " + ex);
                }
                finally
                {
                    //lv.Enabled = true;
                }
            }

            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                if (url.Contains(".pdf")
                    || url.Contains(".png") || url.Contains(".jpg") || url.Contains(".jpeg")
                    || url.Contains(".doc") || url.Contains(".docx")
                    || url.Contains(".xls") || url.Contains(".xlsx")
                    || url.Contains(".ppt") || url.Contains(".pptx")
                    )
                {
                    CmmDroidFunction.showProcessingDialog("Loading...", mainAct, false);
                    bool ret = false;
                    Task.Run(() =>
                    {
                        string localDocumentFilepath = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDocuments);//Android.OS.Environment.DirectoryDocuments;
                        string fileName = url.Substring(url.LastIndexOf('/') + 1);
                        string localPath = System.IO.Path.Combine(localDocumentFilepath, fileName);
                        ret = mainAct.providerBase.DownloadFile(url, localPath, CmmVariable.M_AuthenticatedHttpClient);
                        mainAct.RunOnUiThread(() =>
                        {
                            CmmDroidFunction.HideProcessingDialog();
                            openFile(localPath);
                        });
                    });

                    return true;
                }
                return base.ShouldOverrideUrlLoading(view, url);
            }
        }
    }
}