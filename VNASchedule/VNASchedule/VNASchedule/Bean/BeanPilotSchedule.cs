using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace VNASchedule.Bean
{
    public class BeanPilotSchedule : BeanBase
    {
        [PrimaryKey, PrimaryKeyS]
        public int ID { get; set; }
        public int RoutingId { get; set; }
        public int? FromId { get; set; }
        public int? ToId { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? BoardingTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string FlightNo { get; set; }
        public string FlightNo2 { get; set; }
        public string Apl { get; set; }
        public string Personal { get; set; }
        public string TaskPositionName { get; set; }
        public string Notes { get; set; }
        public string UserId { get; set; }
        public int? Status { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Created { get; set; }
        public string UserModified { get; set; }
        public string Creator { get; set; }
        public DateTime? AlertDate { get; set; }
        public string LstAllPersonal { get; set; }

        /// <summary>
        /// Lấy đường dẫn Url tương ứng lấy dữ liệu từ Server
        /// </summary>
        /// <returns></returns>
        public override string GetServerUrl()
        {
            return "/ApiPublic.ashx?func=get&bname=" + this.GetType().Name;
        }
    }
}
