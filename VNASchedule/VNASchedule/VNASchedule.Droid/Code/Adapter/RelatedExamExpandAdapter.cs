using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using VNASchedule.Bean;

namespace VNASchedule.Droid.Code.Adapter
{
    class RelatedExamExpandAdapter : BaseExpandableListAdapter
    {
        private Context context;
        private List<string> expandableListTitle;
        private int childSelectedPositon;
        private int selectedGroupPositon;
        private Dictionary<string, List<BeanExam>> dictionaryExam;
        private LinearLayout ln_parent_backGround;
        private TextView TilteSupMenu;
        private ImageView img_indicator;
        private MainActivity mainAct;
        View _rootView;
       
        public RelatedExamExpandAdapter (Dictionary<string, List<BeanExam>> expandableListDetail, Context context, List<string> expandableListTitle)
        {
            this.dictionaryExam = expandableListDetail;
            this.context = context;
            this.expandableListTitle = expandableListTitle;
            //this.childSelectedPositon = childSelectedPositon;
            //this.selectedGroupPositon = selectedGroupPositon;
        }
        public override bool HasStableIds
        {
            get
            {
                return false;
            }
        }
        public override int GroupCount
        {
            get
            {
                return this.dictionaryExam.Count;
            }
        }
        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return this.dictionaryExam[this.expandableListTitle[groupPosition]].Count();
                 
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.list_item_exam, null);
            TextView tv = _rootView.FindViewById<TextView>(Resource.Id.tv_ListItem_Exam);
            LinearLayout ln_parent_backGround = _rootView.FindViewById<LinearLayout>(Resource.Id.ln_child_background);
            List<BeanExam> lstObj = dictionaryExam[this.expandableListTitle[groupPosition]];
            BeanExam beanScheduleWeekWorkingDetail = new BeanExam();
            beanScheduleWeekWorkingDetail = lstObj[childPosition];
            tv.Text = beanScheduleWeekWorkingDetail.Title;
            //if (childPosition == childSelectedPositon)
            //{
            //    tv.SetBackgroundColor(Color.ParseColor("#e7f9ff"));
            //}
            //else
            //{
            //    tv.SetBackgroundColor(Color.White);
            //}
            return _rootView;
        }
      


        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return this.expandableListTitle[groupPosition];
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
                _rootView = mInflater.Inflate(Resource.Layout.list_group_exam, null);

            }
            ln_parent_backGround = _rootView.FindViewById<LinearLayout>(Resource.Id.ln_parent_background);
            TilteSupMenu = _rootView.FindViewById<TextView>(Resource.Id.tv_ListHeader_Exam);
            img_indicator = _rootView.FindViewById<ImageView>(Resource.Id.img_indicator);
            var key = dictionaryExam.ElementAt(groupPosition).Key;
            TilteSupMenu.Text =dictionaryExam.ElementAt(groupPosition).Key;
            if (dictionaryExam[dictionaryExam.ElementAt(groupPosition).Key].Count() == 1)
            {
                img_indicator.Visibility = ViewStates.Gone;
            }

            else
            {
                img_indicator.Visibility = ViewStates.Visible;
            }
            if (isExpanded == true)
            {
                img_indicator.SetImageResource(Resource.Drawable.icon_arrow_down);

                //TilteSupMenu.SetTextColor(Color.ParseColor("#00637A"));

             
            }
            else
            {
                img_indicator.SetImageResource(Resource.Drawable.icon_right_arrow);

                //TilteSupMenu.SetTextColor(Color.ParseColor("#000000"));
               


            }
            if (groupPosition == selectedGroupPositon)
            {
                //ln_parent_backGround.SetBackgroundColor(Color.ParseColor("#e7f9ff"));
            }
            else
            {
                //ln_parent_backGround.SetBackgroundColor(Color.White);
            }
            //List<BeanExam> lstObj = expandableListDetail[groupPosition].listItemMenu;


            return _rootView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
    public static class ObjectTypeHelper
    {
        public static T Cast<T>(this Java.Lang.Object obj) where T : class
        {
            var propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }
    }
}