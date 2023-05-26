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

namespace VNASchedule.Droid.Code.Adapter
{
    class CustomDialogExtendAdapter : Dialog
    {
        public CustomDialogExtendAdapter(Activity activity) : base(activity)
        {

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.ExtendCustomDialog);

           
        }
    }
}