using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.PracticalBlocks
{
    public class GetPracticalBlockByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
    }
}