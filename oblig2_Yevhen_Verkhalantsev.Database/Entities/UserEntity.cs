using System.Collections.ObjectModel;

namespace oblig2_Yevhen_Verkhalantsev.Database;

public class UserEntity
{
    public UserEntity()
    {
        Comments = new Collection<CommentEntity>();
        Blogs = new Collection<BlogEntity>();
    }
    
    public long Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public ICollection<CommentEntity> Comments { get; set; }
    public ICollection<BlogEntity> Blogs { get; set; }
}