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

    public async Task<ResponseService> Create(CreateBlogHttpPostModel vm)
    {
        BlogEntity blogEntity = new BlogEntity()
        {
            Title = vm.Title,
            CreatedAt = DateTime.Now
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

    public ICollection<BlogEntity> GetAll()
    {
        return _blogRepository.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public async Task<ResponseService<BlogEntity>> GetById(long id)
    {
        BlogEntity blog = await _blogRepository.GetById(id);
        if (blog == null)
        {
            return ResponseService<BlogEntity>.Error("No blog with that id");
        }

        return ResponseService<BlogEntity>.Ok(blog);
    }
    
    
}