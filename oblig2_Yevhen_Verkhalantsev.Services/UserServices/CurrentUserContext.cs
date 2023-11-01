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
            // Check if _httpContextAccessor or HttpContext is null
            if (_httpContextAccessor?.HttpContext == null)
            {
                throw new Exception("HttpContext is not available.");
            }

            // Check if User or Claims is null
            if (_httpContextAccessor.HttpContext.User?.Claims == null)
            {
                throw new Exception("User claims are not available.");
            }

            // Try to get the claim
            var claim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            // Check if the claim is found and not empty
            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                throw new Exception("Can not get user id.");
            }

            // Parse the value to long
            if (long.TryParse(claim.Value, out long id))
            {
                return id;
            }
            else
            {
                throw new Exception("User id is not in a valid format.");
            }
        }
    }


    public string Username
    {
        get
        {
            if (_httpContextAccessor?.HttpContext?.User?.Claims == null)
            {
                throw new Exception("HttpContext or User Claims are not available.");
            }

            var claim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Name);

            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                throw new Exception("Can not get username");
            }

            return claim.Value;
        }
    }


    public bool IsAuthenticated
    {
        get
        {
            // Check if the HttpContext is available
            if (_httpContextAccessor?.HttpContext == null)
            {
                return false;
            }

            // Return the authentication status
            return _httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? false;
        }
    }

}