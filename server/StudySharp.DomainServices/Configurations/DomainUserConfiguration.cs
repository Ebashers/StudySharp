using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models;

namespace StudySharp.DomainServices.Configurations
{
    public sealed class DomainUserConfiguration : IEntityTypeConfiguration<DomainUser>
    {
        public void Configure(EntityTypeBuilder<DomainUser> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .Property(_ => _.FirstName)
                .IsRequired();

            builder
                .Property(_ => _.LastName)
                .IsRequired();

            builder
                .HasOne(_ => _.Teacher)
                .WithOne()
                .HasForeignKey<DomainUser>(_ => _.TeacherId);
        }
    }
}
