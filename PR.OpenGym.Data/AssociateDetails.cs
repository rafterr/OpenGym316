namespace PR.OpenGym.Data
{
    public class AssociateDetails : BaseEntity
    {
        public int AssociateId { get; set; }
        public Associate? Associate { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighborhood { get; set; }
        public string? ZipCode  { get; set; }
        public string? City  { get; set; }
        public string? State  { get; set; }
        public string? Country { get; set; }
    }
}
