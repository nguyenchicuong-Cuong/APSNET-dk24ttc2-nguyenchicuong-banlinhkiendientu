using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinhKienDienTu.Data;
using LinhKienDienTu.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace LinhKienDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            
            // New orders (Pending status)
            var newOrders = await _context.Orders
                .Where(o => o.Status == OrderStatus.Pending)
                .CountAsync();
            
            // Low stock products (< 10)
            var lowStockProducts = await _context.Products
                .Where(p => p.StockQuantity < 10)
                .CountAsync();
            
            // New customers (registered today)
            var newCustomers = await _context.Users
                .Where(u => u.LockoutEnd == null) // Simple filter, can be improved
                .CountAsync();
            
            // Today's revenue
            var todayRevenue = await _context.Orders
                .Where(o => o.OrderDate >= today)
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            ViewBag.NewOrders = newOrders;
            ViewBag.LowStockProducts = lowStockProducts;
            ViewBag.NewCustomers = newCustomers;
            ViewBag.TodayRevenue = todayRevenue;

            // Recent activities
            var recentOrders = await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToListAsync();

            ViewBag.RecentOrders = recentOrders;

            return View();
        }
    }
}
