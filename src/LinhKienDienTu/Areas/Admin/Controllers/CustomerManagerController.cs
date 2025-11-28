using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinhKienDienTu.Data;

namespace LinhKienDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CustomerManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerManagerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/CustomerManager
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            
            var customerData = users.Select(u => new
            {
                User = u,
                OrderCount = _context.Orders.Count(o => o.UserId == u.Id),
                TotalSpent = _context.Orders.Where(o => o.UserId == u.Id).Sum(o => (decimal?)o.TotalAmount) ?? 0
            }).ToList();

            return View(customerData);
        }

        // GET: Admin/CustomerManager/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Where(o => o.UserId == id)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            ViewBag.Orders = orders;
            ViewBag.TotalSpent = orders.Sum(o => o.TotalAmount);
            ViewBag.OrderCount = orders.Count;

            return View(user);
        }
    }
}
