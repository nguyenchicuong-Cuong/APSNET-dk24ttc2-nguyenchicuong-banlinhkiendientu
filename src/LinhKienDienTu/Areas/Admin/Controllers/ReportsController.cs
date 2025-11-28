using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinhKienDienTu.Data;
using LinhKienDienTu.Models;

namespace LinhKienDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var thisMonth = new DateTime(today.Year, today.Month, 1);
            var thisYear = new DateTime(today.Year, 1, 1);

            // Today's stats
            var todayOrders = await _context.Orders
                .Where(o => o.OrderDate >= today)
                .ToListAsync();
            
            ViewBag.TodayRevenue = todayOrders.Sum(o => o.TotalAmount);
            ViewBag.TodayOrders = todayOrders.Count;

            // This month stats
            var monthOrders = await _context.Orders
                .Where(o => o.OrderDate >= thisMonth)
                .ToListAsync();
            
            ViewBag.MonthRevenue = monthOrders.Sum(o => o.TotalAmount);
            ViewBag.MonthOrders = monthOrders.Count;

            // This year stats
            var yearOrders = await _context.Orders
                .Where(o => o.OrderDate >= thisYear)
                .ToListAsync();
            
            ViewBag.YearRevenue = yearOrders.Sum(o => o.TotalAmount);
            ViewBag.YearOrders = yearOrders.Count;

            // Total stats
            ViewBag.TotalRevenue = await _context.Orders.SumAsync(o => o.TotalAmount);
            ViewBag.TotalOrders = await _context.Orders.CountAsync();
            ViewBag.TotalCustomers = await _context.Users.CountAsync();
            ViewBag.TotalProducts = await _context.Products.CountAsync();

            // Top selling products
            var topProducts = await _context.OrderItems
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(oi => oi.Quantity),
                    TotalRevenue = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(10)
                .ToListAsync();

            var topProductsWithDetails = topProducts.Select(tp => new
            {
                Product = _context.Products.Find(tp.ProductId),
                TotalQuantity = tp.TotalQuantity,
                TotalRevenue = tp.TotalRevenue
            }).ToList();

            ViewBag.TopProducts = topProductsWithDetails;

            // Low stock products
            var lowStockProducts = await _context.Products
                .Where(p => p.StockQuantity < 10)
                .OrderBy(p => p.StockQuantity)
                .Take(10)
                .ToListAsync();

            ViewBag.LowStockProducts = lowStockProducts;

            // Recent orders
            var recentOrders = await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Take(10)
                .ToListAsync();

            ViewBag.RecentOrders = recentOrders;

            // Order status distribution
            var ordersByStatus = await _context.Orders
                .GroupBy(o => o.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            ViewBag.OrdersByStatus = ordersByStatus;

            return View();
        }
    }
}
