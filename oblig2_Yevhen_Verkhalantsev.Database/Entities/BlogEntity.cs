using System.Collections.ObjectModel;

namespace oblig2_Yevhen_Verkhalantsev.Database;

public class BlogEntity
{
    public BlogEntity()
    {
        Posts = new Collection<PostEntity>();
    }
    
    public long Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsOpen { get; set; }
    
    public long UserFk { get; set; }
    public UserEntity User { get; set; }
    
    public ICollection<PostEntity> Posts { get; set; }
}