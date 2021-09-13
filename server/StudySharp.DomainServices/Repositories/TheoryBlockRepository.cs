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
        private readonly StudySharpDbContext _context;

        public TheoryBlockRepository(StudySharpDbContext context)
        {
            _context = context;
        }
        
        public async Task<OperationResult<TheoryBlock>> CreateTheoryBlockAsync(TheoryBlock theoryBlock)
        {
            await _context.TheoryBlocks.AddAsync(theoryBlock);
            await _context.SaveChangesAsync();
            return new OperationResult<TheoryBlock> { Result = theoryBlock, IsSucceeded = true };
        }

        public async Task<OperationResult<TheoryBlock>> GetTheoryBlockByIdAsync(int id)
        {
            var theoryBlock = await _context.TheoryBlocks.FindAsync(id);
            if (theoryBlock == null)
            {
                var result = new OperationResult<TheoryBlock> { Result = null, IsSucceeded = false };
                result.Errors.Add("Could not find TheoryBlock`s Id");
                return result;
            }
            return new OperationResult<TheoryBlock> { Result = theoryBlock, IsSucceeded = true };
        }

        public async Task<OperationResult<List<TheoryBlock>>> GetTheoryBlocksByCourseIdAsync(int courseId)
        {
            var theoryBlocks = await _context.TheoryBlocks.Where(_ => _.CourseId == courseId).ToListAsync();
            return new OperationResult<List<TheoryBlock>> { Result = theoryBlocks, IsSucceeded = true };
        }

        public async Task<OperationResult<TheoryBlock>> UpdateTheoryBlockAsync(TheoryBlock theoryBlock)
        {
            if (theoryBlock == null)
            {
                var result = new OperationResult<TheoryBlock> { Result = null, IsSucceeded = false };
                result.Errors.Add("There`s no TheoryBlock you can modify");
                return result;
            }
            _context.Entry(theoryBlock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new OperationResult<TheoryBlock> { Result = theoryBlock, IsSucceeded = true };
        }

        public async Task<OperationResult<TheoryBlock>> RemoveTheoryBlockByIdAsync(int id)
        {
            var theoryBlock = await _context.TheoryBlocks.FindAsync(id);
            if (theoryBlock == null)
            {
                var result = new OperationResult<TheoryBlock> { Result = null, IsSucceeded = false };
                result.Errors.Add("Could not find TheoryBlock`s Id");
                return result;
            }
            _context.TheoryBlocks.Remove(theoryBlock);
            await _context.SaveChangesAsync();
            return new OperationResult<TheoryBlock> { Result = theoryBlock, IsSucceeded = true };
        }
    }
}