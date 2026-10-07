namespace PR.OpenGym.Data
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Associate> Associates { get; set; }
    }
}
