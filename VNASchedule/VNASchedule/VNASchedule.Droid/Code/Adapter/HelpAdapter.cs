using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using VNASchedule.Bean;

namespace VNASchedule.Droid.Code.Adapter
{
    public class HelpAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        private Context context;
        private List<BeanHelpDeskCategory> lst_FAQs;
        private List<BeanDepartment> lst_Department;


        public HelpAdapter(Context context, List<BeanHelpDeskCategory> lst_FAQs)
        {
            this.context = context;
            this.lst_FAQs = lst_FAQs;
        }
        public HelpAdapter(Context context, List<BeanDepartment> lst_Department)
        {
            this.context = context;
            this.lst_Department = lst_Department;
        }

        //public override BeanHelpDeskCategory this[int position]
        //{
        //    get
        //    {            
        //            return lst_FAQs[position];
        //    }

        //}
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(lst_Department != null && lst_Department.Count > 0)
            {
                HelpViewHolder vh = holder as HelpViewHolder;
                vh.tv_name.Text = lst_Department[position].TitleEN;
            }
            else if (lst_FAQs != null && lst_FAQs.Count > 0)
            {
                HelpViewHolder vh = holder as HelpViewHolder;
                vh.tv_name.Text = lst_FAQs[position].Title;
            }
          
        }
        public override int ItemCount
        {
            get
            {
                if (lst_Department !=null && lst_Department.Count>0)
                {
                    return lst_Department.Count;
                }
                else if (lst_FAQs != null && lst_FAQs.Count > 0)
                {
                    return lst_FAQs.Count;
                }
                return 0;

            }
        }

        //public override int Count
        //{
        //    get
        //    {

        //            return lst_FAQs.Count;

        //    }

        //}

        //public override long GetItemId(int position)
        //{
        //    return position;
        //}

        //public override View GetView(int position, View convertView, ViewGroup parent)
        //{
        //    LayoutInflater mInflater = LayoutInflater.From(this.context);
        //    View _rootView = mInflater.Inflate(Resource.Layout.ItemUserFamily, null);
        //    TextView tv_name = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemUserFamily);
        //    tv_name.Text = lst_FAQs[position].Title;

        //    return _rootView;
        //}
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.ItemUserFamily, parent, false);
            HelpViewHolder notifyViewHolder = new HelpViewHolder(itemView, OnClick);
            return notifyViewHolder;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
    class HelpViewHolder : RecyclerView.ViewHolder
    {
        public TextView tv_name { get; private set; }
        public HelpViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            tv_name = itemView.FindViewById<TextView>(Resource.Id.tv_ItemUserFamily);
            itemView.Click += (sender, e) => listener(base.Position);

        }
    }
}