using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfilo.Models
{
    public class Menu : BaseEntity
    {
        [Required]
        public int MenuId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DisplayName("Class")]
        public string Class { get; set; } = string.Empty;

        [Required]
        [DisplayName("Url")]
        public string Url { get; set; } = string.Empty;
    }
}
