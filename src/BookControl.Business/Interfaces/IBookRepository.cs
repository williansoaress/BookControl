using BookControl.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookControl.Business.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetAvailableBooks();
        Task<IEnumerable<Book>> GetUnavailableBooks();
    }
}
