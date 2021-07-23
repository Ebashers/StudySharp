using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.Teacher;
using StudySharp.Shared.Constants;

namespace StudySharp.DomainServices
{
    public class StudySharpDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Teacher> Teachers { get; set; }
        public StudySharpDbContext(DbContextOptions<StudySharpDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = nameof(DomainRoles.Teacher), NormalizedName = nameof(DomainRoles.Teacher).ToUpper() });
            base.OnModelCreating(builder);
        }
    }
}
