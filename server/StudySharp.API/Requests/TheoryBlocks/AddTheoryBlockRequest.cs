namespace StudySharp.API.Requests.TheoryBlocks
{
    public class AddTheoryBlockRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
    }
}