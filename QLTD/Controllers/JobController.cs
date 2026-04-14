using Microsoft.AspNetCore.Mvc;
using QLTD.Models.Repository;

namespace QLTD.Controllers
{
    public class JobController : Controller
    {
        private readonly DataContext _context;

        public JobController(DataContext context)
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
            var jobs = _context.Jobs.Where(j => j.CompanyID == companyId).ToList();
            return View(jobs);
        }
    }
}
