using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Portfilo.Models
{
    public class Education : BaseEntity
    {
        [Required]
        public int EducationId { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [DisplayName("Location")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [DisplayName("Brief")]
        public string Brief { get; set; } = string.Empty;

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DisplayName("FromDate")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DisplayName("ToDate")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
    }
}
