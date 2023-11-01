using oblig2_Yevhen_Verkhalantsev.Database;

namespace oblig2_Yevhen_Verkhalantsev.Web.Models.User;

public class UserListHttpGetViewModel
{
    public UserListHttpGetViewModel()
    {
        Users = new List<UserEntity>();
    }

    public ICollection<UserEntity> Users { get; set; }
}