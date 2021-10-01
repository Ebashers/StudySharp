using System.Threading.Tasks;

namespace StudySharp.Domain.ValidationRules
{
    public interface ITagRules : IValidationRule
    {
        Task<bool> IsNameUniqueAsync(string name);
    }
}
