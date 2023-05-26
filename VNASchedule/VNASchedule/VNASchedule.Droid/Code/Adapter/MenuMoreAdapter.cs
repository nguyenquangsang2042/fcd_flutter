using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VNASchedule.Droid.Code.Adapter
{
    public class MenuMoreAdapter : BaseAdapter
    {

        Context context;
        LayoutInflater _inflter;
        List<KeyValuePair<string, int>> listItem = new List<KeyValuePair<string, int>>();
        bool loadDone;

        public MenuMoreAdapter(Context context, Dictionary<string, int> items, bool loadDone)
        {
            this.context = context;
            this.loadDone = loadDone;
            if (items.Count % 2 != 0)
                items.Add("", 0);

            listItem = items.ToList<KeyValuePair<string, int>>();
            _inflter = (LayoutInflater.From(context));
        }

        public KeyValuePair<string, int> GetItemIndex(int position)
        {
            return listItem[position];
        }

        public override bool IsEnabled(int position)
        {
            if (listItem[position].Key.Contains("Contact") && !loadDone)
                return false;
            else
                return true;
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            MenuMainAdapterViewHolder holder = null;
            TextView textView;
            ImageView imageView;
            LinearLayout ln_itemMenu;
            ProgressBar progress_Contact;

            if (holder == null)
                holder = new MenuMainAdapterViewHolder();

            view = view ?? _inflter.Inflate(Resource.Layout.layoutmenumoreitem, parent, false);

            View lineSperator = view.FindViewById<View>(Resource.Id.lineSperator);
            textView = view.FindViewById<TextView>(Resource.Id.tv_itemmenu);
            imageView = view.FindViewById<ImageView>(Resource.Id.imgItemMenu);
            ln_itemMenu = view.FindViewById<LinearLayout>(Resource.Id.ln_ItemMore);
            progress_Contact = view.FindViewById<ProgressBar>(Resource.Id.progressBar1);

            LinearLayout ln6 = view.FindViewById<LinearLayout>(Resource.Id.linearLayout6);

            if(position == 0)
            {
                lineSperator.Visibility = ViewStates.Gone;
                ln6.SetBackgroundResource(Resource.Drawable.menu_more_corner_radius);
            }    

            textView.Text = listItem[position].Key;
            imageView.SetImageResource(listItem[position].Value);
            if (listItem[position].Key.Contains("Contact"))
            {
                ln_itemMenu.Enabled = false;
                progress_Contact.Visibility = ViewStates.Visible;
                if (loadDone)
                    progress_Contact.Visibility = ViewStates.Gone;
            }
            else
            {
                ln_itemMenu.Enabled = true;
                progress_Contact.Visibility = ViewStates.Gone;
            }

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return listItem.Count();
            }
        }
        public void SetVisibleWaiting(bool isVisible)
        {
            this.loadDone = isVisible;
        }
    }

    class MenuMoreAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}