using System.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository,
            IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await _tagRepository.GetallTagsAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };
        
        
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {

            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription, 
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UriHandle = addBlogPostRequest.UriHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible= addBlogPostRequest.Visible
            };

            blogPost.Tags = new List<TagItem>();
            foreach(var selectedTag in addBlogPostRequest.SelectedTags)
            {
                var existingTag = await _tagRepository.GetTagAsync(Guid.Parse(selectedTag));
                if(existingTag != null)
                {
                    blogPost.Tags.Add(existingTag);
                }
            }
            await _blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var list = await _blogPostRepository.GetAllAsync();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> EditBlog(Guid id)
        {
            var existingBlog = await _blogPostRepository.GetAsync(id);
            var tags = await _tagRepository.GetallTagsAsync();

            if (existingBlog != null)
            {
                var editBlogReqItem = new EditBlogRequest
            {
                Heading = existingBlog.Heading,
                PageTitle = existingBlog.PageTitle,
                Content = existingBlog.Content,
                ShortDescription = existingBlog.ShortDescription,
                FeaturedImageUrl = existingBlog.FeaturedImageUrl,
                UriHandle = existingBlog.UriHandle,
                PublishedDate = existingBlog.PublishedDate,
                Author = existingBlog.Author,
                Visible = existingBlog.Visible,
                Id = existingBlog.Id,
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                SelectedTags = existingBlog.Tags.Select(x => x.Id.ToString()).ToArray()


            };
            
                return View(editBlogReqItem);
            }
            return RedirectToAction(null);
            
        }
    }
}
