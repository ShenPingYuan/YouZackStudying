using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityRelationship
{
    public class User: IEntityTypeConfiguration<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            
        }
    }
}
