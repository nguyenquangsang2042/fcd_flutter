using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VNASchedule.Bean
{
    public class BeanHelpDeskLinhVuc : BeanBase
    {
        [PrimaryKey, PrimaryKeyS]
        public int ID { get; set; }
        public string Title_EN { get; set; }
        public string Title_VN { get; set; }
        public int? Status { get; set; }
        public int Order { get; set; }
        public DateTime? Modified { get; set; }

        // <summary>
        // Lấy đường dẫn Url tương ứng lấy dữ liệu từ Server
        // </summary>
        /// <returns></returns>
        public override string GetServerUrl()
        {
            return "/ApiPublic.ashx?func=get&bname=" + this.GetType().Name;
        }
    }
}
