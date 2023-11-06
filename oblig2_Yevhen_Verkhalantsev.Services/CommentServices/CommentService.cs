using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.EntityFramework.Repository;
using oblig2_Yevhen_Verkhalantsev.Services.CommentServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.Response;

namespace oblig2_Yevhen_Verkhalantsev.Services.CommentServices;

public class CommentService: ICommentService
{
    private readonly IGenericRepository<CommentEntity> _commentRepository;

    public CommentService(IGenericRepository<CommentEntity> repository)
    {
        _commentRepository = repository;
    }

    public async Task<ResponseService> Create(CreateCommentHttpPostModel vm)
    {
        CommentEntity commentEntity = new CommentEntity()
        {
            Content = vm.Content,
            CreatedAt = DateTime.Now,
            UserFk = vm.UserId,
            PostFk = vm.PostId
        };
        
        try
        {
            await _commentRepository.Create(commentEntity);
        }
        catch (Exception e)
        {
            return ResponseService.Error(e.Message);
        }
        
        return ResponseService.Ok();
    }

    public Task<ResponseService> Delete(DeleteCommentHttpPostModel vm)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseService<IEnumerable<CommentEntity>>> GetCommentsByPost(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseService<IEnumerable<CommentEntity>>> GetCommentsByBlog(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseService<IEnumerable<CommentEntity>>> GetCommentsByUser(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseService<CommentEntity>> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseService> Update(UpdateCommentHttpPostModel vm)
    {
        throw new NotImplementedException();
    }
}