using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfilo.Models;
using System.Data;

namespace Portfilo.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Menu> Menus { get; set; } = default!;
        public DbSet<Header> Headers { get; set; } = default!;
        public DbSet<About> Abouts { get; set; } = default!;
        public DbSet<SocialMedia> SocialMedias { get; set; } = default!;
        public DbSet<Experience> Experiences { get; set; } = default!;
        public DbSet<Education> Educations { get; set; } = default!;
        public DbSet<ProfessionalSkill> ProfessionalSkills { get; set; } = default!;
        public DbSet<Language> Languages { get; set; } = default!;
        public DbSet<Project> Projects { get; set; } = default!;
        public DbSet<CallToAction> CallToActions { get; set; } = default!;
        public DbSet<TransactionContactUs> TransactionsContactUs { get; set; } = default!;
	}
}
