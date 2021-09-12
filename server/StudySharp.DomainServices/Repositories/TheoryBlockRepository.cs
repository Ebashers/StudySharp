using System;
using System.Collections.Generic;
using System.Linq;
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

        public TheoryBlockRepository(StudySharpDbContext _context)
        {
            context = _context;
        }
        
        public async Task<OperationResult<TheoryBlock>> CreateTheoryBlockAsync(TheoryBlock theoryBlock)
        {
            await context.TheoryBlocks.AddAsync(theoryBlock);
            return new OperationResult<TheoryBlock>() { Result = theoryBlock, IsSucceeded = true };
        }

        public async Task<OperationResult<TheoryBlock>> GetTheoryBlockByIdAsync(int id)
        {
            var theoryBlock = await context.TheoryBlocks.FindAsync(id);
            if (theoryBlock == null)
            {
                var result = new OperationResult<TheoryBlock>() { Result = null, IsSucceeded = false };
                result.Errors.Add("Could not find TheoryBlock`s Id");
                return result;
            }
            return new OperationResult<TheoryBlock>(){ Result = theoryBlock, IsSucceeded = true };
        }

        public async Task<OperationResult<List<TheoryBlock>>> GetTheoryBlocksByCourseIdAsync(int courseId)
        {
            await context.Courses.FindAsync(courseId);
            var theoryBlocks = await context.TheoryBlocks.Where(_ => _.CourseId == courseId).ToListAsync();
            if (theoryBlocks == null)
            {
                var result = new OperationResult<List<TheoryBlock>>() { Result = null, IsSucceeded = false };
                result.Errors.Add("There`re no matches between TheoryBlock.CourseId and courseId");
                return result;
            }
            return new OperationResult<List<TheoryBlock>>(){ Result = theoryBlocks, IsSucceeded = true };
        }

        public async Task<OperationResult<TheoryBlock>> UpdateTheoryBlockAsync(TheoryBlock theoryBlock)
        {
            context.Entry(theoryBlock).State = EntityState.Modified;
            return new OperationResult<TheoryBlock>() { Result = theoryBlock, IsSucceeded = true };
        }

        public async Task<OperationResult<TheoryBlock>> RemoveTheoryBlockByIdAsync(int id)
        {
            var theoryBlock = await context.TheoryBlocks.FindAsync(id);
            if (theoryBlock == null)
            {
                var result = new OperationResult<TheoryBlock>() { Result = null, IsSucceeded = false };
                result.Errors.Add("Could not find TheoryBlock`s Id");
                return result;
            }
            context.TheoryBlocks.Remove(theoryBlock);
            return new OperationResult<TheoryBlock>(){ Result = theoryBlock, IsSucceeded = true };
        }
    }
}