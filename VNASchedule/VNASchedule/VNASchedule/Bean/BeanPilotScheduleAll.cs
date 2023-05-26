using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VNASchedule.Bean
{
    public class BeanPilotScheduleAll: BeanBase
    {
        [PrimaryKey, PrimaryKeyS]

        public int ID { get;  set;}
        public string Title { get; set; }
        public string FilePath { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public string Creator { get; set; }
        public string UserModified { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Created { get; set; }

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
