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
using System.Globalization;
using VNASchedule.Class;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;
using System.Threading.Tasks;
using System.IO;
using VNASchedule.DataProvider;
using Android.Text;
using Android.Support.V4.Content;
using Android.Content.Res;
using Android.Text.Style;

namespace VNASchedule.Droid.Code.Adapter
{
    public class NewsAdapter : BaseAdapter<BeanNotify>,IFilterable
    {
        private MainActivity mainAct;
        private Context context;
        private List<BeanNotify> filtered_news;
        private string icon_url;
        private string filteredText;
        int startPos, endPos;
        string localDocumentFilepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public NewsAdapter(List<BeanNotify> filtered_news, Context context)
        {
            this.filtered_news = filtered_news;
            this.context = context;
        }
        public NewsAdapter(List<BeanNotify> filtered_news, Context context,MainActivity mainAct,string filteredText)
        {
            this.filtered_news = filtered_news;
            this.context = context;
            this.mainAct = mainAct;
            this.filteredText = filteredText;
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

        public Filter Filter { get; private set; }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.ItemNews, null);
            TextView tv_date = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemNews_Date);
            TextView tv_detail = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemNews_Detail);
            TextView tv_title = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemNews_Title);
            ImageView img_answer = _rootView.FindViewById<ImageView>(Resource.Id.img_ItemNews_Answer);
            ImageView img_confirm = _rootView.FindViewById<ImageView>(Resource.Id.img_ItemNews_Confirm);
            ImageView img_flag = _rootView.FindViewById<ImageView>(Resource.Id.img_ItemNews_Flag);
            ImageView img_reply = _rootView.FindViewById<ImageView>(Resource.Id.img_ItemNews_Reply);
            LinearLayout ln= _rootView.FindViewById<LinearLayout>(Resource.Id.linear_ItemNews_Revoked);
            ImageView img_icon = _rootView.FindViewById<ImageView>(Resource.Id.ic_ava_notification);
            tv_title.Text = filtered_news[position].Title;        
            _rootView.SetBackgroundColor(position % 2 == 0 ? new Color(ContextCompat.GetColor(context, Resource.Color.white)) : new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));
            if (!string.IsNullOrEmpty(filtered_news[position].SearCol))
            {
                startPos = filtered_news[position].SearCol.IndexOf(filteredText.ToLower());
                endPos = startPos + filteredText.Length;
            }

            //_rootView.SetBackgroundColor(new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));

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
            if (filtered_news[position].flgImmediately)
            {
                img_flag.Visibility = ViewStates.Visible;
            }
            else
            {
                img_flag.Visibility = ViewStates.Gone;
            }

            if (filtered_news[position].flgConfirm)
            {
                img_confirm.Visibility = ViewStates.Visible;
                if (filtered_news[position].flgConfirmed)
                    img_confirm.Alpha = 0.3f;
                else
                {
                    img_confirm.Alpha = 1f;
                }
            }
            else
            {
                img_confirm.Visibility = ViewStates.Gone;
            }

            if (filtered_news[position].flgReply)
            {
                img_answer.Visibility = ViewStates.Visible;
                if (filtered_news[position].flgReplied)
                    img_answer.Alpha = 0.3f;
                else
                {
                    img_answer.Alpha = 1f;
                }
            }
            else
            {
                img_answer.Visibility = ViewStates.Gone;
            }
            if(filtered_news[position].FlgSurvey)
            {
                img_reply.Visibility = ViewStates.Visible;
            }
            else
            {
                img_reply.Visibility = ViewStates.Gone;
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
            LoadImage(filtered_news[position], img_icon, 200, 200);

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


            //string itemValue = tv_title.Text;

            //else
            //{
            //    tv_title.Text = itemValue;
            //}
            return _rootView;
        }
        private void LoadImage(BeanNotify beanNews, ImageView img, int ImgWidth, int ImgHeight)
        {
            try
            {
                if (!string.IsNullOrEmpty(beanNews.IconPath))
                {
                    string url = CmmVariable.M_Domain + beanNews.IconPath;
                    Glide.With(context).Load(url).Apply(new RequestOptions().Override(ImgWidth, ImgHeight).Error(Resource.Drawable.logo_vna120).InvokeDiskCacheStrategy(Com.Bumptech.Glide.Load.Engine.DiskCacheStrategy.All)).Into(img);
                    CheckFileLocalIsExist(beanNews.IconPath, img);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("NewsNewAdapter - openFile - Err: " + ex.ToString());
            }
        }
    }

}