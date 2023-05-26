using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VNASchedule.Bean
{
    public class BeanSurveyCategory : BeanBase
    {
        [PrimaryKey, PrimaryKeyS]
        public int ID { get; set; }
        public string Title { get; set; }
        public string TitleEN { get; set; }
        public bool? Type { get; set; }
        public string Code { get; set; }
        public int? Status { get; set; }
        public int? Index { get; set; }
        public int? ParentId { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Created { get; set; }
        public bool? IsTheory { get; set; }
        public bool? IsTheoryTest { get; set; }
        public string TestForm { get; set; }
        public string SessionNo { get; set; }
        public string Parent { get; set; }

        [Ignore]
        public bool IsSelected { get; set; }


        public override string GetServerUrl()
        {
            return "/ApiSurvey.ashx?func=GetAllSurveyCategory";
        }
    }
}
