using System;
using SQLite;

namespace VNASchedule.Bean
{
    public class BeanAirport : BeanBase
    {
        [PrimaryKey, PrimaryKeyS]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Created { get; set; }

        public bool IsSelected = false;

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
