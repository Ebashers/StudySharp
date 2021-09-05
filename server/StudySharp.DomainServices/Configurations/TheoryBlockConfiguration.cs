using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models.Learning;

namespace StudySharp.DomainServices.Configurations
{
    public class TheoryBlockConfiguration : IEntityTypeConfiguration<TheoryBlock>
    {
        public void Configure(EntityTypeBuilder<TheoryBlock> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .Property(_ => _.Name)
                .IsRequired();

            builder
                .Property(_ => _.Description)
                .IsRequired();
        }
    }
}