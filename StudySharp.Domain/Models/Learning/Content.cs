using System.Collections.Generic;

namespace StudySharp.Domain.Models.Learning
{
    public class Content
    {
        public int Id { get; set; }
        
        public int CourseId { get; set; }
        public Course Course { get; set; }
        
        public List<TheoryBlock> TheoryBlocks { get; set; }

        public List<PracticalBlock> PracticalBlocks { get; set; }
    }
}