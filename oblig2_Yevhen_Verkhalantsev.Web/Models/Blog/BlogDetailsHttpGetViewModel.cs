using oblig2_Yevhen_Verkhalantsev.Database;

namespace oblig2_Yevhen_Verkhalantsev.Web.Models.Blog;

public class BlogDetailsHttpGetViewModel
{
    public BlogEntity Blog { get; set; }
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; }
}