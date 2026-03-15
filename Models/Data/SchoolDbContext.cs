using Microsoft.EntityFrameworkCore;
using SchoolApi.Models.Entities;

namespace SchoolApi.Models.Data
{
    public class SchoolDbContext(DbContextOptions<SchoolDbContext> options) : DbContext(options)
    {
        public DbSet<ProgramEntity> Programs { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Staff> Staff { get; set; }

        public DbSet<StudentAttendance> StudentAttendances { get; set; }

        public DbSet<StaffAttendance> StaffAttendances { get; set; }

        public DbSet<StaffAssignment> StaffAssignments { get; set; }

        public DbSet<AcademicYear> AcademicYears { get; set; }

        public DbSet<StudentEnrollment> StudentEnrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);
        }
    }


}