using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LinhKienDienTu.Data;
using LinhKienDienTu.Models;
using LinhKienDienTu.ViewModels;
using LinhKienDienTu.Extensions;
using Newtonsoft.Json;

namespace LinhKienDienTu.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private const string CartSessionKey = "ShoppingCart";

        public CheckoutController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var cart = GetCart();
            if (!cart.Items.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var user = await _userManager.GetUserAsync(User);
            var model = new CheckoutViewModel
            {
                Email = user?.Email,
                CustomerName = user?.UserName // Default to username, can be improved if User has FullName property
            };

            ViewBag.Cart = cart;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel model)
        {
            var cart = GetCart();
            if (!cart.Items.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                
                var order = new Order
                {
                    UserId = user.Id,
                    OrderDate = DateTime.Now,
                    CustomerName = model.CustomerName,
                    PhoneNumber = model.PhoneNumber,
                    ShippingAddress = model.ShippingAddress,
                    Email = model.Email,
                    Notes = model.Notes,
                    PaymentMethod = model.PaymentMethod,
                    Status = OrderStatus.Pending,
                    IsPaid = false,
                    TotalAmount = cart.ComputeTotalValue()
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                foreach (var item in cart.Items)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.Product.BulkPrice.HasValue && item.Quantity > 100 
                            ? item.Product.BulkPrice.Value 
                            : item.Product.Price
                    };
                    _context.OrderItems.Add(orderItem);
                }

                await _context.SaveChangesAsync();

                // Clear cart
                HttpContext.Session.Remove(CartSessionKey);

                return RedirectToAction(nameof(Success), new { orderId = order.Id });
            }

            ViewBag.Cart = cart;
            return View(model);
        }

        public IActionResult Success(int orderId)
        {
            return View(orderId);
        }

        private Cart GetCart()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new Cart();
            }

            return JsonConvert.DeserializeObject<Cart>(cartJson) ?? new Cart();
        }
    }
}
