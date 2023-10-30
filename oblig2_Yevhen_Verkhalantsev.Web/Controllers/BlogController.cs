using Microsoft.AspNetCore.Mvc;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.Response;

namespace oblig1_Yevhen_Verkhalantsev.Controllers;

public class BlogController: Controller
{
    
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }
    
    [HttpGet]
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
        var response = await _blogService.Create(vm);
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
}