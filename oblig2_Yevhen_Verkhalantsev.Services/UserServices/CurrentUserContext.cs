using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace oblig2_Yevhen_Verkhalantsev.Services.UserServices;

public class CurrentUserContext: ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long Id
    {
        get
        {
            string value = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Can not get user id");
            }

            return long.Parse(value);
            
        }
    }

    public string Username
    {
        get
        {
            string value = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;

            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Can not get username");
            }
            return value;
        }
    }

    public bool IsAuthenticated
    {
        get
        {
            var isAuthenticated = _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated;
            if (isAuthenticated.HasValue)
            {
                if(isAuthenticated == true)
                {
                    return true;
                }
                
                return false;
            }

            return false;
        }
    }
}