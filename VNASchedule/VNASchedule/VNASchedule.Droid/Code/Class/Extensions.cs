using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Assist;
using VNASchedule.Class;
using VNASchedule.Droid.Code.Class;

namespace VNASchedule.Droid.Code
{
    public static class Extensions
    {
        public static void SetImageUniversal(this ImageView img, string url)
        {
            var options = new DisplayImageOptions.Builder()
                                          .CacheInMemory(true)
                                          .CacheOnDisk(true)
                                          .BitmapConfig(Bitmap.Config.Rgb565)
                                          .ImageScaleType(ImageScaleType.Exactly)
                                          .Build();

            MainActivity.INSTANCE.imageLoader.DisplayImage(url, img, options);
        }

        public static void SetAvatarUniversal(this ImageView img, string url)
        {
            var options = new DisplayImageOptions.Builder()
                                          .CacheInMemory(true)
                                          .CacheOnDisk(true)
                                          .BitmapConfig(Bitmap.Config.Rgb565)
                                          .ImageScaleType(ImageScaleType.Exactly)
                                          .ShowImageOnFail(Resource.Drawable.icon_avatar64)
                                          .ShowImageForEmptyUri(Resource.Drawable.icon_avatar64)
                                          .Build();

            MainActivity.INSTANCE.imageLoader.DisplayImage(url, img, options);
        }

        public static string CombineDomainUrl(this string url)
        {
            return CmmVariable.M_Domain + "/" + url;
        }

        public static void SetValueSharedPreferences(this Context context, string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(context);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString(key, value);
                editor.Commit();
            }
        }

        public static string GetValueSharedPreferences(this Context context, string key, string defaultValue = "")
        {
            if (!string.IsNullOrEmpty(key))
            {
                ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(context);
                return prefs.GetString(key, defaultValue);
            }

            return string.Empty;
        }

        #region Fragment
        public static void BackToHome(this Android.App.Fragment fragment)
        {
            if (MainActivity.INSTANCE == null)
                return;

            if (MainActivity.INSTANCE.FragmentManager == null)
                return;

            for (int i = CmmVariable.M_BackStackFragmentNum - 2; i < MainActivity.INSTANCE.FragmentManager.BackStackEntryCount; i++)
                MainActivity.INSTANCE.FragmentManager.PopBackStack();
        }

        public static void ShowViewGuide(this Android.App.Fragment fragment)
        {
            MainActivity.INSTANCE?.RunOnUiThread(() =>
            {
                Dialog dlg = new Dialog(fragment.View.Context, Resource.Style.DialogShowWebView);
                View ScheduleDetail = fragment.LayoutInflater.Inflate(Resource.Layout.PopupGuide, null);
                ImageView img_close = ScheduleDetail.FindViewById<ImageView>(Resource.Id.img_PopupNotify);
                img_close.Tag = dlg;
                LinearLayout ln = ScheduleDetail.FindViewById<LinearLayout>(Resource.Id.linear_PopupNotify);
                Android.Webkit.WebView webviewnNotify = ScheduleDetail.FindViewById<Android.Webkit.WebView>(Resource.Id.web_PopupNotify);
                HybridWebViewClient1 web2 = new HybridWebViewClient1(MainActivity.INSTANCE);
                webviewnNotify.SetWebChromeClient(web2);
                webviewnNotify.Settings.JavaScriptEnabled = true;
                webviewnNotify.SetWebViewClient(new Android.Webkit.WebViewClient());
                webviewnNotify.Settings.JavaScriptEnabled = true;
                webviewnNotify.VerticalScrollBarEnabled = true;
                webviewnNotify.HorizontalScrollBarEnabled = true;
                webviewnNotify.Settings.SetSupportZoom(true);
                webviewnNotify.Settings.BuiltInZoomControls = true;
                webviewnNotify.Settings.DisplayZoomControls = false;
                webviewnNotify.Settings.LoadWithOverviewMode = true;//WebView hoàn toàn thu nhỏ
                webviewnNotify.Settings.UseWideViewPort = true;//tải webview xem bình thường
                string domain = CmmVariable.M_Domain;
                string url = $"{CmmVariable.M_Domain.TrimEnd('/')}/Application/InformationGuideUser.aspx";
                webviewnNotify.Reload();
                webviewnNotify.LoadUrl(url);
                if (ScheduleDetail != null)
                {
                    Window window = dlg.Window;
                    dlg.RequestWindowFeature(1);
                    dlg.SetCanceledOnTouchOutside(false);
                    dlg.SetCancelable(true);
                    window.SetGravity(GravityFlags.Center);
                    ViewGroup.LayoutParams pa = ScheduleDetail.LayoutParameters;
                    dlg.SetContentView(ScheduleDetail);
                    dlg.Show();
                    WindowManagerLayoutParams s = window.Attributes;
                    if (MainActivity.INSTANCE.displayMetrics != null)
                    {
                        s.Width = MainActivity.INSTANCE.displayMetrics.WidthPixels - 50;
                        s.Height = MainActivity.INSTANCE.displayMetrics.HeightPixels * 4 / 5;
                    }
                    window.Attributes = s;
                }
                img_close.Click += (o, e) =>
                {
                    ImageView img = o as ImageView;
                    if(img.Tag != null)
                        (img.Tag as Dialog)?.Dismiss();
                };
            });
        }
        #endregion

        #region Get Provider
        public static VNASchedule.DataProvider.ProviderBase ProviderBase(this Android.App.Fragment fragment)
        {
            return (MainActivity.INSTANCE != null && MainActivity.INSTANCE.providerBase != null ? MainActivity.INSTANCE.providerBase : new VNASchedule.DataProvider.ProviderBase());
        }

        public static VNASchedule.DataProvider.ProviderAppLang ProviderLang(this Android.App.Fragment fragment)
        {
            return (MainActivity.INSTANCE != null && MainActivity.INSTANCE.providerLang != null ? MainActivity.INSTANCE.providerLang : new VNASchedule.DataProvider.ProviderAppLang());
        }

        public static VNASchedule.DataProvider.ProviderUser ProviderUser(this Android.App.Fragment fragment)
        {
            return (MainActivity.INSTANCE != null && MainActivity.INSTANCE.providerUser != null ? MainActivity.INSTANCE.providerUser : new VNASchedule.DataProvider.ProviderUser());
        }

        public static VNASchedule.DataProvider.ProviderBanner ProviderBanner(this Android.App.Fragment fragment)
        {
            return (MainActivity.INSTANCE != null && MainActivity.INSTANCE.providerBanner != null ? MainActivity.INSTANCE.providerBanner : new VNASchedule.DataProvider.ProviderBanner());
        }

        public static VNASchedule.DataProvider.ProviderLicense ProviderLicense(this Android.App.Fragment fragment)
        {
            return (MainActivity.INSTANCE != null && MainActivity.INSTANCE.providerLicense != null ? MainActivity.INSTANCE.providerLicense : new VNASchedule.DataProvider.ProviderLicense());
        }
        #endregion

        #region List
        public static JavaList<T> ToListJava<T>(this List<T> values)
        {
            if (values == null)
                return null;

            JavaList<T> res = new JavaList<T>();
            foreach (T item in values)
                res.Add(item);
            return res;
        }
        public static List<T> ToListCSharp<T>(this JavaList<T> values)
        {
            if (values == null)
                return null;

            List<T> res = new List<T>();
            foreach (T item in values)
                res.Add(item);
            return res;
        }
        #endregion

        #region EditText
        public static Java.Lang.String JavaText(this EditText editText)
        {
            return new Java.Lang.String(editText.Text);
        }
        #endregion

        #region object
        public static T GetVal<T>(this object _object, string _PropertyName)
        {
            Type type = _object.GetType();

            if (string.IsNullOrEmpty(_PropertyName))
                return default(T);

            System.Reflection.PropertyInfo _property = type.GetProperty(_PropertyName);
            if (_property != null)
                return (T)_property.GetValue(_object);

            return default(T);
        }

        public static string GetStringVal(this object _object, string _PropertyName)
        {
            Type type = _object.GetType();

            if (string.IsNullOrEmpty(_PropertyName))
                return string.Empty;

            System.Reflection.PropertyInfo _property = type.GetProperty(_PropertyName);
            if (_property != null)
            {
                object _pObj = _property.GetValue(_object);
                if (_pObj != null)
                    return _pObj.ToString();
            }

            return string.Empty;
        }
        public static List<T> JsonToListObject<T>(this object _object)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(_object.ToString());
        }
        public static T JsonToObject<T>(this object _object)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(_object.ToString());
        }
        public static object JsonToAnonymousType(this object _object, dynamic refObj)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(_object.ToString(), refObj);
        }
        #endregion

        #region Bitmap
        public static Android.Graphics.Bitmap CreateQRCode(this Android.Graphics.Bitmap bitmap, string content)
        {
            ZXing.QrCode.QRCodeWriter writer = new ZXing.QrCode.QRCodeWriter();
            try
            {
                ZXing.Common.BitMatrix bitMatrix = writer.encode("pilot:contact:" + content, ZXing.BarcodeFormat.QR_CODE, 512, 512);
                int width = bitMatrix.Width;
                int height = bitMatrix.Height;
                Android.Graphics.Bitmap bmp = Android.Graphics.Bitmap.CreateBitmap(width, height, Android.Graphics.Bitmap.Config.Rgb565);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        bmp.SetPixel(x, y, bitMatrix[x, y] ? Android.Graphics.Color.Black : Android.Graphics.Color.White);
                    }
                }
                return bmp;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}