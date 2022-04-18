using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityRelationship
{
    public class Article:IEntityTypeConfiguration<Article>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasQueryFilter(a => a.IsDeleted == false);
        }
    }
}
