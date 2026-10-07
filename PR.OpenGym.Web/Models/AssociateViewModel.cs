using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;
using PR.OpenGym.Utilities.ExtensionMethods;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PR.OpenGym.Web.Models
{
    public class AssociateViewModel
    {
        public int? Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [Display(Name ="Nombre")]
        [Required]
        public string? FirstName { get; set; }
        [Display(Name = "Apellidos")]
        public string? LastName { get; set; }
        [Display(Name = "Genero")]
        public Gender? Gender { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? DateBirth { get; set; }
        [Display(Name = "Edad")]
        public int? Age { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Telefono Movil / Fijo")]
        public string? Phone { get; set; }
        [Display(Name = "Facebook")]
        public string? Facebook { get; set; }
        public string? ImgPath { get; set; }
        [Display(Name = "Sucursal")]
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }

        public AssociateMembershipViewModel? AssociateMembership { get; set; }

        public Status Status { get; set; }
    }

    public class AssociateMembershipViewModel
    {
        public int MembershipId { get; set; }
        public Membership? Membership { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Fecha de inicio")]
        public DateTime? From { get; set; } = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
        public DateTime? To { get; set; }
        public MembershipStatus MembershipStatus { get; set; }
    }
}
