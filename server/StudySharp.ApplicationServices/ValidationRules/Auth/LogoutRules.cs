using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.ValidationRules;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.ValidationRules.Auth
{
    public interface ILogoutRules : IValidationRule
    {
        Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken);
    }

    public sealed class LogoutRules : ILogoutRules
    {
        private readonly StudySharpDbContext _context;

        public LogoutRules(StudySharpDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(_ => _.UserName.ToLower().Equals(userName.ToLower()), cancellationToken);
        }
    }
}
