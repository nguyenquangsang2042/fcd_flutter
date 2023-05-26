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

namespace VNASchedule.Droid.Code.Class
{
    public interface UnderLayoutButtonListener
    {
        void OnClick(int position);
    }
}