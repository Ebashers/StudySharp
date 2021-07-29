namespace StudySharp.Domain.Models.Learning
{
    public class TheoryBlock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}