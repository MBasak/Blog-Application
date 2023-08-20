using Blog.Web.DbContexts;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogPostRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            var result = await _blogDbContext.AddAsync(post);
           await _blogDbContext.SaveChangesAsync();

            return post;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _blogDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await _blogDbContext.BlogPosts.Include(x => x.Tags)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<bool> UpdateAsync(BlogPost post)
        {
            throw new NotImplementedException();
        }
    }
}
