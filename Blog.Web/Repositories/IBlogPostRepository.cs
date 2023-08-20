using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public interface IBlogPostRepository
    {

        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid id);

        Task<bool> DeleteAsync(Guid id);

        Task<BlogPost> AddAsync(BlogPost post);

        Task<bool> UpdateAsync(BlogPost post);
    }
}
