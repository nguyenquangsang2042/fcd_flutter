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
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.Droid.Code.Adapter;
using Android.Graphics;
using Android.Support.V4.Widget;
using VNASchedule.DataProvider;
using System.Threading.Tasks;
using VNASchedule.Droid.Code.Class;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Support.Design.Internal;
using Android.Support.V7.Widget;
using static Android.App.ActionBar;
using Android.Graphics.Drawables;
using SQLite;
using Android.Views.InputMethods;
using Android.Text;
using System.Threading;
using Android.Views.Animations;
using Android.Support.V4.Content;
using static VNASchedule.Droid.Code.Fragment.PilotMainFragment;

namespace VNASchedule.Droid.Code.Fragment
{

    public class NewsFragment : Android.App.Fragment, SwipeRefreshLayout.IOnRefreshListener, View.IOnTouchListener, EditText.IOnEditorActionListener
    {
        //
        private MainActivity mainAct;
        private View _rootView;
        private LayoutInflater _inflater;
        private ImageView img_back;
        private ImageView img_home, img_notification, img_news, img_schedule, img_extend, img_search;
        private TextView tv_news, tv_notifications, tv_newsNum, tv_notificationsNum, tv_traning, tv_license, tv_library, tv_payroll, tv_contact, tv_faq, tv_report, tv_request, tv_cancel;
        private RelativeLayout ln_news, ln_notifications;
        private RecyclerView lv;
        private bool check_news = true;
        private NewsAdapter newsadpter;
        private List<BeanNotify> lst_notify;
        private List<BeanNotify> lst_notifyCount;
        private List<BeanNotify> filtered_news;
        private List<BeanNotify> filtered_notifi;
        private SwipeRefreshLayout swipe;
        private ViewPager viewPager;
        private TabLayout tabLayout;
        private ViewPageNewAdapter viewPagerAdapter;
        private ImageView img_filter_noti;
        private LinearLayout ln_filter_noti;
        private Animation click_animation;
        private string keyNews;
        private TextView txtCountNews;
        private TextView txtCountNotification;
        private ImageView imgSearch;
        private LinearLayout ln_search;
        private LinearLayout bottom_ln_news;
        private LinearLayout ln_nodata;
        private LinearLayout ln_search2;
        private EditText editTextSearch;
        private ImageView img_sort;
        private string sort_type = "";
        private RecyclerNewsAdapter recyclerNewsAdapter;
        private ImageView imgDelete;
        private LinearLayout ln_sort;
        private RadioButton radioButton_defaut, radioButton_unread;
        private RadioButton rd_emergency;
        private RadioButton rd_confirm;
        private TextView tv_default, tv_unread_news;
        private LinearLayout ln_blur;
        private Android.Support.V7.Widget.SearchView searchView;
        private bool showSearchView = false;
        private bool listHasData = true;
        Dialog dialog;
        private RecyclerView recycler_noti_type;
        LinearLayoutManager mLayoutManager;
        private int beanAnnounceID = -1;
        private bool menu_home = false;
        private string queryJobS = "";
        private string queryJob = "";
        private List<BeanNotify> filtered_news_tam;
        public LinearLayoutManager MLayoutManager { get => mLayoutManager; set => mLayoutManager = value; }
        private bool checkMore = true;
        private bool flagReLoad = false;
        private bool flagFirst = true;
        public bool isAllowItemClick;
        private bool isAlowPopupMenuNavigation = true;
        private List<BeanAnnouncementCategory> lst_beanAnnouncementCategory = new List<BeanAnnouncementCategory>();
        private RecyclerNotifyTypeAdapter recyclerNotifyTypeAdapter;

        private RelativeLayout rl_bottom_home;
        private RelativeLayout rl_bottom_safety;
        private RelativeLayout rl_bottom_news;
        private RelativeLayout rl_bottom_schedule;
        private RelativeLayout rl_bottom_extent;

        private int NewsID = Int32.Parse(CmmFunction.GetAppSetting("NEWS_CATEGORY_ID"));
        private int SafetyID = Int32.Parse(CmmFunction.GetAppSetting("SAFETY_CATEGORY_ID"));
        private int QualificationID = Int32.Parse(CmmFunction.GetAppSetting("QUALIFICATION_CATEGORY_ID"));
        private List<string> trainingNotify = new List<string> { "1000", "1010", "5", "0", "3" }; //0 la thong bao nhac nho, ko can hien thi - 3 la Operation thi hien thi ben Safety
        //Thông báo Training tạo từ menu Thông báo : 5
        //Thông báo khi bái kiểm tra sắp diễn ra: 1009
        //Thông báo khi bài kiểm tra thay đổi danh sách học viên: 1008

        public NewsFragment() { }
        public NewsFragment(bool v)
        {
            this.check_news = v;
        }
        public override void OnDestroyView()
        {
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            CmmEvent.UserTicketRequest -= CmmEvent_UpdateCount;
            MainActivity.checkViewListNew = false;
            MEven.ReloadListNews -= ReloadData;
            base.OnDestroyView();
        }
        public override void OnResume()
        {
            base.OnResume();
            //initViewPager();
        }
        public void OnRefresh()
        {

        }
        public bool OnTouch(View v, MotionEvent e)
        {
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            return false;
        }
        public bool OnEditorAction(TextView v, [GeneratedEnum] ImeAction actionId, KeyEvent e)
        {
            CloseKey();
            return true;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            MainActivity.checkViewListNew = true;
            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            flagFirst = true;
            if (_rootView == null)
            {
                mainAct = (MainActivity)this.Activity;
                mainAct.Window.SetSoftInputMode(SoftInput.AdjustPan);
                _inflater = inflater;
                _rootView = inflater.Inflate(Resource.Layout.News, null);
                img_back = _rootView.FindViewById<ImageView>(Resource.Id.img_News_Back);
                img_home = _rootView.FindViewById<ImageView>(Resource.Id.img_home_bottom);
                img_notification = _rootView.FindViewById<ImageView>(Resource.Id.img_notification_bottom);
                img_news = _rootView.FindViewById<ImageView>(Resource.Id.img_news_bottom);
                img_schedule = _rootView.FindViewById<ImageView>(Resource.Id.img_schedule_bottom);
                img_extend = _rootView.FindViewById<ImageView>(Resource.Id.img_extent_bottom);
                tv_news = _rootView.FindViewById<TextView>(Resource.Id.tv_News_News);
                tv_notifications = _rootView.FindViewById<TextView>(Resource.Id.tv_News_Notifications);
                tv_newsNum = _rootView.FindViewById<TextView>(Resource.Id.tv_News_NewsNum);
                txtCountNews = _rootView.FindViewById<TextView>(Resource.Id.txt_count_news);
                tv_notificationsNum = _rootView.FindViewById<TextView>(Resource.Id.tv_News_NotificationsNum);
                ln_news = _rootView.FindViewById<RelativeLayout>(Resource.Id.linear_News_News);
                ln_notifications = _rootView.FindViewById<RelativeLayout>(Resource.Id.linear_News_Notifications);
                txtCountNotification = _rootView.FindViewById<TextView>(Resource.Id.txt_count_notification);
                lv = _rootView.FindViewById<RecyclerView>(Resource.Id.lv_News);
                img_sort = _rootView.FindViewById<ImageView>(Resource.Id.img_News_Sorts);
                swipe = _rootView.FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_News);
                viewPager = _rootView.FindViewById<ViewPager>(Resource.Id.viewpager);
                tabLayout = _rootView.FindViewById<TabLayout>(Resource.Id.tabs);
                ln_nodata = _rootView.FindViewById<LinearLayout>(Resource.Id.ln_EmptyData_News);
                ln_search2 = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_News_Search);
                editTextSearch = _rootView.FindViewById<EditText>(Resource.Id.edt_News_Search);
                imgDelete = _rootView.FindViewById<ImageView>(Resource.Id.img_News_Delete);
                ln_sort = _rootView.FindViewById<LinearLayout>(Resource.Id.ln_sort);
                searchView = _rootView.FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.search_news);
                imgSearch = _rootView.FindViewById<ImageView>(Resource.Id.img_News_Search);
                ln_blur = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_blur);
                ln_search = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_Search_News);
                bottom_ln_news = _rootView.FindViewById<LinearLayout>(Resource.Id.bottom_ln_news);
                //tv_default = _rootView.FindViewById<TextView>(Resource.Id.tv_default);
                //tv_unread_news = _rootView.FindViewById<TextView>(Resource.Id.tv_unread);
                radioButton_defaut = _rootView.FindViewById<RadioButton>(Resource.Id.radioButton_default);
                radioButton_unread = _rootView.FindViewById<RadioButton>(Resource.Id.radioButton_unread);
                rd_emergency = _rootView.FindViewById<RadioButton>(Resource.Id.radioButton_noti_emergency);
                rd_confirm = _rootView.FindViewById<RadioButton>(Resource.Id.radioButton_noti_confirm);
                viewPagerAdapter = new ViewPageNewAdapter(ChildFragmentManager);
                img_filter_noti = _rootView.FindViewById<ImageView>(Resource.Id.img_Noti_filter);
                ln_filter_noti = _rootView.FindViewById<LinearLayout>(Resource.Id.ln_filter_notify);
                recycler_noti_type = _rootView.FindViewById<RecyclerView>(Resource.Id.rcl_filter_notify);
                rl_bottom_home = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_home);
                rl_bottom_safety = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_safety);
                rl_bottom_news = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_news);
                rl_bottom_schedule = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_schedule);
                rl_bottom_extent = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_extent);
                click_animation = AnimationUtils.LoadAnimation(_rootView.Context, Resource.Animation.alpha);
                keyNews = CmmFunction.GetAppSetting("NEWS_CATEGORY_ID");
                img_news.SetColorFilter(new Color(ContextCompat.GetColor(_rootView.Context, Resource.Color.clVNAText)));
                img_news.Enabled = false;
                img_news.Focusable = false;
                txtCountNews.Visibility = ViewStates.Invisible;
                txtCountNotification.Visibility = ViewStates.Invisible;

                MLayoutManager = new LinearLayoutManager(_rootView.Context);
                swipe.SetOnRefreshListener(this);
                swipe.SetDistanceToTriggerSync(150);// in dips                                                         
                swipe.Refresh += HandleRefresh;

                UpdateData();
                //initViewPager();
                img_sort.Click += Sort;
                img_back.Click += Back;
                //Page navigation

                isAllowItemClick = true;
                img_schedule.Focusable = false;
                img_schedule.Enabled = false;
                img_notification.Focusable = false;
                img_notification.Enabled = false;
                img_home.Focusable = false;

                new Handler().PostDelayed(() =>
                {
                    img_schedule.Focusable = true;
                    img_schedule.Enabled = true;
                    img_notification.Focusable = true;
                    img_notification.Enabled = true;
                    img_home.Enabled = true;
                    img_home.Focusable = true;
                }, 500);

                rl_bottom_home.Click += HomePage;

                rl_bottom_schedule.Click += SchedulePage;
                //rl_bottom_extent.Click += PopupNavigationMenu;
                new MoreMenu(new MoreMenuProperties()
                {
                    Fragment = this,
                    RelativeLayoutExtent = rl_bottom_extent,
                });
                rl_bottom_safety.Click += NotificationPage;

                img_filter_noti.Click += Show_Filter;

                ln_news.Click += News;
                ln_notifications.Click += Notifications;
                tv_news.Click += News;
                tv_notifications.Click += Notifications;
                editTextSearch.SetOnEditorActionListener(this);
                MEven.ReloadListNews += ReloadData;
                CmmEvent.UserTicketRequest += CmmEvent_UpdateCount;
                CmmDroidFunction.SetTitleToView(_rootView);
                imgSearch.Click += SearchNews;
                imgDelete.Click += DeleteSeach;
                editTextSearch.TextChanged += Search_New;
                radioButton_defaut.Click += Default_Sort;
                radioButton_unread.Click += UnreadSort;
                rd_confirm.Click += ConfirmClick;
                rd_emergency.Click += EmergencyClick;
                ln_blur.Click += BlurClick;
                lv.SetOnTouchListener(this);
                lv.ScrollChange += (sender, e) =>
                {
                    var visibleItemCount = lv.ChildCount;
                    var totalItemCount = lv.GetAdapter().ItemCount;
                    var pastVisiblesItems = mLayoutManager.FindFirstVisibleItemPosition();
                    if ((visibleItemCount + pastVisiblesItems) >= totalItemCount)
                    {
                        if (checkMore)
                            MoreData(editTextSearch.Text, sort_type, beanAnnounceID);
                    }
                };
                ln_search2.Visibility = ViewStates.Gone;
            }
            else
            {
                flagReLoad = true;
                editTextSearch.Text = "";
                LoadList();
                LoadData();

            }
            flagFirst = false;
            return _rootView;
        }

        private void BlurClick(object sender, EventArgs e)
        {
            if (ln_sort.Visibility == ViewStates.Visible)
                ln_sort.Visibility = ViewStates.Gone;
            if (ln_filter_noti.Visibility == ViewStates.Visible)
                ln_filter_noti.Visibility = ViewStates.Gone;

            ln_blur.Visibility = ViewStates.Gone;
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
        }

        #region Event
        private void Sort(object sender, EventArgs e)
        {
            img_sort.StartAnimation(click_animation);
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            if (ln_sort.Visibility == ViewStates.Gone)
            {
                ln_sort.Visibility = ViewStates.Visible;
                ln_blur.Visibility = ViewStates.Visible;
            }
            else
            {
                ln_blur.Visibility = ViewStates.Gone;
                ln_sort.Visibility = ViewStates.Gone;
            }
            ln_filter_noti.Visibility = ViewStates.Gone;
        }
        private void Back(object sender, EventArgs e)
        {
            img_back.StartAnimation(click_animation);
            mainAct.BackFragment();
        }
        private void ConfirmClick(object sender, EventArgs e)
        {
            img_sort.SetImageResource(Resource.Drawable.icon_sort_check);
            ln_sort.Visibility = ViewStates.Gone;
            sort_type = "confirm";
            ln_blur.Visibility = ViewStates.Gone;
            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);

        }
        private void EmergencyClick(object sender, EventArgs e)
        {
            lst_notify.Clear();
            img_sort.SetImageResource(Resource.Drawable.icon_sort_check);
            ln_sort.Visibility = ViewStates.Gone;
            ln_blur.Visibility = ViewStates.Gone;
            sort_type = "emergency";
            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);
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
                tv_request.Click += RequestClick;
                tv_contact.Click += ContactClick;
                tv_cancel.Click += DismissMenuDialog;
                tv_faq.Click += FaqsClick;
                dialog = new Dialog(_rootView.Context, Resource.Style.Dialog);
                Window window = dialog.Window;
                dialog.RequestWindowFeature(1);
                window.SetGravity(GravityFlags.Bottom);
                Android.Util.DisplayMetrics dm = Resources.DisplayMetrics;
                ViewGroup.LayoutParams pa = ScheduleDetail.LayoutParameters;
                dialog.SetCancelable(false);
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

            try
            {
                dialog.Dismiss();
                mainAct.FragmentManager.PopBackStack();
                mainAct.ShowFragmentAnim(FragmentManager, new ReportsFragment(), "Faqs", 0);
            }
            catch (Exception)
            {

            }

        }
        private void SchedulePage(object sender, EventArgs e)
        {
            try
            {
                Android.App.Fragment fragment = null;
                fragment = new FlightScheduleMDFragment();
                mainAct.FragmentManager.PopBackStack();

                if (CmmVariable.SysConfig.UserType == 1)//1 là phi công 
                {
                    FlightScheduleFragment FlightSchedule = new FlightScheduleFragment();
                    mainAct.ShowFragmentAnim(FragmentManager, FlightSchedule, "FlightSchedule", 0);
                }
                else if (CmmVariable.SysConfig.UserType == 2)//2 là mặt đất
                {
                    FlightScheduleMDFragment FlightScheduleMD = new FlightScheduleMDFragment();
                    mainAct.ShowFragmentAnim(FragmentManager, FlightScheduleMD, "FlightScheduleMD", 0);
                }
            }
            catch (Exception)
            {

            }
        }
        private void NewsPage(object sender, EventArgs e)
        {
            try
            {
                Android.App.Fragment fragment = null;
                fragment = new NewsFragment(true);

                mainAct.FragmentManager.PopBackStack();
                mainAct.ShowFragment(FragmentManager, fragment, "News");
            }
            catch (Exception)
            {

            }
        }
        private void NotificationPage(object sender, EventArgs e)
        {

            try
            {
                FragmentManager fm = mainAct.FragmentManager;
                Android.App.Fragment fragment = null;
                fragment = new NotificationFragment();
                mainAct.FragmentManager.PopBackStack();

                mainAct.ShowFragmentAnim(FragmentManager, fragment, "Notifications", 0);
            }
            catch (Exception)
            {

            }
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
                //if (CmmVariable.sysConfig.Nationality.ToLower() != Contry.ToLower())
                //{
                //    TicketRequestFragment TicketRequest = new TicketRequestFragment();
                //    mainAct.FragmentManager.PopBackStack();
                //    dialog.Dismiss();
                //    mainAct.ShowFragment(FragmentManager, TicketRequest, "TicketRequest");
                //}
                //else
                //{
                //    Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                //    alert.SetTitle("Vietnam Airlines");
                //    if (!string.IsNullOrEmpty(keyNews))
                //    {
                //        alert.SetMessage(keyNews);
                //    }
                //    else
                //    {
                //        alert.SetMessage("Hiện tại việc yêu cầu vé ID cho phi công VN và ngoài khối EU đã được triển khai đăng ký online nên mục yêu cầu vé này chỉ dành cho PCNN trong khối EU. Nếu sau này ứng dụng cập nhật thêm các chức năng mới trong mục này sẽ có thông báo và hướng dẫn sử dụng sau. Trân trọng!");
                //    }
                //    alert.SetNegativeButton("Close", (senderAlert, args) =>
                //    {
                //        alert.Dispose();
                //    });
                //    Dialog dialog = alert.Create();
                //    dialog.SetCanceledOnTouchOutside(false);
                //    dialog.Show();
                //}
            }
            catch (Exception ex)
            { }
        }
        private void FaqsClick(object sender, EventArgs e)
        {
            try
            {
                dialog.Dismiss();
                mainAct.FragmentManager.PopBackStack();

                mainAct.ShowFragmentAnim(FragmentManager, new SupportFragment(), "SupportFragment", 0);
            }
            catch (Exception)
            {

            }
        }
        private void ContactClick(object sender, EventArgs e)
        {
            try
            {
                dialog.Dismiss();
                mainAct.FragmentManager.PopBackStack();
                mainAct.ShowFragmentAnim(FragmentManager, new ContactsFragment(), "Faqs", 0);
            }
            catch (Exception)
            {

            }
        }
        private void PayrollClick(object sender, EventArgs e)
        {
            try
            {
                dialog.Dismiss();

                mainAct.FragmentManager.PopBackStack();
                mainAct.ShowFragmentAnim(FragmentManager, new PayrollFragment(), "Faqs", 0);
            }
            catch (Exception)
            {

            }

        }
        private void LibraryClick(object sender, EventArgs e)
        {
            try
            {
                dialog.Dismiss();
                mainAct.FragmentManager.PopBackStack();
                mainAct.ShowFragmentAnim(FragmentManager, new LibraryFragment(), "Faqs", 0);
            }
            catch (Exception)
            {

            }

        }
        private void LicenseClick(object sender, EventArgs e)
        {
            try
            {
                dialog.Dismiss();
                mainAct.FragmentManager.PopBackStack();
                mainAct.ShowFragmentAnim(FragmentManager, new LicenceFragment(), "Faqs", 0);
            }
            catch (Exception)
            {

            }

        }
        private void TraningClick(object sender, EventArgs e)
        {
            try
            {
                dialog.Dismiss();
                mainAct.FragmentManager.PopBackStack();
                mainAct.ShowFragmentAnim(FragmentManager, new TrainingFragment(), "Faqs", 0);
            }
            catch (Exception)
            {

            }
        }
        private void DeleteSeach(object sender, EventArgs e)
        {
            editTextSearch.Text = "";
        }
        private void SearchNews(object sender, EventArgs e)
        {
            imgSearch.StartAnimation(click_animation);
            if (ln_filter_noti.Visibility == ViewStates.Visible)
            {
                ln_filter_noti.Visibility = ViewStates.Gone;
            }
            if (ln_blur.Visibility == ViewStates.Visible)
            {
                ln_blur.Visibility = ViewStates.Gone;
            }
            if (ln_sort.Visibility == ViewStates.Visible)
            {
                ln_sort.Visibility = ViewStates.Gone;
            }
            if (showSearchView == false)
            {
                ln_search2.Visibility = ViewStates.Visible;

                InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                editTextSearch.RequestFocus();
                inputMethodManager.ShowSoftInput(_rootView, 0);
                inputMethodManager.ToggleSoftInput(ShowFlags.Forced, 0);
                //searchView.SetIconifiedByDefault(false);
                //ln_search.Visibility = ViewStates.Visible;
                showSearchView = true;
            }
            else
            {
                ln_search2.Visibility = ViewStates.Gone;
                //ln_search.Visibility = ViewStates.Gone;
                showSearchView = false;
                editTextSearch.Text = "";

                InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            }
        }
        private void Search_New(object sender, TextChangedEventArgs e)
        {
            if (!flagFirst)
            {
                LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);
            }
        }
        private void ListClick(object sender, int e)
        {
            if (CmmDroidFunction.hasConnection())
            {
                if (isAllowItemClick == true)
                {
                    if (showSearchView == true)
                    {
                        InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                        inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
                    }

                    isAllowItemClick = false;
                    if (lst_notify[e].IsSurveyPoll == 1)
                    {
                        BeanSurvey beanSurvey = new BeanSurvey()
                        {
                            UrlReview = lst_notify[e].Link,
                            Title = lst_notify[e].Title,
                            PerId = lst_notify[e].PermissionId,
                            ActionStatus = lst_notify[e].ActionStatus
                        };

                        DetailExamFragment detailExamFragment = new DetailExamFragment(beanSurvey, true);
                        mainAct.AddFragment(FragmentManager, detailExamFragment, "SurveyListPilot", 1);

                        isAllowItemClick = true;
                    }
                    else
                    {
                        if (lst_notify != null && lst_notify.Count > 0)
                        {
                            if (lst_notify[e].ANStatus.HasValue)
                            {
                                if (lst_notify[e].ANStatus.Value == -1)
                                {

                                }
                                else
                                {
                                    WebNewsFragment WebNews = new WebNewsFragment(this, lst_notify[e]);
                                    mainAct.ShowFragment(FragmentManager, WebNews, "WebNews");
                                }
                            }
                            else
                            {
                                WebNewsFragment WebNews = new WebNewsFragment(this, lst_notify[e]);
                                mainAct.AddFragment(FragmentManager, WebNews, "WebNews", 1);
                            }
                        }
                    }
                }
            }
            else
            {
                mainAct.RunOnUiThread(() =>
                {
                    try
                    {
                        Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(_rootView.Context);
                        alert.SetTitle("VietnamAirlines");
                        alert.SetMessage("Please connect to the internet!");
                        alert.SetCancelable(false);

                        alert.SetNegativeButton("Ok", (senderAlert, args) =>
                        {

                            alert.Dispose();
                        });
                        Dialog dialog = alert.Create();
                        dialog.Show();
                    }
                    catch (Exception)
                    {


                    }
                });
            }
        }
        #endregion

        #region Data
        /// <summary>
        /// Load list filter lnotify
        /// </summary>
        private void LoadNotifyCategory()
        {
            //SQLiteConnection connection = new SQLiteConnection(CmmVariable.M_DataPath);
            try
            {
                //string _query = string.Format(@"SELECT * FROM BeanAnnouncementCategory NOLOCK where ID <> {0} and ID <> {1}", SafetyID, QualificationID);
                //string _query = string.Format(@"SELECT * FROM BeanAnnouncementCategory WHERE ID <> {0} AND ID <> {1} AND ID <> {2} AND ID <> {3} AND ID <> {4} ORDER BY ID",
                //    NewsID, SafetyID, QualificationID, trainingNotify[0], trainingNotify[1]);

                //string _query = string.Format(@"SELECT * FROM BeanAnnouncementCategory WHERE ID NOT IN ({0},{1},{2},{3},{4}) ORDER BY ID",
                //     NewsID, SafetyID, QualificationID, trainingNotify[0], trainingNotify[1]);

                // ID = 3 Operation
                string _query = string.Format(@"SELECT * FROM BeanAnnouncementCategory WHERE ID NOT IN ({0},{1},{2},{3},{4},{5},{6}) ORDER BY Orders",
                SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3], trainingNotify[4]);

                BeanAnnouncementCategory beanAnnouncement = new BeanAnnouncementCategory
                {
                    Title = "All",
                    ID = 0,
                    Modified = null
                };
                lst_beanAnnouncementCategory.Add(beanAnnouncement); // All đứng đầu tiên
                //lst_beanAnnouncementCategory.AddRange(connection.Query<BeanAnnouncementCategory>(_query));
                lst_beanAnnouncementCategory.AddRange(SQLiteHelper.GetList<BeanAnnouncementCategory>(_query).ListData);
                //lst_beanAnnouncementCategory = lst_beanAnnouncementCategory.OrderBy(x => x.ID).ToList();

                if (lst_beanAnnouncementCategory != null && lst_beanAnnouncementCategory.Count >= 0)
                {
                    recycler_noti_type.SetLayoutManager(new LinearLayoutManager(mainAct, LinearLayoutManager.Vertical, false));
                    recyclerNotifyTypeAdapter = new RecyclerNotifyTypeAdapter(lst_beanAnnouncementCategory, _rootView.Context, mainAct);
                    recyclerNotifyTypeAdapter.mSelectedItem = 0; // Set All
                    recyclerNotifyTypeAdapter.ItemClick += NotiFy_Type_Click;
                    recycler_noti_type.SetAdapter(recyclerNotifyTypeAdapter);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                CmmDroidFunction.logErr(ex, "khoahd - LoadNotifyCatagory", this.GetType().Name);
#endif
            }
            //finally
            //{
            //    connection.Close();
            //}
        }
        private void NotiFy_Type_Click(object sender, int e)
        {
            beanAnnounceID = lst_beanAnnouncementCategory[e].ID;
            if (e != 0)
            {
                img_filter_noti.SetImageResource(Resource.Drawable.icon_filter_check);
            }
            else
            {
                img_filter_noti.SetImageResource(Resource.Drawable.icon_filter);

            }
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            ln_filter_noti.Visibility = ViewStates.Gone;
            if (ln_blur.Visibility == ViewStates.Visible)
            {
                ln_blur.Visibility = ViewStates.Gone;
            }
            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);
        }
        private void LoadListAdvance(string keyWord, string sort_type, int filterType)
        {
            //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
            string query = string.Empty;
            keyWord = keyWord.Trim();
            try
            {
                if (!string.IsNullOrEmpty(keyWord)) // Search
                {
                    lst_notify.Clear();
                    if (filterType == 0 || filterType == -1) // Filter default hoặc chưa Filter
                    {
                        switch (sort_type)
                        {
                            case "": // default - date
                                //query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId <> {1} AND AnnounCategoryId <> {2} ORDER BY Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID);
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId NOT IN ({1},{2},{3},{4},{5},{6},{7}) ORDER BY Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3], trainingNotify[4]);
                                break;
                            case "unread":
                                //query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId <> {1} AND AnnounCategoryId <> {2} ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID);
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId NOT IN ({1},{2},{3},{4},{5},{6},{7}) ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3], trainingNotify[4]);
                                break;
                            case "emergency":
                                //query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId <> {1} AND AnnounCategoryId <> {2}  ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID);
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId NOT IN ({1},{2},{3},{4},{5},{6},{7}) ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3], trainingNotify[4]);
                                break;
                            case "confirm":
                                //query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId <> {1} AND AnnounCategoryId <> {2} ORDER BY   flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID);
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId NOT IN ({1},{2},{3},{4},{5},{6},{7}) ORDER BY flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3], trainingNotify[4]);
                                break;
                        }
                    }
                    else
                    {
                        switch (sort_type)
                        {
                            case "":  // default - date
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE SearCol  LIKE '%{0}%' AND ANStatus <> -1  AND AnnounCategoryId = {1} ORDER BY Created DESC LIMIT ? OFFSET ? ", keyWord, beanAnnounceID);
                                break;
                            case "unread":
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId = {1}  ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ? ", keyWord, beanAnnounceID);
                                break;
                            case "emergency":
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId = {1} ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ? ", keyWord, beanAnnounceID);
                                break;
                            case "confirm":
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId = {1} ORDER BY   flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ?", keyWord, beanAnnounceID);
                                break;
                        }
                    }

                }
                else // No Search
                {
                    if (filterType == 0 || filterType == -1) // Filter default hoặc chưa Filter
                    {
                        if (string.IsNullOrEmpty(keyNews)) keyNews = "7";
                        switch (sort_type)
                        {
                            case "":  // default - date
                                //query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId <> {1} ORDER BY Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID);
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId NOT IN ({0},{1},{2},{3},{4},{5},{6}) ORDER BY Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3], trainingNotify[4]);
                                break;
                            case "unread":
                                //query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId <> {1}  ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID);
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId NOT IN ({0},{1},{2},{3},{4},{5},{6}) ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3], trainingNotify[4]);
                                break;
                            case "emergency":
                                //query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId <> {1} ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID);
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId NOT IN ({0},{1},{2},{3},{4},{5},{6}) ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3], trainingNotify[4]);
                                break;
                            case "confirm":
                                //query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId <> {1} ORDER BY  flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID);
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND  AnnounCategoryId NOT IN ({0},{1},{2},{3},{4},{5},{6}) ORDER BY  flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3], trainingNotify[4]);
                                break;
                        }
                    }
                    else
                    {
                        switch (sort_type)
                        {
                            case "":  // default - date
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId = {0}  ORDER BY Created DESC LIMIT ? OFFSET ?", beanAnnounceID);
                                break;
                            case "unread":
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId = {0}  ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ?", beanAnnounceID);
                                break;
                            case "emergency":
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId = {0} ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ?", beanAnnounceID);
                                break;
                            case "confirm":
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId = {0} ORDER BY  flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ?", beanAnnounceID);
                                break;
                        }
                    }
                }
                //lst_notify = conn.Query<BeanNotify>(query, 20, 0);
                lst_notify = SQLiteHelper.GetList<BeanNotify>(query, 20, 0).ListData;
                if (lst_notify.Count >= 20)
                    checkMore = true;
                else
                    checkMore = false;
                initRecyclerView(keyWord);
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    conn.Close();
            //}
        }
        public void initRecyclerView(string searchKey)
        {
            if (lst_notify != null && lst_notify.Count > 0)
            {
                ln_nodata.Visibility = ViewStates.Gone;

                recyclerNewsAdapter = new RecyclerNewsAdapter(lst_notify, _rootView.Context, mainAct, searchKey);
                lv.SetAdapter(recyclerNewsAdapter);
                recyclerNewsAdapter.ItemClick += ListClick;
                recyclerNewsAdapter.NotifyDataSetChanged();
                lv.Visibility = ViewStates.Visible;
            }
            else
            {
                ln_nodata.Visibility = ViewStates.Visible;
                lv.Visibility = ViewStates.Gone;
            }
        }
        private void Show_Filter(object sender, EventArgs e)
        {
            try
            {
                // Handle filter
                img_filter_noti.StartAnimation(click_animation);

                if (ln_filter_noti.Visibility == ViewStates.Gone)
                {
                    ln_blur.Visibility = ViewStates.Visible;
                    ln_filter_noti.Visibility = ViewStates.Visible;
                }
                else
                {
                    ln_blur.Visibility = ViewStates.Gone;
                    ln_filter_noti.Visibility = ViewStates.Gone;
                }

                ln_sort.Visibility = ViewStates.Gone;
            }
            catch (Exception ex)
            {
#if DEBUG
                CmmDroidFunction.logErr(ex, "khoahd - Show_Filter", this.GetType().Name);
#endif
            }
        }
        private void Default_Sort(object sender, EventArgs e)
        {
            img_sort.SetImageResource(Resource.Drawable.icon_sort_descending);
            ln_sort.Visibility = ViewStates.Gone;
            ln_blur.Visibility = ViewStates.Gone;
            sort_type = "";
            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);

        }
        private void UnreadSort(object sender, EventArgs e)
        {
            img_sort.SetImageResource(Resource.Drawable.icon_sort_check);
            ln_sort.Visibility = ViewStates.Gone;
            ln_blur.Visibility = ViewStates.Gone;
            sort_type = "unread";
            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);

        }
        private void MoreData(string keyWord, string sortType, int filterType)
        {
            //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
            try
            {
                string query = string.Empty;
                keyWord = keyWord.Trim();
                if (!string.IsNullOrEmpty(keyWord))
                {
                    if (filterType == 0 || filterType == -1)
                    {
                        switch (sort_type)
                        {
                            case "": // default - date
                                     //query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId <> {1} AND AnnounCategoryId <> {2} ORDER BY Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID);
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId NOT IN ({1},{2},{3},{4},{5},{6}) ORDER BY Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3]);
                                break;
                            case "unread":
                                //query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId <> {1} AND AnnounCategoryId <> {2} ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID);
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId NOT IN ({1},{2},{3},{4},{5},{6}) ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3]);
                                break;
                            case "emergency":
                                //query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId <> {1} AND AnnounCategoryId <> {2}  ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID);
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId NOT IN ({1},{2},{3},{4},{5},{6}) ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3]);
                                break;
                            case "confirm":
                                //query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId <> {1} AND AnnounCategoryId <> {2} ORDER BY   flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID);
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND ANStatus <> -1 AND AnnounCategoryId NOT IN ({1},{2},{3},{4},{5},{6}) ORDER BY flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ? ", keyWord, SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3]);
                                break;
                        }
                    }
                    else
                    {
                        switch (sort_type)
                        {
                            case "": // default - date
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE SearCol  LIKE '%{0}%' AND   ANStatus <> -1  AND AnnounCategoryId = {1} ORDER BY Created DESC LIMIT ? OFFSET ? ", keyWord, beanAnnounceID);
                                break;
                            case "unread":
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND   ANStatus <> -1 AND AnnounCategoryId = {1}  ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ? ", keyWord, beanAnnounceID);
                                break;
                            case "emergency":
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND   ANStatus <> -1 AND AnnounCategoryId = {1} ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ? ", keyWord, beanAnnounceID);
                                break;
                            case "confirm":
                                query = string.Format("SELECT * FROM BeanNotify NOLOCK WHERE  SearCol  LIKE '%{0}%' AND   ANStatus <> -1 AND AnnounCategoryId = {1} ORDER BY   flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ?", keyWord, beanAnnounceID);
                                break;
                        }
                    }
                }
                else
                {
                    if (filterType == 0 || filterType == -1)
                    {

                        if (string.IsNullOrEmpty(keyNews)) keyNews = "7";
                        switch (sort_type)
                        {
                            case "":  // default - date
                                      //query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId <> {1} ORDER BY Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID);
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId NOT IN ({0},{1},{2},{3},{4},{5}) ORDER BY Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3]);
                                break;
                            case "unread":
                                //query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId <> {1}  ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID);
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId NOT IN ({0},{1},{2},{3},{4},{5}) ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3]);
                                break;
                            case "emergency":
                                //query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId <> {1} ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID);
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId NOT IN ({0},{1},{2},{3},{4},{5}) ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3]);
                                break;
                            case "confirm":
                                //query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId <> {1} ORDER BY  flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID);
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND  AnnounCategoryId NOT IN ({0},{1},{2},{3},{4},{5}) ORDER BY  flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3]);
                                break;
                        }

                    }
                    else
                    {
                        switch (sort_type)
                        {
                            case "": // default - date
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId = {0}  ORDER BY Created DESC LIMIT ? OFFSET ?", beanAnnounceID);
                                break;
                            case "unread":
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId = {0}  ORDER BY FlgRead,Created DESC LIMIT ? OFFSET ?", beanAnnounceID);
                                break;
                            case "emergency":
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId = {0} ORDER BY flgImmediately DESC ,Created DESC LIMIT ? OFFSET ?", beanAnnounceID);
                                break;
                            case "confirm":
                                query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId = {0} ORDER BY  flgConfirm DESC, flgConfirmed   ,Created DESC LIMIT ? OFFSET ?", beanAnnounceID);
                                break;
                        }


                    }
                }
                //var tam = conn.Query<BeanNotify>(query, 20, lst_notify.Count);
                var tam = SQLiteHelper.GetList<BeanNotify>(query, 20, lst_notify.Count).ListData;
                lst_notify.AddRange(tam);
                recyclerNewsAdapter.NotifyDataSetChanged();

                //lv.RemoveFooterView(footerLv);
                if (tam.Count < 20)
                {
                    checkMore = false;
                }
                else
                {
                    checkMore = true;
                }
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    conn.Close();
            //}
        }
        private void DismissMenuDialog(object sender, EventArgs e)
        {
            isAlowPopupMenuNavigation = true;
            dialog.Dismiss();
        }
        public void initViewPager()
        {
            viewPager.SaveEnabled = false;

            viewPager.Adapter = viewPagerAdapter;
            tabLayout.SetupWithViewPager(viewPager);
            viewPagerAdapter.NotifyDataSetChanged();


        }
        private void CmmEvent_UpdateCount(object sender, EventArgs e)
        {
            UpdateData();
        }
        private async void UpdateData()
        {
            try
            {

                if (CmmVariable.M_IS_SAFETY_QUALIFICATION_DEPARTMENT)
                    bottom_ln_news.WeightSum = 5;

                CmmDroidFunction.showProcessingDialog("Loading...", mainAct, false);
                await Task.Run(() =>
                {
                    mainAct.providerBase.UpdateMasterData<BeanNotify>(true);
                    mainAct.RunOnUiThread(() =>
                    {
                        CmmDroidFunction.HideProcessingDialog();
                        LoadList();
                        LoadData();
                        LoadNotifyCategory();
                    });
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
                    CmmDroidFunction.HideProcessingDialog();
                });
            }
        }
        private void ReloadData(object sender, MEven.ChangeListNewEventArgs e)
        {
            check_news = e.IsSuccess;
            LoadList();
            LoadData();
        }
        private async void HandleRefresh(object sender, EventArgs e)
        {
            try
            {
                ln_search.Visibility = ViewStates.Gone;
                showSearchView = false;
                searchView.SetQuery("", false);
                ln_nodata.Visibility = ViewStates.Gone;
                ln_search2.Visibility = ViewStates.Gone;
                InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
                editTextSearch.Text = "";
                swipe.Refreshing = true;
                ProviderBase p_base = new ProviderBase();
                await Task.Run(() =>
                {
                    p_base.UpdateMasterData<BeanNotify>(true);
                    mainAct.RunOnUiThread(() =>
                    {
                        LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);
                        LoadData();
                        swipe.Refreshing = false;
                        swipe.Enabled = true;
                    });
                });
            }
            catch (Exception ex)
            {
                swipe.Refreshing = false;
                swipe.Enabled = true;
            }
        }
        private void Notifications(object sender, EventArgs e)
        {
            check_news = false;
            ln_notifications.SetBackgroundResource(Resource.Drawable.textbotronblue);
            tv_notifications.SetTextColor(Color.ParseColor("#ffffff"));
            ln_news.SetBackgroundResource(Resource.Drawable.textbotrovienblue);
            tv_news.SetTextColor(Color.ParseColor("#0073C7"));
            LoadData();
        }
        private void News(object sender, EventArgs e)
        {
            check_news = true;
            ln_news.SetBackgroundResource(Resource.Drawable.textbotronblue);
            tv_news.SetTextColor(Color.ParseColor("#ffffff"));
            ln_notifications.SetBackgroundResource(Resource.Drawable.textbotrovienblue);
            tv_notifications.SetTextColor(Color.ParseColor("#0073C7"));
            LoadData();
        }
        private void LoadList()
        {
            //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
            try
            {
                ////string query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE  ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId <> {1}  ORDER BY Created DESC LIMIT ? OFFSET ?", SafetyID, QualificationID);

                string _defaultQuery = string.Format("SELECT * FROM BeanNotify WHERE AnnounCategoryId NOT IN ({0},{1},{2},{3},{4},{5},{6}) AND ANStatus <> -1 ORDER BY Created DESC LIMIT ? OFFSET ?",
                    SafetyID, QualificationID, trainingNotify[0], trainingNotify[1], trainingNotify[2], trainingNotify[3], trainingNotify[4]);

                //lst_notify = conn.Query<BeanNotify>(_defaultQuery, 20, 0);
                lst_notify = SQLiteHelper.GetList<BeanNotify>(_defaultQuery, 20, 0).ListData;
                if (lst_notify.Count >= 20)
                {
                    checkMore = true;
                }
                else
                {
                    checkMore = false;
                }
            }
            catch (Exception ex)
            {
                CmmDroidFunction.HideProcessingDialog();
            }
            //finally
            //{
            //    conn.Close();
            //}

        }
        private void LoadData()
        {
            //var conn = new SQLiteConnection(CmmVariable.M_DataPath);
            try
            {
                if (lst_notify != null && lst_notify.Count >= 0)
                {
                    string queryTempNews = string.Format(@"SELECT COUNT(*) AS CountUnReadNews FROM BeanNotify NOLOCK WHERE AnnounCategoryId NOT IN ({0},{1},{2},{3},{4},{5},3) AND FlgRead = 0 AND ANStatus <> -1 ORDER BY Created DESC", SafetyID, QualificationID, mainAct.trainingNotify[0], mainAct.trainingNotify[1], mainAct.trainingNotify[2], mainAct.trainingNotify[3]);
                    //var tempNews = conn.Query<CountNum>(queryTempNews);
                    var tempNews = SQLiteHelper.GetList<CountNum>(queryTempNews).ListData;
                    CmmVariable.M_NewsCount = tempNews[0].CountUnReadNews;

                    if (CmmVariable.M_NewsCount != 0)
                    {
                        if (CmmVariable.M_NewsCount >= 100)
                        {
                            txtCountNews.SetTextSize(ComplexUnitType.Sp, 8);
                        }
                        txtCountNews.Visibility = ViewStates.Visible;
                        txtCountNews.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NewsCount);
                    }
                    else
                    {
                        txtCountNews.Visibility = ViewStates.Invisible;
                    }
                    if (CmmVariable.M_NotiCount != 0)
                    {
                        txtCountNotification.Visibility = ViewStates.Visible;
                        txtCountNotification.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NotiCount);
                        tv_newsNum.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NewsCount);
                    }
                    else
                    {

                        txtCountNotification.Visibility = ViewStates.Invisible;
                    }
                    tv_notificationsNum.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NotiCount);

                    if (check_news)
                    {
                        ln_news.SetBackgroundResource(Resource.Drawable.textbotronblue);
                        tv_news.SetTextColor(Color.ParseColor("#ffffff"));
                        ln_notifications.SetBackgroundResource(Resource.Drawable.textbotrovienblue);
                        tv_notifications.SetTextColor(Color.ParseColor("#0073C7"));
                        //filtered_news = lst_notify.Where(x => x.AnnounCategoryId.ToString() == keyNews).ToList();
                        //int a = lst_notify.Where(x => x.AnnounCategoryId.ToString() == keyNews && x.FlgRead == false).Count();
                        if (lst_notify != null && lst_notify.Count >= 0)
                        {
                            lv.SetLayoutManager(MLayoutManager);
                            recyclerNewsAdapter = new RecyclerNewsAdapter(lst_notify, _rootView.Context, mainAct, "");
                            recyclerNewsAdapter.ItemClick += ListClick;
                            lv.SetAdapter(recyclerNewsAdapter);
                        }
                        if (lst_notify.Count >= 20)
                        {
                            checkMore = true;
                        }
                        else
                        {
                            checkMore = false;
                        }
                    }
                    else
                    {
                        ln_notifications.SetBackgroundResource(Resource.Drawable.textbotronblue);
                        tv_notifications.SetTextColor(Color.ParseColor("#ffffff"));
                        ln_news.SetBackgroundResource(Resource.Drawable.textbotrovienblue);
                        tv_news.SetTextColor(Color.ParseColor("#0073C7"));
                        //filtered_notifi = lst_notify.Where(x => x.AnnounCategoryId.ToString() != keyNews).ToList();
                        //int b = lst_notify.Where(x => x.AnnounCategoryId.ToString() != keyNews && x.FlgRead == false).Count();
                        if (lst_notify != null && lst_notify.Count >= 0)
                        {
                            lv.SetLayoutManager(MLayoutManager);
                            recyclerNewsAdapter = new RecyclerNewsAdapter(lst_notify, _rootView.Context, mainAct, "");
                            lv.SetAdapter(recyclerNewsAdapter);
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            //finally
            //{
            //    conn.Close();
            //}
        }
        private void CloseKey()
        {
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
        }
        #endregion
    }
}