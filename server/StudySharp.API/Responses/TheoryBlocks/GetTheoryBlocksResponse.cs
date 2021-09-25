using System.Collections.Generic;
using StudySharp.Domain.Models;

namespace StudySharp.API.Responses.TheoryBlocks
{
    public class GetTheoryBlocksResponse
    {
        public List<TheoryBlock> TheoryBlocks { get; set; }
    }
}