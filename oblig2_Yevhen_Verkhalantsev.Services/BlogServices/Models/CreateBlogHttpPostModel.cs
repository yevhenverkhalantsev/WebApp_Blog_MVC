namespace oblig2_Yevhen_Verkhalantsev.Services.BlogServices.Models;
using FluentValidation;

public class CreateBlogHttpPostModel
{
    public string Title { get; set; }
    
}

public class CreateBlogHttpPostValidator : AbstractValidator<CreateBlogHttpPostModel>
{
    public CreateBlogHttpPostValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(3)
            .MaximumLength(200)
            .NotEmpty()
            .NotNull();
    }
}