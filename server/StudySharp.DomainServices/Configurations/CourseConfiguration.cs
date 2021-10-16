using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models;

namespace StudySharp.DomainServices.Configurations
{
    public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .Property(_ => _.Name)
                .IsRequired();

            builder
                .Property(_ => _.DateModified)
                .IsRequired(false);

            builder
                .Property(_ => _.DateCreated)
                .IsRequired();

            builder
                .HasOne(_ => _.Teacher)
                .WithMany(_ => _.Courses)
                .HasForeignKey(_ => _.TeacherId);

            builder
                .HasMany(_ => _.Tags)
                .WithMany(_ => _.Courses);

            builder
                .HasMany(_ => _.TheoryBlocks)
                .WithOne(_ => _.Course)
                .HasForeignKey(_ => _.CourseId);

            builder
                .HasMany(_ => _.PracticalBlocks)
                .WithOne(_ => _.Course)
                .HasForeignKey(_ => _.CourseId);
        }
    }
}