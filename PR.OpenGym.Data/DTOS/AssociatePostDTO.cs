namespace PR.OpenGym.Data.DTOS
{
    public class AssociatePostDTO : AssociateBaseDTO
    {
        public AssociateMembershipDTO? AssociateMembership { get; set; }

        /// <summary>
        /// Solo en el alta: si viene informado se cobra la primera membresia y se genera recibo
        /// </summary>
        public PaymentMethod? PaymentMethod { get; set; }
        public string? IssuedBy { get; set; }
    }
}
