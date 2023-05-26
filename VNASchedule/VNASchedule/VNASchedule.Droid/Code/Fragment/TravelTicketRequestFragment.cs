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
using VNASchedule.Class;
using VNASchedule.Bean;
using VNASchedule.Droid.Code.Adapter;
using System.Globalization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using VNASchedule.DataProvider;
using VNASchedule.Droid.Code.Class;
using Android.Views.InputMethods;
using Android.Graphics;
using static VNASchedule.Droid.Code.Fragment.PilotMainFragment;
using Android.Views.Animations;
using Android.Icu.Util;

namespace VNASchedule.Droid.Code.Fragment
{
    public class TravelTicketRequestFragment : Android.App.Fragment
    {
        private MainActivity mainAct;
        private View _rootView;
        private LayoutInflater _inflater;
        private ImageView img_back;
        private TextView tv_title, tv_fleet, tv_crewcode, tv_notionality, tv_home, tv_working, tv_ExpectedDate, tv_DepartureFlightFrom,
            tv_ETD, tv_DepartureStationFrom, tv_ArravilStationFrom, tv_DepartureFlightTo, tv_name,
            tv_ETA, tv_DepartureStationTo, tv_ArravilStationTo, tv_save, tv_status, tv_cancel;
        private TextView tv_contact;
        private TextView tv_faq, tv_report;
        private LinearLayout lnBottomMenu;
        private ImageView img_home;
        private ImageView img_notification;
        private ImageView img_news;
        private TextView tv_count_notification;
        private TextView tv_count_news;
        private ImageView img_schedule;
        private ImageView img_extend;
        private Animation click_animation;

        private EditText edt_from, edt_to;
        private LinearLayout ln_edtfrom, ln_editto, ln_hide;
        private bool check_datefrom = true, check_timefrom = true, check_KeyEditFrom = true, check_KeyBoard = false;
        private List<BeanAirport> lst_airports;
        private AirportAdapter airprotadapter;
        /// <summary>
        /// airport=1 departureStationFrom
        /// airport=2 departureStationTo
        /// airport=3 arrivalStationFrom
        /// airport=4 arrivalStationFrom
        /// </summary>
        private int airport = 0;
        private Dialog dig;
        private BeanUserTicketCategory beanUserTicketCategory;
        private BeanAirport airportDeparFrom, airportDeparTo, airportArrviFrom, airportArrviTo;
        private BeanUserTicketDetail ticketDetailDeparture;
        private BeanUserTicketClone beanUserTicketClone;
        private List<BeanUserRelationshipClone> lst_TicketOfUser;
        private bool check_edit = false;
        private List<BeanUserTicketDetail> lst_ticketDetail;
        private int numUserTicket = 0;
        private TextView tv_traning;
        private TextView tv_request;
        private TextView tv_license;
        private TextView tv_library;
        private TextView tv_payroll;
        private Dialog dialog;
        private Android.Icu.Util.Calendar from_date_calendar;
        private Android.Icu.Util.Calendar to_date_calendar;
        private bool isAlowPopupMenuNavigation = true;
        private RelativeLayout rl_bottom_home;
        private RelativeLayout rl_bottom_safety;
        private RelativeLayout rl_bottom_news;
        private RelativeLayout rl_bottom_schedule;
        private RelativeLayout rl_bottom_extent;
        public TravelTicketRequestFragment() { }
        public TravelTicketRequestFragment(BeanUserTicketCategory beanUserTicketCategory)
        {
            this.beanUserTicketCategory = beanUserTicketCategory;
        }
        public TravelTicketRequestFragment(BeanUserTicketClone beanUserTicketClone)
        {
            this.beanUserTicketClone = beanUserTicketClone;
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
            _rootView = inflater.Inflate(Resource.Layout.TravelTicketRequest, null);
            img_back = _rootView.FindViewById<ImageView>(Resource.Id.img_TravelTicketRequest_Back);
            tv_ArravilStationFrom = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_ArrivalStationFrom);
            tv_ArravilStationTo = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_ArrivalStationTo);
            tv_crewcode = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_CrewCode);
            tv_DepartureFlightFrom = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_DateDeparture);
            tv_DepartureFlightTo = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_DateReturn);
            tv_DepartureStationFrom = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_DepartureStationFrom);
            tv_DepartureStationTo = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_DepartureStationTo);
            tv_ETA = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_ETA);
            tv_ETD = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_ETD);
            tv_ExpectedDate = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_ExpectedDate);
            tv_fleet = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_Fleet);
            tv_home = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_HomeResidence);
            tv_notionality = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_Nationality);
            tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_Title);
            tv_working = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_WorkingPattern);
            edt_from = _rootView.FindViewById<EditText>(Resource.Id.edt_TravelTicketRequest_FlightNoFrom);
            edt_to = _rootView.FindViewById<EditText>(Resource.Id.edt_TravelTicketRequest_FlightNoTo);
            tv_name = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_Name);
            tv_save = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_Save);
            ln_editto = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_TravelTicketRequest_FlightNoTo);
            ln_edtfrom = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_TravelTicketRequest_FlightNoFrom);
            ln_hide = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_TravelTicketRequest_HideKeyBoard);
            tv_status = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_Status);
            tv_cancel = _rootView.FindViewById<TextView>(Resource.Id.tv_TravelTicketRequest_Cancel);
            rl_bottom_home = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_home);
            rl_bottom_safety = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_safety);
            rl_bottom_news = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_news);
            rl_bottom_schedule = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_schedule);
            rl_bottom_extent = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_extent);
            lnBottomMenu = _rootView.FindViewById<LinearLayout>(Resource.Id.bottom_ln_news);
            img_home = _rootView.FindViewById<ImageView>(Resource.Id.img_home_bottom);
            img_notification = _rootView.FindViewById<ImageView>(Resource.Id.img_notification_bottom);
            img_news = _rootView.FindViewById<ImageView>(Resource.Id.img_news_bottom);
            tv_count_notification = _rootView.FindViewById<TextView>(Resource.Id.txt_count_notification);
            tv_count_news = _rootView.FindViewById<TextView>(Resource.Id.txt_count_news);
            img_schedule = _rootView.FindViewById<ImageView>(Resource.Id.img_schedule_bottom);
            img_extend = _rootView.FindViewById<ImageView>(Resource.Id.img_extent_bottom);
            click_animation = AnimationUtils.LoadAnimation(_rootView.Context, Resource.Animation.alpha);

            if (CmmVariable.M_IS_SAFETY_QUALIFICATION_DEPARTMENT)
            {
                lnBottomMenu.WeightSum = 5;
            }
            else
            {
            }

            CmmEvent.UserTicketRequest += CmmEvent_UpdateCount;

            //img_extend.SetColorFilter(Color.ParseColor("#1E88E5"));

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



            GetAirport();
            SetData();
            rl_bottom_home.Click += HomePage;
            rl_bottom_news.Click += NewsPage;
            rl_bottom_schedule.Click += SchedulePage;
            //rl_bottom_extent.Click += PopupNavigationMenu;
            new MoreMenu(new MoreMenuProperties()
            {
                Fragment = this,
                RelativeLayoutExtent = rl_bottom_extent,
                HideControlIds = new int[] { Resource.Id.ln_request }
            });
            rl_bottom_safety.Click += NotificationPage;
            tv_DepartureFlightFrom.Click += ChooseDateFrom;
            tv_DepartureFlightTo.Click += ChooseDateTo;
            tv_ETA.Click += ChooseTimeTo;
            tv_ETD.Click += ChooseTimeFrom;
            tv_DepartureStationFrom.Click += DepartureStationFrom;
            tv_DepartureStationTo.Click += DepartureStationTo;
            tv_ArravilStationFrom.Click += ArravilStationFrom;
            tv_ArravilStationTo.Click += ArravilStationTo;
            tv_save.Click += Save;
            ln_editto.Click += EditTo;
            ln_edtfrom.Click += EditFrom;
            ln_hide.Click += CloseKeyBroad;
            img_back.Click += Back;
            tv_cancel.Click += ACCCancel;
            _rootView.SetOnTouchListener(mainAct);
            CmmDroidFunction.SetTitleToView(_rootView);
            return _rootView;
        }

        private async void CmmEvent_UpdateCount(object sender, EventArgs e)
        {
            try
            {
                //ProviderBase p_base = new ProviderBase();
                //CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                await Task.Run(() =>
                {
                    this.ProviderBase().UpdateMasterData<BeanNotify>(true);
                    mainAct.RunOnUiThread(() =>
                    {
                        int SafetyID = Int32.Parse(CmmFunction.GetAppSetting("SAFETY_CATEGORY_ID"));
                        int QualificationID = Int32.Parse(CmmFunction.GetAppSetting("QUALIFICATION_CATEGORY_ID"));
                        //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                        string querytempNotifi = string.Format(@"SELECT COUNT(*) AS CountUnReadNews FROM BeanNotify NOLOCK WHERE  ANStatus <> -1 AND AnnounCategoryId = {0} OR AnnounCategoryId = {1} AND FlgRead = 0  ORDER BY Created DESC", SafetyID, QualificationID);
                        //var tempNoti = conn.Query<CountNum>(querytempNotifi);
                        //conn.Close();
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
                tv_report = ScheduleDetail.FindViewById<TextView>(Resource.Id.txt_report);
                tv_report.Click += ReportClick;
                LinearLayout ln_ticket = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.ln_request);

                ln_ticket.Visibility = ViewStates.Gone;
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
            dialog.Dismiss();
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new ReportsFragment(), "ReportsFragment", 0);
        }
        private void DismissMenuDialog(object sender, EventArgs e)
        {
            isAlowPopupMenuNavigation = true;
            dialog.Dismiss();
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

        private void ACCCancel(object sender, EventArgs e)
        {
            try
            {
                tv_cancel.Enabled = false;
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                alert.SetTitle("Vietnam Airlines");
                alert.SetMessage("Please confirm cancel ticket.");
                alert.SetNegativeButton("Confirm", (senderAlert, args) =>
                {
                    Cancel();
                    alert.Dispose();
                });
                alert.SetPositiveButton("Cancel", (senderAlert, args) =>
                {
                    tv_cancel.Enabled = true;
                    alert.Dispose();
                });
                Dialog dialog = alert.Create();
                dialog.SetCanceledOnTouchOutside(false);
                dialog.Show();
            }
            catch (Exception ex)
            { }
        }

        private async void Cancel()
        {
            try
            {
                tv_cancel.Enabled = false;
                CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                await Task.Run(() =>
                {
                    ProviderTicket p_ticket = new ProviderTicket();
                    var status = p_ticket.cancelTicket(beanUserTicketClone);
                    if (status)
                    {
                        //ProviderBase provider = new ProviderBase();
                        this.ProviderBase().UpdateMasterData<BeanUserTicket>(true, 30, false);
                        this.ProviderBase().UpdateMasterData<BeanUserTicketDetail>(true, 30, false);
                        mainAct.RunOnUiThread(() =>
                        {
                            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                            alert.SetTitle("Vietnam Airlines");
                            alert.SetMessage("Cancel ticket success");
                            alert.SetNegativeButton("Close", (senderAlert, args) =>
                            {
                                alert.Dispose();
                                Back(null, null);
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
                            alert.SetMessage("Cancel ticket error");
                            alert.SetNegativeButton("Close", (senderAlert, args) =>
                            {
                                alert.Dispose();
                                Back(null, null);
                            });
                            Dialog dialog = alert.Create();
                            dialog.SetCanceledOnTouchOutside(false);
                            dialog.Show();
                        });
                    }
                });
            }
            catch (Exception ex)
            { }
            finally
            {
                CmmDroidFunction.HideProcessingDialog();
                tv_cancel.Enabled = true;
            }
        }

        private void Back(object sender, EventArgs e)
        {
            img_back.StartAnimation(click_animation);
            mainAct.BackFragment();
        }
        private void CloseKeyBroad(object sender, EventArgs e)
        {
            try
            {
                if (check_KeyBoard)
                {
                    check_KeyBoard = false;
                    edt_to.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
                    edt_to.FocusableInTouchMode = false;
                    edt_to.Focusable = false;
                    edt_to.RequestFocus();
                    edt_to.ClearFocus();
                    edt_from.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
                    edt_from.FocusableInTouchMode = false;
                    edt_from.Focusable = false;
                    edt_from.RequestFocus();
                    edt_from.ClearFocus();
                    InputMethodManager inputManager = (InputMethodManager)_rootView.Context.GetSystemService(Context.InputMethodService);
                    inputManager.HideSoftInputFromWindow(_rootView.WindowToken, HideSoftInputFlags.NotAlways);
                    ln_hide.Visibility = ViewStates.Gone;
                    //inputManager.HideSoftInputFromInputMethod(_rootView.WindowToken, HideSoftInputFlags.ImplicitOnly);
                }
            }
            catch (Exception ex)
            { }
        }

        private void EditFrom(object sender, EventArgs e)
        {
            check_KeyEditFrom = true;
            ShowKeyBroad();
        }

        private void EditTo(object sender, EventArgs e)
        {
            check_KeyEditFrom = false;
            ShowKeyBroad();
        }
        private void ShowKeyBroad()
        {
            try
            {
                if (check_KeyEditFrom)
                {
                    ln_hide.Visibility = ViewStates.Visible;
                    edt_from.Focusable = true;
                    edt_from.FocusableInTouchMode = true;
                    edt_from.RequestFocus();
                    mainAct.Window.SetSoftInputMode(SoftInput.AdjustPan);
                    InputMethodManager inputMethodManager = mainAct.GetSystemService(Context.InputMethodService) as InputMethodManager;
                    //inputMethodManager.ShowSoftInput(relative_menu_bottom, ShowFlags.Forced);
                    inputMethodManager.ShowSoftInput(edt_from, ShowFlags.Implicit);
                    check_KeyBoard = true;
                }
                else
                {
                    ln_hide.Visibility = ViewStates.Visible;
                    edt_to.Focusable = true;
                    edt_to.FocusableInTouchMode = true;
                    edt_to.RequestFocus();
                    mainAct.Window.SetSoftInputMode(SoftInput.AdjustPan);
                    InputMethodManager inputMethodManager = mainAct.GetSystemService(Context.InputMethodService) as InputMethodManager;
                    //inputMethodManager.ShowSoftInput(relative_menu_bottom, ShowFlags.Forced);
                    inputMethodManager.ShowSoftInput(edt_to, ShowFlags.Implicit);
                    check_KeyBoard = true;
                }
            }
            catch (System.Exception ex)
            { }
        }
        private void Save(object sender, EventArgs e)
        {
            try
            {
                if (beanUserTicketClone == null)
                {
                    tv_save.Enabled = false;
                    if (!string.IsNullOrEmpty(tv_DepartureFlightFrom.Text) && !string.IsNullOrEmpty(tv_DepartureFlightTo.Text)
                        && !string.IsNullOrEmpty(tv_ETA.Text) && !string.IsNullOrEmpty(tv_ETD.Text)
                        && !string.IsNullOrEmpty(tv_DepartureStationFrom.Text) && !string.IsNullOrEmpty(tv_DepartureStationTo.Text)
                        && !string.IsNullOrEmpty(tv_ArravilStationFrom.Text) && !string.IsNullOrEmpty(tv_ArravilStationTo.Text)
                        && !string.IsNullOrEmpty(edt_from.Text) && !string.IsNullOrEmpty(edt_to.Text))
                    {
                        string dateFrom = tv_DepartureFlightFrom.Text + " " + tv_ETD.Text;
                        DateTime TimeFrom = DateTime.ParseExact(dateFrom, "dd MMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-GB"));
                        if (DateTime.Compare(TimeFrom, DateTime.Now) > 0)
                        {
                            string dateTo = tv_DepartureFlightTo.Text + " " + tv_ETA.Text;
                            DateTime TimeTo = DateTime.ParseExact(dateTo, "dd MMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-GB"));
                            if (DateTime.Compare(TimeTo, TimeFrom) > 0)
                            {
                                SaveSuc(TimeFrom, TimeTo);
                            }
                            else
                            {
                                AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                                alert.SetTitle("Warring");
                                alert.SetMessage("Please choose date of return flight greater than date of departure flight.");
                                alert.SetPositiveButton("Close", (senderAlert, args) =>
                                {
                                    alert.Dispose();
                                });

                                Dialog dialog = alert.Create();
                                dialog.SetCanceledOnTouchOutside(false);
                                dialog.Show();
                            }
                        }
                        else
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                            alert.SetTitle("Warring");
                            alert.SetMessage("Please choose date of departure flight greater than today.");
                            alert.SetPositiveButton("Close", (senderAlert, args) =>
                            {
                                alert.Dispose();
                            });

                            Dialog dialog = alert.Create();
                            dialog.SetCanceledOnTouchOutside(false);
                            dialog.Show();
                        }

                    }
                    else if (string.IsNullOrEmpty(tv_DepartureFlightFrom.Text))
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please choose date of departure flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                    else if (string.IsNullOrEmpty(tv_DepartureFlightTo.Text))
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please choose date of return flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                    else if (string.IsNullOrEmpty(tv_ETA.Text))
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please choose ETA of return flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                    else if (string.IsNullOrEmpty(tv_ETD.Text))
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please choose ETD of departure flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                    else if (string.IsNullOrEmpty(tv_DepartureStationFrom.Text))
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please choose departure station of departure flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                    else if (string.IsNullOrEmpty(tv_DepartureStationTo.Text))
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please choose arrival station of departure flight");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                    else if (string.IsNullOrEmpty(tv_ArravilStationFrom.Text))
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please choose departure station of return flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                    else if (string.IsNullOrEmpty(tv_ArravilStationTo.Text))
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please choose arrival station of return flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                    else if (string.IsNullOrEmpty(edt_from.Text))
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please enter flight no of departure flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                    else if (string.IsNullOrEmpty(edt_to.Text))
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please enter flight no of return flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                }
                else
                {

                    if (check_edit)
                    {
                        tv_save.Text = "Edit";
                        tv_cancel.Visibility = ViewStates.Gone;
                        check_edit = false;
                        SaveEditTicket();
                        notEdit();
                    }
                    else
                    {
                        tv_cancel.Visibility = ViewStates.Visible;
                        tv_save.Text = "Save";
                        check_edit = true;
                        tv_DepartureFlightFrom.Enabled = true;
                        tv_DepartureFlightTo.Enabled = true;
                        tv_ETA.Enabled = true;
                        tv_ETD.Enabled = true;
                        tv_DepartureStationFrom.Enabled = true;
                        tv_DepartureStationTo.Enabled = true;
                        tv_ArravilStationFrom.Enabled = true;
                        tv_ArravilStationTo.Enabled = true;
                        tv_save.Enabled = true;
                        ln_editto.Enabled = true;
                        ln_edtfrom.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                tv_save.Enabled = true;
            }
        }

        private void SaveEditTicket()
        {
            try
            {
                tv_save.Enabled = false;
                if (!string.IsNullOrEmpty(tv_DepartureFlightFrom.Text) && !string.IsNullOrEmpty(tv_DepartureFlightTo.Text)
                    && !string.IsNullOrEmpty(tv_ETA.Text) && !string.IsNullOrEmpty(tv_ETD.Text)
                    && !string.IsNullOrEmpty(tv_DepartureStationFrom.Text) && !string.IsNullOrEmpty(tv_DepartureStationTo.Text)
                    && !string.IsNullOrEmpty(tv_ArravilStationFrom.Text) && !string.IsNullOrEmpty(tv_ArravilStationTo.Text)
                    && !string.IsNullOrEmpty(edt_from.Text) && !string.IsNullOrEmpty(edt_to.Text))
                {
                    string dateFrom = tv_DepartureFlightFrom.Text + " " + tv_ETD.Text;
                    DateTime TimeFrom = DateTime.ParseExact(dateFrom, "dd MMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-GB"));
                    if (DateTime.Compare(TimeFrom, DateTime.Now) > 0)
                    {
                        string dateTo = tv_DepartureFlightTo.Text + " " + tv_ETA.Text;
                        DateTime TimeTo = DateTime.ParseExact(dateTo, "dd MMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-GB"));
                        if (DateTime.Compare(TimeTo, TimeFrom) > 0)
                        {
                            SaveSuc(TimeFrom, TimeTo);
                        }
                        else
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                            alert.SetTitle("Warring");
                            alert.SetMessage("Please choose date of return flight greater than date of departure flight.");
                            alert.SetPositiveButton("Close", (senderAlert, args) =>
                            {
                                alert.Dispose();
                            });

                            Dialog dialog = alert.Create();
                            dialog.SetCanceledOnTouchOutside(false);
                            dialog.Show();
                        }
                    }
                    else
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please choose date of departure flight greater than today.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }

                }
                else if (string.IsNullOrEmpty(tv_DepartureFlightFrom.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                    alert.SetTitle("Warring");
                    alert.SetMessage("Please choose date of departure flight.");
                    alert.SetPositiveButton("Close", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });

                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                }
                else if (string.IsNullOrEmpty(tv_DepartureFlightTo.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                    alert.SetTitle("Warring");
                    alert.SetMessage("Please choose date of return flight.");
                    alert.SetPositiveButton("Close", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });

                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                }
                else if (string.IsNullOrEmpty(tv_ETA.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                    alert.SetTitle("Warring");
                    alert.SetMessage("Please choose ETA of return flight.");
                    alert.SetPositiveButton("Close", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });

                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                }
                else if (string.IsNullOrEmpty(tv_ETD.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                    alert.SetTitle("Warring");
                    alert.SetMessage("Please choose ETD of departure flight.");
                    alert.SetPositiveButton("Close", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });

                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                }
                else if (string.IsNullOrEmpty(tv_DepartureStationFrom.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                    alert.SetTitle("Warring");
                    alert.SetMessage("Please choose departure station of departure flight.");
                    alert.SetPositiveButton("Close", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });

                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                }
                else if (string.IsNullOrEmpty(tv_DepartureStationTo.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                    alert.SetTitle("Warring");
                    alert.SetMessage("Please choose arrival station of departure flight");
                    alert.SetPositiveButton("Close", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });

                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                }
                else if (string.IsNullOrEmpty(tv_ArravilStationFrom.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                    alert.SetTitle("Warring");
                    alert.SetMessage("Please choose departure station of return flight.");
                    alert.SetPositiveButton("Close", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });

                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                }
                else if (string.IsNullOrEmpty(tv_ArravilStationTo.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                    alert.SetTitle("Warring");
                    alert.SetMessage("Please choose arrival station of return flight.");
                    alert.SetPositiveButton("Close", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });

                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                }
                else if (string.IsNullOrEmpty(edt_from.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                    alert.SetTitle("Warring");
                    alert.SetMessage("Please enter flight no of departure flight.");
                    alert.SetPositiveButton("Close", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });

                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                }
                else if (string.IsNullOrEmpty(edt_to.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                    alert.SetTitle("Warring");
                    alert.SetMessage("Please enter flight no of return flight.");
                    alert.SetPositiveButton("Close", (senderAlert, args) =>
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
        }

        private async void SaveSuc(DateTime timeFrom, DateTime timeTo)
        {
            try
            {
                if (beanUserTicketClone != null && lst_ticketDetail.Count > 1)
                {
                    airportDeparFrom = lst_airports.Where(i => i.ID == lst_ticketDetail[0].FromId).FirstOrDefault();
                    airportDeparTo = lst_airports.Where(i => i.ID == lst_ticketDetail[0].ToId).FirstOrDefault();
                    airportArrviFrom = lst_airports.Where(i => i.ID == lst_ticketDetail[1].FromId).FirstOrDefault();
                    airportArrviTo = lst_airports.Where(i => i.ID == lst_ticketDetail[1].ToId).FirstOrDefault();
                    if (airportDeparFrom != null && airportDeparTo != null && airportArrviFrom != null && airportArrviTo != null)
                    {
                        BeanUserTicketDetail ticketDetailDeparture = lst_ticketDetail[0];
                        ticketDetailDeparture.TicketId = 0;
                        ticketDetailDeparture.UserRelationshipId = 0;
                        string routeDeparture = airportDeparFrom.Title + " - " + airportDeparTo.Title;
                        ticketDetailDeparture.ChangBay = routeDeparture;
                        ticketDetailDeparture.UserId = CmmVariable.SysConfig.UserId;
                        ticketDetailDeparture.IdentityNum = CmmVariable.SysConfig.IdentityNum;
                        ticketDetailDeparture.LoaiVe = "";
                        ticketDetailDeparture.NoiXuatVe = "";
                        ticketDetailDeparture.FromId = airportDeparFrom.ID;
                        ticketDetailDeparture.ToId = airportDeparTo.ID;
                        ticketDetailDeparture.DepartureTime = timeFrom.ToUniversalTime();
                        ticketDetailDeparture.FlightNo = edt_from.Text;
                        BeanUserTicketDetail ticketDetailReturn = lst_ticketDetail[1];
                        ticketDetailReturn.TicketId = 0;
                        ticketDetailReturn.UserRelationshipId = 0;
                        string routeReturn = airportArrviFrom.Title + " - " + airportArrviTo.Title;
                        ticketDetailReturn.ChangBay = routeReturn;
                        ticketDetailReturn.UserId = CmmVariable.SysConfig.UserId;
                        ticketDetailReturn.IdentityNum = CmmVariable.SysConfig.IdentityNum;
                        ticketDetailReturn.LoaiVe = "";
                        ticketDetailReturn.NoiXuatVe = "";
                        ticketDetailReturn.FromId = airportArrviFrom.ID;
                        ticketDetailReturn.ToId = airportArrviTo.ID;
                        ticketDetailReturn.DepartureTime = timeTo.ToUniversalTime();
                        ticketDetailReturn.FlightNo = edt_to.Text;
                        lst_ticketDetail = new List<BeanUserTicketDetail>();
                        lst_ticketDetail.Add(ticketDetailDeparture);
                        lst_ticketDetail.Add(ticketDetailReturn);

                        var jsonTicketDetail = JsonConvert.SerializeObject(lst_ticketDetail);

                        beanUserTicketClone.LstTicketDetail = jsonTicketDetail;
                        beanUserTicketClone.Status = 1;
                        CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                        await Task.Run(() =>
                        {
                            ProviderTicket p_ticket = new ProviderTicket();
                            var status = p_ticket.updateTicket(beanUserTicketClone);

                            if (status)
                            {
                                //ProviderBase provider = new ProviderBase();
                                this.ProviderBase().UpdateMasterData<BeanUserTicket>(true, 30, false);
                                this.ProviderBase().UpdateMasterData<BeanUserTicketDetail>(true, 30, false);
                                mainAct.RunOnUiThread(() =>
                                {
                                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                                    alert.SetTitle("Update ticket");
                                    alert.SetMessage("Update ticket success");
                                    alert.SetPositiveButton("Close", (senderAlert, args) =>
                                    {
                                        alert.Dispose();
                                        mainAct.BackFragment();
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
                                    AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                                    alert.SetTitle("Warning");
                                    alert.SetMessage("Update ticket error");
                                    alert.SetPositiveButton("Close", (senderAlert, args) =>
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
                }
                else if (beanUserTicketClone == null)
                {
                    BeanUserTicketDetail ticketDetailDeparture = new BeanUserTicketDetail();
                    ticketDetailDeparture.TicketId = 0;
                    ticketDetailDeparture.UserRelationshipId = 0;
                    string routeDeparture = airportDeparFrom.Title + " - " + airportDeparTo.Title;
                    ticketDetailDeparture.ChangBay = routeDeparture;
                    ticketDetailDeparture.UserId = CmmVariable.SysConfig.UserId;
                    ticketDetailDeparture.IdentityNum = CmmVariable.SysConfig.IdentityNum;
                    ticketDetailDeparture.LoaiVe = "";
                    ticketDetailDeparture.NoiXuatVe = "";
                    ticketDetailDeparture.FromId = airportDeparFrom.ID;
                    ticketDetailDeparture.ToId = airportDeparTo.ID;
                    ticketDetailDeparture.DepartureTime = timeFrom.ToUniversalTime();
                    ticketDetailDeparture.FlightNo = edt_from.Text;
                    BeanUserTicketDetail ticketDetailReturn = new BeanUserTicketDetail();
                    ticketDetailReturn.TicketId = 0;
                    ticketDetailReturn.UserRelationshipId = 0;
                    string routeReturn = airportArrviFrom.Title + " - " + airportArrviTo.Title;
                    ticketDetailReturn.ChangBay = routeReturn;
                    ticketDetailReturn.UserId = CmmVariable.SysConfig.UserId;
                    ticketDetailReturn.IdentityNum = CmmVariable.SysConfig.IdentityNum;
                    ticketDetailReturn.LoaiVe = "";
                    ticketDetailReturn.NoiXuatVe = "";
                    ticketDetailReturn.FromId = airportArrviFrom.ID;
                    ticketDetailReturn.ToId = airportArrviTo.ID;
                    ticketDetailReturn.DepartureTime = timeTo.ToUniversalTime();
                    ticketDetailReturn.FlightNo = edt_to.Text;
                    lst_ticketDetail = new List<BeanUserTicketDetail>();
                    lst_ticketDetail.Add(ticketDetailDeparture);
                    lst_ticketDetail.Add(ticketDetailReturn);

                    var jsonTicketDetail = JsonConvert.SerializeObject(lst_ticketDetail);

                    BeanUserTicket ticket = new BeanUserTicket();
                    ticket.FullName = CmmVariable.SysConfig.DisplayName;
                    ticket.UserId = CmmVariable.SysConfig.UserId;
                    ticket.LstTicketDetail = jsonTicketDetail;
                    ticket.Department = CmmVariable.SysConfig.Department.ToString();
                    ticket.Position = CmmVariable.SysConfig.Position.ToString();
                    ticket.Num = 1;
                    ticket.Status = 1;
                    ticket.CategoryId = 2;

                    if (CmmVariable.SysConfig.IdentityIssueDate.HasValue)
                    {
                        ticket.IdentityIssueDate = CmmVariable.SysConfig.IdentityIssueDate.Value;
                    }
                    else
                    {
                        ticket.IdentityIssueDate = null;
                    }

                    ticket.IdentityIssuePlace = CmmVariable.SysConfig.IdentityIssuePlace != "" ? CmmVariable.SysConfig.IdentityIssuePlace : "IdentityIssuePlace";
                    CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                    await Task.Run(() =>
                    {
                        ProviderTicket p_ticket = new ProviderTicket();
                        var status = p_ticket.sendTicket(ticket);

                        if (status)
                        {
                            //ProviderBase provider = new ProviderBase();
                            this.ProviderBase().UpdateMasterData<BeanUserTicket>(true, 30, false);
                            this.ProviderBase().UpdateMasterData<BeanUserTicketDetail>(true, 30, false);
                            mainAct.RunOnUiThread(() =>
                            {
                                AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                                alert.SetTitle("Create ticket");
                                alert.SetMessage("Create ticket success");
                                alert.SetPositiveButton("Close", (senderAlert, args) =>
                                {
                                    alert.Dispose();
                                    mainAct.BackFragment();
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
                                AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                                alert.SetTitle("Warning");
                                alert.SetMessage("Create ticket error");
                                alert.SetPositiveButton("Close", (senderAlert, args) =>
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
            }
            catch (Exception ex)
            { }
            finally
            {
                CmmDroidFunction.HideProcessingDialog();
            }
        }

        private void GetAirport()
        {
            try
            {
                lst_airports = new List<BeanAirport>();
                //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                string query = @"SELECT * FROM BeanAirport ";
                //lst_airports = conn.Query<BeanAirport>(query);
                //conn.Close();
                lst_airports = SQLiteHelper.GetList<BeanAirport>(query).ListData;
            }
            catch (Exception ex)
            { }
        }

        private void ArravilStationTo(object sender, EventArgs e)
        {
            try
            {
                airport = 4;
                View ScheduleDetail = _inflater.Inflate(Resource.Layout.PopupAirPort, null);
                ImageView img_close = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupAirPort_Close);
                ImageView img_delete = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupAirPort_Delete);
                EditText auto = ScheduleDetail.FindViewById<EditText>(Resource.Id.auto_PopupAirPort);
                ListView lv = ScheduleDetail.FindViewById<ListView>(Resource.Id.lv_PopupAirPort);
                TextView tv_title = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupAirPort_Title);
                LinearLayout ln_edt = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.linear_PopupAirPort_edt);
                auto.FocusableInTouchMode = false;
                auto.Focusable = false;
                auto.RequestFocus();
                ln_edt.Click += (ee, vv) =>
                {
                    ShowKeyBroad(ln_edt, auto);
                };
                //lst_airports = new List<BeanAirport>();
                //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                //string query = @"SELECT * FROM BeanAirport";
                //lst_airports = conn.Query<BeanAirport>(query);
                img_delete.Click += (ee, vv) =>
                {
                    auto.Text = "";
                    Search(auto, lv);
                };
                auto.TextChanged += (ee, vv) =>
                {
                    Search(auto, lv);
                };
                if (lst_airports != null && lst_airports.Count >= 0)
                {
                    airprotadapter = new AirportAdapter(lst_airports, ScheduleDetail.Context);
                    lv.Adapter = airprotadapter;
                    airprotadapter.NotifyDataSetChanged();

                }
                if (ScheduleDetail != null)
                {
                    dig = new Dialog(_rootView.Context);
                    Window window = dig.Window;
                    dig.RequestWindowFeature(1);
                    dig.SetCanceledOnTouchOutside(false);
                    dig.SetCancelable(true);
                    window.SetGravity(GravityFlags.Center);
                    Android.Util.DisplayMetrics dm = Resources.DisplayMetrics;
                    ViewGroup.LayoutParams pa = ScheduleDetail.LayoutParameters;

                    dig.SetContentView(ScheduleDetail);
                    dig.Show();
                    WindowManagerLayoutParams s = window.Attributes;
                    s.Width = dm.WidthPixels;
                    s.Height = dm.HeightPixels - 50;
                    window.Attributes = s;
                }
                lv.ItemClick += (ee, vv) =>
                {
                    Choose(vv.Position, ln_edt, auto);
                    auto.Text = "";
                    auto.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
                    auto.FocusableInTouchMode = false;
                    auto.Focusable = false;
                    auto.RequestFocus();
                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(auto.WindowToken, 0);

                };
                img_close.Click += (ee, vv) =>
                {
                    auto.Text = "";
                    auto.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
                    auto.FocusableInTouchMode = false;
                    auto.Focusable = false;
                    auto.RequestFocus();
                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(auto.WindowToken, 0);
                    dig.Dismiss();
                };
                ScheduleDetail.SetOnTouchListener(mainAct);
            }
            catch (Exception ex)
            { }
        }

        private void ArravilStationFrom(object sender, EventArgs e)
        {
            try
            {
                airport = 3;
                View ScheduleDetail = _inflater.Inflate(Resource.Layout.PopupAirPort, null);
                ImageView img_close = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupAirPort_Close);
                ImageView img_delete = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupAirPort_Delete);
                EditText auto = ScheduleDetail.FindViewById<EditText>(Resource.Id.auto_PopupAirPort);
                ListView lv = ScheduleDetail.FindViewById<ListView>(Resource.Id.lv_PopupAirPort);
                TextView tv_title = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupAirPort_Title);
                LinearLayout ln_edt = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.linear_PopupAirPort_edt);
                auto.FocusableInTouchMode = false;
                auto.Focusable = false;
                auto.RequestFocus();
                ln_edt.Click += (ee, vv) =>
                {
                    ShowKeyBroad(ln_edt, auto);
                };
                //lst_airports = new List<BeanAirport>();
                //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                //string query = @"SELECT * FROM BeanAirport";
                //lst_airports = conn.Query<BeanAirport>(query);
                img_delete.Click += (ee, vv) =>
                {
                    auto.Text = "";
                    Search(auto, lv);
                };
                auto.TextChanged += (ee, vv) =>
                {
                    Search(auto, lv);
                };
                if (lst_airports != null && lst_airports.Count >= 0)
                {
                    airprotadapter = new AirportAdapter(lst_airports, ScheduleDetail.Context);
                    lv.Adapter = airprotadapter;
                    airprotadapter.NotifyDataSetChanged();

                }
                if (ScheduleDetail != null)
                {
                    dig = new Dialog(_rootView.Context);
                    Window window = dig.Window;
                    dig.RequestWindowFeature(1);
                    dig.SetCanceledOnTouchOutside(false);
                    dig.SetCancelable(true);
                    window.SetGravity(GravityFlags.Center);
                    Android.Util.DisplayMetrics dm = Resources.DisplayMetrics;
                    ViewGroup.LayoutParams pa = ScheduleDetail.LayoutParameters;

                    dig.SetContentView(ScheduleDetail);
                    dig.Show();
                    WindowManagerLayoutParams s = window.Attributes;
                    s.Width = dm.WidthPixels;
                    s.Height = dm.HeightPixels - 50;
                    window.Attributes = s;
                }
                lv.ItemClick += (ee, vv) =>
                {
                    Choose(vv.Position, ln_edt, auto);
                    auto.Text = "";
                    auto.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
                    auto.FocusableInTouchMode = false;
                    auto.Focusable = false;
                    auto.RequestFocus();
                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(auto.WindowToken, 0);

                };
                img_close.Click += (ee, vv) =>
                {
                    auto.Text = "";
                    auto.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
                    auto.FocusableInTouchMode = false;
                    auto.Focusable = false;
                    auto.RequestFocus();
                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(auto.WindowToken, 0);
                    dig.Dismiss();
                };
                ScheduleDetail.SetOnTouchListener(mainAct);
            }
            catch (Exception ex)
            { }
        }

        private void DepartureStationTo(object sender, EventArgs e)
        {
            try
            {
                airport = 2;
                View ScheduleDetail = _inflater.Inflate(Resource.Layout.PopupAirPort, null);
                ImageView img_close = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupAirPort_Close);
                ImageView img_delete = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupAirPort_Delete);
                EditText auto = ScheduleDetail.FindViewById<EditText>(Resource.Id.auto_PopupAirPort);
                ListView lv = ScheduleDetail.FindViewById<ListView>(Resource.Id.lv_PopupAirPort);
                TextView tv_title = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupAirPort_Title);
                LinearLayout ln_edt = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.linear_PopupAirPort_edt);
                auto.FocusableInTouchMode = false;
                auto.Focusable = false;
                auto.RequestFocus();
                ln_edt.Click += (ee, vv) =>
                {
                    ShowKeyBroad(ln_edt, auto);
                };
                //lst_airports = new List<BeanAirport>();
                //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                //string query = @"SELECT * FROM BeanAirport";
                //lst_airports = conn.Query<BeanAirport>(query);
                img_delete.Click += (ee, vv) =>
                {
                    auto.Text = "";
                    Search(auto, lv);
                };
                auto.TextChanged += (ee, vv) =>
                {
                    Search(auto, lv);
                };
                if (lst_airports != null && lst_airports.Count >= 0)
                {
                    airprotadapter = new AirportAdapter(lst_airports, ScheduleDetail.Context);
                    lv.Adapter = airprotadapter;
                    airprotadapter.NotifyDataSetChanged();

                }
                lv.ItemClick += (ee, vv) =>
                {
                    Choose(vv.Position, ln_edt, auto);
                    auto.Text = "";
                    auto.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
                    auto.FocusableInTouchMode = false;
                    auto.Focusable = false;
                    auto.RequestFocus();
                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(auto.WindowToken, 0);

                };
                if (ScheduleDetail != null)
                {
                    dig = new Dialog(_rootView.Context);
                    Window window = dig.Window;
                    dig.RequestWindowFeature(1);
                    dig.SetCanceledOnTouchOutside(false);
                    dig.SetCancelable(true);
                    window.SetGravity(GravityFlags.Center);
                    Android.Util.DisplayMetrics dm = Resources.DisplayMetrics;
                    ViewGroup.LayoutParams pa = ScheduleDetail.LayoutParameters;

                    dig.SetContentView(ScheduleDetail);
                    dig.Show();
                    WindowManagerLayoutParams s = window.Attributes;
                    s.Width = dm.WidthPixels;
                    s.Height = dm.HeightPixels - 50;
                    window.Attributes = s;
                }
                img_close.Click += (ee, vv) =>
                {
                    auto.Text = "";
                    auto.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
                    auto.FocusableInTouchMode = false;
                    auto.Focusable = false;
                    auto.RequestFocus();
                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(auto.WindowToken, 0);
                    dig.Dismiss();
                };
                ScheduleDetail.SetOnTouchListener(mainAct);
            }
            catch (Exception ex)
            { }
        }

        private void DepartureStationFrom(object sender, EventArgs e)
        {
            try
            {
                airport = 1;
                View ScheduleDetail = _inflater.Inflate(Resource.Layout.PopupAirPort, null);
                ImageView img_close = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupAirPort_Close);
                ImageView img_delete = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupAirPort_Delete);
                EditText auto = ScheduleDetail.FindViewById<EditText>(Resource.Id.auto_PopupAirPort);
                ListView lv = ScheduleDetail.FindViewById<ListView>(Resource.Id.lv_PopupAirPort);
                TextView tv_title = ScheduleDetail.FindViewById<TextView>(Resource.Id.tv_PopupAirPort_Title);
                LinearLayout ln_edt = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.linear_PopupAirPort_edt);
                auto.FocusableInTouchMode = false;
                auto.Focusable = false;
                auto.RequestFocus();
                ln_edt.Click += (ee, vv) =>
                {
                    ShowKeyBroad(ln_edt, auto);
                };
                //lst_airports = new List<BeanAirport>();
                //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                //string query = @"SELECT * FROM BeanAirport";
                // lst_airports = conn.Query<BeanAirport>(query);
                img_delete.Click += (ee, vv) =>
                {
                    auto.Text = "";
                    Search(auto, lv);
                };
                auto.TextChanged += (ee, vv) =>
                {
                    Search(auto, lv);
                };
                if (lst_airports != null && lst_airports.Count >= 0)
                {
                    airprotadapter = new AirportAdapter(lst_airports, ScheduleDetail.Context);
                    lv.Adapter = airprotadapter;
                    airprotadapter.NotifyDataSetChanged();

                }
                if (ScheduleDetail != null)
                {
                    dig = new Dialog(_rootView.Context);
                    Window window = dig.Window;
                    dig.RequestWindowFeature(1);
                    dig.SetCanceledOnTouchOutside(false);
                    dig.SetCancelable(true);
                    window.SetGravity(GravityFlags.Center);
                    Android.Util.DisplayMetrics dm = Resources.DisplayMetrics;
                    ViewGroup.LayoutParams pa = ScheduleDetail.LayoutParameters;

                    dig.SetContentView(ScheduleDetail);
                    dig.Show();
                    WindowManagerLayoutParams s = window.Attributes;
                    s.Width = dm.WidthPixels;
                    s.Height = dm.HeightPixels - 50;
                    window.Attributes = s;
                }
                lv.ItemClick += (ee, vv) =>
                {
                    Choose(vv.Position, ln_edt, auto);
                    auto.Text = "";
                    auto.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
                    auto.FocusableInTouchMode = false;
                    auto.Focusable = false;
                    auto.RequestFocus();
                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(auto.WindowToken, 0);

                };
                img_close.Click += (ee, vv) =>
                {
                    auto.Text = "";
                    auto.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
                    auto.FocusableInTouchMode = false;
                    auto.Focusable = false;
                    auto.RequestFocus();
                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(auto.WindowToken, 0);
                    dig.Dismiss();
                };
                ScheduleDetail.SetOnTouchListener(mainAct);
            }
            catch (Exception ex)
            { }
        }

        private void ShowKeyBroad(LinearLayout ln_edt, EditText auto)
        {
            try
            {
                ln_edt.Visibility = ViewStates.Gone;
                auto.Focusable = true;
                auto.FocusableInTouchMode = true;
                auto.RequestFocus();
                auto.SetSelectAllOnFocus(true);
                auto.SelectAll();
                mainAct.Window.SetSoftInputMode(SoftInput.AdjustPan);
                InputMethodManager inputMethodManager = mainAct.GetSystemService(Context.InputMethodService) as InputMethodManager;
                //inputMethodManager.ShowSoftInput(relative_menu_bottom, ShowFlags.Forced);
                inputMethodManager.ToggleSoftInput(ShowFlags.Forced, 0);
            }
            catch (Exception ex)
            { }
        }

        private void Choose(int e, LinearLayout ln_edt, EditText auto)
        {
            try
            {
                ln_edt.Visibility = ViewStates.Visible;
                if (airport == 1)
                {
                    airportDeparFrom = new BeanAirport();
                    CmmFunction.MapData(lst_airports[e], airportDeparFrom);
                    tv_DepartureStationFrom.Text = airportDeparFrom.Title;
                    dig.Dismiss();
                }
                else if (airport == 2)
                {
                    airportDeparTo = new BeanAirport();
                    CmmFunction.MapData(lst_airports[e], airportDeparTo);
                    tv_DepartureStationTo.Text = airportDeparTo.Title;
                    dig.Dismiss();
                }
                else if (airport == 3)
                {
                    airportArrviFrom = new BeanAirport();
                    CmmFunction.MapData(lst_airports[e], airportArrviFrom);
                    tv_ArravilStationFrom.Text = airportArrviFrom.Title;
                    dig.Dismiss();
                }
                else if (airport == 4)
                {
                    airportArrviTo = new BeanAirport();
                    CmmFunction.MapData(lst_airports[e], airportArrviTo);
                    tv_ArravilStationTo.Text = airportArrviTo.Title;
                    dig.Dismiss();
                }

            }
            catch (Exception ex)
            { }
        }


        private void Search(EditText auto, ListView lv)
        {
            try
            {
                if (!string.IsNullOrEmpty(auto.Text))
                {
                    string content = auto.Text.ToLowerInvariant();
                    //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                    lst_airports = new List<BeanAirport>();
                    string query_user = string.Format(@"SELECT * FROM BeanAirport WHERE 
                                                        (Title LIKE '%{0}%' OR Description LIKE '%{0}%' OR Code LIKE '%{0}%')", content);
                    //lst_airports = conn.Query<BeanAirport>(query_user);
                    //conn.Close();
                    lst_airports = SQLiteHelper.GetList<BeanAirport>(query_user).ListData;

                    airprotadapter = new AirportAdapter(lst_airports, _rootView.Context);
                    lv.Adapter = airprotadapter;
                    airprotadapter.NotifyDataSetChanged();
                }
                else
                {
                    lst_airports = new List<BeanAirport>();
                    //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                    string query = @"SELECT * FROM BeanAirport";
                    //lst_airports = conn.Query<BeanAirport>(query);
                    //conn.Close();
                    lst_airports = SQLiteHelper.GetList<BeanAirport>(query).ListData;

                    airprotadapter = new AirportAdapter(lst_airports, _rootView.Context);
                    lv.Adapter = airprotadapter;
                    airprotadapter.NotifyDataSetChanged();
                }
            }
            catch (Exception ex)
            { }
        }

        private void ChooseTimeTo(object sender, EventArgs e)
        {
            try
            {
                check_timefrom = false;
                var dialog = new TimePickerDialog(mainAct, TimePickerDialog.ThemeHoloLight, TimePickerDataSet1, DateTime.Now.Hour, DateTime.Now.Minute, false);
                dialog.Show();
            }
            catch (Exception ex)
            { }
        }

        private void ChooseTimeFrom(object sender, EventArgs e)
        {
            try
            {
                check_timefrom = true;
                var dialog = new TimePickerDialog(mainAct, TimePickerDialog.ThemeHoloLight, TimePickerDataSet1, DateTime.Now.Hour, DateTime.Now.Minute, false);
                dialog.Show();
            }
            catch (Exception ex)
            { }
        }
        private void TimePickerDataSet1(object sender, TimePickerDialog.TimeSetEventArgs e)

        {

            int hour = e.HourOfDay;

            int minute = e.Minute;
            if (check_timefrom)
            {
                tv_ETD.Text = string.Format("{0}:{1}", hour.ToString().PadLeft(2, '0'), minute.ToString().PadLeft(2, '0'));
            }
            else
            {
                tv_ETA.Text = string.Format("{0}:{1}", hour.ToString().PadLeft(2, '0'), minute.ToString().PadLeft(2, '0'));
            }

        }
        private void ChooseDateFrom(object sender, EventArgs e)
        {
            try
            {

                //Android.Icu.Util.Calendar calendar = Android.Icu.Util.Calendar.Instance;
                //calendar.Time = Android.Icu.Util.Calendar.Instance.Time;
                //calendar.Add(Android.Icu.Util.Calendar.Date, 40);
                //long minDate = calendar.Time.Time;
                //var validDate = DateTime.Now.AddDays(40);
                //check_datefrom = true;
                //DatePickerDialog datePickerDialog = new DatePickerDialog(mainAct, TimePickerDialog.ThemeHoloLight, TimePickerDataSet1, calendar.Get(Android.Icu.Util.Calendar.Year), calendar.Get(Android.Icu.Util.Calendar.Month), calendar.Get(Android.Icu.Util.Calendar.DayOfMonth));

                //datePickerDialog.DatePicker.MinDate = minDate;
                //datePickerDialog.Show();

                check_datefrom = true;
                //DatePickerDialog datePickerDialog = new DatePickerDialog(mainAct, TimePickerDialog.ThemeHoloLight, TimePickerDataSet1, validDate.Year, validDate.Month - 1, validDate.Day);
                DatePickerDialog datePickerDialog = new DatePickerDialog(mainAct);
                from_date_calendar = Android.Icu.Util.Calendar.Instance;
                from_date_calendar.Time = Android.Icu.Util.Calendar.Instance.Time;
                ////from_date_calendar.Add(Android.Icu.Util.Calendar.Date, 40);
                from_date_calendar.Add(CalendarField.Date, 40);
                long minDate = from_date_calendar.Time.Time;
                datePickerDialog.DatePicker.MinDate = minDate;
                datePickerDialog.Show();
                datePickerDialog.DateSet += TimePickerDataSet1;
            }
            catch (Exception ex)
            { }
        }
        private void TimePickerDataSet1(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            try
            {
                string year = e.Year.ToString().PadLeft(2, '0');
                string month = (e.Month + 1).ToString().PadLeft(2, '0');
                string day = e.DayOfMonth.ToString().PadLeft(2, '0');
                DateTime tgtc = DateTime.ParseExact(day + "/" + month + "/" + year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (DateTime.Compare(DateTime.Now, tgtc.AddHours(23).AddMinutes(59)) < 0)
                {
                    if (check_datefrom)
                    {
                        tv_DepartureFlightFrom.Text = tgtc.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                    }
                    else
                    {
                        tv_DepartureFlightTo.Text = tgtc.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                    }
                }
                else
                {
                    if (check_datefrom)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please reselect date of departure flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }
                    else
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(mainAct);
                        alert.SetTitle("Warring");
                        alert.SetMessage("Please reselect date of return flight.");
                        alert.SetPositiveButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ChooseDateTo(object sender, EventArgs e)
        {
            try
            {
                check_datefrom = false;
                //DatePickerDialog datePickerDialog = new DatePickerDialog(mainAct, TimePickerDialog.ThemeHoloLight, TimePickerDataSet1, validDate.Year, validDate.Month - 1, validDate.Day);
                DatePickerDialog datePickerDialog = new DatePickerDialog(mainAct);
                to_date_calendar = Android.Icu.Util.Calendar.Instance;
                to_date_calendar.Time = from_date_calendar.Time;
                // to_date_calendar.Add(Android.Icu.Util.Calendar.Date, 1);
                to_date_calendar.Add(CalendarField.Date, 1);
                long minDate = to_date_calendar.Time.Time;
                datePickerDialog.DatePicker.MinDate = minDate;
                datePickerDialog.Show();
                datePickerDialog.DateSet += TimePickerDataSet1;
            }
            catch (Exception ex)
            { }
        }

        private void SetData()
        {
            try
            {
                if (beanUserTicketClone == null)
                {
                    tv_cancel.Visibility = ViewStates.Gone;
                    tv_status.Visibility = ViewStates.Invisible;
                    tv_save.Text = "Save";
                    ticketDetailDeparture = new BeanUserTicketDetail();
                    tv_name.Text = CmmVariable.SysConfig.DisplayName;
                    tv_fleet.Text = CmmVariable.SysConfig.DepartmentName;
                    tv_crewcode.Text = CmmVariable.SysConfig.Code;
                    tv_notionality.Text = CmmVariable.SysConfig.Nationality;
                    tv_home.Text = CmmVariable.SysConfig.Address;
                    tv_working.Text = CmmVariable.SysConfig.WorkingPattern;
                    tv_ExpectedDate.Text = "";
                    edt_from.FocusableInTouchMode = false;
                    edt_from.Focusable = false;
                    edt_from.ClearFocus();
                    edt_to.FocusableInTouchMode = false;
                    edt_to.Focusable = false;
                    edt_to.ClearFocus();
                    ln_hide.Visibility = ViewStates.Gone;
                }
                else
                {
                    tv_cancel.Visibility = ViewStates.Gone;
                    tv_save.Text = "Edit";
                    tv_name.Text = CmmVariable.SysConfig.DisplayName;
                    tv_fleet.Text = CmmVariable.SysConfig.DepartmentName;
                    tv_crewcode.Text = CmmVariable.SysConfig.Code;
                    tv_notionality.Text = CmmVariable.SysConfig.Nationality;
                    tv_home.Text = CmmVariable.SysConfig.Address;
                    tv_working.Text = CmmVariable.SysConfig.WorkingPattern;
                    tv_ExpectedDate.Text = "";
                    var statusString = beanUserTicketClone.UserTicketStatus;
                    loadListUserTicketDetail();
                    switch (statusString)
                    {
                        case "Pending":

                            if (beanUserTicketClone.Created.ToLocalTime().AddHours(24) >= DateTime.Now)
                            {
                                tv_status.Visibility = ViewStates.Gone;
                                tv_save.Enabled = true;
                                getMyUserInfo();
                                notEdit();
                            }
                            else
                            {
                                tv_status.Visibility = ViewStates.Gone;
                                tv_save.Visibility = ViewStates.Gone;
                                notEdit();
                                tv_save.Enabled = false;
                            }
                            break;
                        case "Approved":
                            if (beanUserTicketClone.TickedExpectedDate.HasValue)
                            {
                                tv_ExpectedDate.Text = beanUserTicketClone.TickedExpectedDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                            }
                            else
                            {
                                tv_ExpectedDate.Text = "";
                            }
                            tv_status.Text = "Approved";
                            tv_status.Visibility = ViewStates.Visible;
                            tv_status.SetBackgroundResource(Resource.Drawable.textbotronbule);
                            tv_status.SetTextColor(Color.ParseColor("#0073C7"));
                            tv_save.Visibility = ViewStates.Gone;
                            tv_cancel.Visibility = ViewStates.Gone;
                            notEdit();
                            tv_save.Enabled = false;
                            break;
                        case "Cancel":
                            tv_status.Text = "Cancel";
                            tv_status.Visibility = ViewStates.Visible;
                            tv_status.SetBackgroundResource(Resource.Drawable.Textbotronred);
                            tv_status.SetTextColor(Color.ParseColor("#b3000a"));
                            tv_save.Visibility = ViewStates.Gone;
                            tv_cancel.Visibility = ViewStates.Gone;
                            notEdit();
                            tv_save.Enabled = false;
                            break;
                        case "Rejected":
                            tv_status.Text = "Rejected";
                            tv_status.Visibility = ViewStates.Visible;
                            tv_status.SetBackgroundResource(Resource.Drawable.Textbotronred);
                            tv_status.SetTextColor(Color.ParseColor("#b3000a"));
                            tv_save.Visibility = ViewStates.Gone;
                            tv_cancel.Visibility = ViewStates.Gone;
                            notEdit();
                            tv_save.Enabled = false;
                            break;
                        default:

                            break;
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        private void notEdit()
        {
            try
            {
                tv_DepartureFlightFrom.Enabled = false;
                tv_DepartureFlightTo.Enabled = false;
                tv_ETA.Enabled = false;
                tv_ETD.Enabled = false;
                tv_DepartureStationFrom.Enabled = false;
                tv_DepartureStationTo.Enabled = false;
                tv_ArravilStationFrom.Enabled = false;
                tv_ArravilStationTo.Enabled = false;

                ln_editto.Enabled = false;
                ln_edtfrom.Enabled = false;
            }
            catch (Exception ex)
            { }
        }

        public override void OnDestroyView()
        {
            CmmEvent.UserTicketRequest -= CmmEvent_UpdateCount;
            //MainActivity.checkViewListNew = false;
            //MEven.ReloadListNews -= ReloadData;
            base.OnDestroyView();

        }
        private async void getMyUserInfo()
        {

            await Task.Run(() =>
            {
                ProviderUser p_user = new ProviderUser();
                var data = p_user.GetMyUsersInfo();

                if (data != null)
                {
                    mainAct.RunOnUiThread(() =>
                    {
                        numUserTicket = (data.TicketLimitNumber - data.TicketLimitNumberUsed);
                        if (numUserTicket > 0)
                        {
                            //Lbl_num.Text = "Ticket remain: " + numUserTicket + "/" + data.TicketLimitNumber;
                        }
                        else
                        {
                            //Lbl_num.Text = "Ticket remain: ";
                            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                            alert.SetTitle("Vietnam Airlines");
                            alert.SetMessage("You have not ticket remain");
                            alert.SetNegativeButton("Close", (senderAlert, args) =>
                            {
                                alert.Dispose();
                            });

                            Dialog dialog = alert.Create();
                            dialog.SetCanceledOnTouchOutside(false);
                            dialog.Show();
                        }
                    });
                }
                else
                {
                    mainAct.RunOnUiThread(() =>
                    {
                        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                        alert.SetTitle("Vietnam Airlines");
                        alert.SetMessage("Load user info error");
                        alert.SetNegativeButton("Close", (senderAlert, args) =>
                        {
                            alert.Dispose();
                        });

                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                        //Lbl_num.Text = "Ticket remain: ";
                    });
                }

            });
        }
        private void loadListUserTicketDetail()
        {
            try
            {
                if (beanUserTicketClone != null)
                {
                    //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                    string query = @"SELECT * FROM BeanUserTicketDetail WHERE TicketId = ?";
                    //lst_ticketDetail = conn.Query<BeanUserTicketDetail>(query, beanUserTicketClone.ID);
                    //conn.Close();
                    lst_ticketDetail = SQLiteHelper.GetList<BeanUserTicketDetail>(query, beanUserTicketClone.ID).ListData;

                    if (lst_ticketDetail.Count >= 2)
                    {
                        var dateDepartureString = lst_ticketDetail[0].DepartureTime.Value.ToLocalTime().ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                        var timeDepartureString = lst_ticketDetail[0].DepartureTime.Value.ToLocalTime().ToString("HH:mm");
                        string routing = lst_ticketDetail[0].ChangBay;
                        if (routing.Contains("-"))
                        {
                            tv_DepartureStationFrom.Text = routing.Split('-')[0];
                            tv_DepartureStationTo.Text = routing.Split('-')[1];
                        }

                        var dateReturnString = lst_ticketDetail[1].DepartureTime.Value.ToLocalTime().ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                        var timeReturnString = lst_ticketDetail[1].DepartureTime.Value.ToLocalTime().ToString("HH:mm", CultureInfo.CreateSpecificCulture("en-GB"));
                        string routingReturn = lst_ticketDetail[1].ChangBay;
                        if (routingReturn.Contains("-"))
                        {
                            tv_ArravilStationFrom.Text = routingReturn.Split('-')[0];
                            tv_ArravilStationTo.Text = routingReturn.Split('-')[1];
                        }
                        tv_DepartureFlightFrom.Text = dateDepartureString;
                        tv_DepartureFlightTo.Text = dateReturnString;
                        tv_ETD.Text = timeDepartureString;
                        tv_ETA.Text = timeReturnString;
                        edt_from.Text = lst_ticketDetail[0].FlightNo;
                        edt_to.Text = lst_ticketDetail[1].FlightNo;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}