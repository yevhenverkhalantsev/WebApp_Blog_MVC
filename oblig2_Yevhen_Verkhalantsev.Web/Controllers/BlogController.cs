using Microsoft.AspNetCore.Mvc;

namespace oblig1_Yevhen_Verkhalantsev.Controllers;

public class BlogController: Controller
{
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    
}