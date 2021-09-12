using System.Collections.Generic;
using System.Threading.Tasks;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;

namespace StudySharp.Domain.Repositories
{
    public interface ITheoryBlockRepository : IRepository
    {
        Task<OperationResult<TheoryBlock>> CreateTheoryBlockAsync(TheoryBlock theoryBlock);
        Task<OperationResult<TheoryBlock>> GetTheoryBlockByIdAsync(int id);
        Task<OperationResult<List<TheoryBlock>>> GetTheoryBlocksByCourseIdAsync(int courseId);
        Task<OperationResult<TheoryBlock>> UpdateTheoryBlockAsync(TheoryBlock theoryBlock);
        Task<OperationResult<TheoryBlock>> RemoveTheoryBlockByIdAsync(int id);
    }
}