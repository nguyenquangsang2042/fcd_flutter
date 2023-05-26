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
using VNASchedule.Bean;
using Android.Graphics;
using Com.Bumptech.Glide;
using VNASchedule.Class;
using Com.Bumptech.Glide.Request;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using VNASchedule.DataProvider;
using Android.Support.V4.Content;
using Android.Text;
using Android.Content.Res;
using Android.Text.Style;
using static Android.Widget.ImageView;

namespace VNASchedule.Droid.Code.Adapter
{
    class NewsNewAdapter : BaseAdapter<BeanNotify>
    {
        private Context context;
        private List<BeanNotify> filtered_news;
        private MainActivity mainAct;
        int startPos, endPos;
        string filteredText;
        string localDocumentFilepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public NewsNewAdapter(List<BeanNotify> filtered_news, Context context)
        {
            this.filtered_news = filtered_news;
            this.context = context;
        }
        public NewsNewAdapter(List<BeanNotify> filtered_news, Context context,MainActivity mainActivity, string filteredText)
        {
            this.filtered_news = filtered_news;
            this.context = context;
            this.filteredText = filteredText;
            this.mainAct = mainActivity;
        }
        public override BeanNotify this[int position]
        {
            get
            {
                return filtered_news[position];
            }
        }

        public override int Count
        {
            get
            {
                return filtered_news.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.ItemNewsNew, null);
            TextView tv_date = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemNewsNew_Date);
            TextView tv_detail = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemNewsNew_Detail);
            TextView tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemNewsNew_Title);
            ImageView img = _rootView.FindViewById<ImageView>(Resource.Id.img_ItemNewsNew);
            LinearLayout ln = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_ItemNewsNew_Revoked);
            _rootView.SetBackgroundColor(position % 2 == 0 ? new Color(ContextCompat.GetColor(context, Resource.Color.white)) : new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));
            tv_title.Text = filtered_news[position].Title;
            if (filtered_news[position].Modified.HasValue)
            {
                tv_date.Text = filtered_news[position].Modified.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
            }
            else
            {
                tv_date.Text = "";
            }
            tv_detail.Text = filtered_news[position].Content;
            if (filtered_news[position].FlgRead)
            {
                tv_title.SetTypeface(Typeface.Create("sans-serif-light", TypefaceStyle.Normal), TypefaceStyle.Normal);
            }
            else
            {
                tv_title.SetTypeface(Typeface.Create("sans-serif-light", TypefaceStyle.Bold), TypefaceStyle.Bold);
            }
            if (filtered_news[position].ANStatus.HasValue)
            {
                if (filtered_news[position].ANStatus.Value == -1)// revoked
                {
                    ln.Visibility = ViewStates.Visible;
                }
                else
                {
                    ln.Visibility = ViewStates.Gone;
                }
            }
            else
            {
                ln.Visibility = ViewStates.Gone;
            }
            LoadImage(filtered_news[position], img, 200, 200);
            if (!string.IsNullOrEmpty(filtered_news[position].Title))
            {
                string tittle = CmmFunction.RemoveSignVietnamese(filtered_news[position].Title.Trim().ToLowerInvariant());
                startPos = tittle.IndexOf(filteredText.ToLower());
                endPos = startPos + filteredText.Length;

                if (startPos != -1)
                {
                    ISpannable spannable = new SpannableString(filtered_news[position].Title.Trim());
                    ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
                    TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
                    spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
                    tv_title.SetText(spannable, TextView.BufferType.Spannable);
                }
            }
            if (!string.IsNullOrEmpty(filtered_news[position].Content))
            {
                string content = CmmFunction.RemoveSignVietnamese(filtered_news[position].Content.Trim().ToLowerInvariant());
                startPos = content.IndexOf(filteredText.ToLower());
                endPos = startPos + filteredText.Length;

                if (startPos != -1)
                {
                    ISpannable spannable = new SpannableString(filtered_news[position].Content.Trim());
                    ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
                    TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
                    spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
                    tv_detail.SetText(spannable, TextView.BufferType.Spannable);
                }
            }
            return _rootView;
        }
        private void LoadImage(BeanNotify beanNews, ImageView img, int ImgWidth, int ImgHeight)
        {
            try
            {
                if (!string.IsNullOrEmpty(beanNews.Icon))
                {
                    string url = CmmVariable.M_Domain + beanNews.Icon;
                    //Glide.With(context).Load(url).Apply(new RequestOptions().Override(ImgWidth, ImgHeight).Error(Resource.Drawable.logo_vna120).InvokeDiskCacheStrategy(Com.Bumptech.Glide.Load.Engine.DiskCacheStrategy.All)).Into(img);
                    CheckFileLocalIsExist(beanNews.Icon, img);
                }
                else
                {
                    img.SetImageResource(Resource.Drawable.avatar_news);
                }
            }
            catch (Exception ex)
            { }
        }
        public async void CheckFileLocalIsExist(string filepathURL, ImageView image_view)
        {
            try
            {
                //var filenames = filepathURL.Split('/');
                //string filename = filenames[filenames.Length - 2] + "_" + filenames[filenames.Length - 1];
                //filepathURL = CmmVariable.M_Domain + filepathURL;
                //string localfilePath = System.IO.Path.Combine(localDocumentFilepath, filename);

                bool result = false;
                string filename = filepathURL.Substring(filepathURL.LastIndexOf('/') + 1);
                filepathURL = CmmVariable.M_Domain + "/" + CmmVariable.SysConfig.Subsite + filepathURL;

                string localfilePath = System.IO.Path.Combine(localDocumentFilepath, filename);
                if (!File.Exists(localfilePath))
                {
                    await Task.Run(() =>
                    {
                        ProviderBase provider = new ProviderBase();
                        if (provider.DownloadFile(filepathURL, localfilePath, CmmVariable.M_AuthenticatedHttpClient))
                        {
                            mainAct.RunOnUiThread(() =>
                            {
                                OpenFile(filename, image_view);
                            });
                        }
                        else
                        {
                            //myActivity.RunOnUiThread(() =>
                            //{
                            //    image_view.ima = Image.FromFile("Icons/icon_image_empty.png");
                            //});
                        }
                    });
                }
                else
                {
                    OpenFile(filename, image_view);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("NewsNewAdapter - checkFileLocalIsExist - Err: " + ex.ToString());
            }
        }
        public void OpenFile(string localfilename, ImageView image_view)
        {
            try
            {
                byte[] pictByteArray;
                string localfilePath = System.IO.Path.Combine(localDocumentFilepath, localfilename);
                Android.Net.Uri uri = Android.Net.Uri.FromFile(new Java.IO.File(localfilePath));
                image_view.SetImageURI(uri);
                image_view.SetScaleType(ScaleType.FitXy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("NewsNewAdapter - openFile - Err: " + ex.ToString());
            }
        }
    }
}