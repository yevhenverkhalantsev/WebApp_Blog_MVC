using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.Response;

namespace oblig2_Yevhen_Verkhalantsev.Services.PostServices;

public interface IPostService
{
    Task<ResponseService> Create(CreatePostHttpPostModel vm);
    ICollection<PostEntity> GetAll();
    
    ICollection<PostEntity> GetAllByUserId(long userId);

    Task<ResponseService<PostEntity>> GetById(long id);
    
    Task<ResponseService> Update(UpdatePostHttpPostModel vm);
    
    Task<ResponseService> Delete(DeletePostHttpPostModel vm);
    
    Task<ResponseService<IEnumerable<PostEntity>>> GetPostsByBlog(long id);
}