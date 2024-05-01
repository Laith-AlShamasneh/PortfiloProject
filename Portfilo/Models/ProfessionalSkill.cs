using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Portfilo.Models
{
    public class ProfessionalSkill : BaseEntity
    {
        [Required]
        public int ProfessionalSkillId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;
    }
}
