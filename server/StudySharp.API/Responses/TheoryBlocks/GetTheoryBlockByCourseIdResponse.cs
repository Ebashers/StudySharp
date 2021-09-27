using StudySharp.Domain.Models;

namespace StudySharp.API.Responses.TheoryBlocks
{
    public class GetTheoryBlockByCourseIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}