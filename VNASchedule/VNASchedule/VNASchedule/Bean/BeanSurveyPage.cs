using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VNASchedule.Bean
{
    class BeanSurveyPage :BeanBase
    {
        [PrimaryKey, PrimaryKeyS]
        public string SurveyTableId { get; set; }
        public string UserId1 { get; set; }
        public string FullName1 { get; set; }
        public string Mobile1 { get; set; }
        public string Avatar1 { get; set; }
        public string DepartmentName1 { get; set; }
        public string Step1 { get; set; }
        public string UserId2 { get; set; }
        public string FullName2 { get; set; }
        public string Mobile2 { get; set; }
        public string Avatar2 { get; set; }
        public string DepartmentName2 { get; set; }
        public string Step2 { get; set; }
        public override string GetServerUrl()
        {
            return "/ApiPublic.ashx?func=get&bname=" + this.GetType().Name;
        }
    }
}
