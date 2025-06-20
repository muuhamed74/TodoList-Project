
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Todo.Domain.Entities;
using Todo.Domain.Services;
using Todo.Serv;
using TodoApi.DTOs;
using Xunit;
using Todo.Domain.Entities.Identity;



public class TodoItemServiceTests
    {
        private readonly Mock<ITodoItemRepository> _repoMock;
        private readonly Mock<UserManager<Appuser>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly TodoItemService _service;

        public TodoItemServiceTests()
        {
            _repoMock = new Mock<ITodoItemRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            //  Mock setting for  UserManager<AppUser>
            var userStoreMock = new Mock<IUserStore<Appuser>>();
            _userManagerMock = new Mock<UserManager<Appuser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null
            );

            // Fake Http setting (fake HttpContext) with email
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, "test@email.com")
        };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext { User = principal };
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);

            _service = new TodoItemService(
                _repoMock.Object,
                _httpContextAccessorMock.Object,
                _mapperMock.Object,
                _userManagerMock.Object
                
            );

            _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new Appuser { Email = "test@email.com" });
        }

      [Fact]
    public async Task CreateAsync_ShouldAddItemAndReturnDto()
    {
        // Arrange
        var dto = new CreateTodoItemDto { Title = "Test Task", Description = "Desc" };
        var todoItem = new TodoItem { Id = 1, Title = "Test Task", UserEmail = "test@email.com" };
        var todoItemDto = new TodoItemDto { Id = 1, Title = "Test Task" };

        _mapperMock.Setup(m => m.Map<TodoItem>(dto)).Returns(todoItem);
        _repoMock.Setup(r => r.AddAsync(todoItem)).Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);
        _mapperMock.Setup(m => m.Map<TodoItemDto>(todoItem)).Returns(todoItemDto);
        _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new Appuser());

        // Act
        var result = await _service.CreateAsync(dto, "test@email.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(todoItemDto.Id, result.Id);
        _repoMock.Verify(r => r.AddAsync(It.IsAny<TodoItem>()), Times.Once);
    }
  }
    






