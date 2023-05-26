using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Views.InputMethods;
using Android.Widget;
using SQLite;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.DataProvider;
using VNASchedule.Droid.Code.Adapter;
using VNASchedule.Droid.Code.Class;

namespace VNASchedule.Droid.Code.Fragment
{
    public class NotificationFragment : Android.App.Fragment, View.IOnTouchListener, EditText.IOnEditorActionListener
    {
        private bool check_news = true;
        private MainActivity mainAct;
        private LayoutInflater _inflater;
        private Dialog dialog;
        private ImageView img_home, img_notification, img_news, img_schedule, img_extend, img_search, img_Safety_filter;
        private LinearLayout lnNodata;
        private List<BeanNotify> lst_notify;
        //private List<BeanNotify> filtered_notifi;
        //private List<BeanNotify> filtered_notifiS;
        private RecyclerView listView;
        private SwipeRefreshLayout swipe;
        private NewsAdapter newsadpter;
        private ImageView imgBack;
        private RecyclerNotifyAdapter recyclerAdapter;
        private RecyclerView rcl_filter_notify;
        private LinearLayout linear_top_bar;
        private LinearLayout ln_filter;
        private TextView tv_request;
        private TextView tv_news, tv_notifications, tv_newsNum, tv_notificationsNum, tv_traning, tv_license, tv_library, tv_payroll, tv_contact, tv_faq, tv_report;
        private TextView tv_cancel;
        private TextView tv_count_notification;
        private TextView tv_count_news;
        private LinearLayout ln_search;
        private LinearLayout ln_search2;
        private EditText editTextSearch;
        private TextView tv_bottom_safety;
        private LinearLayout ln_sort_notify;
        private ImageView img_sort;
        private RadioButton rd_defaut, rd_unread, rd_confirm, rd_emergency;
        private LinearLayout ln_blur_noti;
        private Animation click_animation;
        private string beanAnnounceID;
        private RecyclerNotifyTypeAdapter recyclerNotifyTypeAdapter;
        Animation slideUp;
        Animation slideDown;
        private string sort_type = "";
        LinearLayoutManager mLayoutManager;
        private ImageView imgDelete;
        public LinearLayoutManager MLayoutManager { get => mLayoutManager; set => mLayoutManager = value; }
        public LinearLayoutManager MLayoutManager_Noti_Type { get => mLayoutManager; set => mLayoutManager = value; }
        int firstItem = 0;
        private string icon_url;
        private String queryJobS = "";
        private bool showSearchView = false;
        private Android.Support.V7.Widget.SearchView searchView;
        private LinearLayout bottom_ln_news;
        View view, footerLv;

        private string queryJob = "";
        private bool checkMore;
        int mLastFirstVisibleItem;
        private List<BeanAnnouncementCategory> lst_beanAnnouncementCategory;
        private List<string> DefaultSafety = new List<string>();
        private bool isFilterClicked;
        private string keyNews = "";
        public bool isAllowItemClick;
        private bool isAlowPopupMenuNavigation = true;
        private LinearLayout linear_Tab;
        private TextView tabSafety;
        private TextView tabQualification;

        private bool SafetyFlag = true;
        private Color clwhite;
        private Color darker_gray;
        private Color headerBlue;
        private RelativeLayout rl_bottom_home;
        private RelativeLayout rl_bottom_safety;
        private RelativeLayout rl_bottom_news;
        private RelativeLayout rl_bottom_schedule;
        private RelativeLayout rl_bottom_extent;
        private Color clgray;
        public NotificationFragment() { }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
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
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (view == null)
            {
                view = inflater.Inflate(Resource.Layout.Notification, container, false);
                _inflater = inflater;
                mainAct = (MainActivity)this.Activity;
                listView = view.FindViewById<RecyclerView>(Resource.Id.lv_Notification);
                swipe = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_Notification);
                imgBack = view.FindViewById<ImageView>(Resource.Id.img_Notification_Back);
                lnNodata = view.FindViewById<LinearLayout>(Resource.Id.ln_EmptyData_Notifications);
                img_home = view.FindViewById<ImageView>(Resource.Id.img_home_bottom);
                img_notification = view.FindViewById<ImageView>(Resource.Id.img_notification_bottom);
                img_news = view.FindViewById<ImageView>(Resource.Id.img_news_bottom);
                img_schedule = view.FindViewById<ImageView>(Resource.Id.img_schedule_bottom);
                tv_count_notification = view.FindViewById<TextView>(Resource.Id.txt_count_notification);
                tv_count_news = view.FindViewById<TextView>(Resource.Id.txt_count_news);
                ln_search = view.FindViewById<LinearLayout>(Resource.Id.linear_Search_Noti);
                img_extend = view.FindViewById<ImageView>(Resource.Id.img_extent_bottom);
                img_search = view.FindViewById<ImageView>(Resource.Id.img_Noti_Search);
                ln_search2 = view.FindViewById<LinearLayout>(Resource.Id.linear_Noti_Search);
                editTextSearch = view.FindViewById<EditText>(Resource.Id.edt_Noti_Search);
                tv_bottom_safety = view.FindViewById<TextView>(Resource.Id.tv_bottom_safety);
                imgDelete = view.FindViewById<ImageView>(Resource.Id.img_Noti_Delete);
                MLayoutManager = new LinearLayoutManager(view.Context);
                MLayoutManager_Noti_Type = new LinearLayoutManager(view.Context);
                ln_sort_notify = view.FindViewById<LinearLayout>(Resource.Id.ln_sort_notify);
                img_sort = view.FindViewById<ImageView>(Resource.Id.img_Noti_Sorts);
                rd_defaut = view.FindViewById<RadioButton>(Resource.Id.radioButton_noti_default);
                rd_unread = view.FindViewById<RadioButton>(Resource.Id.radioButton_noti_unread);
                rd_emergency = view.FindViewById<RadioButton>(Resource.Id.radioButton_noti_emergency);
                rd_confirm = view.FindViewById<RadioButton>(Resource.Id.radioButton_noti_confirm);
                ln_blur_noti = view.FindViewById<LinearLayout>(Resource.Id.linear_blur_noti);
                linear_top_bar = view.FindViewById<LinearLayout>(Resource.Id.linear_top_bar);
                slideUp = AnimationUtils.LoadAnimation(view.Context, Resource.Animation.slide_up);
                slideDown = AnimationUtils.LoadAnimation(view.Context, Resource.Animation.slide_down);
                click_animation = AnimationUtils.LoadAnimation(view.Context, Resource.Animation.alpha);
                linear_Tab = view.FindViewById<LinearLayout>(Resource.Id.linear_Tab);
                tabSafety = view.FindViewById<TextView>(Resource.Id.tabSafety);
                tabQualification = view.FindViewById<TextView>(Resource.Id.tabQualification);
                img_Safety_filter = view.FindViewById<ImageView>(Resource.Id.img_Safety_filter);
                ln_filter = view.FindViewById<LinearLayout>(Resource.Id.fy);
                rcl_filter_notify = view.FindViewById<RecyclerView>(Resource.Id.rcl_filter_notify);

                rl_bottom_home = view.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_home);
                rl_bottom_safety = view.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_safety);
                rl_bottom_news = view.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_news);
                rl_bottom_schedule = view.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_schedule);
                rl_bottom_extent = view.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_extent);
                try
                {
                    // Default
                    DefaultSafety.Add(Int32.Parse(CmmFunction.GetAppSetting("SAFETY_CATEGORY_ID")).ToString());
                    DefaultSafety.Add(Int32.Parse(CmmFunction.GetAppSetting("QUALIFICATION_CATEGORY_ID")).ToString());
                    beanAnnounceID = "'" + String.Join("','", DefaultSafety.ToArray()) + "'";
                }
                catch
                {

                }

                img_notification.SetColorFilter(new Color(ContextCompat.GetColor(view.Context, Resource.Color.clVNAText)));
                tv_bottom_safety.SetTextColor(new Color(ContextCompat.GetColor(view.Context, Resource.Color.clVNAText)));
                img_notification.Focusable = false;
                img_notification.Enabled = false;
                lst_beanAnnouncementCategory = new List<BeanAnnouncementCategory>();
                searchView = view.FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.search_noti);
                bottom_ln_news = view.FindViewById<LinearLayout>(Resource.Id.bottom_ln_news);
                swipe.SetDistanceToTriggerSync(150);// in dips                                                         
                CmmDroidFunction.SetTitleToView(view);

                UpdateData();

                img_schedule.Focusable = false;
                img_schedule.Enabled = false;
                img_home.Focusable = false;
                img_home.Enabled = false;
                img_news.Focusable = false;
                img_news.Enabled = false;
                isAllowItemClick = true;
                Handler h = new Handler();
                Action myAction = () =>
                {
                    img_schedule.Focusable = true;
                    img_schedule.Enabled = true;
                    img_news.Focusable = true;
                    img_news.Enabled = true;
                    img_home.Enabled = true;
                    img_home.Focusable = true;
                };
                h.PostDelayed(myAction, 500);

                #region delegate
                rl_bottom_home.Click += HomePage;
                rl_bottom_news.Click += NewsPage;
                rl_bottom_schedule.Click += SchedulePage;
                //rl_bottom_extent.Click += PopupNavigationMenu;
                new MoreMenu(new MoreMenuProperties()
                {
                    Fragment = this,
                    RelativeLayoutExtent = rl_bottom_extent,
                });
                imgDelete.Click += DeleteSeach;
                ln_blur_noti.Click += LnBlur_Click;
                editTextSearch.TextChanged += SearchNoti;
                editTextSearch.SetOnEditorActionListener(this);
                img_search.Click += SearchNotify;
                img_sort.Click += ShowLinearSort;
                listView.ScrollChange += ListViewOnScroll;
                listView.SetOnTouchListener(this);
                rd_defaut.Click += DefautSort;
                rd_unread.Click += UnreadSort;
                rd_confirm.Click += ConfirmClick;
                rd_emergency.Click += EmergencyClick;
                MEven.ReloadListNews += ReloadData;
                CmmEvent.UserTicketRequest += CmmEvent_UpdateCount;
                imgBack.Click += Back;
                swipe.Refresh += HandleRefreshAsync;
                tabSafety.Click += TabSafety_Click;
                tabQualification.Click += TabQualification_Click;
                img_Safety_filter.Click += ShowFilter;
                #endregion

                ln_search2.Visibility = ViewStates.Gone;
                if (CmmVariable.M_NewsCount == 0)
                {
                    tv_count_news.Visibility = ViewStates.Gone;
                }

                else
                {
                    tv_count_news.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NewsCount);
                    if (CmmVariable.M_NewsCount >= 100)
                    {
                        //  txtCountNews.SetWidth(50);
                        tv_count_news.SetTextSize(ComplexUnitType.Sp, 8);
                    }

                }

                clwhite = new Color(ContextCompat.GetColor(view.Context, Resource.Color.white));
                darker_gray = new Color(ContextCompat.GetColor(view.Context, Resource.Color.darker_gray));
                headerBlue = new Color(ContextCompat.GetColor(view.Context, Resource.Color.clVNAText));
                clgray = new Color(ContextCompat.GetColor(view.Context, Resource.Color.grey_50));

            }
            else
            {
                LoadData();
                LoadList();
            }


            return view;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void ShowFilter(object sender, EventArgs e)
        {
            try
            {
                // Handle filter
                img_Safety_filter.StartAnimation(click_animation);

                if (ln_filter.Visibility == ViewStates.Gone)
                {
                    ln_filter.Visibility = ViewStates.Visible;
                    ln_blur_noti.Visibility = ViewStates.Visible;
                }
                else
                {
                    ln_filter.Visibility = ViewStates.Gone;
                    ln_blur_noti.Visibility = ViewStates.Gone;
                }
                ln_sort_notify.Visibility = ViewStates.Gone;
            }
            catch (Exception ex)
            {

            }
        }

        #region Event
        private void HomePage(object sender, EventArgs e)
        {
            this.BackToHome();
        }
        private void TabQualification_Click(object sender, EventArgs e)
        {
            if (SafetyFlag)
            {
                img_Safety_filter.Visibility = ViewStates.Invisible;
                tabSafety.SetBackgroundResource(Resource.Drawable.boder_grey_bot);
                tabSafety.SetTextColor(darker_gray);
                tabSafety.SetTypeface(null, TypefaceStyle.Normal);
                tabQualification.SetBackgroundColor(clwhite);
                tabQualification.SetTextColor(headerBlue);
                tabQualification.SetTypeface(null, TypefaceStyle.Bold);
                //beanAnnounceID = Int32.Parse(CmmFunction.GetAppSetting("QUALIFICATION_CATEGORY_ID")); dòng này cũ khi còn chia ra 2 tab Safety và Qualification
                beanAnnounceID = "3"; // 3 là Operation
                LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);
                LoadData();
                SafetyFlag = false;
            }
        }
        private void TabSafety_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SafetyFlag)
                {
                    img_Safety_filter.Visibility = ViewStates.Visible;
                    tabQualification.SetBackgroundResource(Resource.Drawable.boder_grey_bot);
                    tabQualification.SetTextColor(darker_gray);
                    tabQualification.SetTypeface(null, TypefaceStyle.Normal);
                    tabSafety.SetBackgroundColor(clwhite);
                    tabSafety.SetTextColor(headerBlue);
                    tabSafety.SetTypeface(null, TypefaceStyle.Bold);
                    DefaultSafety.Add(Int32.Parse(CmmFunction.GetAppSetting("SAFETY_CATEGORY_ID")).ToString());
                    DefaultSafety.Add(Int32.Parse(CmmFunction.GetAppSetting("QUALIFICATION_CATEGORY_ID")).ToString());
                    beanAnnounceID = "'" + String.Join("','", DefaultSafety.ToArray()) + "'";
                    LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);
                    LoadData();
                    SafetyFlag = true;
                }
            }
            catch (Exception)
            {

            }

        }
        private void NewsPage(object sender, EventArgs e)
        {
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new NewsFragment(true), "NewsFragment", 0);
        }
        private void SchedulePage(object sender, EventArgs e)
        {
            this.BackToHome();
            if (CmmVariable.SysConfig.UserType == 1)//1 là phi công 
                mainAct.ShowFragmentAnim(FragmentManager, new FlightScheduleFragment(), "FlightScheduleFragment", 0);
            else if (CmmVariable.SysConfig.UserType == 2)//2 là mặt đất
                mainAct.ShowFragmentAnim(FragmentManager, new FlightScheduleMDFragment(), "FlightScheduleMDFragment", 0);
        }

        private void DeleteSeach(object sender, EventArgs e)
        {
            editTextSearch.Text = string.Empty;
        }
        private void LnBlur_Click(object sender, EventArgs e)
        {
            if (ln_sort_notify.Visibility == ViewStates.Visible)
                ln_sort_notify.Visibility = ViewStates.Gone;
            if (ln_filter.Visibility == ViewStates.Visible)
                ln_filter.Visibility = ViewStates.Gone;

            ln_blur_noti.Visibility = ViewStates.Gone;
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
        }
        private void SearchNoti(object sender, TextChangedEventArgs e)
        {
            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);
        }
        private void SearchNotify(object sender, EventArgs e)
        {
            if (ln_blur_noti.Visibility == ViewStates.Visible)
            {
                ln_blur_noti.Visibility = ViewStates.Gone;
            }
            img_search.StartAnimation(click_animation);
            if (ln_sort_notify.Visibility == ViewStates.Visible)
            {
                ln_sort_notify.Visibility = ViewStates.Gone;
            }
            if (showSearchView == false)
            {

                ln_search2.Visibility = ViewStates.Visible;
                editTextSearch.RequestFocus();
                InputMethodManager inputMethodManager = mainAct.GetSystemService(Context.InputMethodService) as InputMethodManager;
                inputMethodManager.ShowSoftInput(view, ShowFlags.Forced);
                inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);

                showSearchView = true;
            }
            else
            {
                ln_search2.Visibility = ViewStates.Gone;

                showSearchView = false;
                editTextSearch.Text = "";

                InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            }
        }
        private void ShowLinearSort(object sender, EventArgs e)
        {
            img_sort.StartAnimation(click_animation);
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            if (ln_sort_notify.Visibility == ViewStates.Visible)
            {
                ln_blur_noti.Visibility = ViewStates.Gone;
                ln_sort_notify.Visibility = ViewStates.Gone;
            }
            else
            {
                ln_sort_notify.Visibility = ViewStates.Visible;
                ln_blur_noti.Visibility = ViewStates.Visible;
            }

            ln_filter.Visibility = ViewStates.Gone;
        }
        private void ListViewOnScroll(object sender, View.ScrollChangeEventArgs e)
        {
            var visibleItemCount = listView.ChildCount;
            var totalItemCount = listView.GetAdapter().ItemCount;
            var pastVisiblesItems = mLayoutManager.FindFirstVisibleItemPosition();
            if ((visibleItemCount + pastVisiblesItems) >= totalItemCount)
            {
                if (checkMore)
                {
                    //MoreData();
                }
            }
        }
        private void DefautSort(object sender, EventArgs e)
        {
            img_sort.SetImageResource(Resource.Drawable.icon_sort_descending);
            ln_sort_notify.Visibility = ViewStates.Gone;
            ln_blur_noti.Visibility = ViewStates.Gone;
            sort_type = "";
            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);

        }
        private void UnreadSort(object sender, EventArgs e)
        {
            img_sort.SetImageResource(Resource.Drawable.icon_sort_check);
            ln_sort_notify.Visibility = ViewStates.Gone;
            ln_blur_noti.Visibility = ViewStates.Gone;
            sort_type = "unread";
            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);

        }
        private void ConfirmClick(object sender, EventArgs e)
        {
            img_sort.SetImageResource(Resource.Drawable.icon_sort_check);
            ln_sort_notify.Visibility = ViewStates.Gone;
            sort_type = "confirm";
            ln_blur_noti.Visibility = ViewStates.Gone;
            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);

        }
        private void EmergencyClick(object sender, EventArgs e)
        {
            lst_notify.Clear();
            img_sort.SetImageResource(Resource.Drawable.icon_sort_check);
            ln_sort_notify.Visibility = ViewStates.Gone;
            ln_blur_noti.Visibility = ViewStates.Gone;
            sort_type = "emergency";
            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);
        }
        private void ReloadData(object sender, MEven.ChangeListNewEventArgs e)
        {
            check_news = e.IsSuccess;
            LoadList();
            LoadData();
        }
        private void CmmEvent_UpdateCount(object sender, EventArgs e)
        {
            UpdateData();

        }
        private void Back(object sender, EventArgs e)
        {
            imgBack.StartAnimation(click_animation);
            mainAct.BackFragment();
        }
        private async void HandleRefreshAsync(object sender, EventArgs e)
        {
            ln_search.Visibility = ViewStates.Gone;
            searchView.SetQuery("", false);
            isFilterClicked = false;
            showSearchView = false;
            //lnNodata.Visibility = ViewStates.Gone;
            //ln_search2.Visibility = ViewStates.Gone;
            //editTextSearch.Text = "";
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            try
            {
                swipe.Refreshing = true;
                await Task.Run(() =>
                {
                    this.ProviderBase().UpdateMasterData<BeanNotify>(true);
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
        #endregion

        #region Data
        private async void UpdateData()
        {
            try
            {
                if (CmmVariable.M_IS_SAFETY_QUALIFICATION_DEPARTMENT)
                {
                    bottom_ln_news.WeightSum = 5;
                }
                else
                {
                }
                CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);

                await Task.Run(() =>
                {
                    this.ProviderBase().UpdateMasterData<BeanNotify>(true);
                    mainAct.RunOnUiThread(() =>
                    {
                        CmmDroidFunction.HideProcessingDialog();
                        keyNews = CmmFunction.GetAppSetting("NEWS_CATEGORY_ID");
                        LoadList();
                        LoadData();
                        LoadListFilter();
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

        private void LoadListFilter()
        {
            try
            {
                var SafetyID = Int32.Parse(CmmFunction.GetAppSetting("SAFETY_CATEGORY_ID")).ToString();
                var QualificationID = Int32.Parse(CmmFunction.GetAppSetting("QUALIFICATION_CATEGORY_ID")).ToString();

                string _query = string.Format(@"SELECT * FROM BeanAnnouncementCategory WHERE ID IN ({0},{1}) ",
                SafetyID, QualificationID);

                BeanAnnouncementCategory beanAnnouncement = new BeanAnnouncementCategory
                {
                    Title = "All",
                    ID = 0,
                    Modified = null
                };

                lst_beanAnnouncementCategory.Add(beanAnnouncement); // All đứng đầu tiên
                lst_beanAnnouncementCategory.AddRange(SQLiteHelper.GetList<BeanAnnouncementCategory>(_query).ListData);
                if (lst_beanAnnouncementCategory != null && lst_beanAnnouncementCategory.Count >= 0)
                {
                    rcl_filter_notify.SetLayoutManager(new LinearLayoutManager(mainAct, LinearLayoutManager.Vertical, false));
                    recyclerNotifyTypeAdapter = new RecyclerNotifyTypeAdapter(lst_beanAnnouncementCategory, view.Context, mainAct);
                    recyclerNotifyTypeAdapter.mSelectedItem = 0; // Set All
                    recyclerNotifyTypeAdapter.ItemClick += ItemFilter_Click;
                    rcl_filter_notify.SetAdapter(recyclerNotifyTypeAdapter);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private void ItemFilter_Click(object sender, int e)
        {
            if (e != 0)
            {
                beanAnnounceID = lst_beanAnnouncementCategory[e].ID.ToString();
                img_Safety_filter.SetImageResource(Resource.Drawable.icon_filter_check);
            }
            else
            {
                beanAnnounceID = "'" + String.Join("','", DefaultSafety.ToArray()) + "'";
                img_Safety_filter.SetImageResource(Resource.Drawable.icon_filter);
            }

            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            ln_filter.Visibility = ViewStates.Gone;
            if (ln_blur_noti.Visibility == ViewStates.Visible)
                ln_blur_noti.Visibility = ViewStates.Gone;

            LoadListAdvance(editTextSearch.Text, sort_type, beanAnnounceID);
        }

        private void LoadList()
        {
            try
            {
                string query = "";
                if (string.IsNullOrEmpty(keyNews))
                    keyNews = "7";

                query = string.Format(@"SELECT * FROM BeanNotify WHERE ANStatus <> -1 AND AnnounCategoryId IN ({0}) AND AnnounCategoryId <> {1} ORDER BY Created DESC", beanAnnounceID, keyNews);
                lst_notify = SQLiteHelper.GetList<BeanNotify>(query).ListData;
                initRecyclerView("");
            }
            catch (Exception)
            {


            }
        }
        private void LoadListAdvance(string keyWord, string sortType, string filterType)
        {
            try
            {
                string query = string.Empty;
                keyWord = keyWord.Trim();
                if (!string.IsNullOrEmpty(keyWord))
                {
                    lst_notify.Clear();
                    if (filterType.Equals("0") || filterType.Equals("-1"))
                    {
                        switch (sort_type)
                        {
                            case "unread":
                                query = string.Format("SELECT * FROM BeanNotify WHERE AnnounCategoryId <> {0} AND SearCol  LIKE '%{1}%' AND AnnounCategoryId IN ({2}) AND ANStatus <> -1 ORDER BY FlgRead,Created DESC ", keyNews, keyWord, beanAnnounceID);
                                break;
                            case "emergency":
                                query = string.Format("SELECT * FROM BeanNotify WHERE AnnounCategoryId <> {0} AND SearCol  LIKE '%{1}%' AND  AnnounCategoryId IN ({2}) AND ANStatus <> -1  ORDER BY flgImmediately DESC ,Created DESC ", keyNews, keyWord, beanAnnounceID);
                                break;
                            case "confirm":
                                query = string.Format("SELECT * FROM BeanNotify WHERE AnnounCategoryId <> {0} AND SearCol  LIKE '%{1}%' AND  AnnounCategoryId IN ({2}) AND ANStatus <> -1  ORDER BY   flgConfirm DESC, flgConfirmed   ,Created DESC ", keyNews, keyWord, beanAnnounceID);
                                break;
                            case "":
                                query = string.Format("SELECT * FROM BeanNotify WHERE AnnounCategoryId <> {0} AND SearCol  LIKE '%{1}%' AND  AnnounCategoryId IN ({2}) AND ANStatus <> -1  ORDER BY Created DESC ", keyNews, keyWord, beanAnnounceID);
                                break;
                        }
                    }
                    else
                    {
                        switch (sort_type)
                        {
                            case "unread":
                                query = string.Format("SELECT * FROM BeanNotify WHERE AnnounCategoryId <> {0} AND SearCol  LIKE '%{1}%' AND   ANStatus <> -1 AND AnnounCategoryId IN ({2})  ORDER BY FlgRead,Created DESC ", keyNews, keyWord, beanAnnounceID);
                                break;
                            case "emergency":
                                query = string.Format("SELECT * FROM BeanNotify WHERE AnnounCategoryId <> {0} AND SearCol  LIKE '%{1}%' AND   ANStatus <> -1 AND AnnounCategoryId IN ({2}) ORDER BY flgImmediately DESC ,Created DESC ", keyNews, keyWord, beanAnnounceID);
                                break;
                            case "confirm":
                                query = string.Format("SELECT * FROM BeanNotify WHERE AnnounCategoryId <> {0} AND SearCol  LIKE '%{1}%' AND   ANStatus <> -1 AND AnnounCategoryId IN ({2}) ORDER BY   flgConfirm DESC, flgConfirmed   ,Created DESC", keyNews, keyWord, beanAnnounceID);
                                break;
                            case "":
                                query = string.Format("SELECT * FROM BeanNotify WHERE AnnounCategoryId <> {0} AND SearCol  LIKE '%{1}%' AND   ANStatus <> -1  AND AnnounCategoryId IN ({2}) ORDER BY Created DESC ", keyNews, keyWord, beanAnnounceID);
                                break;
                        }
                    }

                }
                else
                {
                    if (filterType.Equals("0") || filterType.Equals("-1"))
                    {

                        if (string.IsNullOrEmpty(keyNews)) keyNews = "7";
                        switch (sort_type)
                        {
                            case "unread":
                                query = string.Format(@"SELECT * FROM BeanNotify WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId IN ({1}) ORDER BY FlgRead,Created DESC", keyNews, beanAnnounceID);

                                break;
                            case "emergency":
                                query = string.Format(@"SELECT * FROM BeanNotify WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId IN ({1}) ORDER BY flgImmediately DESC ,Created DESC", keyNews, beanAnnounceID);
                                break;
                            case "confirm":
                                query = string.Format(@"SELECT * FROM BeanNotify WHERE ANStatus <> -1 AND AnnounCategoryId  <> {0} AND AnnounCategoryId IN ({1}) ORDER BY  flgConfirm DESC, flgConfirmed   ,Created DESC", keyNews, beanAnnounceID);
                                break;
                            case "":
                                query = string.Format(@"SELECT * FROM BeanNotify WHERE ANStatus <> -1 AND AnnounCategoryId <> {0} AND AnnounCategoryId IN ({1}) ORDER BY Created DESC", keyNews, beanAnnounceID);
                                break;
                        }

                    }
                    else
                    {
                        switch (sort_type)
                        {
                            case "unread":
                                query = string.Format(@"SELECT * FROM BeanNotify WHERE ANStatus <> -1 AND AnnounCategoryId IN ({0})  AND AnnounCategoryId <> {1} ORDER BY FlgRead,Created DESC", beanAnnounceID, keyNews);
                                break;
                            case "emergency":
                                query = string.Format(@"SELECT * FROM BeanNotify WHERE ANStatus <> -1 AND AnnounCategoryId IN ({0}) AND AnnounCategoryId <> {1} ORDER BY flgImmediately DESC ,Created DESC", beanAnnounceID, keyNews);
                                break;
                            case "confirm":
                                query = string.Format(@"SELECT * FROM BeanNotify WHERE ANStatus <> -1 AND AnnounCategoryId IN ({0}) AND AnnounCategoryId <> {1} ORDER BY  flgConfirm DESC, flgConfirmed   ,Created DESC", beanAnnounceID, keyNews);
                                break;
                            case "":
                                query = string.Format(@"SELECT * FROM BeanNotify WHERE ANStatus <> -1 AND AnnounCategoryId IN ({0}) AND AnnounCategoryId <> {1} ORDER BY Created DESC", beanAnnounceID, keyNews);
                                break;
                        }
                    }
                }
                lst_notify = SQLiteHelper.GetList<BeanNotify>(query).ListData;
                initRecyclerView(keyWord);
            }
            catch (Exception)
            {

            }
        }
        private void LoadData()
        {
            try
            {
                var qua = Int32.Parse(CmmFunction.GetAppSetting("QUALIFICATION_CATEGORY_ID"));
                var saf = Int32.Parse(CmmFunction.GetAppSetting("SAFETY_CATEGORY_ID"));
                var query = string.Format(@"SELECT * FROM BeanNotify NOLOCK WHERE ANStatus <> -1 AND AnnounCategoryId = {0} OR AnnounCategoryId = {1} OR AnnounCategoryId = 3 ORDER BY Created DESC", qua, saf);
                var lst_notifyCount = SQLiteHelper.GetList<BeanNotify>(query).ListData;

                int b = lst_notifyCount.Where(x => !x.FlgRead && x.ANStatus.Value != -1).Count();
                CmmVariable.M_NotiCount = b;

                if (b == 0)
                {
                    tv_count_notification.Visibility = ViewStates.Invisible;
                }
                else
                {
                    tv_count_notification.Visibility = ViewStates.Visible;
                    tv_count_notification.Text = MainActivity.DisplayUnreadNews(b);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void initRecyclerView(string searchKey)
        {
            if (lst_notify != null && lst_notify.Count > 0)
            {
                listView.SetLayoutManager(MLayoutManager);
                recyclerAdapter = new RecyclerNotifyAdapter(lst_notify, view.Context, mainAct, searchKey);
                recyclerAdapter.ItemClick += ItemNotifyClick;
                listView.SetAdapter(recyclerAdapter);
                lnNodata.Visibility = ViewStates.Gone;
                listView.Visibility = ViewStates.Visible;
            }
            else
            {
                lnNodata.Visibility = ViewStates.Visible;
                listView.Visibility = ViewStates.Gone;
            }
        }
        private void ItemNotifyClick(object sender, int e)
        {
            if (CmmDroidFunction.hasConnection())
            {
                try
                {
                    if (isAllowItemClick == true)
                    {
                        isAllowItemClick = false;
                        if (lst_notify != null && lst_notify.Count > 0)
                        {
                            if (lst_notify[e].ANStatus.HasValue)
                            {
                                if (lst_notify[e].ANStatus == -1)
                                {

                                }
                                else
                                {
                                    if (lst_notify[e].FlgRead == false)
                                    {
                                        lst_notify[e].FlgRead = true;
                                        //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                                        CmmVariable.M_NotiCount = CmmVariable.M_NotiCount - 1;
                                        //conn.Update(lst_notify[e]);
                                        //conn.Close();
                                        SQLiteHelper.Update(lst_notify[e]);
                                    }


                                    WebNewsFragment WebNews = new WebNewsFragment(this, lst_notify[e], false);
                                    mainAct.ShowFragment(FragmentManager, WebNews, "WebNews");
                                    if (showSearchView == true)
                                    {
                                        InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                                        inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
                                    }

                                }
                            }
                            else
                            {
                                lst_notify[e].FlgRead = true;
                                var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                                conn.Update(lst_notify[e]);
                                conn.Close();
                                WebNewsFragment WebNews = new WebNewsFragment(this, lst_notify[e], false);
                                mainAct.ShowFragment(FragmentManager, WebNews, "WebNews");
                                recyclerAdapter.NotifyDataSetChanged();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CmmDroidFunction.logErr(ex);
                }
            }
            else
            {
                mainAct.RunOnUiThread(() =>
                {
                    try
                    {
                        Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(view.Context);
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
        private void CloseKey()
        {
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
        }
        #endregion

    }
}