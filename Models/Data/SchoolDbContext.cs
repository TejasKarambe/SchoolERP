using Microsoft.EntityFrameworkCore;
using SchoolApi.Models.Entities;

namespace SchoolApi.Models.Data
{
    public class SchoolDbContext(DbContextOptions<SchoolDbContext> options) : DbContext(options)
    {
        // ── Academic Structure ────────────────────────────────────────────────
        public DbSet<ProgramEntity> Programs { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }

        // ── People ────────────────────────────────────────────────────────────
        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<User> Users { get; set; }

        // ── Enrollment & Assignment ────────────────────────────────────────────
        public DbSet<StudentEnrollment> StudentEnrollments { get; set; }
        public DbSet<StaffAssignment> StaffAssignments { get; set; }

        // ── Attendance ─────────────────────────────────────────────────────────
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
        public DbSet<StaffAttendance> StaffAttendances { get; set; }

        // ── Finance ────────────────────────────────────────────────────────────
        public DbSet<FeeStructure> FeeStructures { get; set; }
        public DbSet<FeePayment> FeePayments { get; set; }

        // ── Exams & Results ────────────────────────────────────────────────────
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }

        // ── Notifications ──────────────────────────────────────────────────────
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);
        }
    }
}