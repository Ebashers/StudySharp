namespace StudySharp.Domain.Models
{
    public sealed class TheoryBlock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
