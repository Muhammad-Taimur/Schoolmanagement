using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models.Entities
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime DOB { get; set; }

        //[ForeignKey ("Course")]
        //public int CourseID { get; set; }
        //public virtual Course Course { get; set; }
    }
}
