using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using VNASchedule.Bean;

namespace VNASchedule.Droid.Code.Adapter
{
    class ExamAdapter : BaseAdapter<BeanSurveyTable>
    {
        private Context context;
        private List<BeanSurveyTable> lst_survey;

        public ExamAdapter(List<BeanSurveyTable> lst_survey, Context context)
        {
            this.lst_survey = lst_survey;
            this.context = context;
        }

        public override BeanSurveyTable this[int position]
        {
            get
            {
                return lst_survey[position];
            }
        }

        public override int Count
        {
            get
            {
                return lst_survey.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.ItemExam, null);
            TextView tv_start = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemExam_Start);
            TextView tv_due = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemExam_Due);
            TextView tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemExam_Title);
            LinearLayout ln = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_ItemExam_Done);
            _rootView.SetBackgroundColor(position % 2 == 0 ? new Color(ContextCompat.GetColor(context, Resource.Color.white)) : new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));
            tv_title.Text = lst_survey[position].Title;
            if (lst_survey[position].StartDate.HasValue)
            {
                tv_start.Text = lst_survey[position].StartDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));

            }
            else
            {
                tv_start.Text = "";
            }

            if (lst_survey[position].DueDate.HasValue)
            {
                tv_due.Text = lst_survey[position].DueDate.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
            }
            else
            {
                tv_due.Text = "";
            }

            if (lst_survey[position].ActionStatus == 2)
            {
                ln.Visibility = ViewStates.Visible;
            }
            else
            {
                ln.Visibility = ViewStates.Gone;
            }       

            return _rootView;
        }
    }
}