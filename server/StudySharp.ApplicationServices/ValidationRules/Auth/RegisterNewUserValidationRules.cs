using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.ValidationRules;
using StudySharp.DomainServices;

namespace StudySharp.ApplicationServices.ValidationRules.Auth
{
    public interface IRegisterNewUserValidationRules : IValidationRule
    {
        Task<bool> UserNameIsUnique(string userName, CancellationToken cancellationToken);
    }

    public sealed class RegisterNewUserValidationRules : IRegisterNewUserValidationRules
    {
        private readonly StudySharpDbContext _context;

        public RegisterNewUserValidationRules(StudySharpDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserNameIsUnique(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users.AllAsync(_ => !_.UserName.ToLower().Equals(userName.ToLower()), cancellationToken);
        }
    }
}
