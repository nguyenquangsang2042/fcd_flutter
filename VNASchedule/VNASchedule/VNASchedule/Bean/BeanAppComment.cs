using System;
using System.Collections.Generic;
using System.Text;

namespace VNASchedule.Bean
{
    public class BeanAppComment : BeanBase
    {
        public string Comment { get; set; }
        public DateTime? DateUpdate { get; set; }
        public int ApprovalStatus { get; set; }
    }
}
