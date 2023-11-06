using oblig2_Yevhen_Verkhalantsev.Database;

namespace oblig2_Yevhen_Verkhalantsev.Web.Models.Comment;

public class CreateCommentHttpGetViewModel
{
    public CreateCommentHttpGetViewModel()
    {
        Blogs = new List<BlogEntity>();
        Users = new List<UserEntity>();
        Posts = new List<PostEntity>();
    }
    
    public ICollection<BlogEntity> Blogs { get; set; }
    public ICollection<UserEntity> Users { get; set; }
    public ICollection<PostEntity> Posts { get; set; }
}