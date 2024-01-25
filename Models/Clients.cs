using Carvilla.Models.Common;

namespace Carvilla.Models
{
    public class Clients:BaseEntity
    {
        public string Description { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
    }
}
