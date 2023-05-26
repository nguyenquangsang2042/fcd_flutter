using System;
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
using SQLite;
using Newtonsoft.Json;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;
using VNASchedule.DataProvider;
using System.Threading.Tasks;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using System.Globalization;
using Android.Views.Animations;
using Android.Support.V7.Widget;
using Android.Support.V4.Content;

namespace VNASchedule.Droid.Code.Fragment
{
    public class FlightSchedulePilotFragment : Android.App.Fragment
    {
        private MainActivity mainAct;
        private View _rootView;
        private LayoutInflater _inflater;
        private ImageView img_back, img_filter, img_inday;
        private RadCalendarView calendarView;
        private List<BeanPilotScheduleClone> lst_schedule, lst_scheduleInMonth;
        private Dialog dig;
        private ExpandableListView lv;
        private LinearLayout linear_calendar, linear_lv, ln_WorkSchedule, ln_FlightSchedule;
        private LinearLayout linear_noda;
        bool check_calendar = false;
        private ScheduleListAdapter listadapter;
        private List<SupTitleMenu> objSupTitleMenu;
        private TextView tv_FlightSchedule, tv_WorkSchedule, tv_calendar, tv_daily_flight, tv_list;

        private TextView tv_notify;
        private Animation click_animation;
        private ImageView img_day;
        private WebView web;
        private TextView tv_title, tv_day, tv_done;
        string url = "";
        private NumberPicker num;
        private LinearLayout ln_pro, ln_bottom;
        private List<BeanPilotSchedulePdf> lst_SchedulePdf;
        private BeanPilotSchedulePdf schedulePdf;
        private FlightListSheduleAdapter flightListSheduleAdapter;

        private string title = "";

        private RecyclerView recyclerFlightSchedule;
        private LinearLayoutManager mLayoutManager;
        public bool isAllowScheduleClick;
        public bool isAllowItemCalendarClick;
        public bool isAllowDailyFlightClick;

        private LinearLayoutManager MLayoutManager { get => mLayoutManager; set => mLayoutManager = value; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            mainAct = (MainActivity)this.Activity;
            _inflater = inflater;
            _rootView = inflater.Inflate(Resource.Layout.FlightSchedulePilot, null);
            img_back = _rootView.FindViewById<ImageView>(Resource.Id.img_FlightSchedule_Back);
            img_inday = _rootView.FindViewById<ImageView>(Resource.Id.img_FlightSchedule_InDay);
            img_filter = _rootView.FindViewById<ImageView>(Resource.Id.img_FlightSchedule_Filter);
            calendarView = _rootView.FindViewById<RadCalendarView>(Resource.Id.calendar_FlightSchedule);
            tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightSchedule_Title);
            linear_calendar = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightSchedule_Calendar);
            linear_noda = _rootView.FindViewById<LinearLayout>(Resource.Id.ln_EmptyData_Course);
            linear_lv = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightSchedule_Exlist);
            lv = _rootView.FindViewById<ExpandableListView>(Resource.Id.expandable_FlightSchedule);
            lv.SetGroupIndicator(null);
            tv_FlightSchedule = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightSchedule_FlightSchedule);
            tv_WorkSchedule = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightSchedule_WorkSchedule);
            ln_FlightSchedule = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightSchedule_FlightSchedule);
            ln_WorkSchedule = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightSchedule_WorkSchedule);
            tv_calendar = _rootView.FindViewById<TextView>(Resource.Id.tv_calendar_flight_pilot);
            tv_daily_flight = _rootView.FindViewById<TextView>(Resource.Id.tv_daily_flight_pilot);
            tv_list = _rootView.FindViewById<TextView>(Resource.Id.tv_list_flight_pilot);
            tv_notify = _rootView.FindViewById<TextView>(Resource.Id.tv_noti);
            recyclerFlightSchedule = _rootView.FindViewById<RecyclerView>(Resource.Id.recycler_FlightSchedule);
            click_animation = AnimationUtils.LoadAnimation(_rootView.Context, Resource.Animation.alpha);
            MLayoutManager = new LinearLayoutManager(_rootView.Context);
            ln_FlightSchedule.Visibility = ViewStates.Visible;
            ln_WorkSchedule.Visibility = ViewStates.Gone;
            linear_calendar.Visibility = ViewStates.Visible;
            linear_lv.Visibility = ViewStates.Gone;
            tv_calendar.SetTextColor(new Color(ContextCompat.GetColor(_rootView.Context, Resource.Color.headerBlue)));
            //SetViewWorkSchedule();
            SetView();
            UpdateData();
            //SetCalendar();
            img_inday.Click += InDay;
            img_back.Click += Back;
            img_filter.Click += ChangeView;
            lv.ChildClick += LVClick;
            tv_calendar.Click += CalendarClick;
            tv_daily_flight.Click += DailyFlightClick;
            tv_list.Click += ListClick;
            //tv_FlightSchedule.Click += click_FlightSchedule;
            //tv_WorkSchedule.Click += Click_WorkSchedule;
            CmmDroidFunction.SetTitleToView(_rootView);
            isAllowItemCalendarClick = true;
            isAllowDailyFlightClick = true;
            isAllowScheduleClick = true;
            //var showDevelopnoti = Int32.Parse(CmmFunction.getAppSetting("IsEnablePilotSchedule"));
            //if (showDevelopnoti == 0)
            //{
            //    showNoti();
            //}
            //else
            //{
            //    tv_notify.Visibility = ViewStates.Gone;
            //}

            return _rootView;
        }

        private void ListClick(object sender, EventArgs e)
        {
            try
            {
                linear_noda.Visibility = ViewStates.Gone;
                tv_calendar.SetTextColor(Color.ParseColor("#000000"));
                tv_list.SetTextColor(new Color(ContextCompat.GetColor(_rootView.Context, Resource.Color.headerBlue)));
                calendarView.Visibility = ViewStates.Gone;
                if (lst_schedule != null && lst_schedule.Count > 0)
                {
                    linear_noda.Visibility = ViewStates.Gone;
                    linear_lv.Visibility = ViewStates.Visible;
                }
                else
                {
                    linear_noda.Visibility = ViewStates.Visible;

                }
                check_calendar = true;
                tv_title.Text = "Schedule";
            }
            catch (Exception ex)
            {
            }

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
                        tv_notify.Text = "This module is being edited to sync with MyAVES. Availble on the new version.";
                        return;
                    }
                    else
                    {
                        tv_notify.Text = "Module này đang được chỉnh sửa đồng bộ dữ liệu với phần mềm MyAves. Sẽ có trong phiên bản mới. ";
                        return;
                    }
                }

            }
            catch (Exception ex)
            { }
        }

        private void DailyFlightClick(object sender, EventArgs e)
        {
            try
            {
                if (isAllowDailyFlightClick == true)
                {
                    isAllowDailyFlightClick = false;
                    ScheduleInDayFragment ScheduleInDay = new ScheduleInDayFragment(this,true);

                    FragmentManager childFragMan = mainAct.FragmentManager;
                    FragmentTransaction childFragTrans = childFragMan.BeginTransaction();

                    childFragTrans.Add(Resource.Id.frmMain, ScheduleInDay);
                    childFragTrans.AddToBackStack(null);
                    childFragTrans.Commit();
                }


                //mainAct.AddFragment(FragmentManager, ScheduleInDay, "ScheduleInDay",1);
            }
            catch (Exception ex)
            {


            }
        }

        private void CalendarClick(object sender, EventArgs e)
        {
            try
            {
                calendarView.Visibility = ViewStates.Visible;
                tv_calendar.SetTextColor(new Color(ContextCompat.GetColor(_rootView.Context, Resource.Color.headerBlue)));
                tv_list.SetTextColor(Color.ParseColor("#000000"));
                linear_calendar.Visibility = ViewStates.Visible;
                linear_lv.Visibility = ViewStates.Gone;
                linear_noda.Visibility = ViewStates.Gone;
                check_calendar = false;
                tv_title.Text = "Schedule";
            }
            catch (Exception ex)
            {

            }
        }

        private async void UpdateData()
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                ProviderBase p_base = new ProviderBase();
                //CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                await Task.Run(() =>
                {

                    loadScheduleDataFromDB();
                    if (lst_scheduleInMonth != null && lst_scheduleInMonth.Count() > 0)
                    {
                        GetTitleDay(lst_scheduleInMonth);
                    }
                    mainAct.RunOnUiThread(() =>
                    {
                        SetList();
                        watch.Stop();
                        var elapsedMs = watch.ElapsedMilliseconds;
                        Console.WriteLine("Error - FlightSchedulePilotFragment - UpdateData - Err" + elapsedMs.ToString());
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
        private void SetViewWorkSchedule()
        {
            try
            {
                img_day = _rootView.FindViewById<ImageView>(Resource.Id.img_FlightSchedule_WorkSchedule_Day);
                web = _rootView.FindViewById<WebView>(Resource.Id.web_FlightSchedule_WorkSchedule);
                web.Visibility = ViewStates.Gone;
                tv_day = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightSchedule_WorkSchedule_Day);
                tv_done = _rootView.FindViewById<TextView>(Resource.Id.tv_FlightSchedule_WorkSchedule_Done);
                ln_pro = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightSchedule_WorkSchedule_Pro);
                ln_bottom = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_FlightSchedule_WorkSchedule_Bottom);
                num = _rootView.FindViewById<NumberPicker>(Resource.Id.num_FlightSchedule_WorkSchedule);
                num.DescendantFocusability = DescendantFocusability.BlockDescendants;
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
                SetDataWorkSchedule();
                LoadWeb();
                tv_day.Click += ChooseDay;
                img_day.Click += ChooseDay;
                tv_done.Click += Choose;
            }
            catch (Exception ex)
            {

            }
        }
        private void Choose(object sender, EventArgs e)
        {
            try
            {
                ln_bottom.Visibility = ViewStates.Gone;
                schedulePdf = lst_SchedulePdf[num.Value];
                tv_day.Text = lst_SchedulePdf[num.Value].ScheduleDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                tv_title.Text = lst_SchedulePdf[num.Value].Title;
                title = lst_SchedulePdf[num.Value].Title;
                web.Visibility = ViewStates.Gone;
                ln_pro.Visibility = ViewStates.Visible;
                LoadWeb();
            }
            catch (Exception ex)
            {


            }
        }

        private void ChooseDay(object sender, EventArgs e)
        {
            try
            {
                ln_bottom.Visibility = ViewStates.Visible;

            }
            catch (Exception ex)
            { }
        }

        private void LoadWeb()
        {
            try
            {
                if (lst_SchedulePdf != null && lst_SchedulePdf.Count > 0)
                {
                    string supportGoogle = "https://docs.google.com/viewerng/viewer?embedded=true&url=";
                    string domain = CmmVariable.M_Domain;
                    url = supportGoogle + domain + schedulePdf.FilePath;
                    web.Reload();
                    web.LoadUrl(url);
                }
            }
            catch (Exception ex)
            { }
        }
        private void SetDataWorkSchedule()
        {
            SQLiteConnection conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
            try
            {
                ln_bottom.Visibility = ViewStates.Gone;

                string query = string.Format("SELECT * FROM BeanPilotSchedulePdf ORDER BY ID DESC");
                lst_SchedulePdf = conn.Query<BeanPilotSchedulePdf>(query);
                //lst_SchedulePdf= lst_SchedulePdf.OrderBy(x=>x.)
                if (lst_SchedulePdf != null && lst_SchedulePdf.Count > 0)
                {
                    schedulePdf = lst_SchedulePdf[0];
                    title = lst_SchedulePdf[0].Title;
                    if (lst_SchedulePdf[0].ScheduleDate.HasValue)
                    {
                        tv_day.Text = lst_SchedulePdf[0].ScheduleDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                        num.MinValue = 0;

                        num.WrapSelectorWheel = false;
                        num.MaxValue = lst_SchedulePdf.Count - 1;
                        var typedColors = lst_SchedulePdf.ConvertAll(input => input.ScheduleDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB")) as string).ToArray();
                        num.SetDisplayedValues(typedColors);
                        //num.ScaleX = 0.5f;
                    }
                    else
                    {
                        tv_day.Text = "Schedule";
                    }
                }
            }
            catch (Exception ex)
            {


            }
            finally
            {
                conn.Close();
            }
        }

        private void InDay(object sender, EventArgs e)
        {
            try
            {
                ScheduleInDayFragment ScheduleInDay = new ScheduleInDayFragment(this,true);
                //mainAct.ShowFragment(FragmentManager, ScheduleInDay, "ScheduleInDay");
                FragmentManager childFragMan = mainAct.FragmentManager;
                FragmentTransaction childFragTrans = childFragMan.BeginTransaction();
                childFragTrans.Add(Resource.Id.frmMain, ScheduleInDay);
                childFragTrans.AddToBackStack(null);
                childFragTrans.Commit();
            }
            catch (Exception ex)
            {


            }
        }

        private void LVClick(object sender, ExpandableListView.ChildClickEventArgs e)
        {
            try
            {
                SupTitleMenu tam = objSupTitleMenu[e.GroupPosition];
                BeanPilotScheduleClone schedule = tam.listItemMenu[e.ChildPosition];
                if (schedule != null && schedule.ID != 0)
                {
                    bbc(null, schedule);
                }
            }
            catch (Exception ex)
            { }
        }

        private void ChangeView(object sender, EventArgs e)
        {
            try
            {
                if (check_calendar)
                {
                    linear_calendar.Visibility = ViewStates.Visible;
                    linear_lv.Visibility = ViewStates.Gone;
                    check_calendar = false;
                    tv_title.Text = "Schedule";
                }
                else
                {
                    linear_calendar.Visibility = ViewStates.Gone;
                    linear_lv.Visibility = ViewStates.Visible;
                    check_calendar = true;
                    tv_title.Text = "Schedule";
                }
            }
            catch (Exception ex)
            { }
        }

        private void Back(object sender, EventArgs e)
        {
            img_back.StartAnimation(click_animation);
            mainAct.BackFragment();
        }
        private void SetView()
        {
            try
            {

                var watch = System.Diagnostics.Stopwatch.StartNew();
                calendarView.SelectionMode = CalendarSelectionMode.Single;
                calendarView.DisplayMode = CalendarDisplayMode.Month;
                calendarView.EventsDisplayMode = EventsDisplayMode.Inline;
                calendarView.HorizontalScroll = true;//xem ngang 
                CalendarDayCellFilter todayCellFilter = new CalendarDayCellFilter();
                CalendarDayCellStyle todayCellStyle = new CalendarDayCellStyle();
                Java.Lang.Float c = Java.Lang.Float.ValueOf(30f);
                todayCellStyle.TextSize = c;
                todayCellStyle.Filter = todayCellFilter;
                todayCellStyle.BackgroundColor = new Java.Lang.Integer(Color.ParseColor("#F2F2F2").ToArgb());
                todayCellStyle.TextColor = new Java.Lang.Integer(Color.Black.ToArgb());

                CalendarDayCellFilter selectDayFilter = new CalendarDayCellFilter();
                CalendarDayCellStyle selectDayCellStyle = new CalendarDayCellStyle();
                selectDayFilter.IsSelected = new Java.Lang.Boolean(true);
                selectDayCellStyle.Filter = selectDayFilter;
                selectDayCellStyle.BorderColor = new Java.Lang.Integer(Color.ParseColor("#1E88E5").ToArgb());

                CalendarDayCellFilter todayCellFilter1 = new CalendarDayCellFilter();
                CalendarDayCellStyle todayCellStyle1 = new CalendarDayCellStyle();
                todayCellFilter1.IsWeekend = new Java.Lang.Boolean(true);
                todayCellStyle1.Filter = todayCellFilter1;
                todayCellStyle1.TextColor = new Java.Lang.Integer(Color.Red.ToArgb());

                CalendarDayCellFilter fromCurrentMonthFilter = new CalendarDayCellFilter();
                CalendarDayCellStyle fromCurrentMonthtyle = new CalendarDayCellStyle();
                fromCurrentMonthFilter.IsFromCurrentMonth = new Java.Lang.Boolean(false);
                fromCurrentMonthtyle.Filter = fromCurrentMonthFilter;
                fromCurrentMonthtyle.BackgroundColor = new Java.Lang.Integer(Color.ParseColor("#FBFBFB").ToArgb());

                calendarView.AddDayCellStyle(todayCellStyle);
                calendarView.AddDayCellStyle(todayCellStyle1);
                calendarView.AddDayCellStyle(selectDayCellStyle);
                calendarView.AddDayCellStyle(fromCurrentMonthtyle);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("Error - FlightSchedulePilotFragment - SetView - Err" + elapsedMs.ToString());
            }
            catch (Exception ex)
            { }
        }
        private void loadScheduleDataFromDB()
        {
            try
            {
                var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                string query = @"SELECT Schedule.*, AP.Code AS ApCodeFrom, AP2.Code AS ApCodeTo
                            FROM BeanPilotSchedule Schedule 
                            INNER JOIN BeanAirport AP 
                                ON Schedule.FromId = AP.ID
                            INNER JOIN BeanAirport AP2 
                                ON Schedule.ToId = AP2.ID WHERE STATUS <> -1
                            ORDER BY DepartureTime  ";
                lst_schedule = conn.Query<BeanPilotScheduleClone>(query);
                conn.Close();

                if (lst_schedule != null && lst_schedule.Count >= 0)
                {
                    SetCalendar();
                    var dd = DateTime.Now.AddDays(-DateTime.Now.Day);
                    var bbb = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day);
                    lst_scheduleInMonth = lst_schedule.Where<BeanPilotScheduleClone>(o => o.ArrivalTime.Value.Date > DateTime.Now.AddDays(-DateTime.Now.Day).Date
                     && o.ArrivalTime.Value.Date <= DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day).Date).ToList();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
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
                        if (dt.Date != lstOrderTemp[i].DepartureTime.Value.Date)
                        {
                            SetTitleMenuWorker(lstOrderTemp[i].DepartureTime.Value);
                        }
                        dt = lstOrderTemp[i].DepartureTime.Value;
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
                obj.TitleName = value.ToString(" MMMMM dd, yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
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
                            if (o.DepartureTime.Value.Date == dt.Date)
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
                recyclerFlightSchedule.SetLayoutManager(MLayoutManager);
                flightListSheduleAdapter = new FlightListSheduleAdapter(lst_schedule, _rootView.Context);
                flightListSheduleAdapter.ItemClick += itemScheduleClick;
                recyclerFlightSchedule.SetAdapter(flightListSheduleAdapter);
            }
            catch (Exception ex)
            { }
        }

        private void itemScheduleClick(object sender, int e)
        {
            /* showFlightInfo(lst_schedule[e]);*/
            if (isAllowScheduleClick == true)
            {
                isAllowScheduleClick = false;
                DetailScheduleFragment detailScheduleFragment = new DetailScheduleFragment(this, lst_schedule[e]);
                mainAct.AddFragment(mainAct.FragmentManager, detailScheduleFragment, "DetailScheduleFragment", 1);
            }

        }

        private void SetCalendar()
        {
            // Creating some events
            List<Event> events = new List<Event>();
            try
            {
                Java.Util.Calendar calendar = Java.Util.Calendar.Instance;
                DateTime dateNow = System.DateTime.Now;
                List<Java.Lang.Long> DatetoLongs = new List<Java.Lang.Long>();
                
                /*DatetoLongs.Add(GetDateTimeMSLong
                        (lst_schedule[0].DepartureTime.Value.Year,
                        lst_schedule[0].DepartureTime.Value.Month - 1,
                        lst_schedule[0].DepartureTime.Value.Day,
                        lst_schedule[0].DepartureTime.Value.Hour,
                        lst_schedule[0].DepartureTime.Value.Minute));
                calendarView.SelectedDates = DatetoLongs;*/

                foreach (BeanPilotScheduleClone schedule in lst_schedule)
                {
                    if (schedule.DepartureTime.HasValue && schedule.ArrivalTime.HasValue)
                    {
                        var departure = schedule.DepartureTime.Value;
                        var arrival = schedule.ArrivalTime.Value;
                        Event tam = new Event(schedule.ID.ToString(), GetDateTimeMS(departure.Year, departure.Month - 1, departure.Day, departure.Hour, departure.Minute), GetDateTimeMS(arrival.Year, arrival.Month - 1, arrival.Day, arrival.Hour, arrival.Minute));
                        if (schedule.ApCodeFrom.Equals(schedule.ApCodeTo))
                        {
                            tam.EventColor = Color.ParseColor("#01c61d");
                        }
                        else
                        {
                            tam.EventColor = Color.ParseColor("#b3000a");
                        }
                        events.Add(tam);
                    }
                }
                mainAct.RunOnUiThread(() =>
                {
                    int[] colors = { Color.Red };

                    calendarView.EventAdapter.Events = events;
                    MyEventRenderer eventRenderer = new MyEventRenderer(_rootView.Context);
                    calendarView.EventAdapter.Renderer = eventRenderer;
                    SkylineEventsAdapter adapter = new SkylineEventsAdapter(_rootView.Context, lst_schedule);
                    calendarView.EventsManager().Adapter = adapter;
                    adapter.ItemClick += bbc;
                });
            }
            catch (Exception ex)
            {


            }

            // << calendar-custom-inline-events-adapter-init
        }

        private void bbc(object sender, BeanPilotScheduleClone e)
        {
            //showFlightInfo(e);

            if (isAllowItemCalendarClick == true)
            {
                isAllowItemCalendarClick = false;
                DetailScheduleFragment detailScheduleFragment = new DetailScheduleFragment(this, e);
                mainAct.AddFragment(mainAct.FragmentManager, detailScheduleFragment, "DetailScheduleFragment", 1);
            }

        }

        public override void OnResume()
        {
            base.OnResume();
        }
        public override void OnPause()
        {
            base.OnPause();
        }
        public override void OnStart()
        {
            base.OnStart();
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
                try
                {
                    Java.Util.Random random = new Java.Util.Random();
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

                    for (int i = 0; i < cell.Events.Count; i++)
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
                catch (Exception ex)
                {


                }
            }
            // << calendar-custom-event-renderer
        }
        // << calendar-custom-inline-events-adapter
    }
}