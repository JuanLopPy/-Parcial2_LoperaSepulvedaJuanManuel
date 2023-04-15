 using System.ComponentModel.DataAnnotations;
 namespace Parcial2.DAL.Entities

{
    public class Entity
    {
        [Key]
        public virtual Guid Id { get; set; }


        [Display(Name = ("Fecha de uso de la boleta"))]
        public virtual DateTime UseDate { get; set; }= DateTime.Now;

        [Display(Name = ("Uso de la boleta"))]
        public bool IsUsed { get; set; } = false;
        [Display(Name = ("Portería"))]
        public String EntranceGate { get; set; } = "null";
    }
}
