using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using VNASchedule.Bean;
using VNASchedule.Class;

namespace VNASchedule.Droid.Code.Adapter
{
    class LibraryAdapter : BaseAdapter<BeanLibrary>
    {
        private Context context;
        private List<BeanLibrary> lst_library;
        int startPos, endPos;
        string filteredText;

        public LibraryAdapter(Context context, List<BeanLibrary> lst_library,string filteredText)
        {
            this.filteredText = filteredText;
            this.context = context;
            this.lst_library = lst_library;
        }

        public override BeanLibrary this[int position]
        {
            get
            {
                return lst_library[position];
            }
        }

        public override int Count
        {
            get
            {
                return lst_library.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.ItemLibrary, null);
            TextView tv_date = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemLibrary_Date);
            TextView tv_name = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemLibrary_Name);
            ImageView img_more = _rootView.FindViewById<ImageView>(Resource.Id.img_ItemLibrary_More);
            ImageView img_foder = _rootView.FindViewById<ImageView>(Resource.Id.img_ItemLibrary_Fo);
            _rootView.SetBackgroundColor(position % 2 == 0 ? new Color(ContextCompat.GetColor(context, Resource.Color.white)) : new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));
            if (lst_library[position].Type == 2)
            {
                img_more.Visibility = ViewStates.Invisible;
                if (lst_library[position].Created.HasValue)
                {
                    tv_date.Text = lst_library[position].Created.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
                }
                else
                {
                    tv_date.Text = "";
                }
                if(lst_library[position].FileType.Equals(".pdf"))
                {
                    img_foder.SetImageResource(Resource.Drawable.icon_pdf);
                }
                else if(lst_library[position].FileType.Equals(".xlsx")|| (lst_library[position].FileType.Equals(".doc") ))
                {
                    img_foder.SetImageResource(Resource.Drawable.icon_xlsx);
                }
                else if ((lst_library[position].FileType.Equals(".docx")) || (lst_library[position].FileType.Equals(".xls")))
                {
                    img_foder.SetImageResource(Resource.Drawable.icon_docx);
                }
                else if (lst_library[position].FileType.Equals(".jpg") || (lst_library[position].FileType.Equals(".jpeg"))
                    || (lst_library[position].FileType.Equals(".png")))
                {
                    img_foder.SetImageResource(Resource.Drawable.icon_image);
                }
                else if (lst_library[position].FileType.Equals(".mp3"))
                {
                    img_foder.SetImageResource(Resource.Drawable.icon_mp3);
                }
                else if (lst_library[position].FileType.Equals(".mp4"))
                {
                    img_foder.SetImageResource(Resource.Drawable.icon_mp4);
                }
                else
                {
                    img_foder.SetImageResource(Resource.Drawable.icon_file_blank);
                }
            }
            else if(lst_library[position].Type == 1)
            {
                tv_date.Text = lst_library[position].Items.ToString()+" Items";
                img_foder.SetImageResource(Resource.Drawable.icon_folder);
                img_more.Visibility = ViewStates.Visible;
            }
            tv_name.Text = lst_library[position].Name;
            if (!string.IsNullOrEmpty(lst_library[position].Name))
            {
                string tittle = CmmFunction.RemoveSignVietnamese(lst_library[position].Name.Trim().ToLowerInvariant());
                startPos = tittle.IndexOf(filteredText.ToLower());
                endPos = startPos + filteredText.Length;

                if (startPos != -1)
                {
                    ISpannable spannable = new SpannableString(lst_library[position].Name.Trim());
                    ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { Color.Orange });
                    TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
                    spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
                    tv_name.SetText(spannable, TextView.BufferType.Spannable);
                }
            }
            return _rootView;
        }
    }
}