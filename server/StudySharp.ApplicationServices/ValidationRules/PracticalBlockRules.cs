using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.ValidationRules;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.ValidationRules
{
    public class PracticalBlockRules : IPracticalBlockRules
    {
        private readonly StudySharpDbContext _context;

        public PracticalBlockRules(StudySharpDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsPracticalBlockIdExistAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.PracticalBlocks.AnyAsync(_ => _.Id == id, cancellationToken);
        }

        public async Task<bool> IsCourseIdExistAsync(int courseId, CancellationToken cancellationToken)
        {
            return await _context.PracticalBlocks.AnyAsync(_ => _.CourseId == courseId, cancellationToken);
        }
    }
}
