using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.Domain.Repositories;

namespace StudySharp.DomainServices.Repositories
{
    public sealed class TheoryBlockRepository : ITheoryBlockRepository
    {
        private StudySharpDbContext context;
        
        public Task<OperationResult<TheoryBlock>> CreateTheoryBlockAsync(TheoryBlock theoryBlock)
        {
            context.TheoryBlocks.Add(theoryBlock);
        }

        public Task<OperationResult<TheoryBlock>> GetTheoryBlockByIdAsync(int id)
        {
            return context.TheoryBlocks.Find(id);
        }

        public Task<OperationResult<List<TheoryBlock>>> GetTheoryBlocksByCourseIdAsync(int courseId)
        {
            return context.Courses.Find(courseId);
        }

        public Task<OperationResult<TheoryBlock>> UpdateTheoryBlockAsync(TheoryBlock theoryBlock)
        {
            context.Entry(theoryBlock).State = EntityState.Modified;
        }

        public Task<OperationResult<TheoryBlock>> RemoveTheoryBlockByIdAsync(int id)
        {
            TheoryBlock theoryBlock = context.TheoryBlocks.Find(id);
            if (theoryBlock != null)
                context.TheoryBlocks.Remove(theoryBlock);
        }
    }
}