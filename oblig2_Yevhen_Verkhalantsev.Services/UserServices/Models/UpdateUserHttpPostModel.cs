namespace oblig2_Yevhen_Verkhalantsev.Services.UserServices.Models;

public class UpdateUserHttpPostModel
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}