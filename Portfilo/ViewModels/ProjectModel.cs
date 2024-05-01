using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Portfilo.ViewModels
{
    public class ProjectModel
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; } = string.Empty;

        [DisplayName("Image")]
        public string? Image { get; set; }

        [DisplayName("File")]
        public IFormFile? ImageFile { get; set; }
    }
}
