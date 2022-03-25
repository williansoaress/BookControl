using BookControl.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookControl.Business.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentsBooks();
        Task<Student> GetStudentBooks(Guid id);
    }
}
