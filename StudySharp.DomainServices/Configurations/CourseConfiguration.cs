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
                .Property(_ => _.DateModified)
                .IsRequired(false);

            builder
                .Property(_ => _.DateCreated)
                .IsRequired();

            //TODO: update this functional in scope STD-13
            builder
                .Ignore(_ => _.Content);
        }
    }
}
