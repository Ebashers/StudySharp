using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models.Learning;

namespace StudySharp.DomainServices.Configurations
{
    public class PracticalBlockConfiguration : IEntityTypeConfiguration<PracticalBlock>
    {
        public void Configure(EntityTypeBuilder<PracticalBlock> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .Property(_ => _.Name)
                .IsRequired();

            builder
                .Property(_ => _.Description)
                .IsRequired();
            
            builder
                .HasOne(_ => _.Quiz)
                .WithOne(_ => _.PracticalBlock)
                .HasForeignKey<PracticalBlock>(_ => _.QuizId);
        }
    }
}
