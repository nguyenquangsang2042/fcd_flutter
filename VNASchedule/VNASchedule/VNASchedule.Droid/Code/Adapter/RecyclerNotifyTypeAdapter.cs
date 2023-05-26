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
    class RecyclerNotifyTypeAdapter : RecyclerView.Adapter
    {
        List<BeanAnnouncementCategory> lst_beanAnnounce = new List<BeanAnnouncementCategory>();
        List<BeanDepartment> lst_department = new List<BeanDepartment> ();
        private bool showEngName;
        Context context;
        public event EventHandler<int> ItemClick;
        private MainActivity mainAct;
        private View itemView;
        public int mSelectedItem = -1;

        public RecyclerNotifyTypeAdapter(List<BeanAnnouncementCategory> lst_beanAnnounce, Context context,    MainActivity mainAct)
        {
            this.context = context;
            this.mainAct = mainAct;
            this.lst_beanAnnounce = lst_beanAnnounce;
        }
        public RecyclerNotifyTypeAdapter(List<BeanDepartment> lst_department, Context context, MainActivity mainAct,bool showEngname)
        {
            this.context = context;
            this.mainAct = mainAct;
            this.lst_department = lst_department;
            this.showEngName = showEngname;
        }
        public override int ItemCount
        {
            get
            {

                if (lst_beanAnnounce != null && lst_beanAnnounce.Count>0)
                {
                    return lst_beanAnnounce.Count;

                }
                else if (lst_department != null && lst_department.Count > 0)
                {
                    return lst_department.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (lst_beanAnnounce != null && lst_beanAnnounce.Count>0)
            {
                BeanAnnouncementCategory beanAnnouncementCategory = new BeanAnnouncementCategory();
                beanAnnouncementCategory = lst_beanAnnounce[position];
                RecyclerNotifyTypeViewHolder vh = holder as RecyclerNotifyTypeViewHolder;
                vh.rdBTn.Text = beanAnnouncementCategory.Title;
                vh.rdBTn.Checked = (position == mSelectedItem);
            }
            else if (lst_department!= null && lst_department.Count > 0)
            {

                BeanDepartment beanDepartment = new BeanDepartment();
                beanDepartment = lst_department[position];
                RecyclerNotifyTypeViewHolder vh = holder as RecyclerNotifyTypeViewHolder;
                if(!string.IsNullOrEmpty(beanDepartment.TitleEN)&&showEngName == true)
                {
                    vh.rdBTn.Text = beanDepartment.TitleEN;

                }
                else
                {
                    vh.rdBTn.Text = beanDepartment.Title;

                }
                vh.rdBTn.Checked = (position == mSelectedItem);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemCatagoryNotify, parent, false);
            RecyclerNotifyTypeViewHolder recyclerNotifyTypeViewHolder = new RecyclerNotifyTypeViewHolder(itemView, OnClick);
            return recyclerNotifyTypeViewHolder;
        }
        public class RecyclerNotifyTypeViewHolder : RecyclerView.ViewHolder
        {
            public RadioButton rdBTn { get; private set; }
            public RelativeLayout rl_notify_type { get; private set; }
            public RecyclerNotifyTypeViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                rdBTn = itemView.FindViewById<RadioButton>(Resource.Id.radioButton_noti_catagory);
                rl_notify_type = itemView.FindViewById<RelativeLayout>(Resource.Id.rl_notify_type);
                rdBTn.Click += (ee, vv) => listener(Position);
                rl_notify_type.Click += (ee, vv) => listener(Position);
            }
        }
        private void OnClick(int obj)
        {
            mSelectedItem = obj;
            NotifyDataSetChanged();

            if (ItemClick != null)
                ItemClick(this, obj);
        }

    }
}