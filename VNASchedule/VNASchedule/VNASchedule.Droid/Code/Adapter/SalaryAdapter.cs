using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using VNASchedule.Bean;

namespace VNASchedule.Droid.Code.Adapter
{
    public class SalaryAdapter : BaseAdapter<BeanSalary>
    {
        private Context context;
        private List<BeanSalary> lst_salary;

        public SalaryAdapter(List<BeanSalary> lst_salary, Context context)
        {
            this.lst_salary = lst_salary;
            this.context = context;
        }

        public override BeanSalary this[int position]
        {
            get
            {
                return lst_salary[position];
            }
        }

        public override int Count
        {
            get
            {
                return lst_salary.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.ItemPayroll, null);
            TextView tv_content = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemPayroll_Content);
            TextView tv_date = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemPayroll_date);
            _rootView.SetBackgroundColor(position % 2 == 0 ? new Color(ContextCompat.GetColor(context, Resource.Color.white)) : new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));
            tv_content.Text = lst_salary[position].Title;
            if (lst_salary[position].AtDate.HasValue)
            {
                tv_date.Text = lst_salary[position].AtDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
            }
            else
            {
                tv_date.Text = "";
            }

            return _rootView;
        }
    }
}