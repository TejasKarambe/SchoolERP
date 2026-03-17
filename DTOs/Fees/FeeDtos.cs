namespace SchoolApi.DTOs.Fees
{
    public class CreateFeeStructureDto
    {
        public int ProgramId { get; set; }
        public int? ClassId { get; set; }
        public int AcademicYearId { get; set; }
        public string FeeType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateFeeStructureDto
    {
        public string FeeType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class CreateFeePaymentDto
    {
        public int StudentId { get; set; }
        public int FeeStructureId { get; set; }
        public int AcademicYearId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public string? TransactionId { get; set; }
        public string Status { get; set; } = "Paid";
        public string? Remarks { get; set; }
    }

    public class UpdateFeePaymentDto
    {
        public decimal AmountPaid { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public string? TransactionId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remarks { get; set; }
    }
}
