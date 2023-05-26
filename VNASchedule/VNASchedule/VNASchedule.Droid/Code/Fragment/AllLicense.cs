using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.DataProvider;
using VNASchedule.Droid.Code.Adapter;
using VNASchedule.Droid.Code.Class;

namespace VNASchedule.Droid.Code.Fragment
{
    public class AllLicense : Android.App.Fragment
    {
        private MainActivity mainAct;
        private List<BeanUserLicense> lst_license;
        private LicenceAdapter licenceadapter;
        private ListView listViewLicense;
        private LinearLayout lnNoData;
        View view;
        public AllLicense() { }
        public AllLicense(List<BeanUserLicense> lst_license)
        {
            this.lst_license = lst_license;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.AllLicense, container, false);
            listViewLicense = view.FindViewById<ListView>(Resource.Id.lv_AllLicense);
            lnNoData = view.FindViewById<LinearLayout>(Resource.Id.ln_EmptyData_All_License);
            mainAct = (MainActivity)this.Activity;

            SetData();
            //LoadList();

            return view;
        }
        private void SetData()
        {
            try
            {
                if (lst_license != null && lst_license.Count > 0)
                {
                    LoadData();
                }
                else
                {
                    lnNoData.Visibility = ViewStates.Visible;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void LoadList()
        {
            try
            {
                ProviderLicense p_license = new ProviderLicense();
                ProviderBase pBase = new ProviderBase();
                if (CmmDroidFunction.hasConnection())
                {
                    await Task.Run(() =>
                    {
                        lst_license = p_license.GetlicenseFromServer();
                        pBase.UpdateMasterData<BeanUserLicense>(true, 30, false);
                        if (lst_license != null && lst_license.Count > 0)
                        {
                            // lst_license = lst_license.OrderBy(t => t.ExpireDate.Value).OrderByDescending(t => t.ExpireDate.Value).ThenByDescending(i => i.IsImportant).ToList();
                            mainAct.RunOnUiThread(() =>
                            {
                                LoadData();
                            });
                        }
                        else
                        {
                            mainAct.RunOnUiThread(() =>
                            {
                                lnNoData.Visibility = ViewStates.Visible;
                            });
                        }
                    });
                }
                else
                {
                    var conn = new SQLiteConnection(CmmVariable.M_DataPath);
                    var _query = "SELECT * FROM BeanUserLicense ";
                    lst_license = conn.Query<BeanUserLicense>(_query);
                    conn.Close();
                    if (lst_license != null && lst_license.Count > 0)
                    {
                        mainAct.RunOnUiThread(() =>
                        {
                            LoadData();
                        });
                    }
                    else
                    {
                        mainAct.RunOnUiThread(() =>
                        {
                            lnNoData.Visibility = ViewStates.Visible;
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CmmDroidFunction.HideProcessingDialog();
            }
        }

        private void LoadData()
        {
            var datelimit = CmmFunction.GetAppSetting("T2");
            //var lst_filter_all = lst_license.OrderByDescending(t => t.ExpireDate.Value).ThenByDescending(i => i.IsImportant).ToList();
            //var lst_filter_red = lst_license.Where(l => (l.ExpireDate.Value.ToLocalTime().Date < DateTime.Now.Date) && l.Status == 1).OrderBy(t => t.ExpireDate.Value).ThenByDescending(i => i.IsImportant).ToList();
            //var lst_filter_orange = lst_license.Where(l => (l.ExpireDate.Value.ToLocalTime().Date > DateTime.Now.Date) && (l.ExpireDate.Value.ToLocalTime().Date < DateTime.Now.AddDays(Convert.ToDouble(datelimit)).Date) && (l.Status == 1)).OrderBy(t => t.ExpireDate.Value).ThenByDescending(i => i.IsImportant).ToList();
            //var lst_filter_gray = lst_license.Where(l => (l.ExpireDate.Value.ToLocalTime().Date < DateTime.Now.Date) && l.Status == -1).OrderBy(t => t.ExpireDate.Value).ThenByDescending(i => i.IsImportant).ToList();
            //var lst_filter_black = lst_license.Where(l => (l.ExpireDate.Value.ToLocalTime().Date >= DateTime.Now.AddDays(Convert.ToDouble(datelimit)))).OrderBy(t => t.ExpireDate.Value).ThenByDescending(i => i.IsImportant).ToList();
       
            var lst_filter_red = lst_license
                .Where(l => l.ExpireDate.HasValue && (l.ExpireDate.Value.ToLocalTime().Date < DateTime.Now.Date) && l.Status == 1)
                .OrderBy(t => t.ExpireDate.Value)
                .ThenByDescending(i => i.IsImportant)
                .ToList();

            var lst_filter_orange = lst_license
                .Where(l => l.ExpireDate.HasValue && (l.ExpireDate.Value.ToLocalTime().Date >= DateTime.Now.Date) 
                && (l.ExpireDate.Value.ToLocalTime().Date <= DateTime.Now.AddDays(Convert.ToDouble(datelimit)).Date) && (l.Status == 1))
                .OrderBy(t => t.ExpireDate.Value)
                .ThenByDescending(i => i.IsImportant)
                .ToList();

            var lst_filter_gray = lst_license
                .Where(l => l.ExpireDate.HasValue && (l.ExpireDate.Value.ToLocalTime().Date < DateTime.Now.Date) && l.Status == -1)
                .OrderBy(t => t.ExpireDate.Value)
                .ThenByDescending(i => i.IsImportant).ToList();

            var lst_filter_black = lst_license
                .Where(l => !l.ExpireDate.HasValue || (l.ExpireDate.Value.ToLocalTime().Date > DateTime.Now.AddDays(Convert.ToDouble(datelimit)).Date))
                .OrderByDescending(t => t.ExpireDate.HasValue)
                .ThenByDescending(i => i.IsImportant).ToList();

            if (lst_filter_orange != null && lst_filter_orange.Count > 0)
                lst_filter_red.AddRange(lst_filter_orange);

            if (lst_filter_black != null && lst_filter_black.Count > 0)
                lst_filter_red.AddRange(lst_filter_black);

            if (lst_filter_gray != null && lst_filter_gray.Count > 0)
                lst_filter_red.AddRange(lst_filter_gray);

            licenceadapter = new LicenceAdapter(lst_filter_red, view.Context, datelimit);
            listViewLicense.Adapter = licenceadapter;
            licenceadapter.NotifyDataSetChanged();
        }
    }
}