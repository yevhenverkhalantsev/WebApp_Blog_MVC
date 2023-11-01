using oblig2_Yevhen_Verkhalantsev.Database;

namespace oblig2_Yevhen_Verkhalantsev.Web.Models.Blog;

public class BlogListHttpGetViewModel
{
    public BlogListHttpGetViewModel()
    {
        Blogs = new List<BlogEntity>();
        Users = new List<UserEntity>();
    }

    public ICollection<BlogEntity> Blogs { get; set; }
    public ICollection<UserEntity> Users { get; set; }
}