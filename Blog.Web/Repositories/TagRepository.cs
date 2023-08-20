using Blog.Web.DbContexts;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private BlogDbContext _blogDbContext;

        public TagRepository(BlogDbContext dbContext)
        {
            _blogDbContext = dbContext;
        }

        public async Task<TagItem> AddAsync(TagItem tag)
        {
            await _blogDbContext.Tags.AddAsync(tag);
            await _blogDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var tag = await _blogDbContext.Tags.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (tag != null)
            {
                _blogDbContext.Tags.Remove(tag);
                await _blogDbContext.SaveChangesAsync();
                return true;
            }

            return false;

        }


        public async Task<IEnumerable<TagItem>> GetallTagsAsync()
        {
            var result = await _blogDbContext.Tags.ToListAsync();

            return result;
        }

        public async Task<TagItem> GetTagAsync(Guid id)
        {
            var tag = await _blogDbContext.Tags.Where(x => x.Id == id).FirstOrDefaultAsync();
            return tag;
        }

        public async Task<bool> EditAsync(TagItem tag)
        {
            var existingTag = await _blogDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await _blogDbContext.SaveChangesAsync();
                return true;
              
            }
            return false;

        }
    }
}
