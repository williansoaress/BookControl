using BookControl.Business.Models;
using System;
using System.Threading.Tasks;

namespace BookControl.Business.Interfaces
{
    public interface IStudentService : IDisposable
    {
        Task Add(Student student);
        Task Update(Student student);
        Task Remove(Guid Id);
    }
}
