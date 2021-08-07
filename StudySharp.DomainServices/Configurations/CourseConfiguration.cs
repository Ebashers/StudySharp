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
            
            // connections
            builder
                .HasOne(_ => _.Teacher)
                .WithMany(_ => _.Courses);

            builder
                .HasMany(_ => _.Tags)
                .WithMany(_ => _.Courses);

            builder
                .HasMany(_ => _.TheoryBlocks)
                .WithOne(_ => _.Course);

            builder
                .HasMany(_ => _.PracticalBlocks)
                .WithOne(_ => _.Course);
        }
    }
}