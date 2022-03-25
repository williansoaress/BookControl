using BookControl.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookControl.Data.Context
{
    public class BookControlDbContext : DbContext
    {
        public BookControlDbContext(DbContextOptions<BookControlDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookControlDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}