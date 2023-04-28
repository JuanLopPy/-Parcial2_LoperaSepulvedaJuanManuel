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


        public int NumTickets { get; set; }

        [Display(Name = ("Fecha de uso de la boleta"))]
        public virtual DateTime UseDate { get; set; } = DateTime.Now;

        [Display(Name = ("Uso de la boleta"))]
        public bool IsUsed { get; set; } = false;
        [Display(Name = ("Portería"))]
        public String EntranceGate { get; set; } = "null";

    }
}
