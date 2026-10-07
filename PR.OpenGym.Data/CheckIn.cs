namespace PR.OpenGym.Data
{
    public class CheckIn : BaseEntity
    {
        public int AssociateId { get; set; }
        public Associate Associate { get; set; }
    }
}
