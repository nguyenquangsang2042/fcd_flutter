using System;
using System.Collections.Generic;
using System.Text;

namespace VNASchedule.Bean
{
    public class BeanUserDraff: BeanUser
    {
        public string IdUpdate { get; set; }
        //public string Mobile { get; set; }
        ////public string Address { get; set; }
        //public string Email { get; set; }
        //public string Email2 { get; set; }
        //public DateTime? Birthday { get; set; }
        //public string Code2 { get; set; }
        //public string Code3 { get; set; }
        //public string IdentityNum { get; set; }
        //public DateTime? IdentityIssueDate { get; set; }
        //public string IdentityIssuePlace { get; set; }
        //public string ImageIdentityFront { get; set; }
        //public string ImageIdentityBack { get; set; }
        //public string Passport { get; set; }
        //public DateTime? PassportIssueDate { get; set; }
        //public DateTime? PassportIssueExpirationDate { get; set; }
        //public string ImagePassportFront { get; set; }
        //public string ImagePassportBack { get; set; }
        public int ApprovalStatus { get; set; }
        //public int Action { get; set; }
        public string Comment { get; set; }
        public DateTime? DateUpdate { get; set; }
        //
        public string ColumnChange { get; set; }
    }
}
