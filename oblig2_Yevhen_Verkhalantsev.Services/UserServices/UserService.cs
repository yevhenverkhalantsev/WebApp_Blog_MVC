using Microsoft.EntityFrameworkCore;
using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.EntityFramework.Repository;
using oblig2_Yevhen_Verkhalantsev.Services.Response;
using oblig2_Yevhen_Verkhalantsev.Services.UserServices.Models;

namespace oblig2_Yevhen_Verkhalantsev.Services.UserServices;

public class UserService: IUserService
{

    private readonly IGenericRepository<UserEntity> _userRepository;

    public UserService(IGenericRepository<UserEntity> userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ResponseService<long>> Create(CreateUserHttpPostModel vm)
    {
        ResponseService<UserEntity> result = await GetByUsername(vm.Username);
        if (!result.IsError)
        {
            return ResponseService<long>.Error("Username already exists");
        }
        
        UserEntity user = new UserEntity
        {
            Username = vm.Username,
            Password = vm.Password
        };

        try
        {
            await _userRepository.Create(user);
        }
        catch (Exception e)
        {
            return ResponseService<long>.Error(e.Message);
        }
        
        return ResponseService<long>.Ok(user.Id);
    }

    public Task<ResponseService> Update(UpdateUserHttpPostModel vm)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseService> Delete(DeleteUserHttpPostModel vm)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseService<UserEntity>> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseService<UserEntity>> GetByUsername(string username)
    {
        UserEntity user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == username);
        if (user == null)
        {
            return ResponseService<UserEntity>.Error("User not found");
        }

        return ResponseService<UserEntity>.Ok(user);
    }

    public async Task<ResponseService<UserEntity>> GetByUsernameAndPassword(string username, string password)
    {
        UserEntity user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        if (user == null)
        {
            return ResponseService<UserEntity>.Error("User not found");
        }

        return ResponseService<UserEntity>.Ok(user);

    }
    
    public async Task<ICollection<UserEntity>> GetAll()
    {
        return await _userRepository.GetAll().ToListAsync();
    }
}