using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleAppEntityRelationship
{
    public class Teacher:IEntityTypeConfiguration<Teacher>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();

        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasMany(t => t.Students).WithMany(s => s.Teachers).UsingEntity(j => j.ToTable("Students_Teachers"));
        }
    }
}