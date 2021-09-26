using System.Threading.Tasks;

namespace StudySharp.Domain.Validators
{
    public interface ITagRules : IValidationRule
    {
        Task<bool> IsNameUniqueAsync(string name);
    }
}
