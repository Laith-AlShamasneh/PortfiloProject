using System.ComponentModel.DataAnnotations;

namespace Portfilo.Models
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public string CreateUser { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CreateDate { get; set; }

        public string EditUser { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? EditDate { get; set; }
    }
}
