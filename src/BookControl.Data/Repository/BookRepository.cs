using BookControl.Business.Interfaces;
using BookControl.Business.Models;
using BookControl.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookControl.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookControlDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetAvailableBooks()
        {
            return await Db.Books.AsNoTracking()
                .Where(b => b.StudentId == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetUnavailableBooks()
        {
            return await Db.Books.AsNoTracking()
                .Where(b => b.StudentId != null)
                .ToListAsync();
        }
    }
}
