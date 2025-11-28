using System.Collections.Generic;
using System.Linq;

namespace LinhKienDienTu.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        
        public decimal TotalPrice => (Product.BulkPrice.HasValue && Quantity > 100 ? Product.BulkPrice.Value : Product.Price) * Quantity;
    }

    public class Cart
    {
        private List<CartItem> _items = new List<CartItem>();

        public IEnumerable<CartItem> Items => _items;

        public void AddItem(Product product, int quantity)
        {
            var item = _items.FirstOrDefault(p => p.Product.Id == product.Id);
            if (item == null)
            {
                _items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product)
        {
            _items.RemoveAll(l => l.Product.Id == product.Id);
        }

        public decimal ComputeTotalValue() => _items.Sum(e => e.TotalPrice);

        public void Clear() => _items.Clear();
    }
}
