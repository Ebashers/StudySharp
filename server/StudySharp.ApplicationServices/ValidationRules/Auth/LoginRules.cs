using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.ValidationRules;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.ValidationRules.Auth
{
    public interface ILoginRules : IValidationRule
    {
        Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken);
    }

    public sealed class LoginRules : ILoginRules
    {
        private readonly StudySharpDbContext _context;

        public LoginRules(StudySharpDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(_ => _.UserName.ToLower().Equals(userName.ToLower()), cancellationToken);
        }
    }
}
