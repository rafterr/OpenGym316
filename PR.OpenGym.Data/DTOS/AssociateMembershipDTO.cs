namespace PR.OpenGym.Data.DTOS
{
    public class AssociateMembershipDTO
    {
        public int Id { get; set; }
        public int MembershipId { get; set; }
        public Membership? Membership { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public MembershipStatus MembershipStatus { get; set; }
    }
}
