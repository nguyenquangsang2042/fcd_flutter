using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VNASchedule.Bean
{
    public class BeanProvince:BeanBase
    {
        [PrimaryKey, PrimaryKeyS]
        public int ID { get; set; }
        public string Title { get; set; }
        public int NationID { get; set; }
        public DateTime? Modified { get; set; }
        public override string GetServerUrl()
        {
            return "/ApiPublic.ashx?func=get&bname=" + this.GetType().Name;
        }
        //address

    }
}
