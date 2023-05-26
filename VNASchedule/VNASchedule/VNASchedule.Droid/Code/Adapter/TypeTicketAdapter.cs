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
    public class TypeTicketAdapter : BaseAdapter<BeanUserTicketCategory>
    {
        private Context context;
        private List<BeanUserTicketCategory> lst_category;

        public TypeTicketAdapter(List<BeanUserTicketCategory> lst_category, Context context)
        {
            this.lst_category = lst_category;
            this.context = context;
        }

        public override BeanUserTicketCategory this[int position]
        {
            get
            {
                return lst_category[position];
            }
        }

        public override int Count
        {
            get
            {
                return lst_category.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.ItemTicketCatelogy, null);
            TextView tv_date = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemTicketCatelogy);
            tv_date.Text = lst_category[position].TitleShort;
            return _rootView;
        }
    }
}