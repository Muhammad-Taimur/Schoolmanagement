using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models.Entities;
using FirstProject.Services;
using AutoMapper;
using FirstProject.Models.ViewModels;

namespace FirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IStudent _studentrepo;
        private readonly IMapper _mapper;

        //private readonly 

        public StudentsController(DBContext context, IStudent studentrepo, IMapper mapper)
        {
            _context = context;
            _studentrepo = studentrepo;
            _mapper = mapper;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            //var mytask = Task.Run(() => GetStudent());
            //IEnumerable<Student> result = await mytask;
            //return await _context.Students.ToListAsync();
            var res = await _studentrepo.Get();
            var ress = res.ToList();
            var userViewModel = _mapper.Map<List<Student>, List<StudentViewModel>>(ress);
            //var resss = userViewModel.ToList();
            return Ok(userViewModel);
            //return data;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {

            var student = await _studentrepo.GetbyID(id);

            if (student == null)
            {
                return NotFound();
            }
            var userViewModel = _mapper.Map<Student, StudentViewModel>(student);
            return Ok(userViewModel);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromBody]Student student)
        {
            if (id != student.StudentID)
            {
                return BadRequest();
            }
            //_context.Entry(student).State = EntityState.Modified;

            await Task.Run(() => _studentrepo.Update(id, student));

            
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!StudentExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            await Task.Run(() => _studentrepo.Add(student));
            //_context.Students.Add(student);
            //await _context.SaveChangesAsync();
            var userViewModel = _mapper.Map<Student, StudentViewModel>(student);
            return CreatedAtAction("GetStudent", new { id = student.StudentID }, userViewModel);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
             var student =  await Task.Run(() => _studentrepo.Delete(id));
            //   var student = await _context.Students.FindAsync(id);
            //if (student == null)
            //{
            //    return NotFound();
            //}

            //_context.Students.Remove(student);
            //await _context.SaveChangesAsync();
            
            return student;
            
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
    }
}
