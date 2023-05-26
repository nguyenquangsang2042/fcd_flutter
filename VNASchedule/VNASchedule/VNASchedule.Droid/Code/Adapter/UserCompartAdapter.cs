using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Assist;
using VNASchedule.Bean;
using VNASchedule.Class;

namespace VNASchedule.Droid.Code.Adapter
{
    class UserCompartAdapter : RecyclerView.Adapter
    {
        private Context context;
        private List<ClassUserCompact> lst_contact = new List<ClassUserCompact>();
        public DisplayImageOptions options;
        private MainActivity mainAct;
        private UserCompactViewHolder vh;
        public event EventHandler<int> ItemClick, ItemPhoneClick;
        private int type = 0; // 0 : Phi công, 1 : Tiếp viên
        public UserCompartAdapter(Context context, List<ClassUserCompact> lst_contact, MainActivity mainAct,int type)
        {
            this.context = context;
            this.mainAct = mainAct;
            this.lst_contact = lst_contact;
            this.type = type;
            this.lst_contact.ForEach(r => r.Position = r.Position.ToUpper());
        }

        //public override ClassUserCompact this[int position]
        //{
        //    get
        //    {
        //        return lst_contact[position];
        //    }
        //}

        //public override int Count
        //{
        //    get
        //    {
        //        return lst_contact.Count;
        //    }
        //}

        public override int ItemCount
        {
            get
            {
                return lst_contact.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        private void loadImageUniversal(string url, ImageView imageView)
        {
            options = new DisplayImageOptions.Builder()
                                          .CacheInMemory(true)
                                          .CacheOnDisk(true)
                                          .BitmapConfig(Bitmap.Config.Rgb565)
                                          .ShowImageForEmptyUri(Resource.Drawable.icon_avatar64)
                                          .ImageScaleType(ImageScaleType.Exactly)
                                          .ShowImageOnFail(Resource.Drawable.icon_avatar64)
                                          .ShowImageOnLoading(Resource.Drawable.icon_avatar64)
                                          .Build();
            mainAct.imageLoader.DisplayImage(url, imageView, options);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            vh = holder as UserCompactViewHolder;
            string url = CmmVariable.M_Domain + "/" + lst_contact[position].Avatar + "?ver=" + DateTime.Now.ToString("yyyyMMddHHmmss");
            if (type == 0)
            {
                vh.tv_name.Text = lst_contact[position].FullName;
                vh.tv_position.Text = lst_contact[position].Position;
                if (string.IsNullOrEmpty(lst_contact[position].Mobile))
                {
                    vh.img_phone.Visibility = ViewStates.Invisible;
                    vh.view_phone.Visibility = ViewStates.Invisible;
                }
                if (!string.IsNullOrEmpty(lst_contact[position].Code2))
                {
                    vh.tv_crew_code.Text = "Crew code: " + lst_contact[position].Code2;
                }
                else
                {
                    vh.tv_crew_code.Text = "Crew code:";
                }
                //holder.tv_avata.Visibility = ViewStates.Visible;
                //holder.img.Visibility = ViewStates.Invisible;
                if (string.IsNullOrEmpty(lst_contact[position].Avatar))
                {
                    //vh.img_pilot_avatar.Visibility = ViewStates.Gone;
                    //vh.tv_ava.Visibility = ViewStates.Visible;
                    //Random rnd = new Random();
                    //var color = Color.Rgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    //vh.tv_ava.BackgroundTintList = ColorStateList.ValueOf(color);
                    //if (!string.IsNullOrEmpty(lst_contact[position].FullName))
                    //{
                    //    vh.tv_ava.Text = lst_contact[position].FullName.Substring(0, 1).ToUpper();
                    //}

                    vh.img_pilot_avatar.Visibility = ViewStates.Visible;
                    vh.tv_ava.Visibility = ViewStates.Gone;
                    vh.img_pilot_avatar.SetImageResource(Resource.Drawable.defaultAvatar);
                }
                else
                {
                    vh.img_pilot_avatar.Visibility = ViewStates.Visible;
                    vh.tv_ava.Visibility = ViewStates.Gone;
                    loadImageUniversal(url, vh.img_pilot_avatar);
                }
                vh.tv_name.SetTextColor(Color.ParseColor("#006784"));
            }
            else
            {
                vh.tv_name.Text = lst_contact[position].FullName;
                vh.tv_position.Text = lst_contact[position].Position;
                if (string.IsNullOrEmpty(lst_contact[position].Mobile))
                {
                    vh.img_phone.Visibility = ViewStates.Invisible;
                    vh.view_phone.Visibility = ViewStates.Invisible;
                }
                if (!string.IsNullOrEmpty(lst_contact[position].Code2))
                {
                    vh.tv_crew_code.Text = "Crew code: " + lst_contact[position].Code2;
                }
                else
                {
                    vh.tv_crew_code.Text = "Crew code:";
                }
                //holder.tv_avata.Visibility = ViewStates.Visible;
                //holder.img.Visibility = ViewStates.Invisible;
                if (string.IsNullOrEmpty(lst_contact[position].Avatar))
                {
                    //vh.img_pilot_avatar.Visibility = ViewStates.Gone;
                    //vh.tv_ava.Visibility = ViewStates.Visible;
                    //Random rnd = new Random();
                    //var color = Color.Rgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    //vh.tv_ava.BackgroundTintList = ColorStateList.ValueOf(color);
                    //if (!string.IsNullOrEmpty(lst_contact[position].FullName))
                    //{
                    //    vh.tv_ava.Text = lst_contact[position].FullName.Substring(0, 1).ToUpper();
                    //}

                    vh.img_pilot_avatar.Visibility = ViewStates.Visible;
                    vh.tv_ava.Visibility = ViewStates.Gone;
                    vh.img_pilot_avatar.SetImageResource(Resource.Drawable.defaultAvatar);
                }
                else
                {
                    vh.img_pilot_avatar.Visibility = ViewStates.Visible;
                    vh.tv_ava.Visibility = ViewStates.Gone;
                    loadImageUniversal(url, vh.img_pilot_avatar);
                }
                vh.tv_name.SetTextColor(Color.ParseColor("#d09b2c"));
            }
        }

        private void OnClick1(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }

        private void OnClickPhoneClick(int obj)
        {
            //if (ItemPhoneClick != null)
            //    ItemPhoneClick(this, obj);
            ItemClick?.Invoke(this, obj);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemUserCompart, parent, false);
            UserCompactViewHolder notifyViewHolder = new UserCompactViewHolder(itemView, OnClick1, OnClickPhoneClick);
            return notifyViewHolder;
        }

        //class AttendantCompactViewHolder: RecyclerView.ViewHolder
        //{
        //    public TextView attendent_name { get; private set; }
        //    public ImageView img_attendent_phone { get; private set; }
        //    public TextView attendent_position { get; private set; }
        //    public ImageView img_attendent_avatar { get; private set; }
        //    public TextView attendent_crew_code { get; private set; }
        //    public TextView attendent_ava { get; private set; }

        //    public AttendantCompactViewHolder(View itemView, Action<int> listenerItemClick, Action<int> listenerPhoneClick) : base(itemView)
        //    {
        //        attendent_name = itemView.FindViewById<TextView>(Resource.Id.tv_ItemUserConpart_Name);
        //        attendent_ava = itemView.FindViewById<TextView>(Resource.Id.tv_ava);
        //        img_attendent_phone = itemView.FindViewById<ImageView>(Resource.Id.img_ItemUserCompart_Phone);
        //        attendent_position = itemView.FindViewById<TextView>(Resource.Id.tv_ItemUserConpart_Position);
        //        img_attendent_avatar = itemView.FindViewById<ImageView>(Resource.Id.img_pilot_avatar);
        //        attendent_crew_code = itemView.FindViewById<TextView>(Resource.Id.tv_crew_code);
        //        img_attendent_phone.Click += (sender, e) => listenerPhoneClick(base.Position);
        //        img_attendent_avatar.Click += (sender, e) => listenerItemClick(base.Position);
        //    }
        //}

        class UserCompactViewHolder : RecyclerView.ViewHolder
        {
            public TextView tv_name { get; private set; }
            public ImageView img_phone { get; private set; }
            public TextView tv_position { get; private set; }
            public TextView view_phone { get; private set; }           
            public ImageView img_pilot_avatar { get; private set; }
            public TextView tv_crew_code { get; private set; }
            public TextView tv_ava { get; private set; }
            public LinearLayout lnContainer_ItemCompart { get; private set; }

            public UserCompactViewHolder(View itemView, Action<int> listenerItemClick, Action<int> listenerPhoneClick) : base(itemView)
            {
                tv_name = itemView.FindViewById<TextView>(Resource.Id.tv_ItemUserConpart_Name);
                tv_ava = itemView.FindViewById<TextView>(Resource.Id.tv_ava);
                view_phone = itemView.FindViewById<TextView>(Resource.Id.view_ItemUserCompart_Phone); 
                img_phone = itemView.FindViewById<ImageView>(Resource.Id.img_ItemUserCompart_Phone);
                tv_position = itemView.FindViewById<TextView>(Resource.Id.tv_ItemUserConpart_Position);
                img_pilot_avatar = itemView.FindViewById<ImageView>(Resource.Id.img_pilot_avatar);
                tv_crew_code = itemView.FindViewById<TextView>(Resource.Id.tv_crew_code);
                lnContainer_ItemCompart = itemView.FindViewById<LinearLayout>(Resource.Id.lnContainer_ItemCompart);
                img_phone.Click += (sender, e) => listenerPhoneClick(base.Position);
                itemView.Click += (sender, e) => listenerItemClick(base.Position);
            }
        }
    }
}