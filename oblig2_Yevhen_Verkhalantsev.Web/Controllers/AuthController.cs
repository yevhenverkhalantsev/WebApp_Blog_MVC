using Microsoft.AspNetCore.Mvc;
using oblig2_Yevhen_Verkhalantsev.Services.AuthServices;
using oblig2_Yevhen_Verkhalantsev.Services.UserServices;
using oblig2_Yevhen_Verkhalantsev.Services.UserServices.Models;

namespace oblig2_Yevhen_Verkhalantsev.Web.Controllers;

public class AuthController: Controller
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, 
        IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }
    
    [HttpGet]
    [Route("LogIn")]
    public async Task<IActionResult> LogIn()
    {
        return View();
    }

    [HttpPost]
    [Route("LogIn")]
    public async Task<IActionResult> LogIn([FromBody] CreateUserHttpPostModel vm)
    {
        var response = await _authService.LogIn(vm.Username, vm.Password);
        if (response.IsError)
        {
            return BadRequest(new {ErrorMessage = response.ErrorMessage});
        }

        return Ok(new {success = true, url = Url.Action("Index", "Home")});
    }
    
    [HttpGet]
    public async Task<IActionResult> SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody]CreateUserHttpPostModel vm)
    {
        var response = await _userService.Create(vm);
        if (response.IsError)
        {
            return BadRequest(new {ErrorMessage = response.ErrorMessage});
        }
        
        var authResponse = await _authService.LogIn(vm.Username, vm.Password);
        if (authResponse.IsError)
        {
            return BadRequest(new {ErrorMessage = authResponse.ErrorMessage});
        }

        return Ok(new {success = true});

    }
    
    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        var response = await _authService.LogOut();
        if (response.IsError)
        {
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("Index", "Home");
    }
}