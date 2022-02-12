using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.Contracts;
using StudySharp.Domain.Models;

namespace StudySharp.DomainServices
{
    public class StudySharpDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<DomainUser> DomainUsers { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationRepresentative> OrganizationRepresentatives { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<PracticalBlock> PracticalBlocks { get; set; }
        public DbSet<TheoryBlock> TheoryBlocks { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Tag> Tags { get; set; }
#pragma warning disable 8618
        public StudySharpDbContext(DbContextOptions<StudySharpDbContext> options)
            : base(options)
#pragma warning restore 8618
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(StudySharpDbContext).Assembly);
        }

        public override int SaveChanges()
        {
            UpdateDates();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateDates();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateDates();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateDates();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateDates()
        {
            var now = DateTime.UtcNow;

            var entries = ChangeTracker
                .Entries()
                .Where(x => x.Entity is IWithDateCreated);

            foreach (var entry in entries)
            {
                var entity = (IWithDateCreated)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.SetDateCreated(now);
                }
                else
                {
                    entry.Property(nameof(IWithDateCreated.DateCreated)).IsModified = false;
                }
            }

            var modifiedEntries = ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Modified)
                .Select(x => x.Entity)
                .OfType<IWithDateModified>();

            foreach (var entity in modifiedEntries)
            {
                entity.SetDateModified(now);
            }
        }
    }
}
