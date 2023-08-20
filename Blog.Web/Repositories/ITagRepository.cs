using Blog.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<TagItem>> GetallTagsAsync();

        Task<TagItem> GetTagAsync(Guid id);

        Task<TagItem> AddAsync(TagItem item);

        Task<bool> EditAsync(TagItem tag);

     

        Task<bool> DeleteByIdAsync(Guid id);
    }
}
