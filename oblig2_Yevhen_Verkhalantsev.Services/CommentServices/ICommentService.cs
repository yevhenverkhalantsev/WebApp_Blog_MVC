using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.Services.CommentServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.Response;

namespace oblig2_Yevhen_Verkhalantsev.Services.CommentServices;

public interface ICommentService
{
    Task<ResponseService<CommentEntity>> Create(CreateCommentHttpPostModel vm);
    
    Task<ResponseService> Delete(DeleteCommentHttpPostModel vm);
    
    Task<ResponseService<IEnumerable<CommentEntity>>> GetCommentsByPost(long id);
    
    Task<ResponseService<IEnumerable<CommentEntity>>> GetCommentsByBlog(long id);
    
    Task<ResponseService<IEnumerable<CommentEntity>>> GetCommentsByUser(long id);
    
    Task<ResponseService<CommentEntity>> GetById(long id);
    
    Task<ResponseService> Update(UpdateCommentHttpPostModel vm);
}