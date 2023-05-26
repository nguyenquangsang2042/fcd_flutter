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
using Android.Webkit;
using Android.Provider;

namespace VNASchedule.Droid.Code.Class
{
    public class HybridWebViewClient1 : WebChromeClient
    {
        private MainActivity myActivity;
        int filechooser = 999;
        public HybridWebViewClient1(MainActivity mainActivity)
        {
            this.myActivity = mainActivity;
        }

        private IValueCallback message;
        public override bool OnShowFileChooser(WebView webView, IValueCallback filePathCallback, FileChooserParams fileChooserParams)
        {
            this.message = filePathCallback;
            Intent chooserIntent = fileChooserParams.CreateIntent();
            chooserIntent.AddCategory(Intent.CategoryOpenable);
            myActivity.StartActivity(Intent.CreateChooser(chooserIntent, "File Chooser"), filechooser, this.OnActivityResult);

            //Intent cam = new Intent(MediaStore.ActionImageCapture);
            //Intent action_pick = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
            //myActivity.StartActivity(cam);

            //myActivity.StartActivity(cam, filechooser, this.OnActivityResult);
            return true;
        }

        private void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (data != null)
            {
                if (null == this.message)
                {
                    return;
                }

                this.message.OnReceiveValue(WebChromeClient.FileChooserParams.ParseResult((int)resultCode, data));
                this.message = null;
            }
        }
    }
}