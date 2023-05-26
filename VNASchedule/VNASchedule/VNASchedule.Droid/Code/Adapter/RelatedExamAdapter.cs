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
using Android.Views;
using Android.Widget;
using VNASchedule.Bean;

namespace VNASchedule.Droid.Code.Adapter
{
    class RelatedExamAdapter : BaseAdapter<BeanExam>
    {
        List<BeanExam> lst_exam;
        private Context context;
        public RelatedExamAdapter(Context context, List<BeanExam> lst_exam)
        {
            this.lst_exam = lst_exam;
            this.context = context;
        }
        public override BeanExam this[int position]
        {
            get
            {
                return lst_exam[position];
            }
        }

        public override int Count
        {
            get
            {
                return lst_exam.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.ItemSelectExam, null);
            LinearLayout ln_parent = _rootView.FindViewById<LinearLayout>(Resource.Id.ln_parent_item_exam);
            TextView tv_exam_title = _rootView.FindViewById<TextView>(Resource.Id.tv_exam_title);
            _rootView.SetBackgroundColor(position % 2 == 0 ? new Color(ContextCompat.GetColor(context, Resource.Color.white)) : new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));
            tv_exam_title.Text = lst_exam[position].Title;

            return _rootView;
        }
    }
}