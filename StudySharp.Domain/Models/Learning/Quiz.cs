namespace StudySharp.Domain.Models.Learning
{
    public class Quiz
    {
        public int Id { get; set; }
        
        //connection part
        public int PracticalBlockId { get; set; }
        public PracticalBlock PracticalBlock { get; set; }
    }
}