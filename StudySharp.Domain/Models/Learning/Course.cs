using System;
using System.Collections.Generic;
using StudySharp.Domain.Contracts;
using StudySharp.Domain.Models.Users;

namespace StudySharp.Domain.Models.Learning
{
    public class Course : IWithDateCreated, IWithDateModified
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Tag> Tags { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<TheoryBlock> TheoryBlocks { get; set; }
        public List<PracticalBlock> PracticalBlocks { get; set; }
        
        public DateTimeOffset DateCreated { get; private set; }
        public DateTimeOffset? DateModified { get; private set; }
        void IWithDateCreated.SetDateCreated(DateTimeOffset value) => DateCreated = value;
        void IWithDateModified.SetDateModified(DateTimeOffset value) => DateModified = value;
    }
}