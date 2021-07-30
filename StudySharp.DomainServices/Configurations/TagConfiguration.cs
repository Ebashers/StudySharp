using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models.Learning;

namespace StudySharp.DomainServices.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .Property(_ => _.Name)
                .IsRequired();
        }
    }
}