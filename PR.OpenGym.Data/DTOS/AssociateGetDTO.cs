using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR.OpenGym.Data.DTOS
{
    public class AssociateGetDTO : AssociateBaseDTO
    {
        public int Id { get; set; }
        public int? AssociateMembershipId { get; set; }
        public AssociateMembershipDTO? AssociateMembership { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
