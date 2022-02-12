using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models;

namespace StudySharp.DomainServices.Configurations
{
    public sealed class OrganizationRepresentativeConfiguration : IEntityTypeConfiguration<OrganizationRepresentative>
    {
        public void Configure(EntityTypeBuilder<OrganizationRepresentative> builder)
        {
            throw new NotImplementedException();
        }
    }
}
