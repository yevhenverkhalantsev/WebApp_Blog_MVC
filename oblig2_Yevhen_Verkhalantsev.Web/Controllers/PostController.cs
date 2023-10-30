using Microsoft.AspNetCore.Mvc;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.PostServices;
using oblig2_Yevhen_Verkhalantsev.Web.Models.Post;

namespace oblig2_Yevhen_Verkhalantsev.Web.Controllers;

public class PostController: Controller
{
    private readonly IPostService _postService;
    private readonly IBlogService _blogService;

    public PostController(IPostService postService, IBlogService blogService )
    {
        _postService = postService;
        _blogService = blogService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> PostList()
    {
        PostListHttpGetViewModel vm = new PostListHttpGetViewModel()
        {
            Posts = _postService.GetAll()
        };
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Update(long id)
    {
        var response = await _postService.GetById(id);
        if(response.IsError)
        {
            return View(new UpdatePostHttpGetViewModel()
            {
                Blogs = _blogService.GetAll(),
                ErrorMessage = response.ErrorMessage,
                IsError = true
            });
        }

        return View(new UpdatePostHttpGetViewModel()
        {
            Blogs = _blogService.GetAll(),
            IsError = false,
            Post = response.Value
        });

    }
    
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdatePostHttpPostModel vm)
    {
        var response = await _postService.Update(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }

        return Ok(new
        {
            success = true
        });
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeletePostHttpPostModel vm)
    {
        var response = await _postService.Delete(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }

        return Ok(new
        {
            success = true
        });
    }

}