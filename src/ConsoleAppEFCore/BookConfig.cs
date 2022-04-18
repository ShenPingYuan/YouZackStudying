using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEFCore
{
    internal class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(50)
                .HasColumnName("Title");
            builder.Ignore(b => b.Price);
            builder.Property(b => b.Price).HasDefaultValue(100);
            builder.HasIndex(b => b.Title);
            builder.HasIndex(b => b.Id).IsUnique();
        }
    }
}
