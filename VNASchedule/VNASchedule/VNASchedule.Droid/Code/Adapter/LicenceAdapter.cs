using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VNASchedule.Bean;
using Android.Graphics;
using VNASchedule.Class;
using System.Globalization;
using Android.Support.V4.Content;

namespace VNASchedule.Droid.Code.Adapter
{
    public class LicenceAdapter : BaseAdapter<BeanUserLicense>
    {
        private Context context;
        private List<BeanUserLicense> lst_filter_valid;
        private string datelimit = "";

        public LicenceAdapter(List<BeanUserLicense> lst_filter_valid, Context context, string datelimit)
        {
            this.lst_filter_valid = lst_filter_valid;
            this.context = context;
            this.datelimit = datelimit;
        }

        public override BeanUserLicense this[int position]
        {
            get
            {
                return lst_filter_valid[position];
            }
        }

        public override int Count
        {
            get
            {
                return lst_filter_valid.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.ItemLicence, null);
            TextView tv_date = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemLicence_Date);
            TextView tv_type = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemLicence_Type);
            TextView tv_note = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemLicence_Note);
            TextView tv_num = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemLicence_Num);
            LinearLayout ln = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_ItemLicence_Warning);

            ln.Visibility = ViewStates.Invisible;
            tv_type.Text = lst_filter_valid[position].LicenseType;
            tv_num.Text = lst_filter_valid[position].Num;
            tv_note.Text = lst_filter_valid[position].Note;
            if (CmmVariable.SysConfig.UserType == 1)//1 là phi công 
            {
                tv_note.Visibility = ViewStates.Gone;
                tv_num.Visibility = ViewStates.Gone;
                //FlightScheduleFragment FlightSchedule = new FlightScheduleFragment();
                //mainAct.ShowFragment(FragmentManager, FlightSchedule, "FlightSchedule");
            }
            _rootView.SetBackgroundColor(position % 2 == 0 ? new Color(ContextCompat.GetColor(context, Resource.Color.white)) : new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));
            if (lst_filter_valid[position].ExpireDate.HasValue)
            {
                tv_date.Text = lst_filter_valid[position].ExpireDate.Value.ToLocalTime().ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                if (lst_filter_valid[position].ExpireDate.Value.ToLocalTime().Date < DateTime.Now.Date && lst_filter_valid[position].Status == -1) // exprire
                {
                    //tv_date.SetTextColor(Color.ParseColor("#6E6E6E"));
                    //tv_note.SetTextColor(Color.ParseColor("#6E6E6E"));
                    //tv_num.SetTextColor(Color.ParseColor("#6E6E6E"));
                    //tv_type.SetTextColor(Color.ParseColor("#6E6E6E"));
                    tv_date.SetTextColor(Color.ParseColor("#525252"));
                    tv_note.SetTextColor(Color.ParseColor("#525252"));
                    tv_num.SetTextColor(Color.ParseColor("#525252"));
                    tv_type.SetTextColor(Color.ParseColor("#525252"));
                    ln.Visibility = ViewStates.Invisible;
                }
                else if ((lst_filter_valid[position].ExpireDate.Value.ToLocalTime().Date < DateTime.Now.Date) && lst_filter_valid[position].Status == 1) // exprire, need update
                {
                    tv_date.SetTextColor(Color.Red);
                    tv_note.SetTextColor(Color.Red);
                    tv_num.SetTextColor(Color.Red);
                    tv_type.SetTextColor(Color.Red);
                    ln.Visibility = ViewStates.Invisible;
                }
                else if (lst_filter_valid[position].ExpireDate.Value.ToLocalTime().Date < DateTime.Now.AddDays(int.Parse(datelimit)).Date) // close exprire 7 days
                {
                    if (lst_filter_valid[position].IsImportant)
                    {
                        tv_date.SetTypeface(Typeface.Create("sans-serif", TypefaceStyle.Normal), TypefaceStyle.Normal);
                        tv_date.SetTextColor(Color.Orange);
                        tv_note.SetTextColor(Color.Orange);
                        tv_num.SetTextColor(Color.Orange);
                        tv_type.SetTextColor(Color.Orange);
                        ln.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        tv_date.SetTypeface(Typeface.Create("sans-serif", TypefaceStyle.Normal), TypefaceStyle.Normal);
                        tv_date.SetTextColor(Color.Orange);
                        tv_note.SetTextColor(Color.Orange);
                        tv_num.SetTextColor(Color.Orange);
                        tv_type.SetTextColor(Color.Orange);
                        ln.Visibility = ViewStates.Invisible;
                    }
                }
                else
                {
                    ln.Visibility = ViewStates.Invisible;

                }
                tv_note.Visibility = ViewStates.Gone;
            }
            else
            {
                tv_date.Text = "";
                tv_note.Visibility = ViewStates.Gone;
            }
            return _rootView;
        }
    }
}