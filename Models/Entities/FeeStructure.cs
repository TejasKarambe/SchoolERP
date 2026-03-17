namespace SchoolApi.Models.Entities
{
    public class FeeStructure : BaseEntity
    {
        public int ProgramId { get; set; }

        public int? ClassId { get; set; }

        public int AcademicYearId { get; set; }

        public string FeeType { get; set; } = string.Empty; // Tuition, Library, Lab, Transport, Exam

        public decimal Amount { get; set; }

        public string Description { get; set; } = string.Empty;

        public ProgramEntity Program { get; set; } = null!;

        public Class? Class { get; set; }

        public AcademicYear AcademicYear { get; set; } = null!;

        public ICollection<FeePayment> FeePayments { get; set; } = [];
    }
}
