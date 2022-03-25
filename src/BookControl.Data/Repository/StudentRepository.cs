using BookControl.Business.Interfaces;
using BookControl.Business.Models;
using BookControl.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookControl.Data.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(BookControlDbContext context) : base(context) { }

        public async Task<IEnumerable<Student>> GetStudentsBooks()
        {
            return await Db.Students.AsNoTracking()
                .Include(s => s.Books)
                .ToListAsync();
        }

        public async Task<Student> GetStudentBooks(Guid id)
        {
            return await Db.Students.AsNoTracking()
                .Include(s => s.Books)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
