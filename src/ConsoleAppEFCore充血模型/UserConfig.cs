using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleAppEFCore充血模型
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property("passwordHash");
            builder.Property(x => x.Remark).HasField("remark");
            builder.Ignore(x => x.Tag);
        }
    }
}
