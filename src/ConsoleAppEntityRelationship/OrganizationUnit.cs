using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityRelationship
{
    public class OrganizationUnit:IEntityTypeConfiguration<OrganizationUnit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OrganizationUnit Parent { get; set; }
        public List<OrganizationUnit> Children { get; set; } = new List<OrganizationUnit>();

        public void Configure(EntityTypeBuilder<OrganizationUnit> builder)
        {
            builder.HasOne(u => u.Parent).WithMany(p => p.Children);
        }
    }
}
