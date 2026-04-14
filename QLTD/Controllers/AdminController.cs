using Microsoft.AspNetCore.Mvc;
using QLTD.Models;
using QLTD.Models.Repository;

namespace QLTD.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly DataContext _context;

        public AdminController(UserRepository userRepository, DataContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("UserID") != null;
        }

        private bool IsAdmin()
        {
            string role = HttpContext.Session.GetString("Role") ?? "";
            return role == "Admin";
        }

        // Dashboard for Admin
        public IActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsAdmin()) return RedirectToAction("Index", "Company");
            return View();
        }

        // Danh sách công ty
        public IActionResult Companies()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsAdmin()) return RedirectToAction("Index", "Company");

            // Get only active companies
            var companies = _context.Companys.Where(c => c.Status == true).ToList();
            return View(companies);
        }

        // Danh sách người dùng
        public IActionResult Users()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsAdmin()) return RedirectToAction("Index", "Company");

            // Get only HR users (PermissionID = 1)
            var hrUsers = _context.Users.Where(u => u.PermissionID == 1).ToList();
            return View(hrUsers);
        }
    }
}
