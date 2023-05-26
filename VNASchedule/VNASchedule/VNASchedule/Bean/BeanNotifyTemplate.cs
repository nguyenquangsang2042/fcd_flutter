using System;
using SQLite;

namespace VNASchedule.Bean
{
    public class BeanNotifyTemplate : BeanBase
    {
        [PrimaryKey, PrimaryKeyS]
        public int ID { get; set; }
        public string Title { get; set; }
        public string TitleEN { get; set; }
        public string TitlePara { get; set; }
        public string Content { get; set; }
        public string ContentPara { get; set; }
        public string ContentEN { get; set; }
        public bool flgConfirm { get; set; }
        public string ToDoListAction { get; set; }
        public int CategoryId { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public bool ShowPopup { get; set; }
    }
}
