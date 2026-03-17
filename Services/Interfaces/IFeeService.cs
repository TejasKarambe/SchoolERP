using SchoolApi.DTOs.Fees;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IFeeService
    {
        // Fee Structure
        Task<List<FeeStructure>> GetAllStructuresAsync();
        Task<FeeStructure?> GetStructureByIdAsync(int id);
        Task<List<FeeStructure>> GetStructuresByProgramAsync(int programId);
        Task<FeeStructure> CreateStructureAsync(CreateFeeStructureDto dto);
        Task<FeeStructure?> UpdateStructureAsync(int id, UpdateFeeStructureDto dto);
        Task<bool> DeleteStructureAsync(int id);

        // Fee Payments
        Task<List<FeePayment>> GetAllPaymentsAsync();
        Task<FeePayment?> GetPaymentByIdAsync(int id);
        Task<List<FeePayment>> GetPaymentsByStudentAsync(int studentId);
        Task<List<FeePayment>> GetPendingPaymentsAsync();
        Task<FeePayment> CreatePaymentAsync(CreateFeePaymentDto dto);
        Task<FeePayment?> UpdatePaymentAsync(int id, UpdateFeePaymentDto dto);
        Task<bool> DeletePaymentAsync(int id);
    }
}
