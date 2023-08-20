using Blog.Web.DbContexts;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {

        private ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

       
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var tag = new TagItem
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            var result = await _tagRepository.AddAsync(tag);

            return RedirectToAction("GetTags");
        }

        [HttpGet]

        public async Task<IActionResult> GetTags()
        {

            var tags = await _tagRepository.GetallTagsAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> EditTag(Guid id)
        {
            var tag = await _tagRepository.GetTagAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditTag(EditTagRequest editTagRequest)
        {
            var tag = new TagItem
            {
                Id=editTagRequest.Id, 
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var result = await _tagRepository.EditAsync(tag);

            if(result)
            {
                return RedirectToAction("GetTags");
            }

            return RedirectToAction("GetTags");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTag(Guid id)
        {

            var result = await _tagRepository.DeleteByIdAsync(id);

            if(result)
            { 
                return RedirectToAction("GetTags");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTag(EditTagRequest editTagRequest)
        {
            bool result = await _tagRepository.DeleteByIdAsync(editTagRequest.Id);

            if (result)
            {
                return RedirectToAction("GetTags");
            }

            return RedirectToAction("GetTags");
        }
    }
}
