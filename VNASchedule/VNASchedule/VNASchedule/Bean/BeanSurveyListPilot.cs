﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VNASchedule.Bean
{
    public class BeanSurveyListPilot : BeanBase
    {
        [PrimaryKey, PrimaryKeyS]
        public string SurveyTableId { get; set; }
        public string UserId1 { get; set; }
        public string FullName1 { get; set; }
        public string Mobile1 { get; set; }
        public string Avatar1 { get; set; }
        public string PerId1 { get; set; }
        public string DepartmentName1 { get; set; }
        public string Step1 { get; set; }
        public string UserId2 { get; set; }
        public string FullName2 { get; set; }
        public string PerId2 { get; set; }
        public string Mobile2 { get; set; }
        public string Avatar2 { get; set; }
        public string DepartmentName2 { get; set; }
        public string Step2 { get; set; }
        public string UrlReview1 { get; set; }
        public string UrlReview2 { get; set; }
        public string User1Code2 { get; set; }
        public string User2Code2 { get; set; }
        public string PilotPair { get; set; }
        public bool? Supporter { get; set; }
        public bool? Supporter1 { get; set; }
        public bool? Supporter2 { get; set; }
        public bool isSelected { get; set; }
        public bool isSelected1a { get; set; }
        public bool isSelected1b { get; set; }
        public string FileExport { get; set; }
        public DateTime? Date { get; set; }
        public int? Status1 { get; set; }
        public int? Status2 { get; set; }

        public override string GetServerUrl()
        {
            return "/ApiPublic.ashx?func=get&bname=" + this.GetType().Name;
        }
    }
}
