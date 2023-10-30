using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.Response;

namespace oblig2_Yevhen_Verkhalantsev.Services.BlogServices;

public interface IBlogService
{
    Task<ResponseService> Create(CreateBlogHttpPostModel vm);
    ICollection<BlogEntity> GetAll();

    Task<ResponseService<BlogEntity>> GetById(long id);
}