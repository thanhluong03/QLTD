using Microsoft.AspNetCore.Mvc;
using QLTD.Models;
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

        public IActionResult Index(string search = "")
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            int companyId = int.Parse(HttpContext.Session.GetString("CompanyID") ?? "0");
            var jobs = _context.Jobs.Where(j => j.CompanyID == companyId).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                jobs = jobs.Where(j =>
                    j.Jobposition.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    j.Address.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    j.Description.Contains(search, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            ViewData["SearchQuery"] = search;
            return View(jobs);
        }

        public IActionResult Create()
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobModel job)
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            // Gán CompanyID từ session
            int companyId = int.Parse(HttpContext.Session.GetString("CompanyID") ?? "0");
            job.CompanyID = companyId;

            // Xóa validation lỗi của navigation property Company
            ModelState.Remove("Company");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Jobs.Add(job);
                    _context.SaveChanges();
                    TempData["Success"] = "Thêm việc làm thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi lưu dữ liệu: " + ex.Message);
                }
            }

            // Hiển thị các lỗi validation
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine("Validation Error: " + error.ErrorMessage);
            }

            return View(job);
        }

        public IActionResult Edit(int id)
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            int companyId = int.Parse(HttpContext.Session.GetString("CompanyID") ?? "0");
            var job = _context.Jobs.FirstOrDefault(j => j.JobID == id && j.CompanyID == companyId);

            if (job == null)
                return NotFound();

            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, JobModel job)
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            if (id != job.JobID)
                return NotFound();

            int companyId = int.Parse(HttpContext.Session.GetString("CompanyID") ?? "0");

            // Gán CompanyID từ session
            job.CompanyID = companyId;

            // Xóa validation lỗi của navigation property Company
            ModelState.Remove("Company");

            var existingJob = _context.Jobs.FirstOrDefault(j => j.JobID == id && j.CompanyID == companyId);

            if (existingJob == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    existingJob.Jobposition = job.Jobposition;
                    existingJob.Address = job.Address;
                    existingJob.Cash = job.Cash;
                    existingJob.Experien = job.Experien;
                    existingJob.Description = job.Description;
                    existingJob.StartTime = job.StartTime;
                    existingJob.EntimeTime = job.EntimeTime;
                    existingJob.RequimentEducation = job.RequimentEducation;
                    existingJob.Quantity = job.Quantity;
                    existingJob.WorkingStyle = job.WorkingStyle;

                    _context.SaveChanges();
                    TempData["Success"] = "Cập nhật việc làm thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi lưu dữ liệu: " + ex.Message);
                }
            }

            return View(job);
        }

        public IActionResult Detail(int id)
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            int companyId = int.Parse(HttpContext.Session.GetString("CompanyID") ?? "0");
            var job = _context.Jobs.FirstOrDefault(j => j.JobID == id && j.CompanyID == companyId);

            if (job == null)
                return NotFound();

            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(int id, JobModel job)
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            if (id != job.JobID)
                return NotFound();

            int companyId = int.Parse(HttpContext.Session.GetString("CompanyID") ?? "0");

            // Gán CompanyID từ session
            job.CompanyID = companyId;

            // Xóa validation lỗi của navigation property Company
            ModelState.Remove("Company");

            var existingJob = _context.Jobs.FirstOrDefault(j => j.JobID == id && j.CompanyID == companyId);

            if (existingJob == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    existingJob.Jobposition = job.Jobposition;
                    existingJob.Address = job.Address;
                    existingJob.Cash = job.Cash;
                    existingJob.Experien = job.Experien;
                    existingJob.Description = job.Description;
                    existingJob.StartTime = job.StartTime;
                    existingJob.EntimeTime = job.EntimeTime;
                    existingJob.RequimentEducation = job.RequimentEducation;
                    existingJob.Quantity = job.Quantity;
                    existingJob.WorkingStyle = job.WorkingStyle;

                    _context.SaveChanges();
                    TempData["Success"] = "Cập nhật việc làm thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi lưu dữ liệu: " + ex.Message);
                }
            }

            return View(existingJob);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!CheckLogin())
                return RedirectToAction("Login", "Account");

            int companyId = int.Parse(HttpContext.Session.GetString("CompanyID") ?? "0");
            var job = _context.Jobs.FirstOrDefault(j => j.JobID == id && j.CompanyID == companyId);

            if (job == null)
                return NotFound();

            _context.Jobs.Remove(job);
            _context.SaveChanges();

            TempData["Success"] = "Xóa việc làm thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
