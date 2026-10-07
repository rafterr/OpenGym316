using PR.OpenGym.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PR.OpenGym.Web.Models
{
    public class AssociateDetailsViewModel
    {
        public int? Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? AssociateId { get; set; }
        public Associate? Associate { get; set; }
        [Display(Name = "Calle")]
        public string? Street { get; set; }
        [Display(Name = "Numero")]
        public string? Number { get; set; }
        [Display(Name = "Colonia")]
        public string? Neighborhood { get; set; }
        [Display(Name = "Código postal")]
        public string? ZipCode { get; set; }
        [Display(Name = "Ciudad")]
        public string? City { get; set; } = "León";
        [Display(Name = "Estado")]
        public string? State { get; set; } = "Guanajuato";
        [Display(Name = "Pais")]
        public string? Country { get; set; } = "México";
    }
}
