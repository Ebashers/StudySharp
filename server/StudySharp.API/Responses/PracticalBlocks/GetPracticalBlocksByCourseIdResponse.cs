using System.Collections.Generic;
using StudySharp.Domain.Models;

namespace StudySharp.API.Responses.PracticalBlocks
{
    public class GetPracticalBlocksByCourseIdResponse
    {
        public List<PracticalBlock> PracticalBlocks { get; set; }
    }
}