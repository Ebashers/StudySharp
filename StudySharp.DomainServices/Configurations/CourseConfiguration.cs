using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models.Learning;

namespace StudySharp.DomainServices.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .Property(_ => _.Name)
                .IsRequired();

            builder
                .Property(_ => _.CreatedAt)
                .IsRequired();

            builder
                .Property(_ => _.RedactedAt)
                .IsRequired();
        }
    }
}