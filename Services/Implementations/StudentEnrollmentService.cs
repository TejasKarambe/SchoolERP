using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Enrollments;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class StudentEnrollmentService : IStudentEnrollmentService
    {
        private readonly SchoolDbContext _context;

        public StudentEnrollmentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentEnrollment>> GetAllAsync()
        {
            return await _context.StudentEnrollments
                .Include(e => e.Student)
                .Include(e => e.Section)
                    .ThenInclude(s => s.Class)
                .Include(e => e.AcademicYear)
                .ToListAsync();
        }

        public async Task<StudentEnrollment?> GetByIdAsync(int id)
        {
            return await _context.StudentEnrollments
                .Include(e => e.Student)
                .Include(e => e.Section)
                    .ThenInclude(s => s.Class)
                .Include(e => e.AcademicYear)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<StudentEnrollment>> GetByStudentAsync(int studentId)
        {
            return await _context.StudentEnrollments
                .Include(e => e.Section)
                    .ThenInclude(s => s.Class)
                .Include(e => e.AcademicYear)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<List<StudentEnrollment>> GetByAcademicYearAsync(int academicYearId)
        {
            return await _context.StudentEnrollments
                .Include(e => e.Student)
                .Include(e => e.Section)
                    .ThenInclude(s => s.Class)
                .Where(e => e.AcademicYearId == academicYearId)
                .ToListAsync();
        }

        public async Task<List<StudentEnrollment>> GetBySectionAsync(int sectionId)
        {
            return await _context.StudentEnrollments
                .Include(e => e.Student)
                .Include(e => e.AcademicYear)
                .Where(e => e.SectionId == sectionId)
                .ToListAsync();
        }

        public async Task<StudentEnrollment> CreateAsync(CreateStudentEnrollmentDto dto)
        {
            var entity = new StudentEnrollment
            {
                StudentId = dto.StudentId,
                SectionId = dto.SectionId,
                AcademicYearId = dto.AcademicYearId
            };

            _context.StudentEnrollments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StudentEnrollments.FindAsync(id);
            if (entity == null) return false;

            _context.StudentEnrollments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
