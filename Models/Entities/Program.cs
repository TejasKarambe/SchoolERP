using System.Security.Claims;

namespace SchoolApi.Models.Entities
{
    public class ProgramEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Class> Classes { get; set; }
    }
}
