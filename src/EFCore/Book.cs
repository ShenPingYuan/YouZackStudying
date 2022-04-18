
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore
{
    public class Book:IEntityTypeConfiguration<Book>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }

        public void Configure(EntityTypeBuilder<Book> builder)
        {
        }
    }
}