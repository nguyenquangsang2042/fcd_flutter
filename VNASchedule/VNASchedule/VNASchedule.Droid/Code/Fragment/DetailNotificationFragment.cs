using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.DataProvider;
using VNASchedule.Droid.Code.Class;

namespace VNASchedule.Droid.Code.Fragment
{
    public class DetailNotificationFragment : Android.App.Fragment
    {
        private View _rootview;
        private WebView web;
        private ProgressBar progressBar;
        private TextView tv_notification_name;
        private ImageView img_back;
        private LayoutInflater _inflater;
        private MainActivity mainAct;
        private HybridWebViewClient web1;
        private string survey_table_id;
        private HybridWebViewClient1 web2;
        private string loginKey;
        private string load_url;
        public DetailNotificationFragment() { }
        public DetailNotificationFragment(string survey_table_id)
        {
            this.survey_table_id = survey_table_id;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _rootview = inflater.Inflate(Resource.Layout.DetailNotification, container, false);
            web = _rootview.FindViewById<WebView>(Resource.Id.web_detail_noti);
            progressBar = _rootview.FindViewById<ProgressBar>(Resource.Id.progress_detail_noti);
            tv_notification_name = _rootview.FindViewById<TextView>(Resource.Id.detail_noti_name);
            img_back = _rootview.FindViewById<ImageView>(Resource.Id.img_detail_noti_Back);
            _inflater = inflater;
            mainAct = (MainActivity)this.Activity;
            web1 = new HybridWebViewClient(mainAct, web, this);
            web2 = new HybridWebViewClient1(mainAct);
            webviewConfiguration();
            img_back.Click += back_frag;
            return _rootview;
        }

        private void back_frag(object sender, EventArgs e)
        {
            mainAct.BackFragment();
        }

        private void webviewConfiguration()
        {
            var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
            string query =string.Format(@"SELECT * FROM BeanSurvey Where ID = ? ");
            var a  = conn.Query<BeanSurvey>(query, survey_table_id);
            conn.Close();
            tv_notification_name.Text = a[0].Title;
            ProviderUser provider = new ProviderUser();
            loginKey = provider.MobileAutoLoginWeb(CmmVariable.SysConfig.UserId);
            web.SetWebChromeClient(web2);
            web.SetWebViewClient(web1);
            web.SetWebChromeClient(web2);
            web.Settings.JavaScriptEnabled = true;
            web.SetWebViewClient(web1);
            web.Settings.JavaScriptEnabled = true;
            web.VerticalScrollBarEnabled = true;
            web.HorizontalScrollBarEnabled = true;
            web.Settings.SetSupportZoom(true);
            web.Settings.BuiltInZoomControls = true;
            web.Settings.DisplayZoomControls = false;
            web.Settings.LoadWithOverviewMode = true;//WebView hoàn toàn thu nhỏ
            web.Settings.UseWideViewPort = true;//tải webview xem bình thường
            
            if (!string.IsNullOrEmpty(survey_table_id))
            {
                load_url = CmmVariable.M_Domain + string.Format("/FrontEnd/SurveyReview.aspx?IID={0}&AutoId={1}&LstUserId={2}&PageId={3}", survey_table_id, loginKey, CmmVariable.SysConfig.UserId,"");
                web.LoadUrl(load_url);
            }

        }
        public class HybridWebViewClient : WebViewClient
        {
            DetailNotificationFragment detailExamFragment;
            MainActivity mainActivity;
            WebView web;
            public HybridWebViewClient(MainActivity mainActivity, WebView web, DetailNotificationFragment detailExamFragment)
            {
                this.detailExamFragment = detailExamFragment;
                this.web = web;
                this.mainActivity = mainActivity;

            }
            public override void OnPageStarted(WebView view, string url, Bitmap favicon)
            {
                detailExamFragment.progressBar.Visibility = ViewStates.Visible;
                base.OnPageStarted(view, url, favicon);

            }
            public override void OnPageFinished(WebView view, string url)
            {
                detailExamFragment.progressBar.Visibility = ViewStates.Gone;

                base.OnPageFinished(view, url);
            }
        }
    }
}