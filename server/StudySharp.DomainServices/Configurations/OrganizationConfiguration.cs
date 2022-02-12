using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudySharp.Domain.Models;

namespace StudySharp.DomainServices.Configurations
{
    public sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            throw new NotImplementedException();
        }
    }
}
