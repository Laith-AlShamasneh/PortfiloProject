using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Portfilo.Models
{
    public class SocialMedia : BaseEntity
    {
        [Required]
        public int SocialMediaId { get; set; }

        [Required]
        [DisplayName("LinkClass")]
        public string LinkClass { get; set; } = string.Empty;

        [Required]
        [DisplayName("IconClass")]
        public string IconClass { get; set; } = string.Empty;

        [Required]
        [DisplayName("Url")]
        public string Url { get; set; } = string.Empty;
    }
}
