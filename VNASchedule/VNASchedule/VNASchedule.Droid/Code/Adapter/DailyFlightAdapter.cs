using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using VNASchedule.Bean;
using VNASchedule.Droid.Code.Class;
using VNASchedule.Droid.Code.Fragment;

namespace VNASchedule.Droid.Code.Adapter
{
    class DailyFlightAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        private Context context;
        private List<BeanDailyClone> lst_daily_flight = new List<BeanDailyClone>();
        private MainActivity mainAct;
        private string filteredText;
        private string searchMode; // From,To,FlightNo
        int startPos, endPos;//, startPosphone, endPosPhone
        private string time_departrue = "";
        private string time_arrival = "";
        private ScheduleInDayFragment scheduleInDayFragment;
        private int lastPositionItem = -1;
        public DailyFlightAdapter(List<BeanDailyClone> lst_daily_flight, Context context, MainActivity mainAct, string filteredText, string searchMode, ScheduleInDayFragment scheduleInDayFragment)
        {
            this.lst_daily_flight = lst_daily_flight;
            this.context = context;
            this.mainAct = mainAct;
            this.filteredText = filteredText;
            this.searchMode = searchMode;
            this.scheduleInDayFragment = scheduleInDayFragment;
            /*for (int i = 0; i < lst_daily_flight.Count; i++)
            {
                if (lst_daily_flight[i].Status != -1 && !string.IsNullOrEmpty(lst_daily_flight[i].LstAllPersonal) && lst_daily_flight[i].DepartureTime > DateTime.UtcNow)
                {
                    lastPositionItem = i;
                    break;
                }
            }*/

            lastPositionItem = lst_daily_flight.FindIndex(r => r.Status != -1 && !string.IsNullOrEmpty(r.LstAllPersonal) && r.DepartureTime > DateTime.UtcNow);
        }

        public int GetPostionItem()
        {
            return lastPositionItem;
        }

        public override int ItemCount
        {
            get
            {
                return lst_daily_flight.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            try
            {
                BeanDailyClone schedule = lst_daily_flight[position];
                DailyFlightViewHolder vh = holder as DailyFlightViewHolder;
                if (!string.IsNullOrEmpty(schedule.FlightNo))
                {
                    vh.tv_flight_no.Text = schedule.FlightNo;
                }
                else
                {
                    vh.tv_flight_no.Text = "";

                }
                //if (!string.IsNullOrEmpty(schedule.))
                //{
                //    vh.tv_flight_no.Text = schedule.FlightNo;
                //}
                //else
                //{
                //    vh.tv_flight_no.Text = "";

                //}
                if (!string.IsNullOrEmpty(schedule.FlightNo))
                {
                    vh.tv_flight_no.Text = schedule.FlightNo;
                }
                else
                {
                    vh.tv_flight_no.Text = "";

                }
                if (!string.IsNullOrEmpty(schedule.ApCodeFrom))
                {
                    vh.tv_apcode_from.Text = schedule.ApCodeFrom;
                }
                else
                {
                    vh.tv_flight_no.Text = "";

                }
                if (!string.IsNullOrEmpty(schedule.ApCodeTo))
                {
                    vh.tv_apcode_to.Text = schedule.ApCodeTo;
                }
                else
                {
                    vh.tv_flight_no.Text = "";

                }
                if (schedule.DepartureTime.HasValue)
                {
                    time_departrue = schedule.DepartureTime.Value.ToString("HH:mm", CultureInfo.CreateSpecificCulture("en-GB"));

                }
                if (schedule.ArrivalTime.HasValue)
                {
                    time_arrival = schedule.ArrivalTime.Value.ToString("HH:mm", CultureInfo.CreateSpecificCulture("en-GB")) + " UTC";

                }
                vh.time_departrue.Text = time_departrue + " - " + time_arrival;
                if ((schedule.Status & 1) >= 0)
                {
                    vh.tv_flight_no.SetTextColor(Color.Black);
                    vh.time_departrue.SetTextColor(Color.Black);
                    vh.tv_apcode_from.SetTextColor(Color.Black);
                    vh.tv_apcode_to.SetTextColor(Color.Black);
                    vh.tv_air_plane_code.SetTextColor(new Color(ContextCompat.GetColor(context, Resource.Color.active_blue)));

                }
                if ((schedule.Status & 2) > 0) // Thay đổi giờ bay
                {
                    vh.time_departrue.SetTextColor(Color.Red);
                }
                if ((schedule.Status & 4) > 0)// Thay đổi tổ đội bay
                {
                    vh.tv_air_plane_code.SetTextColor(Color.ParseColor("#9C8F02"));

                }
                if (schedule.Status == -1 || string.IsNullOrEmpty(schedule.LstAllPersonal) || schedule.DepartureTime <= DateTime.UtcNow)
                {
                    if (schedule.DepartureTime <= DateTime.UtcNow) // đã bay
                    {
                        vh._lnDark.Visibility = ViewStates.Visible;
                        vh.tv_pilot_count.SetBackgroundResource(Resource.Drawable.circle_gray);
                        vh.tv_flight_no.SetTextColor(Color.LightGray);
                        vh.time_departrue.SetTextColor(Color.LightGray);
                        vh.tv_apcode_from.SetTextColor(Color.LightGray);
                        vh.tv_apcode_to.SetTextColor(Color.LightGray);
                        vh.tv_air_plane_code.SetTextColor(Color.LightGray);
                    }
                    else // bị hủy
                    {
                        vh._lnDark.Visibility = ViewStates.Visible;
                        vh.tv_pilot_count.SetBackgroundResource(Resource.Drawable.circle_blue);
                        vh.tv_flight_no.SetTextColor(Color.LightGray);
                        vh.time_departrue.SetTextColor(Color.LightGray);
                        vh.tv_apcode_from.SetTextColor(Color.LightGray);
                        vh.tv_apcode_to.SetTextColor(Color.LightGray);
                        vh.tv_air_plane_code.SetTextColor(Color.LightGray);

                    }
                }
                else // bình thường
                {
                    if ((schedule.Status & 2) > 0) // Thay đổi giờ bay
                        vh.time_departrue.SetTextColor(Color.Red);
                    else
                        vh.time_departrue.SetTextColor(Color.Black);
                    if ((schedule.Status & 4) > 0) // Thay đổi tổ đội bay
                        vh.tv_air_plane_code.SetTextColor(Color.ParseColor("#9C8F02"));
                    else
                        vh.tv_air_plane_code.SetTextColor(new Color(ContextCompat.GetColor(context, Resource.Color.active_blue)));

                    vh._lnDark.Visibility = ViewStates.Gone;
                    vh.tv_pilot_count.SetBackgroundResource(Resource.Drawable.circle_blue);

                    vh.tv_flight_no.SetTextColor(Color.Black);
                    
                    vh.tv_apcode_from.SetTextColor(Color.Black);
                    vh.tv_apcode_to.SetTextColor(Color.Black);
                }

                if (schedule.Status == -1 || string.IsNullOrEmpty(schedule.LstAllPersonal))
                {
                    vh.tv_flight_no.SetTextColor(Color.LightGray);
                    vh.time_departrue.SetTextColor(Color.LightGray);
                    vh.tv_apcode_from.SetTextColor(Color.LightGray);
                    vh.tv_apcode_to.SetTextColor(Color.LightGray);
                    vh.tv_air_plane_code.SetTextColor(Color.LightGray);

                }


                if (!string.IsNullOrEmpty(schedule.FlightNo2))
                {
                    vh.tv_air_plane_code.Text = schedule.FlightNo2;

                }

                if (!string.IsNullOrEmpty(schedule.LstAllPersonal))
                {
                    var lst_user = JsonConvert.DeserializeObject<ClassUserCompact[]>(schedule.LstAllPersonal).ToList<ClassUserCompact>();
                    if (lst_user != null && lst_user.Count > 0)
                    {
                        vh.tv_pilot_count.Visibility = ViewStates.Visible;
                        vh.tv_pilot_count.Text = lst_user.Count().ToString();
                    }
                    else
                    {
                        vh.tv_pilot_count.Visibility = ViewStates.Invisible;
                    }
                }
                else
                {
                    vh.tv_pilot_count.Visibility = ViewStates.Invisible;
                }

                hightLightTextSeach(vh, schedule, searchMode);
            }
            catch (Exception ex)
            {

            }
        }

        private void hightLightTextSeach(DailyFlightViewHolder vh, BeanDailyClone schedule, string searchMode)
        {
            try
            {
                string filter = filteredText;
                if (searchMode.Equals("FlightNo"))
                {
                    string itemValueAirPlaneCode = vh.tv_air_plane_code.Text;
                    searchByAirPlaneCode(schedule, filter, itemValueAirPlaneCode, vh);
                }
                else if (searchMode.Equals("From"))
                {
                    string itemApCodeFrom = vh.tv_apcode_from.Text;
                    searchByCodeFrom(schedule, filter, itemApCodeFrom, vh);
                }

                else if (searchMode.Equals("To"))
                {

                    string itemCodeTo = vh.tv_apcode_to.Text;
                    searchByCodeTo(schedule, filter, itemCodeTo, vh);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void searchByAirPlaneCode(BeanDailyClone schedule, string filter, string itemValueAirPlaneCode, DailyFlightViewHolder vh)
        {
            try
            {

                if (!string.IsNullOrEmpty(schedule.FlightNo2))
                {
                    startPos = schedule.FlightNo2.ToLower().IndexOf(filter.ToLower());
                    endPos = startPos + filter.Length;
                }

                if (startPos != -1)
                {
                    ISpannable spannable = new SpannableString(itemValueAirPlaneCode);
                    ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
                    TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
                    spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
                    vh.tv_air_plane_code.SetText(spannable, TextView.BufferType.Spannable);
                }
                else
                {
                    vh.tv_air_plane_code.Text = itemValueAirPlaneCode;
                }
            }
            catch (Exception ex)
            {


            }
        }
        public void searchByCodeFrom(BeanDailyClone schedule, string filter, string itemApCodeFrom, DailyFlightViewHolder vh)
        {
            try
            {
                if (!string.IsNullOrEmpty(schedule.ApCodeFrom))
                {
                    startPos = schedule.ApCodeFrom.ToLower().IndexOf(filter.ToLower());
                    endPos = startPos + filter.Length;
                }

                if (startPos != -1)
                {
                    ISpannable spannable = new SpannableString(itemApCodeFrom);
                    ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
                    TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
                    spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
                    vh.tv_apcode_from.SetText(spannable, TextView.BufferType.Spannable);
                }
                else
                {
                    vh.tv_apcode_from.Text = itemApCodeFrom;
                }
            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex, "searchByCodeFrom", "DailyFlightAdapter");
            }
        }
        public void searchByCodeTo(BeanDailyClone schedule, string filter, string itemCodeTo, DailyFlightViewHolder vh)
        {
            try
            {
                if (!string.IsNullOrEmpty(schedule.ApCodeTo))
                {
                    startPos = schedule.ApCodeTo.ToLower().IndexOf(filter.ToLower());
                    endPos = startPos + filter.Length;
                }

                if (startPos != -1)
                {
                    ISpannable spannable = new SpannableString(itemCodeTo);
                    ColorStateList Red = new ColorStateList(new int[][] { new int[] { } }, new int[] { new Color(ContextCompat.GetColor(context, Resource.Color.highlight_text)) });
                    TextAppearanceSpan highlightSpan = new TextAppearanceSpan("sans-serif", Android.Graphics.TypefaceStyle.Bold, -1, Red, null);
                    spannable.SetSpan(highlightSpan, startPos, endPos, SpanTypes.ExclusiveExclusive);
                    vh.tv_apcode_to.SetText(spannable, TextView.BufferType.Spannable);
                }
                else
                {
                    vh.tv_apcode_to.Text = itemCodeTo;
                }
            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex, "searchByCodeTo","DailyFlightAdapter");
            }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            try
            {
                View itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.ItemDailyFlight, parent, false);
                DailyFlightViewHolder dailyFlightViewHolder = new DailyFlightViewHolder(itemView, OnClick);
                return dailyFlightViewHolder;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }

        public class DailyFlightViewHolder : RecyclerView.ViewHolder
        {
            public ImageView PilotAva { get; private set; }
            public TextView tv_apcode_from { get; private set; }
            public TextView tv_apcode_to { get; private set; }
            public TextView tv_flight_no { get; private set; }
            public TextView time_arrival { get; private set; }
            public TextView time_departrue { get; private set; }
            public TextView tv_air_plane_code { get; private set; }
            public TextView tv_pilot_count { get; private set; }
            public Refractored.Controls.CircleImageView _imgAvatar { get; private set; }
            public LinearLayout linearLayout;
            public LinearLayout _lnDark { get; private set; }
            public DailyFlightViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                // Locate and cache view references:
                //PilotAva = itemView.FindViewById<ImageView>(Resource.Id.img_pilot_contact);
                try
                {
                    _imgAvatar = itemView.FindViewById<Refractored.Controls.CircleImageView>(Resource.Id.img_ava);
                    tv_flight_no = itemView.FindViewById<TextView>(Resource.Id.tv_flight_no);
                    tv_apcode_from = itemView.FindViewById<TextView>(Resource.Id.tv_apCodeFrom);
                    tv_apcode_to = itemView.FindViewById<TextView>(Resource.Id.tv_apCodeTo);
                    time_departrue = itemView.FindViewById<TextView>(Resource.Id.tv_time_departrue);
                    time_arrival = itemView.FindViewById<TextView>(Resource.Id.tv_time_arrival);
                    tv_pilot_count = itemView.FindViewById<TextView>(Resource.Id.tv_pilot_count);
                    tv_air_plane_code = itemView.FindViewById<TextView>(Resource.Id.tv_air_plane_code);
                    _lnDark = itemView.FindViewById<LinearLayout>(Resource.Id.ln_dark);

                    itemView.Click += (sender, e) => listener(base.Position);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}