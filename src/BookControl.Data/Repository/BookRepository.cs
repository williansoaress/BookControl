using BookControl.Business.Interfaces;
using BookControl.Business.Models;
using BookControl.Data.Context;

namespace BookControl.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookControlDbContext context) : base(context) { }
    }
}
