using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.Domain.Repositories;

namespace StudySharp.DomainServices.Repositories
{
    public class PracticalBlockRepository : IPracticalBlockRepository
    {
        private readonly StudySharpDbContext _context;

        public PracticalBlockRepository(StudySharpDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PracticalBlock>> CreatePracticalBlockAsync(PracticalBlock practicalBlock)
        {
            await _context.PracticalBlocks.AddAsync(practicalBlock);
            await _context.SaveChangesAsync();
            return new OperationResult<PracticalBlock> { Result = practicalBlock, IsSucceeded = true };
        }

        public async Task<OperationResult<PracticalBlock>> UpdatePracticalBlockAsync(PracticalBlock practicalBlock)
        {
            if (practicalBlock == null)
            {
                return BuildErrorResponse("Could not find PracticalBlock`s Id");
            }

            return new OperationResult<PracticalBlock> { Result = practicalBlock, IsSucceeded = true };
        }

        public async Task<OperationResult<PracticalBlock>> GetPracticalBlockByIdAsync(int id)
        {
            var practicalBlock = await _context.PracticalBlocks.FindAsync(id);
            if (practicalBlock == null)
            {
                return BuildErrorResponse("Could not find PracticalBlock`s Id");
            }

            return new OperationResult<PracticalBlock> { Result = practicalBlock, IsSucceeded = true };
        }

        public async Task<OperationResult<PracticalBlock>> RemovePracticalBlockByIdAsync(int id)
        {
            var practicalBlock = await _context.PracticalBlocks.FindAsync(id);
            if (practicalBlock == null)
            {
                return BuildErrorResponse("Could not find PracticalBlock`s Id");
            }

            _context.PracticalBlocks.Remove(practicalBlock);
            await _context.SaveChangesAsync();
            return new OperationResult<PracticalBlock> { Result = practicalBlock, IsSucceeded = true };
        }

        public async Task<OperationResult<List<PracticalBlock>>> GetPracticalBlocksByCourseIdAsync(int courseId)
        {
            var practicalBlock = await _context.PracticalBlocks.Where(_ => _.CourseId == courseId).ToListAsync();
            return new OperationResult<List<PracticalBlock>> { Result = practicalBlock, IsSucceeded = true };
        }

        private OperationResult<PracticalBlock> BuildErrorResponse(string errorText)
        {
            var result = new OperationResult<PracticalBlock> { Result = null, IsSucceeded = false };
            result.Errors.Add(errorText);
            return result;
        }
    }
}