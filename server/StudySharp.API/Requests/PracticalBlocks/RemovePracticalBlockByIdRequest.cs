using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.PracticalBlocks
{
    public class RemovePracticalBlockByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
    }
}