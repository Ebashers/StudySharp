namespace StudySharp.API.Requests.Tags
{
    public class AddTagRequest
    {
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
    }
}