using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models.Learning;
using StudySharp.Domain.Models.Users;

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
                .Property(_ => _.DateModified)
                .IsRequired(false);

            builder
                .Property(_ => _.DateCreated)
                .IsRequired();
            
            builder
                .HasOne(_ => _.Content)
                .WithOne(_ => _.Course)
                .HasForeignKey<Content>(_ => _.CourseId);

            builder
                .HasOne(_ => _.Teacher)
                .WithMany(_ => _.Courses);
        }
    }
}