using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApi.Models.Entities;

namespace SchoolApi.Models.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AdmissionNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(x => x.AdmissionNumber)
                   .IsUnique();

            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}