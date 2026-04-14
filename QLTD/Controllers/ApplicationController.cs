using Microsoft.AspNetCore.Mvc;
using QLTD.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace QLTD.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly DataContext _context;

        public ApplicationController(DataContext context)
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
            var applications = _context.Applications
                .Where(a => a.Job.CompanyID == companyId)
                .Include(a => a.Job)
                .Include(a => a.Candidate)
                .ToList();
            return View(applications);
        }
    }
}
