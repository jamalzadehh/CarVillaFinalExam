namespace Carvilla.ViewModels.Client
{
    public class ClientCreateVM
    {
        public string Description { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
