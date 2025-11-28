using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LinhKienDienTu.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int? ParentId { get; set; }
        [JsonIgnore]
        public virtual Category Parent { get; set; }
        [JsonIgnore]
        public virtual ICollection<Category> Children { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
