using BookControl.Business.Models;
using System;
using System.Threading.Tasks;

namespace BookControl.Business.Interfaces
{
    public interface IBookService : IDisposable
    {
        Task Add(Book book);
        Task Update(Book book);
        Task Remove(Guid Id);
    }
}
