using System.Text;

namespace VNASchedule.Bean
{
    public class BeanDailyClone :BeanPilotScheduleDaily
    {
        public string ApCodeFrom { get; set; }
        public string APFromTitle { get; set; }
        public string ApCodeTo { get; set; }
        public string APToTitle { get; set; }

    }
}
