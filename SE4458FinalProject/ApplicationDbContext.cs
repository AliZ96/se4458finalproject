using Microsoft.EntityFrameworkCore;
using SE4458FinalProject.Models;

namespace SE4458FinalProject
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Job> Jobs { get; set; }
    }
} 