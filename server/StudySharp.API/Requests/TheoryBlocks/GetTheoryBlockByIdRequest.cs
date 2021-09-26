using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.TheoryBlocks
{
    public class GetTheoryBlockByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
    }
}