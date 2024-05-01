using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Portfilo.Models
{
    public class TransactionContactUs
    {
        [Required]
        public int TransactionContactUsId { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; } = default!;

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; } = default!;

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; } = default!;

        [Required]
        [DisplayName("Message")]
        public string Message { get; set; } = default!;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CreateDate { get; set; }
    }
}
