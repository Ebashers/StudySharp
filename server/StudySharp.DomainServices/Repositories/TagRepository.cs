using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.Domain.Repositories;

namespace StudySharp.DomainServices.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly StudySharpDbContext _context;

        public TagRepository(StudySharpDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<Tag>> CreateTagAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return new OperationResult<Tag> { Result = tag, IsSucceeded = true };
        }

        public async Task<OperationResult<Tag>> GetTagByIdAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                var result = new OperationResult<Tag> { Result = null, IsSucceeded = false };
                result.Errors.Add($"Could not find the tag with id = {id}");
                return result;
            }

            return new OperationResult<Tag> { Result = tag, IsSucceeded = true };
        }

        public async Task<OperationResult<List<Tag>>> GetTagsAsync()
        {
            var tags = await _context.Tags.ToListAsync();
            return new OperationResult<List<Tag>> { Result = tags, IsSucceeded = true };
        }

        public async Task<OperationResult<Tag>> RemoveTagByIdAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                var result = new OperationResult<Tag> { Result = null, IsSucceeded = false };
                result.Errors.Add($"Could not find the tag with id = {id}");
                return result;
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return new OperationResult<Tag> { Result = tag, IsSucceeded = true };
        }
    }
}