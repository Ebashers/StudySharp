using System.Threading;
using System.Threading.Tasks;

namespace StudySharp.Domain.ValidationRules
{
    public interface ITagRules : IValidationRule
    {
        Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);

        Task<bool> IsTagIdExistAsync(int id, CancellationToken cancellationToken);
    }
}
