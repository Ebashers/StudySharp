using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.TheoryBlocks
{
    public class AddTheoryBlockRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}