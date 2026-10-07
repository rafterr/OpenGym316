using System.Text.Json.Serialization;

namespace PR.OpenGym.Data
{
    public class AssociateMembership : BaseEntity
    {

        [JsonIgnore]
        public Associate? Associate { get; set; }
        public int? MembershipId { get; set; }
        public Membership? Membership { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public MembershipStatus MembershipStatus { get; set; }
    }
}
