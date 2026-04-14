using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace QLTD.Models.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<PermissionModel> Permissions { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ApplicationModel> Applications { get; set; }
        public DbSet<CompanyModel> Companys { get; set; }
        public DbSet<CompanyModel> Companies { get; set; } // Alias for proper naming
        public DbSet<JobModel> Jobs { get; set; }
        public DbSet<CandidateModel> Candidates { get; set; }
    }
}
