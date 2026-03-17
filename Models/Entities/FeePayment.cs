namespace SchoolApi.Models.Entities
{
    public class FeePayment : BaseEntity
    {
        public int StudentId { get; set; }

        public int FeeStructureId { get; set; }

        public int AcademicYearId { get; set; }

        public decimal AmountPaid { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMode { get; set; } = string.Empty; // Cash, Online, Cheque, BankTransfer

        public string? TransactionId { get; set; }

        public string Status { get; set; } = string.Empty; // Paid, Partial, Pending

        public string? Remarks { get; set; }

        public Student Student { get; set; } = null!;

        public FeeStructure FeeStructure { get; set; } = null!;

        public AcademicYear AcademicYear { get; set; } = null!;
    }
}
