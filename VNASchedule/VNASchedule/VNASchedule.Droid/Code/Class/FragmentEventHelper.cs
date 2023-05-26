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
using VNASchedule.Class;
using VNASchedule.Droid.Code.Fragment;

namespace VNASchedule.Droid.Code.Class
{
    public class FragmentEventHelper
    {
        Android.App.Fragment fragment;
        View view;
        int[] menuIndexListeners;
        int[] moreMenuHideIds;
        public FragmentEventHelper(Android.App.Fragment fragment, View view)
        {
            this.fragment = fragment;
            this.view = view;
        }

        public void SetListener(int[] menuIndexListeners, int[] moreMenuHideIds)
        {
            this.menuIndexListeners = menuIndexListeners;
            this.moreMenuHideIds = moreMenuHideIds;

            foreach (int index in menuIndexListeners)
                SetIndexListener(index);
        }

        private void SetIndexListener(int index)
        {
            switch (index)
            {
                case 0:
                    RelativeLayout rl_bottom_home = view.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_home);
                    rl_bottom_home.Click += HomePage;
                    break;
                case 1:
                    RelativeLayout rl_bottom_safety = view.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_safety);
                    rl_bottom_safety.Click += SafetyPage;
                    break;
                case 2:
                    RelativeLayout rl_bottom_news = view.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_news);
                    rl_bottom_news.Click += NewsPage;
                    break;
                case 3:
                    RelativeLayout rl_bottom_schedule = view.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_schedule);
                    rl_bottom_schedule.Click += SchedulePage;
                    break;
                case 4:
                    {
                        RelativeLayout rl_bottom_extent = view.FindViewById<RelativeLayout>(Resource.Id.rl_bottom_extent);
                        new MoreMenu(new MoreMenuProperties()
                        {
                            Fragment = this.fragment,
                            RelativeLayoutExtent = rl_bottom_extent,
                            HideControlIds = moreMenuHideIds
                        });
                    }
                    break;
            }
        }

        private void HomePage(object sender, EventArgs e)
        {
            this.fragment.BackToHome();
        }

        private void NewsPage(object sender, EventArgs e)
        {
            this.fragment.BackToHome();

            MainActivity.INSTANCE?.ShowFragmentAnim(MainActivity.INSTANCE.FragmentManager, new NewsFragment(true), "Notification", 0);
        }

        private void SchedulePage(object sender, EventArgs e)
        {
            this.fragment.BackToHome();

            if (CmmVariable.SysConfig.UserType == 1)//1 là phi công 
                MainActivity.INSTANCE?.ShowFragmentAnim(MainActivity.INSTANCE.FragmentManager, new FlightScheduleFragment(), "FlightSchedule", 0);
            else if (CmmVariable.SysConfig.UserType == 2)//2 là mặt đất
                MainActivity.INSTANCE?.ShowFragmentAnim(MainActivity.INSTANCE.FragmentManager, new FlightScheduleMDFragment(), "FlightScheduleMD", 0);
        }

        private void SafetyPage(object sender, EventArgs e)
        {
            this.fragment.BackToHome();

            MainActivity.INSTANCE?.ShowFragmentAnim(MainActivity.INSTANCE.FragmentManager, new NotificationFragment(), null, 0);
        }
    }
}