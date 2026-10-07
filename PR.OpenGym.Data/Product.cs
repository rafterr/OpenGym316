
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PR.OpenGym.Data
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public string? Description { get; set; }

    }
}
