using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VNASchedule.Bean
{
    public class BeanFAQs : BeanBase
    {
        [PrimaryKey, PrimaryKeyS]
        public int ID { get; set; }
        public string Question { get; set; }

        public string Answer { get; set; }
        public int Status { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// 1: VN
        /// 2: EN
        /// </value>
        public string Language { get; set; }
        public bool IsSelected = false;
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
