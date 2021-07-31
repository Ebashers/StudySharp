namespace StudySharp.Domain.Models.Learning
{
    public class PracticalBlock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        //connection part
        public int ContentId { get; set; }
        public Content Content { get; set; }
        
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}