using System;
using SQLite;

namespace VNASchedule.Bean
{
    public class BeanUser : BeanBase
    {
        public BeanUser()
        {
            Position = 0;
            Department = 0;
            RegionId = 0;
            ProvinceId = 0;
            CountryId = 0;
            Status = 0;
        }

        [PrimaryKey, PrimaryKeyS, Collation("NOCASE")]
        //public int UserID { get; set; }
        public string ID { get; set; }
        [Ignore]
        public string LoginName { get; set; }
        [Ignore]
        public string Password { get; set; }

        public string Code { get; set; }
        /// <summary>
        /// Crewcode
        /// </summary>
        public string Code2 { get; set; }
        /// <summary>
        /// ID Travel
        /// </summary>
        public string Code3 { get; set; }

        public string FullName { get; set; }
        public string FullNameNoAccent { get; set; }
        public string Alias { get; set; }
        /// <summary>
        /// Sex: 0: Female; 1: Male 
        /// </summary>
        public bool Gender { get; set; }
        public bool IsSelected { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        [Ignore]
        public int? RegionId { get; set; }
        [Ignore]
        public int? ProvinceId { get; set; }
        [Ignore]
        public int? CountryId { get; set; }
        public string Birthplace { get; set; }
        [Ignore]
        public float AdLat { get; set; }
        [Ignore]
        public float AdLong { get; set; }
        [Ignore]
        public string Tel { get; set; }
        public string Mobile { get; set; }
        [Ignore]
        public string Fax { get; set; }
        /// <summary>
        /// Email VNA
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// personal email
        /// </summary>
        public string Email2 { get; set; }
        public string EmailNoDomain { get; set; }
        [Ignore]
        public string Description { get; set; }
        [Ignore]
        public string VISA { get; set; }
        [Ignore]
        public string Passport { get; set; }
        [Ignore]
        public string IdentityNum { get; set; }
        [Ignore]
        public DateTime? IdentityIssueDate { get; set; }
        [Ignore]
        public string IdentityIssuePlace { get; set; }

        public int? Status { get; set; }
        [Ignore]
        public string VerifyCode { get; set; } // otp from email
        [Ignore]
        public string GoogleAuthCode { get; set; } // otp from google authenticate
        [Ignore]
        public int? DeviceOS { get; set; }
        [Ignore]
        public string DeviceInfo { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Created { get; set; }
        [Ignore]
        public string AutoLoginKey { get; set; }
        [Ignore]
        public int? UserType { get; set; }
        public int? Position { get; set; }
        /// <summary>
        /// Rank ~ position
        /// </summary>
        public string PositionName { get; set; }
        public int? Department { get; set; }
        /// <summary>
        /// Department/Fleet
        /// </summary>
        public string DepartmentName { get; set; }
        public int TicketLimitNumber { get; set; }
        public int TicketLimitNumberUsed { get; set; }
        public string WorkingPattern { get; set; }
        public string Nationality { get; set; }
        public string Avatar { get; set; }
        public DateTime? UseAppDate { get; set; }

        public string LowerName { get; set; }

        public string SpecialContent { get; set; }
        public DateTime? PassportIssueExpirationDate { get; set; }
        public DateTime? PassportIssueDate { get; set; }

        public string ImageIdentityFront { get; set; }
        public string ImageIdentityBack { get; set; }
        public string ImagePassportFront { get; set; }
        public string ImagePassportBack { get; set; }
        public int Action { get; set; }

        [Ignore]
        public bool? IsLoadImage { get; set; }

        public int? AddressNationId { get; set; }

        public int? AddressProvinceId { get; set; }

        public int? AddressDistrictId { get; set; }

        public int? AddressWardId { get; set; }

        public string AddressStreet { get; set; }
        public bool? EU { get; set; }
        public string FlightInDay { get; set; }
        public int? FlightTimesInMonth { get; set; }

        public DateTime? NgayVaoDang { get; set; }

        public DateTime? StartDateWork { get; set; }
        public string Base { get; set; }
        public string RewardDiscipline { get; set; }
        public string EstimatedFlightTimeInMonth { get; set; }
        public string HRCode { get; set; }
        public string IDNumber { get; set; }
        public bool? IsBanLanhDao { get; set; }

        public override string GetModifiedColName()
        {
            return "Modified";
        }

        /// <summary>
        /// Lấy đị chỉ Url API xử lý trên mục Bean tương ứng
        /// </summary>
        /// <returns></returns>
        public override string GetApiUrlExec()
        {
            return "/ApiUser.ashx";
        }

        // <summary>
        // Lấy đường dẫn Url tương ứng lấy dữ liệu từ Server
        // </summary>
        //
        /// <returns></returns>
        public override string GetServerUrl()
        {
            return "/ApiPublic.ashx?func=get&bname=" + this.GetType().Name;
        }
    }
}


