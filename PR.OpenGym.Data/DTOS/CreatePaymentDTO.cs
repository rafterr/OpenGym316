namespace PR.OpenGym.Data.DTOS
{
    public class CreatePaymentDTO
    {
        public int AssociateId { get; set; }
        public int MembershipId { get; set; }
        public DateTime StartDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        /// <summary>
        /// Usuario del sistema que registro el cobro
        /// </summary>
        public string? IssuedBy { get; set; }
    }

    public class PaymentResultDTO
    {
        public int PaymentId { get; set; }
        public int ReceiptId { get; set; }
        public string? Folio { get; set; }
    }

    public class CancelReceiptDTO
    {
        public string? Reason { get; set; }
    }
}
