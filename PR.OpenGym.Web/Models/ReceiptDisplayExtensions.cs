using PR.OpenGym.Data;
using System.Globalization;

namespace PR.OpenGym.Web.Models
{
    public static class ReceiptDisplayExtensions
    {
        public static readonly CultureInfo MexicoCulture = new CultureInfo("es-MX");

        public static string ToDisplay(this PaymentMethod paymentMethod) => paymentMethod switch
        {
            PaymentMethod.Transfer => "Transferencia",
            _ => "Efectivo"
        };

        public static string ToDisplay(this ReceiptStatus status) => status switch
        {
            ReceiptStatus.Cancelled => "Cancelado",
            _ => "Vigente"
        };

        public static string ToMoney(this decimal amount) => amount.ToString("C2", MexicoCulture);
    }
}
