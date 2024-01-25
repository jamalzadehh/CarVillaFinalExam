using System.ComponentModel.DataAnnotations.Schema;

namespace Carvilla.ViewModels.Client
{
    public class ClientUpdateVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile?  Image { get; set; }
    }
}
