using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VNASchedule.Bean;
using VNASchedule.Class;

namespace VNASchedule.Droid.Code.Adapter
{
    public class RecyclerApplicationAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        Context context;
        List<BeanMenuApp> Items;
        List<BeanMenuApp> CurrentItems;
        List<BeanMenuApp> RootItems;
        MainActivity mainAct;
        public string filterText { get; set; }

        View itemView;
        private int _LangId = 1066;
        public int LangId
        {
            get => _LangId;
            set => _LangId = value;
        }

        public RecyclerApplicationAdapter(List<BeanMenuApp> Items, Context context, MainActivity mainAct)
        {
            this.RootItems = Items;
            this.CurrentItems = SetCurrentItems();
            this.Items = CurrentItems;
            this.context = context;
            this.mainAct = mainAct;
        }

        public Filter Filter
        {
            get => ApplicationFilterHelper.newInstance(this.CurrentItems, this);
        }

        public void SetItems(List<BeanMenuApp> filtered)
        {
            this.Items = filtered;
        }

        private List<BeanMenuApp> SetCurrentItems()
        {
            return this.RootItems.Where(x => x.LanguageId == this.LangId).ToList();
        }

        public void SetLanguage(int LanguageID)
        {
            this.LangId = LanguageID;
            this.CurrentItems = SetCurrentItems();
            this.Items = CurrentItems;
        }

        public override int ItemCount
        {
            get => this.Items.Count;
        }

        public BeanMenuApp GetItem(int position)
        {
            return Items[position];
        }

        public override long GetItemId(int position)
        {
            //return base.GetItemId(position);
            return Items[position].ID;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerApplicationAdapterViewHolder viewHolder = holder as RecyclerApplicationAdapterViewHolder;
            BeanMenuApp item = Items[position];

            viewHolder.relative_application.SetBackgroundColor(position % 2 == 0 ? new Color(ContextCompat.GetColor(context, Resource.Color.white)) : new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));

            int startPos = -1;
            int endPos = -1;
            if (!string.IsNullOrEmpty(filterText))
            {
                startPos = CmmFunction.RemoveSignVietnamese(item.Title).ToLower().IndexOf(filterText.ToLower());
                endPos = startPos + filterText.Length;
            }

            if (startPos != -1)
                SetTextSpan(viewHolder.tv_ItemApp_Title, item.Title, startPos, endPos);
            else
                viewHolder.tv_ItemApp_Title.Text = item.Title;

        }

        private void SetTextSpan(TextView txt, string text, int sPos, int ePos)
        {
            Android.Text.ISpannable spannable = new Android.Text.SpannableString(text);
            Android.Content.Res.ColorStateList Red = new Android.Content.Res.ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
            Android.Text.Style.TextAppearanceSpan highlightSpan = new Android.Text.Style.TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
            spannable.SetSpan(highlightSpan, sPos, ePos, Android.Text.SpanTypes.ExclusiveExclusive);
            txt.SetText(spannable, TextView.BufferType.Spannable);
        }

        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.ItemApplications, parent, false);
            RecyclerApplicationAdapterViewHolder recyclerApplicationAdapterViewHolder = new RecyclerApplicationAdapterViewHolder(itemView, OnClick);
            return recyclerApplicationAdapterViewHolder;
        }

        public class RecyclerApplicationAdapterViewHolder : RecyclerView.ViewHolder
        {
            public RelativeLayout relative_application { get; set; }
            public TextView tv_ItemApp_Title { get; set; }
            public RecyclerApplicationAdapterViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                relative_application = itemView.FindViewById<RelativeLayout>(Resource.Id.relative_application);
                tv_ItemApp_Title = itemView.FindViewById<TextView>(Resource.Id.tv_ItemApp_Title);
                itemView.Click += (o, e) => listener(base.Position);
            }
        }

        public class ApplicationFilterHelper : Filter
        {
            static List<BeanMenuApp> currentList;
            static RecyclerApplicationAdapter adapter;
            public static ApplicationFilterHelper newInstance(List<BeanMenuApp> currentList, RecyclerApplicationAdapter adapter)
            {
                ApplicationFilterHelper.currentList = currentList;
                ApplicationFilterHelper.adapter = adapter;

                return new ApplicationFilterHelper();
            }
            protected override FilterResults PerformFiltering(Java.Lang.ICharSequence constraint)
            {
                FilterResults results = new FilterResults();
                if (constraint != null && constraint.Length() > 0)
                {
                    string query = CmmFunction.RemoveSignVietnamese(constraint.ToString()).ToLower();
                    JavaList<BeanMenuApp> foundFilters = new JavaList<BeanMenuApp>();

                    foreach (BeanMenuApp item in currentList)
                    {
                        if (VNASchedule.Class.CmmFunction.RemoveSignVietnamese(item.Title).ToLower().Contains(query))
                            foundFilters.Add(item);
                    }

                    results.Count = foundFilters.Size();
                    results.Values = foundFilters;
                }
                else
                {
                    results.Count = currentList.Count;
                    results.Values = currentList.ToListJava();
                }
                return results;
            }

            protected override void PublishResults(Java.Lang.ICharSequence constraint, FilterResults results)
            {
                JavaList<BeanMenuApp> res = (JavaList<BeanMenuApp>)results.Values;

                adapter.filterText = CmmFunction.RemoveSignVietnamese(constraint.ToString());
                adapter.SetItems(res.ToListCSharp());
                adapter.NotifyDataSetChanged();
            }
        }
    }
}