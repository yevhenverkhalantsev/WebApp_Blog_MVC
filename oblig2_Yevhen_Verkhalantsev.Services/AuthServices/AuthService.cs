using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.Services.Response;
using oblig2_Yevhen_Verkhalantsev.Services.UserServices;

namespace oblig2_Yevhen_Verkhalantsev.Services.AuthServices;

public class AuthService: IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<ResponseService> LogIn(string username, string password)
    {
        
        throw new NotImplementedException();
        // ResponseService<UserEntity> result = await _userService.GetByUsernameAndPassword(username, password);
        // if (result.IsError)
        // {
        //     return ResponseService.Error(result.ErrorMessage);
        // }
        // result.
    }

    public Task<ResponseService> LogOut()
    {
        throw new NotImplementedException();
    }
}