using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.Services.Response;
using oblig2_Yevhen_Verkhalantsev.Services.UserServices;

namespace oblig2_Yevhen_Verkhalantsev.Services.AuthServices;

public class AuthService: IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    private readonly IUserService _userService;

    public AuthService(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<ResponseService> LogIn(string username, string password)
    {
        ResponseService<UserEntity> result = await _userService.GetByUsernameAndPassword(username, password);
        if (result.IsError)
        {
            return ResponseService.Error(result.ErrorMessage);
        }

        ICollection<Claim> claims = await GetClaims(result.Value);
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        try
        {
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        }
        catch (Exception e)
        {
            return ResponseService.Error(e.Message);
        }
        
        return ResponseService.Ok();
    }
    
    private async Task<ICollection<Claim>> GetClaims(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)

        };

        return claims;
    } 

    public async Task<ResponseService> LogOut()
    {
        try
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
        catch (Exception e)
        {
            return ResponseService.Error(errorMessage: e.Message);
        }
        
        return ResponseService.Ok();
    }
}