using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Student> AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> DeleteAsync(string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> GetByAsync(string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);
            return student;
        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var dept = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);

            dept.Name = student.Name;
            _context.Students.Update(dept);
            await _context.SaveChangesAsync();

            return student;
        }
    }
}
