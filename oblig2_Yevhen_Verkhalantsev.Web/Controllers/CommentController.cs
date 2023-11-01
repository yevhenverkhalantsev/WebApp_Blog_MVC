using Microsoft.AspNetCore.Mvc;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices;
using oblig2_Yevhen_Verkhalantsev.Services.PostServices;
using oblig2_Yevhen_Verkhalantsev.Web.Models.Comment;

namespace oblig1_Yevhen_Verkhalantsev.Controllers;

public class CommentController: Controller
{
    private readonly IPostService _postService;
    private readonly IBlogService _blogService;

    public CommentController(IPostService postService, IBlogService blogService )
    {
        _postService = postService;
        _blogService = blogService;
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
    
    [HttpGet]
    public async Task<IActionResult> CommentList()
    {
        return View();
    }
}