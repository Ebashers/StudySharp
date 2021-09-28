using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.TheoryBlocks
{
    public class UpdateTheoryBlockByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
    }
}