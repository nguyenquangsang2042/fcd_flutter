using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using VNASchedule.Droid.Code.Fragment;

namespace VNASchedule.Droid.Code.Adapter
{
    public class TrainningPageAdapter : Android.Support.V13.App.FragmentPagerAdapter
    {
        public PagerTrainingFragment pagerTrainingFragment; 
        public CourseFragment courseFragment; 
        public ExamFragment examFragment;
        public TrainingFragment _trainingFragment;

        public TrainningPageAdapter(FragmentManager fm, TrainingFragment _trainingFragment) :base(fm)
        {
            this._trainingFragment = _trainingFragment;
            pagerTrainingFragment = _trainingFragment.pagerTrainingFragment;
            courseFragment = _trainingFragment.courseFragment;
            examFragment = _trainingFragment.examFragment;
        }

        public override int Count   
        {
            get
            {
                return 3;
            }
        }
        public override Android.App.Fragment GetItem(int position)
        {
            if (position == 0)
            {
                //pagerTrainingFragment = new PagerTrainingFragment(_trainingFragment);
                return pagerTrainingFragment;
            }
            else if (position == 1)
            {
                //courseFragment = new CourseFragment();
                return courseFragment;
            }
            else
            {
                //examFragment = new ExamFragment();
                return examFragment;
            }
        }
        public override ICharSequence GetPageTitleFormatted(int position) // Gan title cho tablayout
        {
            if (position == 0)
            {
                return new Java.Lang.String("Training");
            }
            else if (position == 1)
            {
                return new Java.Lang.String("Course");
            }
            else
            {
                return new Java.Lang.String("Exam");
            }
        } 
        public override int GetItemPosition(Java.Lang.Object @object)
        {
            return PositionNone;
        } 
    }
}