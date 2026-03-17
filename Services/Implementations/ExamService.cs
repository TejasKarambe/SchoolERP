using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Exams;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class ExamService : IExamService
    {
        private readonly SchoolDbContext _context;

        public ExamService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Exam>> GetAllAsync()
        {
            return await _context.Exams
                .Include(e => e.Class)
                .Include(e => e.Subject)
                .Include(e => e.AcademicYear)
                .OrderByDescending(e => e.ExamDate)
                .ToListAsync();
        }

        public async Task<Exam?> GetByIdAsync(int id)
        {
            return await _context.Exams
                .Include(e => e.Class)
                .Include(e => e.Subject)
                .Include(e => e.AcademicYear)
                .Include(e => e.ExamResults)
                    .ThenInclude(r => r.Student)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Exam>> GetByClassAsync(int classId)
        {
            return await _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.AcademicYear)
                .Where(e => e.ClassId == classId)
                .OrderByDescending(e => e.ExamDate)
                .ToListAsync();
        }

        public async Task<List<Exam>> GetByAcademicYearAsync(int academicYearId)
        {
            return await _context.Exams
                .Include(e => e.Class)
                .Include(e => e.Subject)
                .Where(e => e.AcademicYearId == academicYearId)
                .OrderByDescending(e => e.ExamDate)
                .ToListAsync();
        }

        public async Task<Exam> CreateAsync(CreateExamDto dto)
        {
            var entity = new Exam
            {
                Name = dto.Name,
                Type = dto.Type,
                AcademicYearId = dto.AcademicYearId,
                ClassId = dto.ClassId,
                SubjectId = dto.SubjectId,
                ExamDate = dto.ExamDate,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                TotalMarks = dto.TotalMarks,
                PassingMarks = dto.PassingMarks
            };

            _context.Exams.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Exam?> UpdateAsync(int id, UpdateExamDto dto)
        {
            var entity = await _context.Exams.FindAsync(id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.Type = dto.Type;
            entity.ExamDate = dto.ExamDate;
            entity.StartTime = dto.StartTime;
            entity.EndTime = dto.EndTime;
            entity.TotalMarks = dto.TotalMarks;
            entity.PassingMarks = dto.PassingMarks;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Exams.FindAsync(id);
            if (entity == null) return false;

            _context.Exams.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Exam Results ───────────────────────────────────────────────────────

        public async Task<List<ExamResult>> GetResultsByExamAsync(int examId)
        {
            return await _context.ExamResults
                .Include(r => r.Student)
                .Include(r => r.Exam)
                .Where(r => r.ExamId == examId)
                .ToListAsync();
        }

        public async Task<List<ExamResult>> GetResultsByStudentAsync(int studentId)
        {
            return await _context.ExamResults
                .Include(r => r.Exam)
                    .ThenInclude(e => e.Subject)
                .Where(r => r.StudentId == studentId)
                .OrderByDescending(r => r.Exam.ExamDate)
                .ToListAsync();
        }

        public async Task<ExamResult> CreateResultAsync(CreateExamResultDto dto)
        {
            var entity = new ExamResult
            {
                ExamId = dto.ExamId,
                StudentId = dto.StudentId,
                MarksObtained = dto.MarksObtained,
                Grade = dto.Grade,
                Remarks = dto.Remarks,
                IsAbsent = dto.IsAbsent
            };

            _context.ExamResults.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<ExamResult>> BulkCreateResultsAsync(BulkExamResultDto dto)
        {
            // Remove existing results for this exam before inserting
            var existing = await _context.ExamResults
                .Where(r => r.ExamId == dto.ExamId)
                .ToListAsync();

            if (existing.Any())
                _context.ExamResults.RemoveRange(existing);

            var entities = dto.Results.Select(r => new ExamResult
            {
                ExamId = dto.ExamId,
                StudentId = r.StudentId,
                MarksObtained = r.MarksObtained,
                Grade = r.Grade,
                Remarks = r.Remarks,
                IsAbsent = r.IsAbsent
            }).ToList();

            _context.ExamResults.AddRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<ExamResult?> UpdateResultAsync(int id, UpdateExamResultDto dto)
        {
            var entity = await _context.ExamResults.FindAsync(id);
            if (entity == null) return null;

            entity.MarksObtained = dto.MarksObtained;
            entity.Grade = dto.Grade;
            entity.Remarks = dto.Remarks;
            entity.IsAbsent = dto.IsAbsent;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteResultAsync(int id)
        {
            var entity = await _context.ExamResults.FindAsync(id);
            if (entity == null) return false;

            _context.ExamResults.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
