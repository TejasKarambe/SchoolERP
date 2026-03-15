using static System.Collections.Specialized.BitVector32;

namespace SchoolApi.Models.Entities
{
    public class Class : BaseEntity
    {
        public int ProgramId { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public ProgramEntity Program { get; set; }

        public ICollection<Section> Sections { get; set; }
    }
}
