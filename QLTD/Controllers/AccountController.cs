using Microsoft.AspNetCore.Mvc;
using QLTD.Models;
using QLTD.Models.Repository;
using System.Security.Cryptography;
using System.Text;

namespace QLTD.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly DataContext _context;

        public AccountController(UserRepository userRepository, DataContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        // GET: Login page
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return RedirectToAction("Index", "Company");
            }
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập tên đăng nhập và mật khẩu";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return View();
            }

            if (!user.Status)
            {
                ViewBag.ErrorMessage = "Tài khoản của bạn đã bị vô hiệu hóa";
                return View();
            }

            // Set session
            HttpContext.Session.SetString("UserID", user.UserID.ToString());
            HttpContext.Session.SetString("FullName", user.FullName ?? "");
            HttpContext.Session.SetString("UserName", user.UserName ?? "");
            HttpContext.Session.SetString("Email", user.Email ?? "");
            HttpContext.Session.SetString("CompanyID", user.CompanyID.ToString());
            HttpContext.Session.SetString("PermissionID", user.PermissionID.ToString());

            // Get user's permission/role
            var permission = _context.Permissions.FirstOrDefault(p => p.PermissionID == user.PermissionID);
            string userRole = permission?.Role ?? "";
            HttpContext.Session.SetString("Role", userRole);

            // Redirect based on role
            if (userRole == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Company");
            }
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Helper method to verify password (simple comparison)
        private bool VerifyPassword(string password, string storedPassword)
        {
            // Compare plain text directly since password in DB is stored as plain text
            return password == storedPassword;
        }

        // Helper method to hash password (for future use)
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
