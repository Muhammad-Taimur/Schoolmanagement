using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models.Entities
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FName { get; set; }
        public string  LName { get; set; }
        public DateTime HireDate { get; set; }  
       
    }
}
