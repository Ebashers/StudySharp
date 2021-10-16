using System.Threading;
using System.Threading.Tasks;

namespace StudySharp.Domain.ValidationRules
{
    public interface IPracticalBlockRules : IValidationRule
    {
        Task<bool> IsCourseIdExistAsync(int id, CancellationToken cancellationToken);
        Task<bool> IsPracticalBlockIdExistAsync(int id, CancellationToken cancellationToken);
    }
}
