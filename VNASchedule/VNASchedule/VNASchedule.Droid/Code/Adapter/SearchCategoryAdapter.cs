using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace VNASchedule.Droid.Code.Adapter
{
    class SearchCategoryAdapter :  RecyclerView.Adapter
    {

        List<string> lst_search_category = new List<string>();
     
        Context context;
        public event EventHandler<int> ItemClick;
        private MainActivity mainAct;
        private View itemView;
        private int mSelectedItem = -1;

        public SearchCategoryAdapter(List<string> search_category, Context context, MainActivity mainAct)
        {
            this.context = context;
            this.mainAct = mainAct;
            this.lst_search_category = search_category;
        }
      
        public override int ItemCount
        {
            get
            {

                return lst_search_category.Count();
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (lst_search_category != null && lst_search_category.Count > 0)
            {
                string selected_category ="";
                selected_category = lst_search_category[position];
                RecyclerNotifyTypeViewHolder vh = holder as RecyclerNotifyTypeViewHolder;
                vh.rdBTn.Text = selected_category;
                vh.rdBTn.Checked = (position == mSelectedItem);
                vh.linear_background.SetBackgroundColor(new Color(ContextCompat.GetColor(context, Resource.Color.headerBlue)));
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
            public LinearLayout linear_background { get; private set; }
            public RecyclerNotifyTypeViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                rdBTn = itemView.FindViewById<RadioButton>(Resource.Id.radioButton_noti_catagory);
                rl_notify_type = itemView.FindViewById<RelativeLayout>(Resource.Id.rl_notify_type);
                linear_background = itemView.FindViewById<LinearLayout>(Resource.Id.ln_back_ground);
                rdBTn.Click += (ee, vv) => listener(Position);
                rl_notify_type.Click += (ee, vv) => listener(Position);
            }
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
            mSelectedItem = obj;
            NotifyDataSetChanged();
        }

    }
}