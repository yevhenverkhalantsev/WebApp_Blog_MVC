using Microsoft.EntityFrameworkCore;
using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.EntityFramework.Repository;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.Response;

namespace oblig2_Yevhen_Verkhalantsev.Services.BlogServices;

public class BlogService: IBlogService
{
    private readonly IGenericRepository<BlogEntity> _blogRepository;

    public BlogService(IGenericRepository<BlogEntity> repository)
    {
        _blogRepository = repository;
    }

    public async Task<ResponseService> Create(CreateBlogHttpPostModel vm, long userId)
    {
        BlogEntity blogEntity = new BlogEntity()
        {
            Title = vm.Title,
            CreatedAt = DateTime.Now,
            UserFk = userId,
            IsOpen = vm.IsOpen
        };

        try
        {
            await _blogRepository.Create(blogEntity);
        }
        catch (Exception e)
        {
            return ResponseService.Error(e.Message);
        }

        return ResponseService.Ok();
        
    }

    public async Task<ICollection<BlogEntity>> GetAll()
    {
        return await _blogRepository.GetAll()
            .Include(x=>x.User)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<ResponseService<BlogEntity>> GetById(long id)
    {
        BlogEntity blog = await _blogRepository.GetAll()
            .Include(x => x.Posts)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (blog == null)
        {
            return ResponseService<BlogEntity>.Error("No blog with that id");
        }

        return ResponseService<BlogEntity>.Ok(blog);
    }

    public async Task<ICollection<BlogEntity>> GetAllByUserId(long userId)
    {
        var blogs = await _blogRepository.GetAll()
            .Where(b=>b.UserFk == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        
        return blogs;
    }
}