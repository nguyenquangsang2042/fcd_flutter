using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VNASchedule.Droid.Code.Class
{
    public class CustomNestedScrollView : NestedScrollView
    {
        private Action scrollerTask;
        private int initialPosition;
        private int newCheck = 30;

        public event EventHandler<int> UnTouched;

        public CustomNestedScrollView(Context context) : base(context)
        {
            init();
        }

        public CustomNestedScrollView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            init();
        }

        public CustomNestedScrollView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            init();
        }

        public void init()
        {
            scrollerTask = new Action(() =>
            {
                int newPosition = ScrollY;
                if (initialPosition - newPosition == 0)
                {

                }
                else
                {
                    initialPosition = ScrollY;
                    this.PostDelayed(scrollerTask, newCheck);
                }
            });
        }

        public void startScrollerTask()
        {
            initialPosition = ScrollY;
            this.PostDelayed(() =>
            {
                if (initialPosition == ScrollY)
                    UnTouched.Invoke(this, initialPosition);
                else
                    startScrollerTask();
            }, newCheck);
        }

        protected override void OnScrollChanged(int l, int t, int oldl, int oldt)
        {
            base.OnScrollChanged(l, t, oldl, oldt);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Up:
                    {
                        startScrollerTask();
                        break;
                    }
            }
            return base.OnTouchEvent(e);
        }
    }
}