using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace LinhKienDienTu.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(50)]
        public string PartNumber { get; set; }

        public string Description { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? BulkPrice { get; set; } // For > 100 items

        public int StockQuantity { get; set; }

        public string? ImageUrl { get; set; }
        
        public string? DatasheetUrl { get; set; }
        
        public string? Manufacturer { get; set; }
        
        public string? Specifications { get; set; }

        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
