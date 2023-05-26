using System;
namespace VNASchedule.Bean
{
    public class BeanUserRelationshipDraff : BeanUserRelationship
    {
        public int IdUpdate { get; set; }// 
        public int StatusSync { get; set; }
        public int Status { get; set; }
        public int ApprovalStatus { get; set; }
        public int Action { get; set; }// 0: thêm mớ, 1: sửa, -1 xóa
        public DateTime? DateUpdate { get; set; }//ngày giờ chỉnh sửa hiện tại
        public string GuidIDAdd { get; set; }
        public string Comment { get; set; }
        public BeanUserRelationshipDraff()
        {
        }
    }
}
