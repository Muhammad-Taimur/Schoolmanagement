using FirstProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Services
{
    public class StudentRepo : IStudent
    {
        private readonly DBContext db;

        public StudentRepo(DBContext db)
        {
            this.db = db;
        }

        public void Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChangesAsync();
        }

        public async Task<Student> Delete(int id)
        {
            var student = db.Students.Find(id);
            db.Students.Remove(student);
           await db.SaveChangesAsync();
            return student;
        }

//

        public async Task <Student> GetbyID(int id)
        {

            return await Task.Run(()=>  db.Students.FirstOrDefault(r => r.StudentID == id));

        }

        public async Task <IEnumerable<Student>> Get()
        {
            //await Task.Delay(0);
            //return db.Students;
           var test = await Task.Run(() => db.Students);
              return  test.ToList().OrderByDescending(a => a.StudentID);
        }

        public async Task<Student> Update(int id, Student student)
        {

            db.Entry(student).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return student;
        }
    }
}