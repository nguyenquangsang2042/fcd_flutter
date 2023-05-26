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

namespace VNASchedule.Droid.Code.Class
{
    public class DeviceInfo
    {
        public string DeviceId { get; set; }
        public string DevicePushToken { get; set; }
        public short DeviceOS { get; set; } // 1: Android   2: IOS  4: WindowPhone
    }
}