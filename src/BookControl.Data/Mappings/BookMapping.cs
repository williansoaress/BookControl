using BookControl.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookControl.Data.Mappings
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(b => b.Author)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(b => b.Description)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.ToTable("Books");
        }
    }
}
