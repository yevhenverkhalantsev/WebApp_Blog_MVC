using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.UserServices;
using oblig2_Yevhen_Verkhalantsev.Web.Models.Blog;

namespace oblig2_Yevhen_Verkhalantsev.Web.Controllers;

public class BlogController: Controller
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBlogHttpPostModel vm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(currentUserId, out long userId))
        {
            return BadRequest(new { errorMessage = "Invalid User ID" });
        }

        var response = await _blogService.Create(vm, userId);
        if (response.IsError)
        {
            return BadRequest(new
            {
                errorMessage = response.ErrorMessage
            });
        }
        
        return Ok(new
        {
            success = true
        });
    }

    [HttpGet]
    public async Task<IActionResult> BlogList(long id)
    {
        BlogListHttpGetViewModel vm = new BlogListHttpGetViewModel()
        {
            Blogs = await _blogService.GetAllByUserId(id)
        };

        return View(vm);
    }
    
    [HttpGet]
    public async Task<IActionResult> BlogAllList()
    {
        BlogListHttpGetViewModel vm = new BlogListHttpGetViewModel()
        {
            Blogs = await _blogService.GetAll()
        };
        
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> BlogDetails(long id)
    {
         var response = await _blogService.GetById(id);
         if (response.IsError)
         {
             return View(new BlogDetailsHttpGetViewModel()
             {
                 IsError = true,
                 ErrorMessage = response.ErrorMessage
             });
         }
         
         return View(new BlogDetailsHttpGetViewModel()
         {
             IsError = false,
             Blog = response.Value
         });
    }
    
    [HttpGet]
    public async Task<IActionResult> MyBlog(long id)
    {
        return View();
    }
}