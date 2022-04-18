using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEFCore充血模型
{
    internal class Student:IEntityTypeConfiguration<Student>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentType StudentType { get; set; }
        public Geo Location{ get; set; }

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.StudentType).HasConversion<string>().HasMaxLength(10);
            builder.OwnsOne(x => x.Location, l =>
            {
                l.Property(l=>l.Longitude).HasMaxLength(10);
                l.Property(l=>l.Latitude).HasMaxLength(10);
            });//owned entity
        }
    }
    //值对象
    public class Geo
    {
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public Geo(double longitude, double latitude)
        {
            if(longitude < -180 || longitude > 180)
            {
                throw new ArgumentException("longitude out of range");
            }
            if (latitude < -180 || latitude > 180)
            {
                throw new ArgumentException("longitude out of range");
            }
            Longitude = longitude;
            Latitude = latitude;
        }
    }

    internal enum StudentType
    {
        [Description("优等生")]
        GOOD,
        [Description("中等生")]
        CENTER,
        [Description("差等生")]
        BAD
    }
}
