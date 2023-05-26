using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using VNASchedule.Class;
using VNASchedule.Bean;
using Android.Text;
using Android.Content.Res;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Text.Style;
using Android.Text.Method;

namespace VNASchedule.Droid.Code.Adapter
{
    class HelpDeskAdapter : BaseExpandableListAdapter
    {
        private Context context;
        private List<BeanHelpDesk> lst_FAQs;
        private View _rootView;
        private string keyWord = "";
        private int startPos;
        private int endPos;

        public HelpDeskAdapter(Context context, List<BeanHelpDesk> lst_FAQs)
        {
            this.context = context;
            this.lst_FAQs = lst_FAQs;
        }
        public HelpDeskAdapter(Context context, List<BeanHelpDesk> lst_FAQs, string keyWord)
        {
            this.context = context;
            this.lst_FAQs = lst_FAQs;
            this.keyWord = keyWord;
        }

        public override int GroupCount
        {
            get
            {
                return lst_FAQs.Count;
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return false;
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            if (!string.IsNullOrEmpty(lst_FAQs[groupPosition].ReplyContent))
                return 1;
            else
                return 0;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(context);
            View rootView = mInflater.Inflate(Resource.Layout.BodyItemFAQs, null, false);
            TextView tv = rootView.FindViewById<TextView>(Resource.Id.tv_BodyItemFAQs);
            tv.TextFormatted = Html.FromHtml(lst_FAQs[groupPosition].ReplyContent);
            tv.MovementMethod = LinkMovementMethod.Instance;
            return rootView;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return groupPosition;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            _rootView = convertView;
            if (_rootView == null)
            {
                LayoutInflater mInflater = LayoutInflater.From(context);
                _rootView = mInflater.Inflate(Resource.Layout.SupTitleHelpDesk, null);

            }
            ImageView img = _rootView.FindViewById<ImageView>(Resource.Id.img_SupTitleHelpDesk_);
            TextView tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_SupTitleHelpDesk_Title);
            tv_title.Text = lst_FAQs[groupPosition].Content;
            TextView tv_date = _rootView.FindViewById<TextView>(Resource.Id.tv_SupTitleHelpDesk_Date);
            if (lst_FAQs[groupPosition].Created.HasValue)
            {
                tv_date.Text = lst_FAQs[groupPosition].Created.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
            }
            else
            {
                tv_date.Text = "";
            }
            if (!string.IsNullOrEmpty(lst_FAQs[groupPosition].ReplyContent))
            {
                img.Visibility = ViewStates.Visible;
            }
            else
            {
                img.Visibility = ViewStates.Invisible;
            }
            if (isExpanded)
            {
                img.SetImageResource(Resource.Drawable.icon_reply);
            }
            else
            {
                img.SetImageResource(Resource.Drawable.icon_reply);
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                if (!string.IsNullOrEmpty(lst_FAQs[groupPosition].Content))
                {
                    string tittle = CmmFunction.RemoveSignVietnamese(lst_FAQs[groupPosition].Content.Trim().ToLowerInvariant());
                    startPos = tittle.IndexOf(keyWord.ToLower());
                    endPos = startPos + keyWord.Length;
                    if (startPos != -1)
                    {
                        ISpannable spannable = new SpannableString(lst_FAQs[groupPosition].Content.Trim());
                        ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
                        TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
                        spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
                        tv_title.SetText(spannable, TextView.BufferType.Spannable);
                    }
                }
            }
            else
            {
                tv_title.Text = lst_FAQs[groupPosition].Content;

            }

            return _rootView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return false;
        }
    }
}