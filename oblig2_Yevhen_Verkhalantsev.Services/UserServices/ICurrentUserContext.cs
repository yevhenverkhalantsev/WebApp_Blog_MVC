namespace oblig2_Yevhen_Verkhalantsev.Services.UserServices;

public interface ICurrentUserContext
{
    public long Id { get; }
    public string Username { get; }
    
    public bool IsAuthenticated { get; }
}