using System.Collections.Generic;
using System.Threading.Tasks;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;

namespace StudySharp.Domain.Repositories
{
    public interface IPracticalBlockRepository
    {
        Task<OperationResult<PracticalBlock>> CreatePracticalBlockAsync(PracticalBlock practicalBlock);
        Task<OperationResult<PracticalBlock>> GetPracticalBlockByIdAsync(int id);
        Task<OperationResult<List<PracticalBlock>>> GetPracticalBlocksByCourseIdAsync(int courseId);
        Task<OperationResult<PracticalBlock>> UpdatePracticalBlockAsync(PracticalBlock practicalBlock);
        Task<OperationResult<PracticalBlock>> RemovePracticalBlockByIdAsync(int id);
    }
}