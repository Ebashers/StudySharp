using System.Threading;
using System.Threading.Tasks;

namespace StudySharp.Domain.ValidationRules
{
    public interface ICourseRules : IValidationRule
    {
        Task<bool> IsCourseIdExistAsync(int id, CancellationToken cancellationToken);
        Task<bool> IsTeacherIdExistAsync(int teacherId, CancellationToken cancellationToken);
        Task<bool> IsNameUniqueAsync(string name, int teacherId, CancellationToken cancellationToken);
    }
}