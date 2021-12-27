using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marketing_Tool.Models
{
    public class jointClass
    {
        public Student_tbl studentDetails { get; set; }
        public Student_user stuMsrDetails { get; set; }

        public Category_tbl categoryList { get; set; }
    }
}