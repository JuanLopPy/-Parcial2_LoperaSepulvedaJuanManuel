using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Parcial2.DAL.Entities
{
    public class Ticket:Entity
    {
        [Display(Name = "Ticket")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Id { get; set; } 

    }
}
