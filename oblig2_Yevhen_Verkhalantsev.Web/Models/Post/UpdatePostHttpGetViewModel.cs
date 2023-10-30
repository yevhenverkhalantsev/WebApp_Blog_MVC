using oblig2_Yevhen_Verkhalantsev.Database;

namespace oblig2_Yevhen_Verkhalantsev.Web.Models.Post;

public class UpdatePostHttpGetViewModel
{
    public UpdatePostHttpGetViewModel()
    {
        ErrorMessage = "";
    }
    
    public PostEntity Post { get; set; }
    public ICollection<BlogEntity> Blogs { get; set; }
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; }
    
}