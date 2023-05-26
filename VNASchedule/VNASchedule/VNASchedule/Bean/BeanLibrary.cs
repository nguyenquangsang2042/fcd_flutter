using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VNASchedule.Bean
{
    public class BeanLibrary : BeanBase
    {
        [PrimaryKey, AutoIncrement]
        public int IDC { get; set; }

        [PrimaryKeyS]
        public int ID { get; set; }
        public string Name { get; set; }
        [PrimaryKeyS]
        public int Type { get; set; }
        public string Path { get; set; }
        public string FileType { get; set; }
        public int Items { get; set; }
        public DateTime? Created { get; set; }
        public int Parent { get; set; }
        public bool Delete { get; set; }

        public bool IsSelected = false;
    }
}
