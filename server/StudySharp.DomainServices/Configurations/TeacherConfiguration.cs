using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models;

namespace StudySharp.DomainServices.Configurations
{
    public sealed class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .HasMany(_ => _.Courses)
                .WithOne(_ => _.Teacher)
                .HasForeignKey(_ => _.TeacherId);
        }
    }
}
