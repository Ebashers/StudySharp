using StudySharp.Domain.Models;

namespace StudySharp.API.Responses.PracticalBlocks
{
    public class GetPracticalBlockByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}