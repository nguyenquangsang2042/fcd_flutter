using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using SQLite;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.DataProvider;
using VNASchedule.Droid.Code.Adapter;
using VNASchedule.Droid.Code.Class;
using static VNASchedule.Droid.Code.Fragment.FlightScheduleFragment;

namespace VNASchedule.Droid.Code.Fragment
{
    public class FlightTabScheduleFragment : Android.App.Fragment
    {

        private MainActivity mainAct;
        private View _rootView;
        private LayoutInflater _inflater;
        private ImageView img_back, img_day;
        private WebView web;
        private TextView tv_day, tv_done, tv_FlightSchedule, tv_WorkSchedule;
        string url = "";
        private NumberPicker num;
        private TextView tv_noti;
        private LinearLayout ln_pro, ln_bottom;
        private List<BeanPilotScheduleAll> lst_ScheduleAll;
        private BeanPilotScheduleAll scheduleAll;
      
        //private WebView webW;
     
        //private NumberPicker numW;
        
     
        private LinearLayout  ln_FlightSchedule;
        private bool check_FlightSchedule = true;
        private bool check = true;
        private string titleFlight = "", titleWork = "";
     
        private FlightScheduleMDFragment flightScheduleMDFragment;
  
        private RelativeLayout relativeScheduleLayout;
        public  FlightTabScheduleFragment()
        {

        }
        public FlightTabScheduleFragment(FlightScheduleMDFragment flightScheduleMDFragment)
        {
            this.flightScheduleMDFragment = flightScheduleMDFragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if(_rootView == null)
            {
                mainAct = (MainActivity)this.Activity;
                _inflater = inflater;
                _rootView = inflater.Inflate(Resource.Layout.FlightTabSchedule, null);
                //img_back = _rootView.FindViewById<ImageView>(Resource.Id.img_FlightScheduleMD_Back);
                img_day = _rootView.FindViewById<ImageView>(Resource.Id.img_FlightScheduleMD_FlightSchedule_Day);
                web = _rootView.FindViewById<WebView>(Resource.Id.web_FlightScheduleMD_FlightSchedule);
                web.Visibility = ViewStates.Gone;
                relativeScheduleLayout = _rootView.FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);
                //tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightScheduleMD_Title);
                tv_day = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightScheduleMD_FlightSchedule_Day);
                tv_done = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightScheduleMD_FlightSchedule_Done);
                ln_pro = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightScheduleMD_FlightSchedule_Pro);
                ln_bottom = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightScheduleMD_FlightSchedule_Bottom);
                num = _rootView.FindViewById<NumberPicker>(Resource.Id.num_FlightScheduleMD_FlightSchedule);
                num.DescendantFocusability = DescendantFocusability.BlockDescendants;
             
                ln_FlightSchedule = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightScheduleMD_FlightSchedule);
               
                ln_FlightSchedule.Visibility = ViewStates.Visible;


                HybridWebViewClient web1 = new HybridWebViewClient(mainAct, ln_pro, web);
                HybridWebViewClient1 web2 = new HybridWebViewClient1(mainAct);
                web.SetWebChromeClient(web2);
                web.SetWebViewClient(web1);
                web.VerticalScrollBarEnabled = true;
                web.HorizontalScrollBarEnabled = true;
                web.Settings.SetSupportZoom(true);
                web.Settings.BuiltInZoomControls = true;
                web.Settings.DisplayZoomControls = false;
                web.Settings.JavaScriptEnabled = true;
                web.Settings.LoadWithOverviewMode = true;
                web.Settings.UseWideViewPort = true;
                web.ScrollBarStyle = ScrollbarStyles.InsideOverlay;
                web.ScrollbarFadingEnabled = true;              
                UpdateData();             
                tv_day.Click += ChooseDay;
                img_day.Click += ChooseDay;
                tv_done.Click += Choose;
                var showDevelopnoti = Int32.Parse(CmmFunction.GetAppSetting("IsEnablePilotSchedule"));
                if (showDevelopnoti == 0)
                {
                    showNoti();
                }
                else
                {
                    tv_noti.Visibility = ViewStates.Gone;
                }
            }
            return _rootView;
        }

        private void showNoti()
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
                        tv_noti.Text = "This module is being edited to sync with MyAVES. Availble on the new version.";

                        return;
                    }
                    else
                    {
                        tv_noti.Text = "Module này đang được chỉnh sửa đồng bộ dữ liệu với phần mềm MyAves. Sẽ có trong phiên bản mới. ";
                        return;
                    }
                }

            }
            catch (Exception ex)
            { }
        }
        //private void initViewPager()
        //{
        //    viewPagerSchedule.SaveEnabled = false;

        //    viewPagerSchedule.Adapter = schedulePagerAdapter;
        //    tabLayoutSchedule.SetupWithViewPager(viewPagerSchedule);
        //    schedulePagerAdapter.NotifyDataSetChanged();
        //}
        private async void UpdateData()
        {
            try
            {
                ProviderBase p_base = new ProviderBase();
                //CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                await Task.Run(() =>
                {
                    //p_base.UpdateMasterData<BeanPilotSchedulePdf>(null, true);
                    //p_base.UpdateMasterData<BeanPilotScheduleAll>(null, true);
                    mainAct.RunOnUiThread(() =>
                    {
                        SetData();
                        //LoadWeb();
                    });
                });
            }
            catch (Exception ex)
            { }
            finally
            {
                //CmmDroidFunction.hideProcessingDialog();
            }
        }
        //private void ChooseDayW(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ln_bottomW.Visibility = ViewStates.Visible;

        //    }
        //    catch (Exception ex)
        //    { }
        //}
     
        //private void LoadWebW()
        //{
        //    try
        //    {
        //        if (lst_SchedulePdf != null && lst_SchedulePdf.Count > 0)
        //        {
        //            string supportGoogle = "https://docs.google.com/viewerng/viewer?embedded=true&url=";
        //            string domain = CmmVariable.M_Domain;
        //            urlW = supportGoogle + domain + schedulePdf.FilePath;
        //            webW.Reload();
        //            webW.LoadUrl(urlW);
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //}
        private void ChooseDay(object sender, EventArgs e)
        {
            try
            {
                ln_bottom.Visibility = ViewStates.Visible;

            }
            catch (Exception ex)
            { }
        }
        private void Choose(object sender, EventArgs e)
        {
            ln_bottom.Visibility = ViewStates.Gone;
            scheduleAll = lst_ScheduleAll[num.Value];
            if (check_FlightSchedule)
            {
                flightScheduleMDFragment.changeflightTitle(scheduleAll.Title,num.Value);
                //tv_title.Text = scheduleAll.Title;
                titleFlight = scheduleAll.Title;
            }
            tv_day.Text = lst_ScheduleAll[num.Value].ScheduleDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
            web.Visibility = ViewStates.Gone;
            ln_pro.Visibility = ViewStates.Visible;
            LoadWeb();
        }
        private void LoadWeb()
        {
            try
            {
               
                    string supportGoogle = "https://docs.google.com/viewerng/viewer?embedded=true&url=";
                    string domain = CmmVariable.M_Domain;
                    url = supportGoogle + domain + scheduleAll.FilePath;
                    //web.Reload();
                    web.LoadUrl(url);
                
            }
            catch (Exception ex)
            { }
        }
        private void SetData()
        {
            ln_bottom.Visibility = ViewStates.Gone;
            SQLiteConnection conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
            string query = string.Format("SELECT * FROM BeanPilotScheduleAll ORDER BY ID DESC");
            lst_ScheduleAll = conn.Query<BeanPilotScheduleAll>(query);
            conn.Close();
            if (lst_ScheduleAll != null && lst_ScheduleAll.Count > 0)
            {
                scheduleAll = lst_ScheduleAll[0];
                LoadWeb();
                if (check_FlightSchedule)
                {
                    //tv_title.Text = lst_ScheduleAll[0].Title;
                    titleFlight = lst_ScheduleAll[0].Title;
                }
                if (lst_ScheduleAll[0].ScheduleDate.HasValue)
                {
                    tv_day.Text = lst_ScheduleAll[0].ScheduleDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                    num.MinValue = 0;

                    num.WrapSelectorWheel = false;
                    num.MaxValue = lst_ScheduleAll.Count - 1;
                    var typedColors = lst_ScheduleAll.ConvertAll(input => input.ScheduleDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"))as string).ToArray();
                    num.SetDisplayedValues(typedColors);
                    //num.ScaleX = 0.5f;
                }
                else
                {
                    tv_day.Text = "Schedule";
                }
            }
            else
            {
                tv_day.Text = "";
                titleFlight = "Schedule";
                //tv_title.Text = "Schedule";
                web.Visibility = ViewStates.Visible;
                ln_pro.Visibility = ViewStates.Gone;
                tv_day.Enabled = false;
                img_day.Enabled = false;
            }
        }
        //private void SetDataW()
        //{
        //    ln_bottomW.Visibility = ViewStates.Gone;
        //    SQLiteConnection conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
        //    string query = string.Format("SELECT * FROM BeanPilotSchedulePdf ORDER BY ID DESC");
        //    lst_SchedulePdf = conn.Query<BeanPilotSchedulePdf>(query);
        //    if (lst_SchedulePdf != null && lst_SchedulePdf.Count > 0)
        //    {
        //        schedulePdf = lst_SchedulePdf[0];
        //        if (!check_FlightSchedule)
        //        {
        //            //tv_title.Text = lst_SchedulePdf[0].Title;
        //            titleWork = lst_SchedulePdf[0].Title;
        //        }
        //        if (lst_SchedulePdf[0].ScheduleDate.HasValue)
        //        {
        //            tv_dayW.Text = lst_SchedulePdf[0].ScheduleDate.Value.ToString("dd MMM yyyy");
        //            numW.MinValue = 0;

        //            numW.WrapSelectorWheel = false;
        //            numW.MaxValue = lst_SchedulePdf.Count - 1;
        //            var typedColors = lst_SchedulePdf.ConvertAll(input => input.ScheduleDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB")) as string).ToArray();
        //            numW.SetDisplayedValues(typedColors);
        //            //num.ScaleX = 0.5f;
        //        }
        //        else
        //        {
        //            tv_dayW.Text = "Schedule";
        //        }
        //    }
        //    else
        //    {
        //        tv_day.Text = "";
        //        titleWork = "Schedule";
        //        //tv_title.Text = "Schedule";
        //        web.Visibility = ViewStates.Visible;
        //        ln_pro.Visibility = ViewStates.Gone;
        //        tv_dayW.Enabled = false;
        //        //img_dayW.Enabled = false;
        //    }
        //}
     
        public class HybridWebViewClient : WebViewClient
        {
            private Activity mActivity;
            private LinearLayout ln_pro;
            private WebView web;
            public HybridWebViewClient(Activity _mActivity, LinearLayout ln_pro, WebView web)
            {
                this.mActivity = _mActivity;
                this.ln_pro = ln_pro;
                this.web = web;
            }
            public override void OnPageStarted(WebView view, string url, Bitmap favicon)
            {
                web.Visibility = ViewStates.Gone;
                ln_pro.Visibility = ViewStates.Visible;
                base.OnPageStarted(view, url, favicon);
            }
            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);
                web.Visibility = ViewStates.Visible;
                ln_pro.Visibility = ViewStates.Gone;
            }

        }
      
        private void Back(object sender, EventArgs e)
        {
            mainAct.BackFragment();
        }
    }
}

