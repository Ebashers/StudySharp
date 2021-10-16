using System;
using System.Collections.Generic;
using StudySharp.Domain.Contracts;

namespace StudySharp.Domain.Models
{
    public sealed class Organization : IWithDateCreated
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public OrganizationStatus Status { get; set; }
        public DateTimeOffset DateCreated { get; private set; }
        void IWithDateCreated.SetDateCreated(DateTimeOffset value) => DateCreated = value;
        public List<DomainUser> Members { get; set; }
    }
}
