using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Views.InputMethods;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.Droid.Code.Adapter;
using VNASchedule.Droid.Code.Class;
using static VNASchedule.Droid.Code.Fragment.PilotMainFragment;

namespace VNASchedule.Droid.Code.Fragment
{
    public class ApplicationFragment : Android.App.Fragment, EditText.IOnEditorActionListener
    {
        #region Variable
        View view;
        List<BeanMenuApp> beanMenuApps;
        bool showSearchView = false;
        bool isAlowPopupMenuNavigation = true;
        int LanguageId = 1066;

        public LinearLayoutManager MLayoutManager { get; set; }

        RecyclerApplicationAdapter adapter;

        #region Control
        Dialog dialog;
        Android.Webkit.WebView web;

        TextView tabVN;
        TextView tabEN;
        TextView tv_count_notification;
        TextView tv_count_news;

        ImageView imgBack;
        ImageView imgSearch;
        ImageView imgFilterDelete;

        LinearLayout ln_blur_noti, ln_sort_notify, ln_search2;
        LinearLayout ln_EmptyData_Notifications;
        EditText editTextSearch;

        RecyclerView rc_application;
        SwipeRefreshLayout swipe;
        #endregion

        #region Color
        Color clwhite;
        Color darker_gray;
        Color headerBlue;
        Color clgray;
        #endregion

        #region Animation
        Animation click_animation;
        #endregion

        #endregion
        public ApplicationFragment()
        {
            
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.Application, null);

            FragmentEventHelper eventHelper = new FragmentEventHelper(this, view);
            eventHelper.SetListener(new int[] { 0, 1, 2, 3, 4 }, new int[] { Resource.Id.ln_application });

            MLayoutManager = new LinearLayoutManager(view.Context);

            #region Control
            swipe = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_application);

            imgBack = view.FindViewById<ImageView>(Resource.Id.img_Notification_Back);
            imgSearch = view.FindViewById<ImageView>(Resource.Id.img_Noti_Search);
            imgFilterDelete = view.FindViewById<ImageView>(Resource.Id.img_Filter_Delete);

            tabVN = view.FindViewById<TextView>(Resource.Id.tabVN);
            tabEN = view.FindViewById<TextView>(Resource.Id.tabEN);

            rc_application = view.FindViewById<RecyclerView>(Resource.Id.rc_application);

            tv_count_notification = view.FindViewById<TextView>(Resource.Id.txt_count_notification);
            tv_count_news = view.FindViewById<TextView>(Resource.Id.txt_count_news);

            ln_EmptyData_Notifications = view.FindViewById<LinearLayout>(Resource.Id.ln_EmptyData_Notifications);
            ln_blur_noti = view.FindViewById<LinearLayout>(Resource.Id.linear_blur_noti);
            ln_sort_notify = view.FindViewById<LinearLayout>(Resource.Id.ln_sort_notify);
            ln_search2 = view.FindViewById<LinearLayout>(Resource.Id.linear_Noti_Search);

            editTextSearch = view.FindViewById<EditText>(Resource.Id.edt_Noti_Search);
            #endregion

            #region Animation
            click_animation = AnimationUtils.LoadAnimation(view.Context, Resource.Animation.alpha);
            #endregion

            #region Color
            clwhite = new Color(ContextCompat.GetColor(view.Context, Resource.Color.white));
            darker_gray = new Color(ContextCompat.GetColor(view.Context, Resource.Color.darker_gray));
            headerBlue = new Color(ContextCompat.GetColor(view.Context, Resource.Color.clVNAText));
            clgray = new Color(ContextCompat.GetColor(view.Context, Resource.Color.grey_50));
            #endregion

            swipe.SetDistanceToTriggerSync(150);// in dips   

            #region Event
            imgBack.Click += Back;
            imgSearch.Click += ImgSearch_Click;
            imgFilterDelete.Click += ImgFilterDelete_Click;
            tabVN.Click += TabVN_Click;
            tabEN.Click += TabEN_Click;
            editTextSearch.TextChanged += EditTextSearch_TextChanged;
            editTextSearch.SetOnEditorActionListener(this);
            swipe.Refresh += HandleRefreshAsync;
            #endregion

            CmmEvent.UserTicketRequest += CmmEvent_UpdateCount;

            if (CmmVariable.M_NewsCount != 0)
            {
                if (CmmVariable.M_NewsCount >= 100)
                    tv_count_news.SetTextSize(ComplexUnitType.Sp, 8);
                tv_count_news.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NewsCount);
            }
            else
                tv_count_news.Visibility = ViewStates.Gone;

            if (CmmVariable.M_NotiCount != 0)
                tv_count_notification.Text = MainActivity.DisplayUnreadNews(CmmVariable.M_NotiCount);
            else
                tv_count_notification.Visibility = ViewStates.Gone;

            SetDefaultControl();

            new Handler().PostDelayed(() =>
            {
                string q = $"SELECT * FROM BeanMenuApp WHERE [Status] = 1";
                beanMenuApps = SQLiteHelper.GetList<BeanMenuApp>(q).ListData;

                //beanMenuApps = SQLiteHelper.GetAll<BeanMenuApp>().ListData;
                if (beanMenuApps != null && beanMenuApps.Count > 0)
                    beanMenuApps = beanMenuApps.Select(x => new BeanMenuApp()
                    {
                        ID = x.ID,
                        Title = x.Title.ToUpper(),
                        LanguageId = x.LanguageId,
                        Range = x.Range,
                        Created = x.Created,
                        Status = x.Status,
                        Url = x.Url
                    }).ToList();

                LoadListRecycler();
            }, 150);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private async void HandleRefreshAsync(object sender, EventArgs e)
        {
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            try
            {
                swipe.Refreshing = true;
                await Task.Run(() =>
                {
                    mainAct.RunOnUiThread(() =>
                    {
                        LoadListRecycler();
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

        private void ImgFilterDelete_Click(object sender, EventArgs e)
        {
            editTextSearch.Text = string.Empty;
        }

        private void EditTextSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            imgFilterDelete.Visibility = (string.IsNullOrEmpty(editTextSearch.Text) ? ViewStates.Gone : ViewStates.Visible);
            adapter?.Filter.InvokeFilter(editTextSearch.JavaText());
        }

        private void LoadListRecycler()
        {
            if (beanMenuApps != null && beanMenuApps.Count > 0)
            {
                rc_application.SetLayoutManager(MLayoutManager);
                adapter = new RecyclerApplicationAdapter(beanMenuApps, view.Context, MainActivity.INSTANCE);
                adapter.ItemClick += Adapter_ItemClick;
                rc_application.SetAdapter(adapter);
                ln_EmptyData_Notifications.Visibility = ViewStates.Gone;
                rc_application.Visibility = ViewStates.Visible;
            }
            else
            {
                rc_application.Visibility = ViewStates.Gone;
                ln_EmptyData_Notifications.Visibility = ViewStates.Visible;
            }
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            BeanMenuApp item = adapter.GetItem(e);

            if (string.IsNullOrEmpty(item.Url))
                return;

            ApplicationWebviewFragment webviewFragment = new ApplicationWebviewFragment(item);
            MainActivity.INSTANCE?.AddFragment(FragmentManager, webviewFragment, "ApplicationWebview", 1);
        }

        private async void CmmEvent_UpdateCount(object sender, EventArgs e)
        {
            try
            {
                //CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                await Task.Run(() =>
                {
                    this.ProviderBase().UpdateMasterData<BeanNotify>(true);
                    MainActivity.INSTANCE?.RunOnUiThread(() =>
                    {
                        int SafetyID = Int32.Parse(CmmFunction.GetAppSetting("SAFETY_CATEGORY_ID"));
                        int QualificationID = Int32.Parse(CmmFunction.GetAppSetting("QUALIFICATION_CATEGORY_ID"));
                        //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                        string querytempNotifi = string.Format(@"SELECT COUNT(*) AS CountUnReadNews FROM BeanNotify NOLOCK WHERE  ANStatus <> -1 AND AnnounCategoryId = {0} OR AnnounCategoryId = {1} AND FlgRead = 0  ORDER BY Created DESC", SafetyID, QualificationID);
                        //var tempNoti = conn.Query<CountNum>(querytempNotifi);
                        var tempNoti = SQLiteHelper.GetList<CountNum>(querytempNotifi).ListData;
                        //conn.Close();
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

        private void SetDefaultControl()
        {
            MainActivity.INSTANCE?.RunOnUiThread(() =>
            {
                ln_search2.Visibility = ViewStates.Gone;
            });
        }

        private void ImgSearch_Click(object sender, EventArgs e)
        {
            if (ln_blur_noti.Visibility == ViewStates.Visible)
                ln_blur_noti.Visibility = ViewStates.Gone;

            imgSearch.StartAnimation(click_animation);
            if (ln_sort_notify.Visibility == ViewStates.Visible)
                ln_sort_notify.Visibility = ViewStates.Gone;

            InputMethodManager inputMethodManager = MainActivity.INSTANCE?.GetSystemService(Context.InputMethodService) as InputMethodManager;

            if (showSearchView == false)
            {
                ln_search2.Visibility = ViewStates.Visible;
                editTextSearch.RequestFocus();
                inputMethodManager.ShowSoftInput(view, ShowFlags.Forced);
                inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);

                showSearchView = true;
            }
            else
            {
                ln_search2.Visibility = ViewStates.Gone;

                showSearchView = false;
                editTextSearch.Text = "";

                inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
            }
        }

        private void TabVN_Click(object sender, EventArgs e)
        {
            SetTextViewFocusStyle(tabEN, false);
            SetTextViewFocusStyle(tabVN);

            adapter?.SetLanguage(1066);

            adapter?.Filter.InvokeFilter(editTextSearch.JavaText());
        }

        private void TabEN_Click(object sender, EventArgs e)
        {
            SetTextViewFocusStyle(tabVN, false);
            SetTextViewFocusStyle(tabEN);

            adapter?.SetLanguage(1033);

            adapter?.Filter.InvokeFilter(editTextSearch.JavaText());
        }

        private void SetTextViewFocusStyle(TextView tv, bool IsFocus = true)
        {
            if (IsFocus)
                tv.SetBackgroundColor(clwhite);
            else
                tv.SetBackgroundResource(Resource.Drawable.boder_grey_bot);
            tv.SetTextColor(IsFocus ? headerBlue : darker_gray);
            tv.SetTypeface(null, IsFocus ? TypefaceStyle.Bold : TypefaceStyle.Normal);
        }

        private void Back(object sender, EventArgs e)
        {
            imgBack.StartAnimation(click_animation);
            MainActivity.INSTANCE?.BackFragment();
        }

        public override void OnDestroyView()
        {
            CmmEvent.UserTicketRequest -= CmmEvent_UpdateCount;
            base.OnDestroyView();
        }

        private void CloseKey()
        {
            InputMethodManager inputMethodManager = (InputMethodManager)MainActivity.INSTANCE.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(editTextSearch.WindowToken, 0);
        }
        public bool OnEditorAction(TextView v, [GeneratedEnum] ImeAction actionId, KeyEvent e)
        {
            CloseKey();
            return true;
        }
    }
}