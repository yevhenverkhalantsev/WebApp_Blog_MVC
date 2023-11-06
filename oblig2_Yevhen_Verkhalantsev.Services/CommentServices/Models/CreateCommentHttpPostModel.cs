namespace oblig2_Yevhen_Verkhalantsev.Services.CommentServices.Models;

public class CreateCommentHttpPostModel
{
    public string Content { get; set; } = null!;
    public long UserId { get; set; }
    public long PostId { get; set; }
}