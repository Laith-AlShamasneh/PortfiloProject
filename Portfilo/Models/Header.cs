using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Portfilo.Models
{
    public class Header : BaseEntity
    {
        [Required]
        public int HeaderId { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [DisplayName("Brief")]
        public string Brief { get; set; } = string.Empty;

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DisplayName("Url 1")]
        public string Url1 { get; set; } = string.Empty;

        [Required]
        [DisplayName("Url 2")]
        public string Url2 { get; set; } = string.Empty;

        [DisplayName("Image")]
        public string? Image { get; set; }
    }
}
