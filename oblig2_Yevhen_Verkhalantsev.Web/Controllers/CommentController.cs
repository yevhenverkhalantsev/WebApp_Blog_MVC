using Microsoft.AspNetCore.Mvc;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices;
using oblig2_Yevhen_Verkhalantsev.Services.CommentServices;
using oblig2_Yevhen_Verkhalantsev.Services.CommentServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.PostServices;
using oblig2_Yevhen_Verkhalantsev.Web.Models.Comment;

namespace oblig2_Yevhen_Verkhalantsev.Web.Controllers;

public class CommentController: Controller
{
    private readonly IPostService _postService;
    private readonly IBlogService _blogService;
    private readonly ICommentService _commentService;

    public CommentController(IPostService postService, IBlogService blogService, ICommentService commentService)
    {
        _postService = postService;
        _blogService = blogService;
        _commentService = commentService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Create(long id)
    {
        CreateCommentHttpGetViewModel vm = new CreateCommentHttpGetViewModel()
        {
            Blogs = await _blogService.GetAllByUserId(id)
        };
        return View(vm);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentHttpPostModel vm)
    {
        var response = await _commentService.Create(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }

        return Ok(new { success = true, result = response.Value});
    }

    [HttpGet]
    public async Task<IActionResult> CommentList()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody]DeleteCommentHttpPostModel vm)
    {
        var response = await _commentService.Delete(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }
        
        return Ok(new { success = true });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCommentById(long id)
    {
        var response = await _commentService.GetById(id);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }
        
        return Ok(new { success = true, result = response.Value});
    }
    
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateCommentHttpPostModel vm)
    {
        var response = await _commentService.Update(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }
        
        return Ok(new { success = true});
    }
    
}