using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace StudySharp.ApplicationServices.ValidationRules
{
    public interface ICourseRules : IValidationRule
    {
        Task<bool> IsCourseIdExistAsync(int id, CancellationToken cancellationToken);
        Task<bool> IsTeacherIdExistAsync(int teacherId, CancellationToken cancellationToken);
        Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);
    }
}