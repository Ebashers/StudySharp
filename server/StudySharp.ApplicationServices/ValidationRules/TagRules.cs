using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.ValidationRules;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.ValidationRules
{
    public class TagRules : ITagRules
    {
        private readonly StudySharpDbContext _context;

        public TagRules(StudySharpDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
        {
            return !await _context.Tags.AnyAsync(_ => _.Name == name, cancellationToken);
        }

        public async Task<bool> IsTagIdExistAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Tags.FindAsync(id, cancellationToken) != null;
        }
    }
}
