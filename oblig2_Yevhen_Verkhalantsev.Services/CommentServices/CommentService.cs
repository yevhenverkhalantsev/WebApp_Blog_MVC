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

    public async Task<ResponseService<CommentEntity>> Create(CreateCommentHttpPostModel vm)
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
            return ResponseService<CommentEntity>.Error(e.Message);
        }
        
        return ResponseService<CommentEntity>.Ok(commentEntity);
    }

    public async Task<ResponseService> Delete(DeleteCommentHttpPostModel vm)
    {
        CommentEntity comment = await _commentRepository.GetById(vm.Id);
        if (comment == null)
        {
            return ResponseService.Error("No comment with that id");
        }

        try
        {
            await _commentRepository.Delete(comment);

        }
        catch (Exception e)
        {
            return ResponseService.Error(e.Message);
        }
        
        return ResponseService.Ok();
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

    public async Task<ResponseService<CommentEntity>> GetById(long id)
    {
        CommentEntity comment = await _commentRepository.GetById(id);
        if (comment == null)
        {
            return ResponseService<CommentEntity>.Error("No comment with that id");
        }
        return ResponseService<CommentEntity>.Ok(comment);
    }

    public async Task<ResponseService> Update(UpdateCommentHttpPostModel vm)
    {
        CommentEntity comment = await _commentRepository.GetById(vm.Id);
        if (comment == null)
        {
            return ResponseService.Error("No comment with that id");
        }
        comment.Content = vm.Content;
        
        try
        {
            await _commentRepository.Update(comment);
        }
        catch (Exception e)
        {
            return ResponseService.Error(e.Message);
        }
        return ResponseService.Ok();
    }
}