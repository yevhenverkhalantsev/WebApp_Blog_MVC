using FluentValidation;

namespace oblig2_Yevhen_Verkhalantsev.Services.BlogServices.Models;

public class CreatePostHttpPostModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    
}

public class CreatePostHttpPostValidator : AbstractValidator<CreatePostHttpPostModel>
{
    public CreatePostHttpPostValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(3)
            .MaximumLength(200)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Content)
            .MinimumLength(3)
            .MaximumLength(500)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.CreatedAt)
            .NotEmpty()
            .NotNull();

    }
}