using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models.Entities
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
            
        //[ForeignKey ("Teacher")]
        //public int TeacherID { get; set; }
        //public virtual Teacher Teacher { get; set; }


    }
}
