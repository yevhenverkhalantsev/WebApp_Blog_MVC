using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.Services.Response;
using oblig2_Yevhen_Verkhalantsev.Services.UserServices.Models;

namespace oblig2_Yevhen_Verkhalantsev.Services.UserServices;

public interface IUserService
{
    Task<ResponseService<long>> Create(CreateUserHttpPostModel vm);
    
    Task<ResponseService> Update(UpdateUserHttpPostModel vm);
    
    Task<ResponseService> Delete(DeleteUserHttpPostModel vm); 
    
    Task<ResponseService<UserEntity>> GetById(long id);
    
    Task<ResponseService<UserEntity>> GetByUsername(string username);
    
    Task<ResponseService<UserEntity>> GetByUsernameAndPassword(string username, string password);
    
    Task<ICollection<UserEntity>> GetAll();
}