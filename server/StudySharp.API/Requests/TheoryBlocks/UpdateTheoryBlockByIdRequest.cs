using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.TheoryBlocks
{
    public class UpdateTheoryBlockByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}