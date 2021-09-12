using System.Collections.Generic;
using System.Threading.Tasks;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.Domain.Repositories;

namespace StudySharp.DomainServices.Repositories
{
    public class TagRepository : ITagRepository
    {
        public Task<OperationResult<Tag>> CreateTagAsync(Tag tag)
        {
            throw new System.NotImplementedException();
        }

        public Task<OperationResult<Tag>> GetTagByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<OperationResult<List<Tag>>> GetTagsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<OperationResult<Tag>> RemoveTagByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}