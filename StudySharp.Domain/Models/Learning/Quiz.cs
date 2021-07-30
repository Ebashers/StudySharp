namespace StudySharp.Domain.Models.Learning
{
    public class Quiz
    {
        public int Id { get; set; }
        public int PracticalBlockId { get; set; }
        public PracticalBlock PracticalBlock { get; set; }
    }
}