using FirstProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Services
{
    public interface IStudent
    {
        Task <IEnumerable<Student>>Get();
        Task <Student> GetbyID(int id);
        void Add(Student student);
        Task <Student> Delete(int id);

        Task<Student> Update(int id, Student student);
    }
}
