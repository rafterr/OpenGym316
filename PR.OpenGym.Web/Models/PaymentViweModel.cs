using PR.OpenGym.Data;
using PR.OpenGym.Utilities.ExtensionMethods;

namespace PR.OpenGym.Web.Models
{
    public class PaymentViweModel
    {
        public List<Product> Memberships { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
    }
}
