using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Portfilo.Models
{
    public class Language : BaseEntity
    {
        [Required]
        public int LanguageId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;
    }
}
