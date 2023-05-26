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
using Java.Util;
using Android.Graphics;
using VNASchedule.Class;
using VNASchedule.Bean;
using VNASchedule.Droid.Code.Adapter;
using VNASchedule.Droid.Code.Class;
using Com.Telerik.Widget.Calendar;
using Com.Telerik.Widget.Calendar.Events;
using Android.Webkit;
using Newtonsoft.Json;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;
using VNASchedule.DataProvider;
using System.Threading.Tasks;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Support.Design.Internal;
using System.Globalization;
using static VNASchedule.Droid.Code.Fragment.PilotMainFragment;
using Android.Views.Animations;
using Android.Support.V4.Content;

namespace VNASchedule.Droid.Code.Fragment
{
    public class FlightScheduleFragment : Android.App.Fragment
    {

        public FlightScheduleFragment(bool ShowProcess)
        {
            this.ShowProcess = ShowProcess;
        }
        public FlightScheduleFragment()
        {

        }
        public FlightScheduleFragment(ObjRedirect objRedirect, bool IsDetailClick)
        {
            this.objRedirect = objRedirect;
            this.IsDetailClick = IsDetailClick;
        }
        private bool ShowProcess = true;
        private bool IsDetailClick = false;
        private ObjRedirect objRedirect;
        private MainActivity mainAct;
        private View _rootView;
        private LayoutInflater _inflater;
        private ImageView img_back, img_filter, img_inday;
        private RadCalendarView calendarView;
        private List<BeanPilotScheduleClone> lst_schedule, lst_scheduleInMonth;
        private Dialog dig;
        private ExpandableListView lv;
        private LinearLayout linear_calendar, linear_lv, ln_WorkSchedule, ln_FlightSchedule;
        private ImageView img_home, img_notification, img_news, img_schedule, img_extend;
        private TextView tv_count_notification;
        private TextView tv_count_news;
        bool check_calendar = false;
        private ScheduleListAdapter listadapter;
        private List<SupTitleMenu> objSupTitleMenu;
        private TextView tv_FlightSchedule, tv_WorkSchedule;
        private bool check_FlightSchedule = true;
        private TextView tv_request;
        private TextView tv_news, tv_notifications, tv_newsNum, tv_notificationsNum, tv_traning, tv_license, tv_library, tv_payroll, tv_contact, tv_faq, tv_report;
        private TextView tv_cancel;
        private ImageView img_day;
        private WebView web;
        private TextView tv_title, tv_day, tv_done;
        string url = "";
        private NumberPicker num;
        private LinearLayout ln_pro, ln_bottom;
        private List<BeanPilotSchedulePdf> lst_SchedulePdf;
        private BeanPilotSchedulePdf schedulePdf;
        private BeanUser user;
        private CustomViewPager viewPagerSchedule;
        private TabLayout tabLayoutSchedule;
        private string title = "";
        private ScheduleFlightPagerAdapter schedulePagerAdapter;
        private Dialog dialog;
        private TextView tabTextview;
        private Animation click_animation;

        private int select_position = -1;
        private List<BeanScheduleWeekWorking> lst_schedule_week_working;
        private LinearLayout linear_bottom;
        private bool isAlowPopupMenuNavigation = true;

        private RelativeLayout rl_bottom_home;
        private RelativeLayout rl_bottom_safety;
        private RelativeLayout rl_bottom_news;
        private RelativeLayout rl_bottom_schedule;
        private RelativeLayout rl_bottom_extent;

        public override void OnDestroyView()
        {
            CmmEvent.UserTicketRequest -= CmmEvent_UpdateCount;
            //MainActivity.checkViewListNew = false;
            //MEven.ReloadListNews -= ReloadData;
            base.OnDestroyView();

        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            mainAct = (MainActivity)this.Activity;
            _inflater = inflater;
            _rootView = inflater.Inflate(Resource.Layout.FlightSchedule, null);
            img_back = _rootView.FindViewById<ImageView>(Resource.Id.img_FlightSchedule_Back);
            img_inday = _rootView.FindViewById<ImageView>(Resource.Id.img_FlightSchedule_InDay);
            img_filter = _rootView.FindViewById<ImageView>(Resource.Id.img_FlightSchedule_Filter);
            calendarView = _rootView.FindViewById<RadCalendarView>(Resource.Id.calendar_FlightSchedule);
            tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightSchedule_Title);
            linear_calendar = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightSchedule_Calendar);
            linear_lv = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightSchedule_Exlist);
            lv = _rootView.FindViewById<ExpandableListView>(Resource.Id.expandable_FlightSchedule);
            lv.SetGroupIndicator(null);
            tv_FlightSchedule = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightSchedule_FlightSchedule);
            tv_WorkSchedule = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightSchedule_WorkSchedule);
            ln_FlightSchedule = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightSchedule_FlightSchedule);
            ln_WorkSchedule = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightSchedule_WorkSchedule);
            viewPagerSchedule = _rootView.FindViewById<CustomViewPager>(Resource.Id.viewpager_Schedule_piliot);
            viewPagerSchedule.SetPagingEnabled(false);
            tabLayoutSchedule = _rootView.FindViewById<TabLayout>(Resource.Id.tablayout_Schedule_piliot);
            linear_bottom = _rootView.FindViewById<LinearLayout>(Resource.Id.bottom_ln_news);
            ln_FlightSchedule.Visibility = ViewStates.Visible;
            ln_WorkSchedule.Visibility = ViewStates.Gone;
            linear_calendar.Visibility = ViewStates.Visible;
            img_home = _rootView.FindViewById<ImageView>(Resource.Id.img_home_bottom);
            img_notification = _rootView.FindViewById<ImageView>(Resource.Id.img_notification_bottom);
            img_news = _rootView.FindViewById<ImageView>(Resource.Id.img_news_bottom);
            img_schedule = _rootView.FindViewById<ImageView>(Resource.Id.img_schedule_bottom);
            img_extend = _rootView.FindViewById<ImageView>(Resource.Id.img_extent_bottom);
            tv_count_notification = _rootView.FindViewById<TextView>(Resource.Id.txt_count_notification);
            tv_count_news = _rootView.FindViewById<TextView>(Resource.Id.txt_count_news);
            View touchView = _rootView.FindViewById(Resource.Id.viewpager_Schedule_piliot);
            img_schedule.SetColorFilter(new Color(ContextCompat.GetColor(_rootView.Context, Resource.Color.clVNAText)));
            click_animation = AnimationUtils.LoadAnimation(_rootView.Context, Resource.Animation.alpha);

            rl_bottom_home = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_home);
            rl_bottom_safety = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_safety);
            rl_bottom_news = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_news);
            rl_bottom_schedule = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_schedule);
            rl_bottom_extent = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_extent);

            lst_SchedulePdf = new List<BeanPilotSchedulePdf>();
            if (!CmmVariable.SysConfig.Nationality.Equals("VN"))
            {
                tabLayoutSchedule.Visibility = ViewStates.Gone;
                schedulePagerAdapter = new ScheduleFlightPagerAdapter(ChildFragmentManager, objRedirect, this, true);

            }
            else
            {
                schedulePagerAdapter = new ScheduleFlightPagerAdapter(ChildFragmentManager, objRedirect, this, false);
            }
            if (CmmVariable.M_IS_SAFETY_QUALIFICATION_DEPARTMENT)
            {
                linear_bottom.WeightSum = 5;
            }
            else
            {

            }
            img_schedule.Enabled = false;
            img_schedule.Focusable = false;
            img_notification.Focusable = false;
            img_notification.Enabled = false;
            img_home.Focusable = false;
            img_home.Enabled = false;
            img_news.Focusable = false;
            img_news.Enabled = false;
            Handler h = new Handler();
            Action myAction = () =>
            {
                img_notification.Focusable = true;
                img_notification.Enabled = true;
                img_news.Focusable = true;
                img_news.Enabled = true;
                img_home.Enabled = true;
                img_home.Focusable = true;
            };
            h.PostDelayed(myAction, 500);
            if (objRedirect != null)
            {
                linear_bottom.Visibility = ViewStates.Gone;
            }
            else
            {
                linear_bottom.Visibility = ViewStates.Visible;

            }
            CmmEvent.UserTicketRequest += CmmEvent_UpdateCount;
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
            linear_lv.Visibility = ViewStates.Gone;
            tv_title.Text = "Schedule";
            UpdateData();
            img_back.Click += Back;
            CmmDroidFunction.SetTitleToView(_rootView);

            rl_bottom_home.Click += HomePage;
            rl_bottom_news.Click += NewsPage;
            rl_bottom_schedule.Click += SchedulePage;
            //rl_bottom_extent.Click += PopupNavigationMenu;
            new MoreMenu(new MoreMenuProperties()
            {
                Fragment = this,
                RelativeLayoutExtent = rl_bottom_extent,
            });
            rl_bottom_safety.Click += NotificationPage;
            return _rootView;
        }

        #region Event

        private void Back(object sender, EventArgs e)
        {
            img_back.StartAnimation(click_animation);
            mainAct.BackFragment();
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
        private void SchedulePage(object sender, EventArgs e)
        {
            this.BackToHome();
            if (CmmVariable.SysConfig.UserType == 1)//1 là phi công 
                mainAct.ShowFragment(FragmentManager, new FlightScheduleFragment(), "FlightScheduleFragment");
            else if (CmmVariable.SysConfig.UserType == 2)//2 là mặt đất
                mainAct.ShowFragment(FragmentManager, new FlightScheduleMDFragment(), "FlightScheduleMDFragment");
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
                        TicketRequestFragment TicketRequest = new TicketRequestFragment();
                        mainAct.FragmentManager.PopBackStack();
                        dialog.Dismiss();
                        mainAct.ShowFragment(FragmentManager, TicketRequest, "TicketRequest");
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
        private void TraningClick(object sender, EventArgs e)
        {
            dialog?.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new TrainingFragment(), "TrainingFragment", 0);
        }
        #endregion

        #region Data
        private async void CmmEvent_UpdateCount(object sender, EventArgs e)
        {
            try
            {
                //CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                await Task.Run(() =>
                {
                    this.ProviderBase().UpdateMasterData<BeanNotify>(true);
                    mainAct.RunOnUiThread(() =>
                    {
                        int SafetyID = Int32.Parse(CmmFunction.GetAppSetting("SAFETY_CATEGORY_ID"));
                        int QualificationID = Int32.Parse(CmmFunction.GetAppSetting("QUALIFICATION_CATEGORY_ID"));
                        string querytempNotifi = string.Format(@"SELECT COUNT(*) AS CountUnReadNews FROM BeanNotify NOLOCK WHERE  ANStatus <> -1 AND AnnounCategoryId = {0} OR AnnounCategoryId = {1} AND FlgRead = 0  ORDER BY Created DESC", SafetyID, QualificationID);
                        var tempNoti = SQLiteHelper.GetList<CountNum>(querytempNotifi).ListData;

                        if (tempNoti != null && tempNoti.Count > 0)
                        {
                            CmmVariable.M_NotiCount = tempNoti[0].CountUnReadNews;
                            tv_count_notification.Visibility = ViewStates.Visible;
                            tv_count_notification.Text = CmmVariable.M_NotiCount.ToString();
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

        private void DismissMenuDialog(object sender, EventArgs e)
        {
            isAlowPopupMenuNavigation = true;

            dialog.Dismiss();
        }

        private async void UpdateData()
        {
            try
            {

                CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);

                await Task.Run(() =>
                {
                    this.ProviderBase().UpdateMasterData<BeanPilotSchedule>(true);
                    this.ProviderBase().UpdateMasterData<BeanPilotScheduleAll>(true);
                    this.ProviderBase().UpdateMasterData<BeanScheduleWeekWorking>(true);
                    this.ProviderBase().UpdateMasterData<BeanScheduleWeekWorkingDetail>(true);

                    mainAct.RunOnUiThread(() =>
                    {
                        CmmDroidFunction.HideProcessingDialog();
                        tabLayoutSchedule.TabSelected += (ee, vv) =>
                        {
                            var a = tabLayoutSchedule.SelectedTabPosition;

                            if (a == 1)
                            {
                                if (lst_SchedulePdf.Count > 0 && lst_SchedulePdf != null)
                                {
                                    if (select_position != -1)
                                    {
                                        tv_title.Text = lst_SchedulePdf[select_position].Title;
                                    }
                                    else
                                    {
                                        tv_title.Text = lst_SchedulePdf[0].Title;

                                    }
                                }
                            }
                            else
                            {
                            }
                        };

                    });
                    //mainAct.RunOnUiThread(() =>
                    //{
                    //    initViewPager();
                    //});
                });
            }
            catch (Exception ex)
            {
                mainAct.RunOnUiThread(() =>
                {
                    CmmDroidFunction.HideProcessingDialog();
                });
            }
            finally
            {
                mainAct.RunOnUiThread(() =>
                {
                    initViewPager();
                });
                CmmDroidFunction.HideProcessingDialog();
            }
        }
        public void changeTitle(string title, int selectPosition)
        {
            if (!string.IsNullOrEmpty(title))
            {
                tv_title.Text = title;

            }
            else
            {
                tv_title.Text = "Schedule";

            }
            select_position = selectPosition;
        }

        private void initViewPager()
        {
            try
            {


                viewPagerSchedule.OffscreenPageLimit = 2;
                viewPagerSchedule.SaveEnabled = false;
                viewPagerSchedule.Adapter = schedulePagerAdapter;
                tabLayoutSchedule.SetupWithViewPager(viewPagerSchedule);
                schedulePagerAdapter.NotifyDataSetChanged();
                if (IsDetailClick == true)
                {
                    viewPagerSchedule.SetCurrentItem(1, true);
                }
                TextView tabBold = (TextView)LayoutInflater.From(_rootView.Context).Inflate(Resource.Layout.custom_tab_bold, null);
                TextView tabNormal = (TextView)LayoutInflater.From(_rootView.Context).Inflate(Resource.Layout.custom_tab_normal, null);
                if (CmmVariable.SysConfig.Nationality.Equals("VN"))
                {
                    var a = tabLayoutSchedule.SelectedTabPosition;
                    if (a == 0)
                    {
                        tabBold.Text = "Flight Schedule";
                        tabNormal.Text = "Work Schedule";
                        //tabTwo.SetBackgroundResource(Resource.Drawable.tab_boder);

                        tabLayoutSchedule.GetTabAt(0).SetCustomView(tabBold);
                        tabLayoutSchedule.GetTabAt(1).SetCustomView(tabNormal);
                    }
                    else
                    {
                        tabNormal.Text = "Flight Schedule";
                        tabBold.Text = "Work Schedule";
                        //tabTwo.SetBackgroundResource(Resource.Drawable.tab_boder);

                        tabLayoutSchedule.GetTabAt(0).SetCustomView(tabNormal);
                        tabLayoutSchedule.GetTabAt(1).SetCustomView(tabBold);
                    }
                    tabLayoutSchedule.TabSelected += (ee, vv) =>
                    {

                        var b = tabLayoutSchedule.SelectedTabPosition;

                        if (b == 0)
                        {
                            tabBold.Text = "Flight Schedule";
                            tabNormal.Text = "Work Schedule";
                            //tabTwo.SetBackgroundResource(Resource.Drawable.tab_boder);

                            tabLayoutSchedule.GetTabAt(0).SetCustomView(tabBold);
                            tabLayoutSchedule.GetTabAt(1).SetCustomView(tabNormal);
                            tv_title.Text = "Schedule";

                        }
                        else
                        {
                            tabNormal.Text = "Flight Schedule";
                            tabBold.Text = "Work Schedule";
                            //tabTwo.SetBackgroundResource(Resource.Drawable.tab_boder);

                            tabLayoutSchedule.GetTabAt(0).SetCustomView(tabNormal);
                            tabLayoutSchedule.GetTabAt(1).SetCustomView(tabBold);
                            MEven.TabWorkSelected(null, null);
                        }
                    };

                }


            }
            catch (Exception ex)
            {


            }


        }

        private void SetView()
        {
            try
            {
                calendarView.SelectionMode = CalendarSelectionMode.Single;
                calendarView.DisplayMode = CalendarDisplayMode.Month;
                calendarView.EventsDisplayMode = EventsDisplayMode.Inline;
                calendarView.HorizontalScroll = true;//xem ngang 
                CalendarDayCellFilter todayCellFilter = new CalendarDayCellFilter();
                CalendarDayCellStyle todayCellStyle = new CalendarDayCellStyle();
                todayCellFilter.IsFromCurrentMonth = new Java.Lang.Boolean(true);
                Java.Lang.Float c = Java.Lang.Float.ValueOf(30f);
                todayCellStyle.TextSize = c;
                todayCellStyle.Filter = todayCellFilter;


                CalendarDayCellFilter todayCellFilter1 = new CalendarDayCellFilter();
                CalendarDayCellStyle todayCellStyle1 = new CalendarDayCellStyle();
                todayCellFilter1.IsWeekend = new Java.Lang.Boolean(true);
                todayCellStyle1.Filter = todayCellFilter1;
                todayCellStyle1.TextColor = new Java.Lang.Integer(Color.Red.ToArgb());
                //calendarView.AddDayCellStyle(todayCellStyle);
                calendarView.AddDayCellStyle(todayCellStyle1);
            }
            catch (Exception ex)
            { }
        }
        //private void loadScheduleDataFromDB()
        //{
        //    try
        //    {
        //        var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
        //        string query = @"SELECT Schedule.*, AP.Code AS ApCodeFrom, AP2.Code AS ApCodeTo
        //                    FROM BeanPilotSchedule Schedule 
        //                    INNER JOIN BeanAirport AP 
        //                        ON Schedule.FromId = AP.ID
        //                    INNER JOIN BeanAirport AP2 
        //                        ON Schedule.ToId = AP2.ID
        //                    ORDER BY DepartureTime ";
        //        lst_schedule = conn.Query<BeanPilotScheduleClone>(query);

        //        if (lst_schedule != null && lst_schedule.Count >= 0)
        //        {
        //            SetCalendar();
        //            var dd = DateTime.Now.AddDays(-DateTime.Now.Day);
        //            var bbb = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day);
        //            lst_scheduleInMonth = lst_schedule.Where<BeanPilotScheduleClone>(o => o.ArrivalTime.Value.ToLocalTime().Date > DateTime.Now.AddDays(-DateTime.Now.Day).Date
        //             && o.ArrivalTime.Value.ToLocalTime().Date <= DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day).Date).ToList();
        //            if(lst_scheduleInMonth != null && lst_scheduleInMonth.Count()>0)
        //            {
        //                GetTitleDay(lst_scheduleInMonth);
        //            }                  
        //            SetList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}
        private void GetTitleDay(List<BeanPilotScheduleClone> lstOrderTemp)
        {
            try
            {

                objSupTitleMenu = new List<SupTitleMenu>();
                DateTime dt = DateTime.Now.AddYears(1);
                for (int i = 0; i < lstOrderTemp.Count; i++)
                {
                    if (lstOrderTemp[i].DepartureTime.HasValue)
                    {
                        if (dt.Date != lstOrderTemp[i].DepartureTime.Value.ToLocalTime().Date)
                        {
                            SetTitleMenuWorker(lstOrderTemp[i].DepartureTime.Value.ToLocalTime());
                        }
                        dt = lstOrderTemp[i].DepartureTime.Value.ToLocalTime();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SetTitleMenuWorker(DateTime value)
        {
            try
            {

                SupTitleMenu obj = new SupTitleMenu();
                obj.TitleName = value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                obj.listItemMenu = GetWork(value);
                if (obj.listItemMenu != null && obj.listItemMenu.Count > 0)
                    objSupTitleMenu.Add(obj);
            }
            catch (Exception ex)
            {

            }
        }
        private List<BeanPilotScheduleClone> GetWork(DateTime dt)
        {
            try
            {
                List<BeanPilotScheduleClone> lst = new List<BeanPilotScheduleClone>();
                if (lst_scheduleInMonth != null && lst_scheduleInMonth.Count > 0)
                {
                    foreach (BeanPilotScheduleClone o in lst_scheduleInMonth)
                    {
                        if (o.DepartureTime.HasValue)
                        {
                            if (o.DepartureTime.Value.ToLocalTime().Date == dt.Date)
                            {
                                lst.Add(o);
                            }
                        }
                    }
                    lst.Sort(new sortNgay());
                }
                return lst;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        private class sortNgay : IComparer<BeanPilotScheduleClone>
        {
            public int Compare(BeanPilotScheduleClone x, BeanPilotScheduleClone y)
            {
                BeanPilotScheduleClone c1 = (BeanPilotScheduleClone)x;
                BeanPilotScheduleClone c2 = (BeanPilotScheduleClone)y;

                return DateTime.Compare(c1.DepartureTime.Value, c2.DepartureTime.Value);


            }
        }
        private void SetList()
        {
            try
            {
                //objSupTitleMenu = objSupTitleMenu.OrderBy(o => o.TitleName).ToList();
                if (objSupTitleMenu != null && objSupTitleMenu.Count > 0)
                {
                    listadapter = new ScheduleListAdapter(objSupTitleMenu, _rootView.Context);
                    lv.SetAdapter(listadapter);
                    if (objSupTitleMenu != null && objSupTitleMenu.Count > 0)
                    {
                        for (int i = 0; i < objSupTitleMenu.Count; i++)
                        {
                            lv.ExpandGroup(i);
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        //private void SetCalendar()
        //{
        //    // Creating some events
        //    List<Event> events = new List<Event>();
        //    Java.Util.Calendar calendar = Java.Util.Calendar.Instance;
        //    foreach(BeanPilotScheduleClone schedule in lst_schedule)
        //    {
        //        if (schedule.DepartureTime.HasValue && schedule.ArrivalTime.HasValue)
        //        {
        //            var departure = schedule.DepartureTime.Value.ToLocalTime();
        //            var arrival = schedule.ArrivalTime.Value.ToLocalTime();
        //            Event tam = new Event(schedule.ID.ToString(), GetDateTimeMS(departure.Year, departure.Month - 1, departure.Day, departure.Hour, departure.Minute), GetDateTimeMS(arrival.Year, arrival.Month - 1, arrival.Day, arrival.Hour, arrival.Minute));

        //            if (schedule.ApCodeFrom.Equals(schedule.ApCodeTo))
        //            {
        //                tam.EventColor = Color.ParseColor("#01c61d");
        //            }
        //            else
        //            {
        //                tam.EventColor = Color.ParseColor("#b3000a");
        //            }
        //            events.Add(tam);
        //        }
        //    }
        //    calendarView.EventAdapter.Events = events;
        //    MyEventRenderer eventRenderer = new MyEventRenderer(_rootView.Context);
        //    calendarView.EventAdapter.Renderer = eventRenderer;
        //    // >> calendar-custom-inline-events-adapter-init
        //    //MyInlineEventsAdapter adapter = new MyInlineEventsAdapter(_rootView.Context);
        //    SkylineEventsAdapter adapter = new SkylineEventsAdapter(_rootView.Context, lst_schedule);
        //    calendarView.EventsManager().Adapter = adapter;
        //    adapter.ItemClick += bbc;
        //    // << calendar-custom-inline-events-adapter-init
        //}

        //private void bbc(object sender, BeanPilotScheduleClone e)
        //{
        //    try
        //    {

        //        View ScheduleDetail = _inflater.Inflate(Resource.Layout.PopupScheduleDetail, null);
        //        TextView tv_date = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_Date);
        //        TextView tv_FltNo = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_FltNo);
        //        TextView tv_From = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_From);
        //        TextView tv_To = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_To);
        //        TextView tv_departure = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_Etd);
        //        TextView tv_arrival = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_Eta);
        //        TextView tv_Apl = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_Apl);
        //        ListView lv_Personal = ScheduleDetail.FindViewById<ListView>(Resource.Id.lv_PopupSchedule_Personal);
        //        ImageView img_CloseDetail = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupSchedule_Close);

        //        ImageView img_Avata = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupSchedule_Avata);
        //        TextView tv_Name = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_Name);
        //        TextView tv_Position = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_Position);
        //        TextView tv_Department = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_Department);
        //        TextView tv_VnCode = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_VNCode);
        //        TextView tv_Phone = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_Phone);
        //        TextView tv_Email = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupSchedule_Email);
        //        ImageView img_ClosePhone = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupSchedule_ClosePhone);
        //        ImageView img_Call = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupSchedule_Call);

        //        LinearLayout ln_detail = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.linear_PopupSchedule_Detail);
        //        LinearLayout ln_phone = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.linear_PopupSchedule_Phone);
        //        ln_detail.Visibility = ViewStates.Visible;
        //        ln_phone.Visibility = ViewStates.Gone;

        //        tv_FltNo.Text = e.FlightNo;
        //        tv_Apl.Text = e.Apl;
        //        tv_From.Text = e.ApCodeFrom + " - " + e.ApCodeTo;
        //        if (e.DepartureTime.HasValue )
        //        {
        //            tv_date.Text = e.DepartureTime.Value.ToLocalTime().ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
        //            tv_departure.Text = e.DepartureTime.Value.ToLocalTime().ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
        //        }
        //        else
        //        {
        //            tv_date.Text = "";
        //            tv_departure.Text = "";
        //        }
        //        tv_To.Text = e.ApCodeTo + " - " + e.ApCodeFrom;
        //        if ( e.ArrivalTime.HasValue)
        //        {
        //            tv_arrival.Text = e.ArrivalTime.Value.ToLocalTime().ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
        //        }
        //        else
        //        {
        //            tv_arrival.Text = "";
        //        }

        //        List<ClassUserCompact> lst_contact = JsonConvert.DeserializeObject<ClassUserCompact[]>(e.LstAllPersonal).ToList<ClassUserCompact>();
        //        if (lst_contact != null && lst_contact.Count >= 0)
        //        {
        //            UserCompartAdapter userAdapter = new UserCompartAdapter(ScheduleDetail.Context, lst_contact,mainAct);
        //            lv_Personal.Adapter = userAdapter;
        //        }                
        //        lv_Personal.ItemClick += (ee, vv) =>
        //        {
        //            if (string.IsNullOrEmpty(lst_contact[vv.Position].Mobile))
        //            { }
        //            else
        //            {
        //                var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
        //                string quer = "SELECT * FROM BeanUser WHERE ID = ? ";
        //                var lst_user = conn.Query<BeanUser>(quer, lst_contact[vv.Position].ID);
        //                if(lst_user!=null && lst_user.Count>0)
        //                {
        //                    ln_detail.Visibility = ViewStates.Gone;
        //                    ln_phone.Visibility = ViewStates.Visible;
        //                    user = lst_user[0];
        //                    SetDataViewPhone(user, img_Avata, tv_Name, tv_Position, tv_Department, tv_VnCode, tv_Phone, tv_Email);
        //                }
        //            }
        //        };
        //        img_Call.Click += (ee, vv) =>
        //        {
        //            try
        //            {
        //                var uri = Android.Net.Uri.Parse("tel:" + user.Mobile);
        //                var intent = new Intent(Intent.ActionDial, uri);
        //                StartActivity(intent);
        //            }
        //            catch (Exception ex)
        //            { }
        //        };
        //        if (ScheduleDetail != null)
        //        {
        //            dig = new Dialog(_rootView.Context);
        //            Window window = dig.Window;
        //            dig.RequestWindowFeature(1);
        //            dig.SetCanceledOnTouchOutside(false);
        //            dig.SetCancelable(true);
        //            window.SetGravity(GravityFlags.Center);
        //            Android.Util.DisplayMetrics dm = Resources.DisplayMetrics;
        //            ViewGroup.LayoutParams pa = ScheduleDetail.LayoutParameters;

        //            dig.SetContentView(ScheduleDetail);
        //            dig.Show();
        //            WindowManagerLayoutParams s = window.Attributes;
        //            s.Width = dm.WidthPixels - 5;
        //            window.Attributes = s;
        //        }
        //        img_CloseDetail.Click += (ee, vv) =>
        //        {
        //            dig.Dismiss();
        //        };
        //        img_ClosePhone.Click += (ee, vv) =>
        //        {
        //            ln_detail.Visibility = ViewStates.Visible;
        //            ln_phone.Visibility = ViewStates.Gone;
        //        };
        //    }
        //    catch(Exception ex)
        //    { }
        //}

        private void SetDataViewPhone(BeanUser user, ImageView img_Avata, TextView tv_Name, TextView tv_Position, TextView tv_Department, TextView tv_VnCode, TextView tv_Phone, TextView tv_Email)
        {
            try
            {
                tv_Department.Text = user.DepartmentName;
                tv_Email.Text = user.Email;
                tv_Name.Text = user.FullName;
                tv_Phone.Text = user.Mobile;
                tv_VnCode.Text = user.Code3;
                tv_Position.Text = user.PositionName;
                if (!string.IsNullOrEmpty(user.Avatar))
                {
                    string url = CmmVariable.M_Domain + "/" + user.Avatar + "?ver=" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    //RequestOptions options = new RequestOptions().CenterCrop().Placeholder(Resource.Drawable.icon_avatar64).Error(Resource.Drawable.icon_avatar64);
                    Glide.With(mainAct).Load(url).Apply(new RequestOptions().Override(200, 200).Error(Resource.Drawable.icon_avatar64).InvokeDiskCacheStrategy(Com.Bumptech.Glide.Load.Engine.DiskCacheStrategy.All)).Into(img_Avata);
                }
            }
            catch (Exception ex)
            { }
        }

        long GetDateTimeMS(int yr, int month, int day, int hr, int min)
        {

            Java.Util.Calendar c = Java.Util.Calendar.GetInstance(Java.Util.TimeZone.Default);
            c.Set(Java.Util.CalendarField.DayOfMonth, day);
            c.Set(Java.Util.CalendarField.HourOfDay, hr);
            c.Set(Java.Util.CalendarField.Minute, min);
            c.Set(Java.Util.CalendarField.Month, month);
            c.Set(Java.Util.CalendarField.Year, yr);

            return c.TimeInMillis;

        }
        #endregion

        // >> calendar-custom-inline-events-adapter
        public class MyInlineEventsAdapter : ArrayAdapter
        {
            private LayoutInflater layoutInflater;

            public MyInlineEventsAdapter(Context context)
                : base(context, Resource.Layout.custom_inline_event_layout)
            {
                this.layoutInflater = LayoutInflater.From(context); ;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = convertView;
                ViewHolder holder;

                if (view == null)
                {
                    view = layoutInflater.Inflate(
                        Resource.Layout.custom_inline_event_layout, parent, false);

                    holder = new ViewHolder();
                    holder.eventTitle = (TextView)view.FindViewById(Resource.Id.event_title);
                    holder.eventTime = (TextView)view.FindViewById(Resource.Id.event_time);

                    view.Tag = holder;
                }
                else
                {
                    holder = (ViewHolder)view.Tag;
                }

                EventsManager.EventInfo eventInfo = (EventsManager.EventInfo)GetItem(position);
                Event event1 = eventInfo.OriginalEvent();
                holder.eventTitle.SetTextColor(new Color(event1.EventColor));
                holder.eventTitle.Text = event1.Title;
                String eventTime = String.Format("{0} - {1}",
                                                 eventInfo.StartTimeFormatted(), eventInfo.EndTimeFormatted());
                holder.eventTime.Text = eventTime;

                return view;
            }

            class ViewHolder : Java.Lang.Object
            {
                public TextView eventTitle;
                public TextView eventTime;
            }
        }
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

                ln_pro.Visibility = ViewStates.Visible;
                web.Visibility = ViewStates.Gone;
                base.OnPageStarted(view, url, favicon);
            }
            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);
                web.Visibility = ViewStates.Visible;
                ln_pro.Visibility = ViewStates.Gone;
            }

        }
        public class MyEventRenderer : EventRenderer
        {

            int shapeSpacing = 25;
            int shapeRadius = 7;
            Paint paint;


            public MyEventRenderer(Context context)
                    : base(context)
            {

                paint = new Paint();
                paint.AntiAlias = true;
            }

            public override void RenderEvents(Canvas canvas, CalendarDayCell cell)
            {
                int startX = cell.Left + shapeSpacing;
                int startY = cell.Top + shapeSpacing;

                Rect drawTextRect = new Rect();
                if (cell.Text != null)
                {
                    String text = cell.Text;
                    cell.TextPaint.GetTextBounds(text, 0, text.Length, drawTextRect);
                }

                int x = startX;
                int y = startY;

                int spacingForDate = drawTextRect.Width();
                int tam = 0;
                if (cell.Events.Count > 1)
                {
                    tam = cell.Events.Count;
                }
                else
                    tam = 1;
                for (int i = 0; i < tam; i++)
                {
                    Event e = cell.Events[i];
                    paint.Color = new Color(e.EventColor);
                    canvas.DrawCircle(x, y, shapeRadius, paint);
                    x += shapeSpacing;
                    if (x > cell.Right - spacingForDate - shapeSpacing)
                    {
                        x = startX;
                        y += shapeSpacing;
                    }
                }
            }
            // << calendar-custom-event-renderer
        }
        // << calendar-custom-inline-events-adapter
    }
}