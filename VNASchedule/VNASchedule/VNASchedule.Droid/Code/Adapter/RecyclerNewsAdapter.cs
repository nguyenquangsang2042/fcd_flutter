using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.DataProvider;
using static Android.Widget.ImageView;

namespace VNASchedule.Droid.Code.Adapter
{
    class RecyclerNewsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        private Context context;
        private List<BeanNotify> filtered_news = new List<BeanNotify>();
        private MainActivity mainAct;
        int startPos, endPos;
        string filteredText;
        string localDocumentFilepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public RecyclerNewsAdapter(List<BeanNotify> filtered_news, Context context)
        {
            this.filtered_news = filtered_news;
            this.context = context;
        }
        public RecyclerNewsAdapter(List<BeanNotify> filtered_news, Context context, MainActivity mainActivity, string filteredText)
        {
            this.filtered_news = filtered_news;
            this.context = context;
            this.filteredText = filteredText;
            this.mainAct = mainActivity;
        }
        public override int ItemCount
        {
            get
            {
                return filtered_news.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            NewsViewHolder vh = holder as NewsViewHolder;
            BeanNotify notify = filtered_news[position];
            vh.News.Visibility = ViewStates.Visible;
            vh.NotifyView.Visibility = ViewStates.Gone;
            if (notify.AnnounCategoryId == 7)
                vh.tv_title.Text = "News";
            else
                vh.tv_title.Text = notify.Title;
            vh.frame_backGround.SetBackgroundColor(position % 2 == 0 ? new Color(ContextCompat.GetColor(context, Resource.Color.white)) : new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));
            vh.img.SetImageResource(0);
            LoadImage(notify, vh.img, 200, 200);
            if (notify.Created.HasValue)
            {
                vh.tv_date.Text = notify.Created.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
            }
            else
            {
                vh.tv_date.Text = "";
            }
            if (notify.AnnounCategoryId == 7)
                vh.tv_detail.Text = notify.Title;
            else
                vh.tv_detail.Text = notify.Content;
            if (!notify.FlgRead || (notify.flgConfirm && !notify.flgConfirmed ) || (notify.flgReply && !notify.flgReplied))
            {
                vh.tv_title.SetTypeface(Typeface.Create("sans-serif-light", TypefaceStyle.Bold), TypefaceStyle.Bold);
            }
            else
            {
                vh.tv_title.SetTypeface(Typeface.Create("sans-serif-light", TypefaceStyle.Normal), TypefaceStyle.Normal);

            }
            if (notify.ANStatus.HasValue)
            {
                if (notify.ANStatus.Value == -1)// revoked
                {
                    vh.ln.Visibility = ViewStates.Visible;
                }
                else
                {
                    vh.ln.Visibility = ViewStates.Gone;
                }
            }
            else
            {
                vh.ln.Visibility = ViewStates.Gone;
            }                   
            if (notify.flgImmediately)
            {
                vh.img_flag.Visibility = ViewStates.Visible;
            }
            else
            {
                vh.img_flag.Visibility = ViewStates.Gone;
            }
            if (notify.flgConfirm)
            {
                vh.img_confirm.Visibility = ViewStates.Visible;
                if (notify.flgConfirmed)
                    vh.img_confirm.Alpha = 0.3f;
                else
                {
                    vh.img_confirm.Alpha = 1f;
                }
            }
            else
            {
                vh.img_confirm.Visibility = ViewStates.Gone;
            }
            if (notify.flgReply)
            {
                vh.img_answer.Visibility = ViewStates.Visible;
                if (notify.flgReplied)
                    vh.img_answer.Alpha = 0.3f;
                else
                {
                    vh.img_answer.Alpha = 1f;
                }
            }
            else
            {
                vh.img_answer.Visibility = ViewStates.Gone;
            }
            if (notify.FlgSurvey)
            {
                vh.img_reply.Visibility = ViewStates.Visible;
            }
            else
            {
                vh.img_reply.Visibility = ViewStates.Gone;
            }

            if (!string.IsNullOrEmpty(notify.Title))
            {
                string tittle = CmmFunction.RemoveSignVietnamese(notify.Title.Trim().ToLowerInvariant());
                startPos = tittle.IndexOf(filteredText.ToLower());
                endPos = startPos + filteredText.Length;

                if (startPos != -1)
                {
                    ISpannable spannable = new SpannableString(notify.Title.Trim());
                    ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
                    TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
                    spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
                    vh.tv_title.SetText(spannable, TextView.BufferType.Spannable);
                }
            }
            if (!string.IsNullOrEmpty(notify.Content))
            {
                string tittle = CmmFunction.RemoveSignVietnamese(notify.Content.Trim().ToLowerInvariant());
                startPos = tittle.IndexOf(filteredText.ToLower());
                endPos = startPos + filteredText.Length;

                if (startPos != -1)
                {
                    ISpannable spannable = new SpannableString(notify.Content.Trim());
                    ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
                    TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
                    spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
                    vh.tv_detail.SetText(spannable, TextView.BufferType.Spannable);
                }
            }



            //else
            //{
            //    NewsViewHolder vh = holder as NewsViewHolder;
            //    vh.NotifyView.Visibility = ViewStates.Visible;
            //    vh.News.Visibility = ViewStates.Gone;
            //    vh.rlBackGround.SetBackgroundColor(position % 2 == 0 ? new Color(ContextCompat.GetColor(context, Resource.Color.white)) : new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));
            //    if (!string.IsNullOrEmpty(notify.SearCol))
            //    {
            //        startPos = notify.SearCol.IndexOf(filteredText.ToLower());
            //        endPos = startPos + filteredText.Length;
            //    }
            //    //_rootView.SetBackgroundColor(new Color(ContextCompat.GetColor(context, Resource.Color.background_item_list)));
            //    if (notify.Created.HasValue)
            //    {
            //        vh.tv_date2.Text = notify.Created.Value.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
            //    }
            //    else
            //    {
            //        vh.tv_date2.Text = "";
            //    }
            //    vh.tv_detail2.Text = notify.Content;

            //    if (notify.ANStatus.HasValue)
            //    {
            //        if (notify.ANStatus.Value == -1)// revoked
            //        {
            //            vh.ln2.Visibility = ViewStates.Visible;

            //        }
            //        else
            //        {
            //            vh.ln2.Visibility = ViewStates.Gone;
            //        }
            //    }
            //    else
            //    {
            //        vh.ln2.Visibility = ViewStates.Gone;
            //    }
            //    LoadImage(notify, vh.img_icon, 200, 200);

            //    if (!string.IsNullOrEmpty(notify.Title))
            //    {
            //        string tittle = CmmFunction.removeSignVietnamese(notify.Title.Trim().ToLowerInvariant());
            //        startPos = tittle.IndexOf(filteredText.ToLower());
            //        endPos = startPos + filteredText.Length;
            //        if (startPos != -1)
            //        {
            //            ISpannable spannable = new SpannableString(notify.Title.Trim());
            //            ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
            //            TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
            //            spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
            //            vh.tv_title2.SetText(spannable, TextView.BufferType.Spannable);
            //        }
            //        else vh.tv_title2.Text = notify.Title;
            //    }
            //    if (!string.IsNullOrEmpty(notify.Content))
            //    {
            //        string content = CmmFunction.removeSignVietnamese(notify.Content.Trim().ToLowerInvariant());
            //        startPos = content.IndexOf(filteredText.ToLower());
            //        endPos = startPos + filteredText.Length;
            //        if (startPos != -1)
            //        {
            //            ISpannable spannable = new SpannableString(notify.Content.Trim());
            //            ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
            //            TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
            //            spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
            //            vh.tv_detail2.SetText(spannable, TextView.BufferType.Spannable);
            //        }
            //        else vh.tv_detail2.Text = notify.Content;
            //    }
            //}

        }
        private void LoadImage(BeanNotify beanNews, ImageView img, int ImgWidth, int ImgHeight)
        {
            try
            {
                string url = string.Empty;

                if (beanNews.AnnounCategoryId.ToString() == CmmFunction.GetAppSetting("NEWS_CATEGORY_ID"))
                {
                    url = CmmVariable.M_Domain + beanNews.Icon;
                }
                else
                {
                    url = CmmVariable.M_Domain + beanNews.IconPath;
                }
                if (url.Contains("avatar_news.png"))
                {
                    img.SetImageResource(Resource.Drawable.avatar_news);
                }
                else
                {
                    Glide.With(context).Load(url).Apply(new RequestOptions().Override(ImgWidth, ImgHeight).Error(Resource.Drawable.avatar_news).InvokeDiskCacheStrategy(Com.Bumptech.Glide.Load.Engine.DiskCacheStrategy.All)).Into(img);

                }
                //CheckFileLocalIsExist(url, img);


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
                            mainAct.RunOnUiThread(() =>
                            {
                                image_view.SetImageResource(Resource.Drawable.avatar_news);
                            });
                        }
                    });
                }
                else
                {
                    mainAct.RunOnUiThread(() =>
                    {
                        OpenFile(filename, image_view);
                    });
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
                if (!string.IsNullOrEmpty(localfilePath))
                {
                    //if (localfilePath.Contains("avatar_news.png"))
                    //{
                    //    image_view.SetImageResource(Resource.Drawable.avatar_news);
                    //}
                    //else
                    //{
                    Bitmap myBitmap = BitmapFactory.DecodeFile(uri.ToString());
                    image_view.SetImageBitmap(myBitmap);
                    image_view.SetScaleType(ScaleType.FitXy);
                    //}

                }
                else image_view.SetImageResource(Resource.Drawable.avatar_news);
            }
            catch (Exception ex)
            {
                Console.WriteLine("NewsNewAdapter - openFile - Err: " + ex.ToString());
            }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.ItemNewsNew, parent, false);
            NewsViewHolder notifyViewHolder = new NewsViewHolder(itemView, OnClick);
            return notifyViewHolder;
        }

        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);


        }
        class NewsViewHolder : RecyclerView.ViewHolder
        {
            public LinearLayout News { get; private set; }
            public TextView tv_date { get; private set; }
            public TextView tv_detail { get; private set; }
            public TextView tv_title { get; private set; }
            public ImageView img { get; private set; }
            public LinearLayout ln { get; private set; }

            public LinearLayout NotifyView { get; private set; }
            public TextView tv_date2 { get; private set; }
            public TextView tv_detail2 { get; private set; }
            public TextView tv_title2 { get; private set; }
            public ImageView img_answer { get; private set; }
            public ImageView img_confirm { get; private set; }
            public ImageView img_flag { get; private set; }
            public ImageView img_reply { get; private set; }
            public ImageView img_icon { get; private set; }
            public LinearLayout ln2 { get; private set; }
            public RelativeLayout rlBackGround { get; private set; }
            public LinearLayout frame_backGround { get; private set; }
            public NewsViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                News = itemView.FindViewById<LinearLayout>(Resource.Id.News);
                tv_date = itemView.FindViewById<TextView>(Resource.Id.tv_ItemNewsNew_Date);
                tv_detail = itemView.FindViewById<TextView>(Resource.Id.tv_ItemNewsNew_Detail);
                tv_title = itemView.FindViewById<TextView>(Resource.Id.tv_ItemNewsNew_Title);
                img = itemView.FindViewById<ImageView>(Resource.Id.img_ItemNewsNew);
                ln = itemView.FindViewById<LinearLayout>(Resource.Id.linear_ItemNewsNew_Revoked);
                frame_backGround = itemView.FindViewById<LinearLayout>(Resource.Id.frame_background);

                NotifyView = itemView.FindViewById<LinearLayout>(Resource.Id.Notify);
                tv_date2 = itemView.FindViewById<TextView>(Resource.Id.tv_ItemNews_Date2);
                tv_detail2 = itemView.FindViewById<TextView>(Resource.Id.tv_ItemNews_Detail);
                tv_title2 = itemView.FindViewById<TextView>(Resource.Id.tv_ItemNews_Title);
                img_answer = itemView.FindViewById<ImageView>(Resource.Id.img_ItemNews_Answer);
                img_confirm = itemView.FindViewById<ImageView>(Resource.Id.img_ItemNews_Confirm);
                img_flag = itemView.FindViewById<ImageView>(Resource.Id.img_ItemNews_Flag);
                img_reply = itemView.FindViewById<ImageView>(Resource.Id.img_ItemNews_Reply);
                ln2 = itemView.FindViewById<LinearLayout>(Resource.Id.linear_ItemNews_Revoked);
                img_icon = itemView.FindViewById<ImageView>(Resource.Id.ic_ava_notification);
                rlBackGround = itemView.FindViewById<RelativeLayout>(Resource.Id.relative_news);

                itemView.Click += (sender, e) => listener(base.Position);
            }
        }
    }
}