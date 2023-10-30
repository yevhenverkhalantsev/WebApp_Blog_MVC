using oblig2_Yevhen_Verkhalantsev.Database;

namespace oblig2_Yevhen_Verkhalantsev.Web.Models.Post;

public class CreatePostHttpGetViewModel
{
    public CreatePostHttpGetViewModel()
    {
        Blogs = new List<BlogEntity>();
    }
    
    public ICollection<BlogEntity> Blogs { get; set; }
}