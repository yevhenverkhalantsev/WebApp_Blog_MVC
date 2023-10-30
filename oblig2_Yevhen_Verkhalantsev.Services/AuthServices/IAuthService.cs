using oblig2_Yevhen_Verkhalantsev.Services.Response;

namespace oblig2_Yevhen_Verkhalantsev.Services.AuthServices;

public interface IAuthService
{
    Task<ResponseService> LogIn(string username, string password);
    Task<ResponseService> LogOut();
}