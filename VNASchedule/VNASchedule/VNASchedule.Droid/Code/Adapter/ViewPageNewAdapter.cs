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
    class ViewPageNewAdapter : Android.Support.V13.App.FragmentPagerAdapter
    {

        
        private List<Android.App.Fragment>listTabFragment;
        private List<string> mFragmentTitleList;
        public ViewPageNewAdapter (FragmentManager fm) :base(fm)
        {
            NotifyDataSetChanged();
        }
        public override int Count
        {
            get
            {
                return 1;
            }
        }

        public override Android.App.Fragment GetItem(int position)
        {
            if (position == 0)
            {
                return new NewsFragment2();
            }
            else
            {
                return new NotificationFragment();
            }
        }
        public override ICharSequence GetPageTitleFormatted(int position)
        {
            
            if (position == 0)
            {
                return new Java.Lang.String("News");
            }
            else
            {
                return new Java.Lang.String("Notification");
            }
        }
        public override int GetItemPosition(Java.Lang.Object @object)
        {
            return PositionNone;
        }
    }
}