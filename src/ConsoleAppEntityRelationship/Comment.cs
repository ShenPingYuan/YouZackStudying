using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleAppEntityRelationship
{
    public class Comment: IEntityTypeConfiguration<Comment>
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public Article Article { get; set; }

        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c=>c.Article).WithMany(a=>a.Comments).IsRequired();
        }
    }
}