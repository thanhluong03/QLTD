using Microsoft.AspNetCore.Mvc;
using QLTD.Models;
using QLTD.Models.Repository;

namespace QLTD.Controllers
{
    public class CompanyController : Controller
    {
        private readonly DataContext _context;

        public CompanyController(DataContext context)
        {
            _context = context;
        }

        private bool CheckLogin()
        {
            var userId = HttpContext.Session.GetString("UserID");
            return !string.IsNullOrEmpty(userId);
        }

        public IActionResult Index()
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            int companyId = int.Parse(HttpContext.Session.GetString("CompanyID") ?? "0");
            var company = _context.Companys.FirstOrDefault(c => c.CompanyID == companyId);
            return View(company);
        }

        // Show edit form
        public IActionResult Edit(int id)
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            var company = _context.Companys.FirstOrDefault(c => c.CompanyID == id);
            if (company == null) return NotFound();
            return View(company);
        }

        // Handle edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CompanyModel company)
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    _context.SaveChanges();
                    TempData["Success"] = "Cập nhật thông tin công ty thành công";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật: " + ex.Message);
                }
            }
            return View(company);
        }

        // Handle delete company - Accept both GET and POST
        [HttpGet]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var company = _context.Companys.FirstOrDefault(c => c.CompanyID == id);
            if (company != null)
            {
                _context.Companys.Remove(company);
                _context.SaveChanges();
            }
            return RedirectToAction("Companies", "Admin");
        }
    }
}
