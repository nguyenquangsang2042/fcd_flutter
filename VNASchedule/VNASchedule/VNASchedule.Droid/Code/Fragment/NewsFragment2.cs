using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.DataProvider;
using VNASchedule.Droid.Code.Adapter;
using VNASchedule.Droid.Code.Class;

namespace VNASchedule.Droid.Code.Fragment
{
    public class NewsFragment2 : Android.App.Fragment, SwipeRefreshLayout.IOnRefreshListener
    {
        private bool check_news = true;
        private MainActivity mainAct;
        private List<BeanNotify> lst_notify;
        private ListView listView;
        private NewsNewAdapter newsNewAdpter;
        private SwipeRefreshLayout swipe;
        private LinearLayout lnNodata;
        private ImageView imgBack;
        View view;
        private List<BeanNotify> filtered_news;
        public NewsFragment2()
        {

        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);          
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.News2, container, false);
            listView = view.FindViewById<ListView>(Resource.Id.lv_News_2);
            swipe = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_News2);
            imgBack = view.FindViewById<ImageView>(Resource.Id.img_News2_Back);
            lnNodata = view.FindViewById<LinearLayout>(Resource.Id.ln_EmptyData_News);
            swipe.SetOnRefreshListener(this);
            swipe.SetDistanceToTriggerSync(150);// in dips                                                         
            swipe.Refresh += HandleRefreshAsync;
            mainAct = (MainActivity)this.Activity;
            LoadList();
            LoadData();
            
            MEven.ReloadListNews += ReloadData;
            CmmEvent.UserTicketRequest += CmmEvent_UpdateCount;
            
            CmmDroidFunction.SetTitleToView(view);
            //listView.ItemClick += See;
            imgBack.Click += Back;


            return view;
        }

        private void Back(object sender, EventArgs e)
        {
            mainAct.BackFragment();
        }

        private async void HandleRefreshAsync(object sender, EventArgs e)
        {
            try
            {
                swipe.Refreshing = true;
                ProviderBase p_base = new ProviderBase();
                await Task.Run(() =>
                {
                    p_base.UpdateMasterData<BeanNotify>(true);
                    mainAct.RunOnUiThread(() =>
                    {
                        LoadList();
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

      

        private void ReloadData(object sender, MEven.ChangeListNewEventArgs e)
        {
            check_news = e.IsSuccess;
            LoadList();
            LoadData();
        }

        private void LoadData()
        {
            var keyNews = CmmFunction.GetAppSetting("NEWS_CATEGORY_ID");
            filtered_news = lst_notify.Where(x => x.AnnounCategoryId.Value.ToString() == keyNews).ToList();
            int a = filtered_news.Where(x => !x.FlgRead && x.ANStatus.Value != -1).Count();
            
            if (filtered_news != null && filtered_news.Count >= 0)
            {
                newsNewAdpter = new NewsNewAdapter(filtered_news, view.Context);
                listView.Adapter = newsNewAdpter;
                newsNewAdpter.NotifyDataSetChanged();               
            }
            else
            {
                lnNodata.Visibility = ViewStates.Visible;
            }
        }

        private void CmmEvent_UpdateCount(object sender, EventArgs e)
        {
            UpdateData();
        }
        private void LoadList()
        {
            var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
            string query = @"SELECT * FROM BeanNotify ORDER BY Modified DESC";
            lst_notify = conn.Query<BeanNotify>(query);
            conn.Close();
        }
        private async void UpdateData()
        {
            try
            {
                ProviderBase p_base = new ProviderBase();
                CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                await Task.Run(() =>
                {
                    p_base.UpdateMasterData<BeanNotify>(true);
                    mainAct.RunOnUiThread(() =>
                    {
                        LoadList();
                        LoadData();
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
        public override void OnResume()
        {
            base.OnResume();
        }

        public void OnRefresh()
        {
            throw new NotImplementedException();
        }
    }
}