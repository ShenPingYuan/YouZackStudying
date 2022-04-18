using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEFCoreConcurrency
{
    public class House:IEntityTypeConfiguration<House>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public byte[] RowVersion { get; set; }//or Guid

        public void Configure(EntityTypeBuilder<House> builder)
        {
            //method 1
            //builder.Property(h => h.Owner).IsConcurrencyToken();
            //method 2
            builder.Property(h=>h.RowVersion).IsRowVersion();
        }
    }
}
