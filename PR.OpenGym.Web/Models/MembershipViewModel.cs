using System.ComponentModel.DataAnnotations;

namespace PR.OpenGym.Web.Models
{
    public class MembershipViewModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name ="Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Precio MNX")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Periodo en dias")]

        public int Period { get; set; }

    }
}
