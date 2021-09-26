using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.TheoryBlocks
{
    public class RemoveTheoryBlockByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
    }
}