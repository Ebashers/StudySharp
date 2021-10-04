using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.TheoryBlocks
{
    public class UpdateTheoryBlockByIdRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}