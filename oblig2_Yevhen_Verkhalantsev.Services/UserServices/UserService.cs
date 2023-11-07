using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.EntityFramework.Repository;
using oblig2_Yevhen_Verkhalantsev.Services.Response;
using oblig2_Yevhen_Verkhalantsev.Services.UserServices.Models;

namespace oblig2_Yevhen_Verkhalantsev.Services.UserServices;

public class UserService: IUserService
{

    private readonly IGenericRepository<UserEntity> _userRepository;
    
    private readonly IPasswordHasher<UserEntity> _passwordHasher;

    public UserService(IGenericRepository<UserEntity> userRepository, IPasswordHasher<UserEntity> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
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
            Password = vm.Password,
        };
        
        user.Password = _passwordHasher.HashPassword(null, user.Password); 
        
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
   
        string hashedPassword = _passwordHasher.HashPassword(null, password);
        
        ICollection<UserEntity> users = await _userRepository.GetAll()
            .Where(x => x.Username == username)
            .ToListAsync();
        
        if (users.Count == 0 )
        {
            return ResponseService<UserEntity>.Error("User not found");
        }

        foreach (var user in users)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, user.Password, password);
            if (result == PasswordVerificationResult.Success)
            {
                return ResponseService<UserEntity>.Ok(user);
            }
        }

        return ResponseService<UserEntity>.Error("User not found");

    }
    
    public async Task<ICollection<UserEntity>> GetAll()
    {
        return await _userRepository.GetAll().ToListAsync();
    }
}