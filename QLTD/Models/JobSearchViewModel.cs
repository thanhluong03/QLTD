using System.ComponentModel.DataAnnotations;

namespace QLTD.Models
{
    public class JobSearchViewModel
    {
        // Filters
        [Display(Name = "Từ khóa")]
        public string Keyword { get; set; } = "";

        [Display(Name = "Địa điểm")]
        public string Location { get; set; } = "";

        [Display(Name = "Kinh nghiệm")]
        public string Experience { get; set; } = "";

        [Display(Name = "Vị trí công việc")]
        public string Position { get; set; } = "";

        [Display(Name = "Mức lương (từ)")]
        public int? SalaryMin { get; set; }

        [Display(Name = "Mức lương (đến)")]
        public int? SalaryMax { get; set; }

        [Display(Name = "Loại công việc")]
        public string WorkingStyle { get; set; } = "";

        [Display(Name = "Yêu cầu học vấn")]
        public string Education { get; set; } = "";

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalJobs { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalJobs / (double)PageSize);

        // Results
        public List<JobModel> Jobs { get; set; } = new List<JobModel>();
    }
}
