using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models.Learning;
using StudySharp.Domain.Models.Users;

namespace StudySharp.DomainServices.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .Property(_ => _.FirstName)
                .IsRequired();

            builder
                .Property(_ => _.LastName)
                .IsRequired();
        }
    }
}
