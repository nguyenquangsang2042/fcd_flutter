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

namespace VNASchedule.Droid.Code.Class
{
    public class SupTitleMenuWork
    {
        public string TitleName;
        public List<BeanScheduleWeekWorkingDetail> listItemMenu;
        public int week;
        public DateTime StartDate;
    }
}