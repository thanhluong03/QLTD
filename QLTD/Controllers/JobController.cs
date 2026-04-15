using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Action cho ứng viên duyệt tất cả công việc với lọc nâng cao
        /// </summary>
        public IActionResult Browse(string keyword = "", string location = "", string experience = "", 
                                   string position = "", string workingStyle = "", string education = "",
                                   int? salaryMin = null, int? salaryMax = null, int page = 1)
        {
            var query = _context.Jobs.AsQueryable();

            // Filter bằng keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(j => 
                    j.Jobposition.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    j.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                );
            }

            // Filter bằng địa điểm
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(j => j.Address.Contains(location, StringComparison.OrdinalIgnoreCase));
            }

            // Filter bằng kinh nghiệm
            if (!string.IsNullOrEmpty(experience))
            {
                query = query.Where(j => j.Experien.Contains(experience, StringComparison.OrdinalIgnoreCase));
            }

            // Filter bằng vị trí công việc
            if (!string.IsNullOrEmpty(position))
            {
                query = query.Where(j => j.Jobposition.Contains(position, StringComparison.OrdinalIgnoreCase));
            }

            // Filter bằng loại công việc
            if (!string.IsNullOrEmpty(workingStyle))
            {
                query = query.Where(j => j.WorkingStyle.Contains(workingStyle, StringComparison.OrdinalIgnoreCase));
            }

            // Filter bằng yêu cầu học vấn
            if (!string.IsNullOrEmpty(education))
            {
                query = query.Where(j => j.RequimentEducation.Contains(education, StringComparison.OrdinalIgnoreCase));
            }

            // Filter bằng khoảng lương
            if (salaryMin.HasValue || salaryMax.HasValue)
            {
                // Giả sử Cash là string, cần parse để so sánh số
                var jobs = query.ToList();
                jobs = jobs.Where(j =>
                {
                    if (TryParseSalary(j.Cash, out var jobSalary))
                    {
                        if (salaryMin.HasValue && jobSalary < salaryMin.Value)
                            return false;
                        if (salaryMax.HasValue && jobSalary > salaryMax.Value)
                            return false;
                        return true;
                    }
                    return true;
                }).ToList();
                query = jobs.AsQueryable();
            }

            var totalJobs = query.Count();
            var pageSize = 12;
            var totalPages = (int)Math.Ceiling(totalJobs / (double)pageSize);

            if (page < 1) page = 1;
            if (page > totalPages && totalPages > 0) page = totalPages;

            var jobs_list = query
                .OrderByDescending(j => j.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(j => j.Company)
                .ToList();

            var model = new JobSearchViewModel
            {
                Keyword = keyword,
                Location = location,
                Experience = experience,
                Position = position,
                WorkingStyle = workingStyle,
                Education = education,
                SalaryMin = salaryMin,
                SalaryMax = salaryMax,
                PageNumber = page,
                PageSize = pageSize,
                TotalJobs = totalJobs,
                Jobs = jobs_list
            };

            return View(model);
        }

        private bool TryParseSalary(string salaryStr, out int salary)
        {
            salary = 0;
            if (string.IsNullOrEmpty(salaryStr))
                return false;

            // Xóa các ký tự không phải số
            var numericStr = new string(salaryStr.Where(char.IsDigit).ToArray());
            return int.TryParse(numericStr, out salary);
        }

        /// <summary>
        /// Xem chi tiết công việc cho ứng viên
        /// </summary>
        public IActionResult BrowseDetail(int id)
        {
            var job = _context.Jobs
                .Include(j => j.Company)
                .Include(j => j.Applications)
                .FirstOrDefault(j => j.JobID == id);

            if (job == null)
                return NotFound();

            return View(job);
        }

        /// <summary>
        /// Ứng dụng cho một công việc
        /// </summary>
        [HttpPost]
        public IActionResult ApplyJob(int jobId)
        {
            var userId = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            // TODO: Implement application logic
            TempData["Success"] = "Bạn đã ứng tuyển thành công!";
            return RedirectToAction("BrowseDetail", new { id = jobId });
        }
    }
}

