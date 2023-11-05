using Microsoft.EntityFrameworkCore;
using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.EntityFramework.Repository;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.Response;

namespace oblig2_Yevhen_Verkhalantsev.Services.PostServices;

public class PostService: IPostService
{
    private readonly IGenericRepository<PostEntity> _postRepository;

    public PostService(IGenericRepository<PostEntity> repository)
    {
        _postRepository = repository;
    }
    
    public async Task<ResponseService> Create(CreatePostHttpPostModel vm)
    {
        PostEntity postEntity = new PostEntity()
        {
            Title = vm.Title,
            Content = vm.Content,
            CreatedAt = DateTime.Now,
            BlogFk = vm.BlogId
        };
        
        try
        {
            await _postRepository.Create(postEntity);
        }
        catch (Exception e)
        {
            return ResponseService.Error(e.Message);
        }
        
        return ResponseService.Ok();
    }

    public ICollection<PostEntity> GetAll()
    {
        return _postRepository.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public async Task<ResponseService<PostEntity>> GetById(long id)
    {
        PostEntity? post = await _postRepository.GetAll()
            .Include(x => x.Blog)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (post == null)
        {
            return ResponseService<PostEntity>.Error("No post with that id");
        }
    
        return ResponseService<PostEntity>.Ok(post);
    }


    
    public async Task<ResponseService> Update(UpdatePostHttpPostModel vm)
    {
        PostEntity postEntity = await _postRepository.GetById(vm.Id);
        if (postEntity == null)
        {
            return ResponseService.Error("Bad post id");
        }

        postEntity.Title = vm.Title;
        postEntity.Content = vm.Content;

        try
        {
            await _postRepository.Update(postEntity);
        }
        catch (Exception e)
        {
            return ResponseService.Error(e.Message);
        }

        return ResponseService.Ok();
    }

    public async Task<ResponseService> Delete(DeletePostHttpPostModel vm)
    {
        PostEntity post = await _postRepository.GetById(vm.Id);
        if (post == null)
        {
            return ResponseService.Error("Post with that id is not found");
        }

        try
        {
            await _postRepository.Delete(post);

        }
        catch (Exception e)
        {
            return ResponseService.Error(e.Message);
        }

        return ResponseService.Ok();
    }
    
}