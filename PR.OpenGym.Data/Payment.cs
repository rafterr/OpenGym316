using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PR.OpenGym.Data
{
    public class Payment : BaseEntity
    {
        public int? AssociateId { get; set; }
        public Associate?  Associate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public string? Concept { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}
