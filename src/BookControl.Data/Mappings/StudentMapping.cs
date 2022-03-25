using BookControl.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookControl.Data.Mappings
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(s => s.LastName)
                .IsRequired()
                .HasColumnType("varchar(200)");

            // 1 : N => Student : Books
            builder
                .HasMany(s => s.Books)
                .WithOne(b => b.Student)
                .HasForeignKey(b => b.StudentId);

            builder.ToTable("Students");
        }
    }
}
