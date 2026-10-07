using PR.OpenGym.Data;
using PR.OpenGym.Utilities.ExtensionMethods;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PR.OpenGym.Web.Models
{
    public class AssociateAndDetailsViewModel
    {
        public AssociateViewModel Associate { get; set; }
        public AssociateDetailsViewModel AssociateDetails { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Inicio Membresia")]
        public DateTime MembershipStarts { get; set; } = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
        [Required]
        [Display(Name = "Membresia")]
        public int MembershipId { get; set; }
        public List<Product>? Memberships  { get; set; }
        public bool IsFaceTerminalConnected { get; set; }
        public string? ImageBase64URIData { get; set; }
    }
}
