using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using oblig2_Yevhen_Verkhalantsev.Database;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices;
using oblig2_Yevhen_Verkhalantsev.Services.BlogServices.Models;
using oblig2_Yevhen_Verkhalantsev.Services.Response;
using oblig2_Yevhen_Verkhalantsev.Web.Controllers;
using oblig2_Yevhen_Verkhalantsev.Web.Models.Blog;
using Xunit;

namespace oblig2_Yevhen_Verkhalantsev.Tests.Blog;

public class BlogControllerTests
{
    private readonly Mock<IBlogService> _mockBlogService;
    private readonly BlogController _controller;
    private readonly Mock<HttpContext> _mockHttpContext;
    private readonly ClaimsPrincipal _user;

    public BlogControllerTests()
    {
        _mockBlogService = new Mock<IBlogService>();
        _controller = new BlogController(_mockBlogService.Object);
        _mockHttpContext = new Mock<HttpContext>();
        _user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
        }, "mock"));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = _mockHttpContext.Object
        };
        _mockHttpContext.SetupGet(x => x.User).Returns(_user);
    }

    [Fact]
    public async Task Create_Post_InvalidModelState_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Title", "Required");
        var blogPost = new CreateBlogHttpPostModel();

        // Act
        var result = await _controller.Create(blogPost);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
    }

    [Fact]
    public async Task Create_Post_ValidModel_CallsBlogServiceAndReturnsOk()
    {
        // Arrange
        var blogPost = new CreateBlogHttpPostModel
        {
            Title = "Test Blog",
            IsOpen = true
        };
        _mockBlogService.Setup(service => service.Create(blogPost))
            .ReturnsAsync(ResponseService.Ok());

        // Mock the user context to simulate a logged-in user
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
        }));
        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _controller.Create(blogPost);

        // Assert
        _mockBlogService.Verify(service => service.Create(blogPost), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }

    
    [Fact]
    public async Task BlogList_ReturnsViewWithCorrectModel()
    {
        // Arrange
        var userId = 1L;
        var mockBlogs = new List<BlogEntity>
        {
            // Populate with test data
        };
        _mockBlogService.Setup(service => service.GetAllByUserId(userId))
            .ReturnsAsync(mockBlogs);
        _mockHttpContext.SetupGet(x => x.User).Returns(_user);

        // Act
        var result = await _controller.BlogList(userId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<BlogListHttpGetViewModel>(viewResult.Model);
        Assert.Equal(mockBlogs, model.Blogs);
    }

    [Fact]
    public async Task BlogAllList_ReturnsViewWithCorrectModel()
    {
        // Arrange
        var mockBlogs = new List<BlogEntity>
        {
            // Populate with test data
        };
        _mockBlogService.Setup(service => service.GetAll())
            .ReturnsAsync(mockBlogs);

        // Act
        var result = await _controller.BlogAllList();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<BlogListHttpGetViewModel>(viewResult.Model);
        Assert.Equal(mockBlogs, model.Blogs);
    }

    [Fact]
    public async Task BlogDetails_WithValidId_ReturnsViewWithCorrectModel()
    {
        // Arrange
        var blogId = 1L;
        var mockBlog = new BlogEntity
        {
            // Populate with test data
        };
        _mockBlogService.Setup(service => service.GetById(blogId))
            .ReturnsAsync(ResponseService<BlogEntity>.Ok(mockBlog));

        // Act
        var result = await _controller.BlogDetails(blogId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<BlogDetailsHttpGetViewModel>(viewResult.Model);
        Assert.False(model.IsError);
        Assert.Equal(mockBlog, model.Blog);
    }

    [Fact]
    public async Task BlogDetails_WithInvalidId_ReturnsViewWithError()
    {
        // Arrange
        var blogId = 1L;
        _mockBlogService.Setup(service => service.GetById(blogId))
            .ReturnsAsync(ResponseService<BlogEntity>.Error("No blog with that id"));

        // Act
        var result = await _controller.BlogDetails(blogId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<BlogDetailsHttpGetViewModel>(viewResult.Model);
        Assert.True(model.IsError);
        Assert.Equal("No blog with that id", model.ErrorMessage);
    }
    
}
