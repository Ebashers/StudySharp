using System.Collections.Generic;
using System.Threading.Tasks;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;

namespace StudySharp.Domain.Repositories
{
    public interface ITagRepository : IRepository
    {
        Task<OperationResult<Tag>> CreateTagAsync(Tag tag);
        Task<OperationResult<Tag>> GetTagByIdAsync(int id);
        Task<OperationResult<List<Tag>>> GetTagsAsync();
        Task<OperationResult<Tag>> RemoveTagByIdAsync(int id);
    }
}