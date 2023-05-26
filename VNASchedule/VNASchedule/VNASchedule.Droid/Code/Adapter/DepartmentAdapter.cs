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

namespace VNASchedule.Droid.Code.Adapter
{
    class DepartmentAdapter : BaseAdapter<BeanDepartment>
    {
        private Context context;
        private List<BeanDepartment> lst_deparment;

        public DepartmentAdapter(List<BeanDepartment> lst_deparment, Context context)
        {
            this.lst_deparment = lst_deparment;
            this.context = context;
        }
        public override BeanDepartment this[int position]
        {
            get
            {
                return lst_deparment[position];
            }
        }

        public override int Count
        {
            get
            {
                return lst_deparment.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.ItemDepartment, null);
            TextView tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemDepartment);
            tv_title.Text = lst_deparment[position].TitleEN;
            return _rootView;
        }
    }
}