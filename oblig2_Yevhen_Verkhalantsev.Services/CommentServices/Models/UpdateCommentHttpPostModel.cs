namespace oblig2_Yevhen_Verkhalantsev.Services.CommentServices.Models;

public class UpdateCommentHttpPostModel
{
    public long Id { get; set; }
    public string Content { get; set; } = null!;
}