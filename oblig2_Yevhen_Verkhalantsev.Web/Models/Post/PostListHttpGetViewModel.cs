using oblig2_Yevhen_Verkhalantsev.Database;

namespace oblig2_Yevhen_Verkhalantsev.Web.Models.Post;

public class PostListHttpGetViewModel
{
    
    public PostListHttpGetViewModel()
    {
        Posts = new List<PostEntity>();
    }

    public ICollection<PostEntity> Posts { get; set; }
}