using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PR.OpenGym.Data
{
    /// <summary>
    /// Recibo de pago. Guarda una copia de los datos al momento del cobro
    /// para que el recibo no cambie si despues se modifica el socio o el precio.
    /// </summary>
    public class Receipt : BaseEntity
    {
        /// <summary>
        /// Folio consecutivo derivado del Id, ej. R-000123
        /// </summary>
        [NotMapped]
        public string Folio => $"R-{Id:D6}";

        public int PaymentId { get; set; }
        public Payment? Payment { get; set; }

        public int? AssociateId { get; set; }
        [MaxLength(200)]
        public string AssociateName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string MembershipName { get; set; } = string.Empty;
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [MaxLength(200)]
        public string? BranchName { get; set; }
        [MaxLength(300)]
        public string? BranchAddress { get; set; }
        [MaxLength(256)]
        public string? IssuedBy { get; set; }
        public ReceiptStatus Status { get; set; }
        [MaxLength(300)]
        public string? CancelReason { get; set; }
    }
}
