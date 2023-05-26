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
using VNASchedule.Class;
using System.Threading.Tasks;
using VNASchedule.DataProvider;
using VNASchedule.Bean;
using Android.Views.InputMethods;
using Android.Text;
using VNASchedule.Droid.Code.Adapter;
using VNASchedule.Droid.Code.Class;
using Android.Graphics;
using System.IO;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Android.Support.Design.Internal;
using static VNASchedule.Droid.Code.Fragment.PilotMainFragment;
using Android.Views.Animations;
using SQLite;

namespace VNASchedule.Droid.Code.Fragment
{
    public class LibraryFragment : Android.App.Fragment
    {
        private MainActivity mainAct;
        private View _rootView;

        private LayoutInflater _inflater;
        private ImageView img_back, img_delete, img_search, img_offline, imgSortAZ;
        private TextView tv_title, tv_noresult, tv_tim;
        private LinearLayout ln_search, ln_tim;
        private ListView lv;
        private EditText edt;
        private bool checksearch = false;
        private List<BeanLibrary> lst_library;
        private List<BeanLibrary> lst_libraryS;
        private List<BeanLibrary> lst_libraryTree;
        private LibraryAdapter libradapter;
        private WebView web;
        private LinearLayout ln_pro;
        private List<int> back = new List<int>();
        private List<string> lst_title = new List<string>();
        private int backDropID = 0;
        private Dialog dialog;
        private TextView tv_request;
        private TextView tv_news, tv_notifications, tv_newsNum, tv_notificationsNum, tv_traning, tv_license, tv_library, tv_payroll, tv_contact, tv_faq, tv_report;
        private TextView tv_cancel;
        private ImageView img_home, img_notification, img_news, img_schedule, img_extend;
        private LinearLayout linear_noda;
        private Animation click_animation;

        private LinearLayout bottom_ln_news;
        private bool menu_home = false;
        private bool isSortAZ = true;
        int root = 0;
        //private Java.IO.File file;
        private bool showweb = false, checkback = false;
        private TextView tv_count_notification;
        private TextView tv_count_news;

        private bool loadingView = false;
        private bool isAlowPopupMenuNavigation = true;
        private RelativeLayout rl_bottom_home;
        private RelativeLayout rl_bottom_safety;
        private RelativeLayout rl_bottom_news;
        private RelativeLayout rl_bottom_schedule;
        private RelativeLayout rl_bottom_extent;

        public LibraryFragment()
        {

        }
        public LibraryFragment(bool loadingView)
        {
            this.loadingView = loadingView;
        }
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
            try
            {
                mainAct = (MainActivity)this.Activity;
                _inflater = inflater;
                _rootView = inflater.Inflate(Resource.Layout.Library, null);

                img_back = _rootView.FindViewById<ImageView>(Resource.Id.img_Library_Back);
                tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_Library_Title);
                img_delete = _rootView.FindViewById<ImageView>(Resource.Id.img_Library_Delete);
                img_search = _rootView.FindViewById<ImageView>(Resource.Id.img_Library_Search);
                img_offline = _rootView.FindViewById<ImageView>(Resource.Id.img_Library_Offline);
                imgSortAZ = _rootView.FindViewById<ImageView>(Resource.Id.img_sortAZ);

                lv = _rootView.FindViewById<ListView>(Resource.Id.lv_Library);
                //ln_loadMore = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_Contacts_LoadMore);
                ln_search = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_Library_Search);
                edt = _rootView.FindViewById<EditText>(Resource.Id.edt_Library);
                web = _rootView.FindViewById<WebView>(Resource.Id.web_Library);
                ln_pro = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_Library_Pro);
                tv_noresult = _rootView.FindViewById<TextView>(Resource.Id.tv_Library_NoResult);
                tv_tim = _rootView.FindViewById<TextView>(Resource.Id.tv_Library_Tim);
                ln_tim = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_Library_Tim);
                HybridWebViewClient web1 = new HybridWebViewClient(mainAct, ln_pro, web);
                HybridWebViewClient1 web2 = new HybridWebViewClient1(mainAct);
                tv_count_notification = _rootView.FindViewById<TextView>(Resource.Id.txt_count_notification);
                tv_count_news = _rootView.FindViewById<TextView>(Resource.Id.txt_count_news);

                img_home = _rootView.FindViewById<ImageView>(Resource.Id.img_home_bottom);
                img_notification = _rootView.FindViewById<ImageView>(Resource.Id.img_notification_bottom);
                img_news = _rootView.FindViewById<ImageView>(Resource.Id.img_news_bottom);
                img_schedule = _rootView.FindViewById<ImageView>(Resource.Id.img_schedule_bottom);
                img_extend = _rootView.FindViewById<ImageView>(Resource.Id.img_extent_bottom);
                linear_noda = _rootView.FindViewById<LinearLayout>(Resource.Id.ln_EmptyData_Lib);
                click_animation = AnimationUtils.LoadAnimation(_rootView.Context, Resource.Animation.alpha);
                rl_bottom_home = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_home);
                rl_bottom_safety = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_safety);
                rl_bottom_news = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_news);
                rl_bottom_schedule = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_schedule);
                rl_bottom_extent = _rootView.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_extent);
                bottom_ln_news = _rootView.FindViewById<LinearLayout>(Resource.Id.bottom_ln_news);
                if (CmmVariable.M_IS_SAFETY_QUALIFICATION_DEPARTMENT)
                {
                    bottom_ln_news.WeightSum = 5;
                }
                else
                {
                }
                CmmEvent.UserTicketRequest += CmmEvent_UpdateCount;

                //img_extend.SetColorFilter(Color.ParseColor("#1E88E5"));

                web.SetWebChromeClient(web2);
                web.SetWebViewClient(web1);
                web.Settings.JavaScriptEnabled = true;
                web.Settings.LoadWithOverviewMode = true;
                web.Settings.UseWideViewPort = true;
                web.ScrollBarStyle = ScrollbarStyles.InsideOverlay;
                web.Settings.PluginsEnabled = true;

                web.ScrollbarFadingEnabled = true;
                requestRead();
                SetData();
                LoadData();

                imgSortAZ.Click += ImgSortAZ_Click;
                img_back.Click += Back;
                edt.TextChanged += WhereAreYou;
                img_search.Click += Search;
                img_delete.Click += Xoa;
                lv.ItemClick += Next;
                edt.EditorAction += HideBoard;

                rl_bottom_home.Click += HomePage;
                rl_bottom_news.Click += NewsPage;
                rl_bottom_schedule.Click += SchedulePage;
                //rl_bottom_extent.Click += PopupNavigationMenu;
                new MoreMenu(new MoreMenuProperties()
                {
                    Fragment = this,
                    RelativeLayoutExtent = rl_bottom_extent,
                    HideControlIds = new int[] { Resource.Id.ln_library }
                });
                rl_bottom_safety.Click += NotificationPage;
                img_offline.Click += LibraryOffline;
                tv_tim.Click += SearchServer;
                _rootView.SetOnTouchListener(mainAct);
                CmmDroidFunction.SetTitleToView(_rootView);

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
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex);
            }
            return null;
        }

        private void ImgSortAZ_Click(object sender, EventArgs e)
        {
            isSortAZ = !isSortAZ;

            mainAct.RunOnUiThread(() =>
            {
                if (isSortAZ)
                    imgSortAZ.SetImageResource(Resource.Drawable.ic_sortza);
                else
                    imgSortAZ.SetImageResource(Resource.Drawable.ic_sortaz);
            });

            lst_libraryS = isSortAZ ? lst_libraryS.OrderBy(x => x.Name.Trim()).ToList() : lst_libraryS.OrderByDescending(x => x.Name.Trim()).ToList();

            lst_library.Clear();
            lst_library.AddRange(lst_libraryS);
            libradapter = new LibraryAdapter(_rootView.Context, lst_library, "");
            lv.Adapter = libradapter;
        }

        private void LibraryOffline(object sender, EventArgs e)
        {
            try
            {

                LibraryOfflineFragment libraryOffline = new LibraryOfflineFragment(false);
                mainAct.ShowFragment(FragmentManager, libraryOffline, "libraryOffline");

            }
            catch (Exception ex)
            { }
        }

        #region Event
        private void Back(object sender, EventArgs e)
        {
            checkback = true;
            img_search.Visibility = ViewStates.Visible;
            if (showweb)
            {
                showweb = false;
                web.Visibility = ViewStates.Gone;
                ln_pro.Visibility = ViewStates.Gone;
                lv.Visibility = ViewStates.Visible;
                linear_noda.Visibility = ViewStates.Gone;
            }
            else if (back.Count > 1)
            {
                InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(edt.WindowToken, 0);
                ln_search.Visibility = ViewStates.Gone;
                linear_noda.Visibility = ViewStates.Gone;
                back.RemoveAt(back.Count - 1);
                root = back[back.Count - 1];
                if (lst_libraryTree.Count > 1)
                    lst_libraryTree.RemoveAt(lst_libraryTree.Count - 1);
                tv_title.Text = lst_title[back.Count - 1];
                LoadData();
            }
            else
            {
                mainAct.BackFragment();
            }
        }
        private void WhereAreYou(object sender, TextChangedEventArgs e)
        {
            try
            {
                string content = CmmFunction.RemoveSignVietnamese(edt.Text.ToLower());
                if (!string.IsNullOrEmpty(edt.Text.Trim()))
                {
                    var items = from item in lst_libraryS
                                where ((!string.IsNullOrEmpty(item.Name) && CmmFunction.RemoveSignVietnamese(item.Name.ToLower()).Contains(content)))
                                select item;
                    var tam = items.ToList();
                    if (tam != null && tam.Count > 0)
                    {
                        lv.Visibility = ViewStates.Visible;
                        tv_noresult.Visibility = ViewStates.Gone;
                        ln_tim.Visibility = ViewStates.Gone;
                        lst_library.Clear();
                        lst_library.AddRange(tam);
                        libradapter = new LibraryAdapter(_rootView.Context, lst_library, content);
                        lv.Adapter = libradapter;
                    }
                    else
                    {
                        lv.Visibility = ViewStates.Gone;
                        tv_noresult.Visibility = ViewStates.Visible;
                        ln_tim.Visibility = ViewStates.Visible;
                    }
                }
                else
                {
                    lst_library.Clear();
                    lst_library.AddRange(lst_libraryS);
                    libradapter = new LibraryAdapter(_rootView.Context, lst_library, "");
                    lv.Adapter = libradapter;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ContactView - textfield_pop_userOrGroup_EditingChanged - Err: " + ex.ToString());
            }
        }
        private void Search(object sender, EventArgs e)
        {
            try
            {
                img_search.StartAnimation(click_animation);
                if (!checksearch)
                {
                    checksearch = true;
                    ln_search.Visibility = ViewStates.Visible;
                    lv.Visibility = ViewStates.Visible;
                    tv_noresult.Visibility = ViewStates.Gone;
                    ln_tim.Visibility = ViewStates.Gone;
                    edt.RequestFocus();
                    InputMethodManager inputMethodManager = mainAct.GetSystemService(Context.InputMethodService) as InputMethodManager;
                    inputMethodManager.ShowSoftInput(_rootView, ShowFlags.Forced);
                    inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
                }
                else
                {
                    checksearch = false;
                    ln_search.Visibility = ViewStates.Gone;
                    ln_tim.Visibility = ViewStates.Gone;
                    tv_noresult.Visibility = ViewStates.Gone;
                    edt.Text = "";
                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(edt.WindowToken, 0);
                }
            }
            catch (Exception ex)
            { }
        }
        private void Xoa(object sender, EventArgs e)
        {
            try
            {
                edt.Text = "";
                ln_tim.Visibility = ViewStates.Gone;
                tv_noresult.Visibility = ViewStates.Gone;
                lv.Visibility = ViewStates.Visible;
            }
            catch (Exception ex)
            { }
        }
        private void Next(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(edt.WindowToken, 0);
                ln_search.Visibility = ViewStates.Gone;
                if (lst_library[e.Position].Type == 2)//file 
                {
                    CreateDirectoryForPictures(CmmVariable.M_DataFolder);
                    string newfilepathurl = CmmVariable.M_Domain + lst_library[e.Position].Path;
                    SetAvata(lst_library[e.Position].Path, lst_library[e.Position]);
                }
                else //foder
                {
                    lst_library[e.Position].Parent = root;
                    lst_libraryTree.Add(lst_library[e.Position]);
                    checkback = false;
                    root = lst_library[e.Position].ID;
                    tv_title.Text = lst_library[e.Position].Name;
                    LoadData();
                }
            }
            catch (Exception ex)
            { }
        }
        private async void SearchServer(object sender, EventArgs e)
        {
            try
            {
                tv_tim.Enabled = false;
                ProviderLicense pro = new ProviderLicense();
                lst_library.Clear();
                await Task.Run(() =>
                {
                    lst_library = pro.SearchLibraryFromServer(edt.Text);
                    mainAct.RunOnUiThread(() =>
                    {
                        if (lst_library != null && lst_library.Count > 0)
                        {
                            tv_noresult.Visibility = ViewStates.Gone;
                            lv.Visibility = ViewStates.Visible;
                            libradapter = new LibraryAdapter(_rootView.Context, lst_library, CmmFunction.RemoveSignVietnamese(edt.Text));
                            lv.Adapter = libradapter;
                            libradapter.NotifyDataSetChanged();
                        }
                        else
                        {
                            tv_noresult.Visibility = ViewStates.Visible;
                            lv.Visibility = ViewStates.Gone;
                            tv_noresult.Visibility = ViewStates.Visible;
                        }
                    });
                });
            }
            catch (Exception ex)
            { }
            finally { tv_tim.Enabled = true; }
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

                LinearLayout ln_library = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.ln_library);

                ln_library.Visibility = ViewStates.Gone;
                tv_traning.Click += TraningClick;
                tv_license.Click += LicenseClick;
                //tv_library.Click += LibraryClick;
                tv_payroll.Click += PayrollClick;
                tv_contact.Click += ContactClick;
                tv_cancel.Click += DismissMenuDialog;
                tv_faq.Click += FaqsClick;
                tv_request.Click += RequestClick;
                dialog = new Dialog(_rootView.Context, Resource.Style.Dialog);
                Window window = dialog.Window;
                dialog.RequestWindowFeature(1);

                window.SetGravity(GravityFlags.Bottom);
                Android.Util.DisplayMetrics dm = Resources.DisplayMetrics;
                ViewGroup.LayoutParams pa = ScheduleDetail.LayoutParameters;

                dialog.SetContentView(ScheduleDetail);
                dialog.SetCancelable(false);
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
        private void NewsPage(object sender, EventArgs e)
        {
            this.BackToHome();
            mainAct.ShowFragmentAnim(FragmentManager, new NewsFragment(true), null, 0);
        }
        #endregion

        #region Data

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

        private void DismissMenuDialog(object sender, EventArgs e)
        {
            isAlowPopupMenuNavigation = true;
            dialog.Dismiss();
        }

        private void SchedulePage(object sender, EventArgs e)
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

        private void SetData()
        {
            //if (back != null)
            //    back.Clear();
            //if (lst_title != null)
            //    lst_title.Clear();
            ln_pro.Visibility = ViewStates.Gone;
            ln_search.Visibility = ViewStates.Gone;
            web.Visibility = ViewStates.Gone;
            tv_noresult.Visibility = ViewStates.Gone;
            ln_tim.Visibility = ViewStates.Gone;
        }

        private void HideBoard(object sender, TextView.EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Done)
            {
                InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(edt.WindowToken, 0);
                SearchServer(null, null);
            }
            if (e.ActionId == ImeAction.Next)
            {
                InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(edt.WindowToken, 0);
                SearchServer(null, null);
            }
        }
        private void requestRead()
        {
            if (ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.WriteExternalStorage) != Android.Content.PM.Permission.Granted ||
                ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.ReadExternalStorage) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(mainAct,
                            new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, MainActivity.MY_PERMISSIONS_REQUEST_CAMERA_EXTERNAL_STORAGE);
            }
            else
            {

            }
        }
        void CreateDirectoryForPictures(string path)
        {
            try
            {
                var file = new Java.IO.File(path);
                if (!file.Exists())
                {
                    file.Mkdirs();

                }
            }
            catch (Exception ex)
            {
            }
        }

        private async void SetAvata(string url, BeanLibrary library)
        {
            try
            {
                if (loadingView == false)
                {
                    CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                }

                ProviderUser p_user = new ProviderUser();
                string extension = url.Replace("/", "_");
                string localPath = (CmmVariable.M_DataFolder + url);
                CreateDirectoryForPictures(CmmVariable.M_DataFolder + System.IO.Path.GetDirectoryName(url));
                //string localPathExten = System.IO.Path.Combine(Android.OS.Environment.DirectoryDownloads, extension);
                string newfilepathurl = CmmVariable.M_Domain + url;
                bool result = false;

                await Task.Run(() =>
                {
                    result = p_user.DownloadFile(newfilepathurl, localPath, CmmVariable.M_AuthenticatedHttpClient);
                    //p_user.DownloadFile(newfilepathurl, localPathExten, CmmVariable.M_AuthenticatedHttpClient);
                    if (result)
                    {
                        mainAct.RunOnUiThread(() =>
                        {
                            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                            alert.SetTitle("Vietnam Airlines");
                            alert.SetMessage("Download successfully to Library offline.");
                            alert.SetNegativeButton("Close", (senderAlert, args) =>
                            {
                                alert.Dispose();
                                var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                                string sqlSel = string.Format("SELECT  * FROM BeanLibrary WHERE ID = {0} AND Type = {1}", library.ID, library.Type);
                                var lst_Library = conn.CreateCommand(sqlSel).ExecuteQuery<BeanLibrary>();
                                var pathOn = library.Path;
                                library.Path = localPath;
                                library.Parent = root;
                                if (lst_Library.Count > 0)
                                {
                                    conn.Update(library);
                                }
                                else
                                {
                                    conn.Insert(library);
                                }
                                library.Path = pathOn;
                                CreateTreeLibrary();
                                loadImage(localPath);
                                conn.Close();
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
                            alert.SetMessage("Download Failed. Try again?");
                            alert.SetNegativeButton("Confirm", (senderAlert, args) =>
                            {
                                SetAvata(url, library);
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
                //Java.IO.File directory = new Java.IO.File(CmmVariable.M_DataFolder);
                //Java.IO.File[] files = directory.ListFiles();
                //foreach (Java.IO.File inFile in files)
                //{
                //    if (inFile.IsDirectory)
                //    {
                //        // is directory
                //    }
                //}

            }
            catch { }
            finally
            {
                CmmDroidFunction.HideProcessingDialog();
            }
        }
        private void CreateTreeLibrary()
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath))
                {
                    var Librarys = conn.CreateCommand("SELECT * FROM BeanLibrary").ExecuteQuery<BeanLibrary>();
                    foreach (var item in lst_libraryTree)
                    {
                        //string sqlSel = string.Format("SELECT  * FROM BeanLibrary WHERE Parent = {0} AND ID = {1} AND Type = {2}", item.Parent, item.ID, item.Type);
                        //var lst_Library = conn.CreateCommand(sqlSel).ExecuteQuery<BeanLibrary>();
                        //if (lst_Library.Count > 0)
                        //{
                        //    conn.Update(item);
                        //}
                        //else
                        //{
                        //    conn.Insert(item);
                        //}
                        if (Librarys.Any(x => x.Parent == item.Parent && x.ID == item.ID && x.Type == item.Type))
                            conn.Update(item);
                        else
                            conn.Insert(item);
                    }
                }
            }
            catch { }
        }
        private void loadImage(string localpath)
        {
            try
            {
                Android.Net.Uri pdfPath = Android.Net.Uri.FromFile(new Java.IO.File(localpath));
                Java.IO.File file = new Java.IO.File(localpath);
                file.SetReadable(true);
                Android.Net.Uri uri = FileProvider.GetUriForFile(mainAct, "com.Vuthao.VNASchedule", file);
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
                        application = "video/mp4";
                        break;
                    default:
                        application = "*/*";
                        break;
                }
                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.SetDataAndType(uri, application);
                StartActivity(intent);
            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex, "ChuaPhanCongFragment - loadImage - Er: " + ex);
            }
            finally
            {
                lv.Enabled = true;
            }
        }

        private async void LoadData()
        {
            if (CmmDroidFunction.hasConnection())
            {
                try
                {
                    if (lst_libraryTree == null)
                        lst_libraryTree = new List<BeanLibrary>();
                    ProviderLicense pro = new ProviderLicense();
                    if (!checkback)
                    {
                        if (back != null && back.Count > 0)
                        {
                            if (back[back.Count - 1] != root)
                                back.Add(root);
                        }
                        else
                            back.Add(root);
                        lst_title.Add(tv_title.Text);
                    }
                    if (loadingView == false)
                    { CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false); }

                    await Task.Run(() =>
                    {
                        lst_libraryS = pro.GetLibraryFromServer(root);
                        if (root == 0 && lst_libraryS != null && lst_libraryS.Count > 0 && backDropID != 0)
                        {
                            lst_libraryS.RemoveAll(x => x.ID == backDropID);
                        }
                        if (lst_libraryS != null && lst_libraryS.Count >= 0)
                        {
                            lst_libraryS = isSortAZ ? lst_libraryS.OrderBy(x => x.Name.Trim()).ToList() : lst_libraryS.OrderByDescending(x => x.Name.Trim()).ToList();

                            lst_library = new List<BeanLibrary>();
                            lst_library.AddRange(lst_libraryS);

                            mainAct.RunOnUiThread(() =>
                            {
                                if (lst_library != null && lst_library.Count > 0)
                                {
                                    lv.Visibility = ViewStates.Visible;
                                    libradapter = new LibraryAdapter(_rootView.Context, lst_library, "");
                                    lv.Adapter = libradapter;
                                    libradapter.NotifyDataSetChanged();
                                }
                                else
                                {
                                    lv.Visibility = ViewStates.Gone;
                                    linear_noda.Visibility = ViewStates.Visible;
                                }
                            });
                        }
                        else
                        {
                            mainAct.RunOnUiThread(() =>
                            {
                                lv.Visibility = ViewStates.Gone;
                                linear_noda.Visibility = ViewStates.Visible;
                            });
                        }
                    });
                }
                catch (Exception ex)
                { }
                finally
                {
                    CmmDroidFunction.HideProcessingDialog();
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
                        alert.SetMessage("Load library fail.");
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
        public class HybridWebViewClient : WebViewClient
        {
            private Activity mActivity;
            private LinearLayout ln_pro;
            private WebView web;
            public HybridWebViewClient(Activity _mActivity, LinearLayout _ln_pro, WebView _web)
            {
                this.mActivity = _mActivity;
                this.ln_pro = _ln_pro;
                this.web = _web;
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
                ln_pro.Visibility = ViewStates.Gone;
                web.Visibility = ViewStates.Visible;
            }
        }
    }
}