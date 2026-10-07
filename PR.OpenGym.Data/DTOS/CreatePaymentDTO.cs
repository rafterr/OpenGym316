namespace PR.OpenGym.Data.DTOS
{
    public class CreatePaymentDTO
    {
        public int AssociateId { get; set; }
        public int MembershipId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
