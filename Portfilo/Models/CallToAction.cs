using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Portfilo.Models
{
    public class CallToAction : BaseEntity
    {
        [Required]
        public int CallToActionId { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [DisplayName("Text")]
        public string Text { get; set; } = string.Empty;

        [Required]
        [DisplayName("Url")]
        public string Url { get; set; } = string.Empty;
    }
}
