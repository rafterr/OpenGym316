using System.Reflection.Metadata.Ecma335;

namespace PR.OpenGym.Data
{
    public class Associate : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateBirth { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Facebook { get; set; }
        public string? ImgPath { get; set; }

        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }

        public int? AssociateMembershipId { get; set; }
        public AssociateMembership? AssociateMembership { get; set; }
        public Status Status { get; set; }
    }
}
