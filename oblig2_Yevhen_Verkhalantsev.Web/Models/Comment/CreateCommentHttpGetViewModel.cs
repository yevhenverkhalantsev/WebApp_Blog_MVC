using oblig2_Yevhen_Verkhalantsev.Database;

namespace oblig2_Yevhen_Verkhalantsev.Web.Models.Comment;

public class CreateCommentHttpGetViewModel
{
    public CreateCommentHttpGetViewModel()
    {
        Blogs = new List<BlogEntity>();
    }
    
    public ICollection<BlogEntity> Blogs { get; set; }
}