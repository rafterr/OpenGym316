using PR.OpenGym.Data;

namespace PR.OpenGym.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<CheckIn> CheckIns { get; set; }
        public decimal PaymentsTotal { get; set; }
    }
}
