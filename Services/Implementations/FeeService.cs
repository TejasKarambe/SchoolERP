using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Fees;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class FeeService : IFeeService
    {
        private readonly SchoolDbContext _context;

        public FeeService(SchoolDbContext context)
        {
            _context = context;
        }

        // ── Fee Structure ──────────────────────────────────────────────────────

        public async Task<List<FeeStructure>> GetAllStructuresAsync()
        {
            return await _context.FeeStructures
                .Include(f => f.Program)
                .Include(f => f.Class)
                .Include(f => f.AcademicYear)
                .ToListAsync();
        }

        public async Task<FeeStructure?> GetStructureByIdAsync(int id)
        {
            return await _context.FeeStructures
                .Include(f => f.Program)
                .Include(f => f.Class)
                .Include(f => f.AcademicYear)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<FeeStructure>> GetStructuresByProgramAsync(int programId)
        {
            return await _context.FeeStructures
                .Include(f => f.Class)
                .Include(f => f.AcademicYear)
                .Where(f => f.ProgramId == programId)
                .ToListAsync();
        }

        public async Task<FeeStructure> CreateStructureAsync(CreateFeeStructureDto dto)
        {
            var entity = new FeeStructure
            {
                ProgramId = dto.ProgramId,
                ClassId = dto.ClassId,
                AcademicYearId = dto.AcademicYearId,
                FeeType = dto.FeeType,
                Amount = dto.Amount,
                Description = dto.Description
            };

            _context.FeeStructures.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FeeStructure?> UpdateStructureAsync(int id, UpdateFeeStructureDto dto)
        {
            var entity = await _context.FeeStructures.FindAsync(id);
            if (entity == null) return null;

            entity.FeeType = dto.FeeType;
            entity.Amount = dto.Amount;
            entity.Description = dto.Description;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteStructureAsync(int id)
        {
            var entity = await _context.FeeStructures.FindAsync(id);
            if (entity == null) return false;

            _context.FeeStructures.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Fee Payments ──────────────────────────────────────────────────────

        public async Task<List<FeePayment>> GetAllPaymentsAsync()
        {
            return await _context.FeePayments
                .Include(p => p.Student)
                .Include(p => p.FeeStructure)
                .Include(p => p.AcademicYear)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<FeePayment?> GetPaymentByIdAsync(int id)
        {
            return await _context.FeePayments
                .Include(p => p.Student)
                .Include(p => p.FeeStructure)
                .Include(p => p.AcademicYear)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<FeePayment>> GetPaymentsByStudentAsync(int studentId)
        {
            return await _context.FeePayments
                .Include(p => p.FeeStructure)
                .Include(p => p.AcademicYear)
                .Where(p => p.StudentId == studentId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<List<FeePayment>> GetPendingPaymentsAsync()
        {
            return await _context.FeePayments
                .Include(p => p.Student)
                .Include(p => p.FeeStructure)
                .Where(p => p.Status == "Pending" || p.Status == "Partial")
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<FeePayment> CreatePaymentAsync(CreateFeePaymentDto dto)
        {
            var entity = new FeePayment
            {
                StudentId = dto.StudentId,
                FeeStructureId = dto.FeeStructureId,
                AcademicYearId = dto.AcademicYearId,
                AmountPaid = dto.AmountPaid,
                PaymentDate = dto.PaymentDate,
                PaymentMode = dto.PaymentMode,
                TransactionId = dto.TransactionId,
                Status = dto.Status,
                Remarks = dto.Remarks
            };

            _context.FeePayments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FeePayment?> UpdatePaymentAsync(int id, UpdateFeePaymentDto dto)
        {
            var entity = await _context.FeePayments.FindAsync(id);
            if (entity == null) return null;

            entity.AmountPaid = dto.AmountPaid;
            entity.PaymentMode = dto.PaymentMode;
            entity.TransactionId = dto.TransactionId;
            entity.Status = dto.Status;
            entity.Remarks = dto.Remarks;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var entity = await _context.FeePayments.FindAsync(id);
            if (entity == null) return false;

            _context.FeePayments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
