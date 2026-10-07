using PR.OpenGym.Data;

namespace PR.OpenGym.Web.Models
{
    public class ReceiptListViewModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<Receipt> Receipts { get; set; } = new List<Receipt>();
    }
}
