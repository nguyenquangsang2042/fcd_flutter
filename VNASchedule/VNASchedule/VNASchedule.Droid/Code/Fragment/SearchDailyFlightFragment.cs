using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
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
    //
    public class SearchDailyFlightFragment : Android.App.Fragment, View.IOnTouchListener
    {
        private MainActivity mainAct;
        private LayoutInflater _inflater;
        private View _rootView;
        //private RecyclerView rcl_search_category;    
        private RecyclerView recyclerListUser;
        private EditText edt_search;
        private LinearLayout ln_empty_data;
        private SelectedUserAdapter selectedUserAdapter;
        public List<BeanUser> lst_user;
        LinearLayoutManager mLayoutManager;
        private SearchCategoryAdapter categoryAdapter;
        private List<string> lst_search_category;
        private string queryJob = "";
        public int load_contact_offset = 0; // vị trí load database
        public LinearLayoutManager MLayoutManager { get => mLayoutManager; set => mLayoutManager = value; }
        private ProviderBase p_base;
        private bool flag_loading;
        private ImageView img_delete;
        private ImageView img_close;
        private TextView tv_confirm;
        private ImageView img_Daily_Search;
        private RelativeLayout ln_search_input;
        private int selectedItemCount;
        private List<string> lst_user_selected_id;
        private bool isSearchPilot;
        private ScheduleInDayFragment scheduleInDayFragment;
        private string userSearchString;
        private string userID;
        private int old_index = -1;
        private BeanUser checked_user;
        private bool isAllowItemClick;
        private List<BeanUser> a;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public SearchDailyFlightFragment() { }
        public SearchDailyFlightFragment(ScheduleInDayFragment scheduleInDayFragment, string userSearchString,string userID)
        {
            this.scheduleInDayFragment = scheduleInDayFragment;
            this.userSearchString = userSearchString;
            this.userID = userID;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            mainAct = (MainActivity)this.Activity;
            _inflater = inflater;
            _rootView = inflater.Inflate(Resource.Layout.SearchDailyFlight, null);
            //rcl_search_category = _rootView.FindViewById<RecyclerView>(Resource.Id.rcl_search_category);
    
            recyclerListUser = _rootView.FindViewById<RecyclerView>(Resource.Id.recyclerView_user);
            edt_search = _rootView.FindViewById<EditText>(Resource.Id.edt_search);
            ln_empty_data = _rootView.FindViewById<LinearLayout>(Resource.Id.ln_EmptyData_Choose_User);
            img_delete = _rootView.FindViewById<ImageView>(Resource.Id.img_clear_text);
            img_close = _rootView.FindViewById<ImageView>(Resource.Id.img_close_view);
            tv_confirm = _rootView.FindViewById<TextView>(Resource.Id.btn_confirm);
            img_Daily_Search = _rootView.FindViewById<ImageView>(Resource.Id.img_Daily_Search);
            ln_search_input = _rootView.FindViewById<RelativeLayout>(Resource.Id.ln_search_input);

            MLayoutManager = new LinearLayoutManager(_rootView.Context);
            lst_user = new List<BeanUser>();
            if (!string.IsNullOrEmpty(userSearchString))
            {
                edt_search.Text = userSearchString;
            }
            checked_user = new BeanUser();
            lst_user_selected_id = new List<string>();
            if (!string.IsNullOrEmpty(userID))
            {
                lst_user_selected_id.Add(userID);

            }
            isAllowItemClick = true;
            p_base = new ProviderBase();
            LoadData();
            isSearchPilot = true;
            edt_search.TextChanged += Edt_TextChange;
            img_close.Click += ImgClose_Click;
            img_delete.Click += ClearText;
            tv_confirm.Click += BtnConfirm_Click;
            recyclerListUser.SetOnTouchListener(this);
            recyclerListUser.ScrollChange += (sender, e) =>
            {
                var visibleItemCount = recyclerListUser.ChildCount;
                var totalItemCount = recyclerListUser.GetAdapter().ItemCount;
                var pastVisiblesItems = mLayoutManager.FindFirstVisibleItemPosition();
                if ((visibleItemCount + pastVisiblesItems) >= totalItemCount)
                {

                    if (flag_loading == true)
                    {
                        flag_loading = false;
                        loadListPilot(load_contact_offset, edt_search.Text.Trim());
                    }
                }
            };

            //tv_search_category.Click += Tv_Searchcategory_Click;
            img_Daily_Search.Click += ShowSearchView;
            if (!string.IsNullOrEmpty(edt_search.Text))
            {
                img_delete.Visibility = ViewStates.Visible;
            }
            else
            {
                img_delete.Visibility = ViewStates.Gone;

            }
            return _rootView;
        }

        private void ShowSearchView(object sender, EventArgs e)
        {
            edt_search.RequestFocus();
            if(ln_search_input.Visibility == ViewStates.Gone)
            {
                edt_search.RequestFocus();
                ln_search_input.Visibility = ViewStates.Visible;
                openKeyboard();

            }
            else
            {
                ln_search_input.Visibility = ViewStates.Gone;
                hideKeyboard();
            }
        }
        private void ClearText(object sender, EventArgs e)
        {
            edt_search.Text = "";
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                scheduleInDayFragment.searchFlight(lst_user_selected_id[0]);
                mainAct.BackFragment();
            }
            catch (Exception)
            {
            }
        }

        private void ImgClose_Click(object sender, EventArgs e)
        {
            mainAct.BackFragment();
        }

        private void Edt_TextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (isSearchPilot == true)
                {
                    if (!string.IsNullOrEmpty(edt_search.Text))
                    {
                        img_delete.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        img_delete.Visibility = ViewStates.Gone;

                    }
                    load_contact_offset = 0;
                    loadListPilot(load_contact_offset, edt_search.Text.Trim());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("ContactView - textfield_pop_userOrGroup_EditingChanged - Err: " + ex.ToString());
            }

        }

        private void setData()
        {
            try
            {
                //lst_search_category = new List<string>();
                //lst_search_category.Add("Pilots name, crew code");
                //lst_search_category.Add("FlightNo");
                //var a = new LinearLayoutManager(mainAct, LinearLayoutManager.Vertical, false);
                ////rcl_search_category.SetLayoutManager(a);
                //categoryAdapter = new SearchCategoryAdapter(lst_search_category, _rootView.Context, mainAct);
                ////categoryAdapter.ItemClick += Search_Category_Click;
                ////rcl_search_category.SetAdapter(categoryAdapter);
              
                ////rcl_search_category.Visibility = ViewStates.Gone;
            }
            catch (Exception)
            {

                
            }
        }
        private async void LoadData()
        {
            try
            {
                CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                ProviderPilotSchedule p_base_schedule = new ProviderPilotSchedule();
                await Task.Run(() =>
                {
                    p_base.UpdateMasterData<BeanUser>(true);

                    //var lst_pilot = p_base_schedule.GetDailyPilot(DateTime.Today);
                    mainAct.RunOnUiThread(() =>
                    {
                        loadListPilot(load_contact_offset, edt_search.Text.Trim());
                        setData();
                        CmmDroidFunction.HideProcessingDialog();
                    });
                });


            }
            catch (Exception ex)
            { CmmDroidFunction.HideProcessingDialog(); }
            finally
            {
                CmmDroidFunction.HideProcessingDialog();
            }
        }
        public void loadListPilot(int loadPosition, string textSearch)
        {
            try
            {
                if (string.IsNullOrEmpty(edt_search.Text))
                {
                    queryJob = string.Format("SELECT  * FROM BeanUser ORDER BY FullNameNoAccent LIMIT ? OFFSET ? ");
                    var queryJob2 = string.Format("SELECT  DISTINCT Mobile,FullName,FullNameNoAccent,Code2,Avatar,UserId AS ID FROM BeanScheduleUser ORDER BY FullNameNoAccent LIMIT ? OFFSET ? ");
                     a = p_base.LoadMoreData<BeanUser>(queryJob2, 20, loadPosition);
                }
                else
                {
                    if (loadPosition == 0)
                    {
                        lst_user.Clear();

                        if (selectedUserAdapter != null)
                        {
                            selectedUserAdapter.NotifyDataSetChanged();

                        }
                        string content = CmmFunction.RemoveSignVietnamese(edt_search.Text.Trim().ToLowerInvariant());
                        queryJob = string.Format("SELECT  * FROM BeanUser WHERE FullNameNoAccent LIKE '%{0}%' OR Mobile LIKE '%{0}%'  OR Code2 LIKE '%{0}%' ORDER BY FullNameNoAccent LIMIT ? OFFSET ? ", content);
                        //var queryJob2 = string.Format("SELECT  DISTINCT Mobile,FullName,FullNameNoAccent,Code2,Avatar,UserId AS ID FROM BeanScheduleUser WHERE FullNameNoAccent LIKE '%{0}%' OR Mobile LIKE '%{0}%'  OR Code2 LIKE '%{0}%' ORDER BY FullNameNoAccent LIMIT ? OFFSET ? ", content);
                        //a = p_base.LoadMoreData<BeanUser>(queryJob2, 20, loadPosition, null);
                    }
                }

                var temp_list_contact = p_base.LoadMoreData<BeanUser>(queryJob, 20, loadPosition);
                if (temp_list_contact != null && temp_list_contact.Count > 0)
                {
                    ln_empty_data.Visibility = ViewStates.Gone;
                    if (temp_list_contact.Count >= 20)
                    {
                        load_contact_offset += temp_list_contact.Count;
                        flag_loading = true;
                    }
                    else flag_loading = false;
                    if (loadPosition == 0)
                    {
                        lst_user = temp_list_contact;
                        if (lst_user != null && lst_user.Count >= 0)
                        {

                            recyclerListUser.SetLayoutManager(MLayoutManager);
                            selectedUserAdapter = new SelectedUserAdapter(lst_user, _rootView.Context, mainAct, textSearch, CmmVariable.M_Folder_Avatar, this, null, lst_user_selected_id);
                            selectedUserAdapter.ItemClick += ListClick;
                            recyclerListUser.SetAdapter(selectedUserAdapter);
                            //recyclerListUser.HasFixedSize = true;
                            //recyclerListUser.SetItemViewCacheSize(20);
                            //recyclerListUser.DrawingCacheEnabled = true;
                            //recyclerListUser.DrawingCacheQuality = DrawingCacheQuality.High;
                        }
                    }
                    else // loadmore data
                    {
                        lst_user.AddRange(temp_list_contact);
                        mainAct.RunOnUiThread(() =>
                        {
                            if (selectedUserAdapter != null)
                            {
                                selectedUserAdapter.NotifyDataSetChanged();

                            }
                        });

                    }

                }
                else
                {
                    ln_empty_data.Visibility = ViewStates.Visible;

                    if (selectedUserAdapter != null)
                    {
                        selectedUserAdapter.NotifyDataSetChanged();

                    }

                }
            }
            catch (Exception)
            {

                
            }
        }

        private void ListClick(object sender, int e)
        {
            try
            {
                if (isAllowItemClick == true)
                {
                    isAllowItemClick = false;
                    if (lst_user_selected_id.Count > 0)
                    {

                        var old_item_index = lst_user.FindIndex(user => user.ID == lst_user_selected_id[0]);
                        if (!lst_user_selected_id[0].Equals(lst_user[e].ID))
                        {
                            lst_user_selected_id.RemoveAt(0);
                            lst_user_selected_id.Add(lst_user[e].ID);

                            selectedUserAdapter.NotifyItemChanged(e);
                            selectedUserAdapter.NotifyItemChanged(old_item_index);
                        }

                        Handler h = new Handler();
                        Action myAction = () =>
                        {
                            scheduleInDayFragment.searchFlight(lst_user[e].ID);
                            mainAct.BackFragment();

                        };
                        h.PostDelayed(myAction, 500);
                        scheduleInDayFragment.userSearchString = edt_search.Text;

                    }
                    else
                    {
                        lst_user_selected_id.Add(lst_user[e].ID);
                        selectedUserAdapter.NotifyItemChanged(e);
                        Handler h = new Handler();
                        Action myAction = () =>
                        {
                            scheduleInDayFragment.searchFlight(lst_user[e].ID);
                            mainAct.BackFragment();

                        };
                        h.PostDelayed(myAction, 500);
                        scheduleInDayFragment.userSearchString = edt_search.Text;
                    }
                }



            }
            catch (Exception ex)
            {


            }

        }


        public void Search_Flight_By_Pilot()
        {

        }
        //private void Tv_Searchcategory_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rcl_search_category.Visibility == ViewStates.Gone)
        //        {
        //            rcl_search_category.Visibility = ViewStates.Visible;

        //        }
        //        else
        //        {
        //            rcl_search_category.Visibility = ViewStates.Gone;

        //        }
        //    }
        //    catch (Exception ex)
        //    {

               
        //    }
        //}
        //private void Search_Category_Click(object sender, int e)
        //{
        //    try
        //    {
        //        switch (e)
        //        {
        //            case 0:
        //                tv_search_category.Text = lst_search_category[e];
        //                rcl_search_category.Visibility = ViewStates.Gone;
        //                recyclerListUser.Visibility = ViewStates.Visible;
        //                tv_search_category.SetTextColor(Color.Black);
        //                edt_search.Text = "";
        //                isSearchPilot = true;
        //                break;
        //            case 1:
        //                tv_search_category.Text = lst_search_category[e];
        //                rcl_search_category.Visibility = ViewStates.Gone;
        //                recyclerListUser.Visibility = ViewStates.Gone;
        //                ln_empty_data.Visibility = ViewStates.Gone;
        //                tv_search_category.SetTextColor(Color.Black);
        //                edt_search.Text = "";
        //                isSearchPilot = false;
        //                break;
        //        }
        //    }
        //    catch (Exception)
        //    {

                
        //    }
        //}
        public override void OnPause()
        {
            try
            {
                base.OnPause();
                scheduleInDayFragment.isAllowSearchUserClick = true;
                isAllowItemClick = true;
            }
            catch (Exception ex)
            {

            }
        }
        public void hideKeyboard()
        {
            try
            {
                InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(edt_search.WindowToken, 0);
            }
            catch (Exception ex)
            {

                
            }
        }
        public void openKeyboard()
        {
            try
            {
                InputMethodManager inputMethodManager = mainAct.GetSystemService(Context.InputMethodService) as InputMethodManager;
                inputMethodManager.ShowSoftInput(_rootView, ShowFlags.Forced);
                inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
            }
            catch (Exception)
            {


            }
        }
        public bool OnTouch(View v, MotionEvent e)
        {
            hideKeyboard();
            //rcl_search_category.Visibility = ViewStates.Gone;
            return false;
        }
    }
}