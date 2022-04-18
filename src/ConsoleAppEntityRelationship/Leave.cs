using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityRelationship
{
    public class Leave: IEntityTypeConfiguration<Leave>
    {
        public int Id { get; set; }
        public int RequesterId { get; set; }
        public int? ApproverId { get; set; }
        public User Requester { get; set; }
        public User Approver { get; set; }
        public string Remarks { get; set; }

        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.HasOne(e => e.Requester).WithMany().HasForeignKey(l=>l.RequesterId).IsRequired();
            builder.HasOne(e => e.Approver).WithMany().HasForeignKey(l=>l.ApproverId);
        }
    }
}
