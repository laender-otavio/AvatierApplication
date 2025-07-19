using APIAvatier.Domain.Entities;
using APIAvatier.Domain.Exceptions;
using APIAvatier.Domain.Interfaces;
using APIAvatier.Services.DTOs;
using APIAvatier.Services.Interfaces;
using APIAvatier.Services.Services;
using Moq;

namespace APIAvatier.Test
{
  public class UserTests
  {
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IUserRolesRepository> _mockUserRolesRepository;
    private readonly IUserService _userService;
    public UserTests()
    {
      _mockUserRepository = new Mock<IUserRepository>();
      _mockUserRolesRepository = new Mock<IUserRolesRepository>();
      _userService = new UserService(_mockUserRepository.Object, _mockUserRolesRepository.Object);
    }
    #region Create User
    [Theory]
    [InlineData("     ")] // White Spaces
    [InlineData("")] // Empty
    [InlineData("short")] // Short name
    [InlineData("thisisaveeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeerylongnameforatestthatshouldfail")] // Long name
    public async Task Create_InvalidName_ThrowsArgumentException(string invalidName)
    {
      var userDto = new UserDTO(invalidName, "validpassword123", "test@example.com");
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.Create(userDto));
      Assert.Equal("Name invalid.", exception.Message);
    }
    [Theory]
    [InlineData("     ")] // White Spaces
    [InlineData("")] // Empty
    [InlineData("short")] // Short password
    [InlineData("thisisaveeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeerylongpasswordforatestthatshouldfail")] // Long password
    public async Task Create_InvalidPassword_ThrowsArgumentException(string invalidPassword)
    {
      var userDto = new UserDTO("validName", invalidPassword, "test@example.com");
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.Create(userDto));
      Assert.Equal("Password invalid.", exception.Message);
    }
    [Theory]
    [InlineData("     ")] // White Spaces
    [InlineData("")] // Empty
    [InlineData("invalid-email")] // Invalid Format
    [InlineData("test@")] // Without domain
    [InlineData("test@.com")] // Invalid domain
    [InlineData("test@domain")] // Without TLD (ex: .com)
    [InlineData("a@b.c")] // Short email
    [InlineData("thisisaveeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeerylongemail@gmail.com")] // Long email
    public async Task Create_InvalidEmail_ThrowsArgumentException(string invalidEmail)
    {
      var userDto = new UserDTO("validName", "validpassword123", invalidEmail);
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.Create(userDto));
      Assert.Equal("Email invalid.", exception.Message);
    }
    [Fact]
    public async Task Create_ValidUser_ReturnsUser()
    {
      var userDto = new UserDTO("validName", "validPassword", "test@example.com");

      var expectedUser = new User
      {
        Name = userDto.Name,
        Password = userDto.Password,
        Email = userDto.Email
      };

      _mockUserRepository.Setup(r => r.Add(It.IsAny<User>())).ReturnsAsync(expectedUser);

      var result = await _userService.Create(userDto);
      Assert.NotNull(result);
      Assert.IsType<User>(result);
      Assert.Equal(userDto.Name, result.Name);
      Assert.Equal(userDto.Email, result.Email);
      Assert.Equal(userDto.Password, result.Password);

      _mockUserRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
    }
    #endregion
    #region AssignRole
    [Theory]
    [InlineData("     ")] // White Spaces
    [InlineData("")] // Empty
    [InlineData("Administrator")] // Not into defined values "ADMIN", "SELLER", "RECEPTIONIST"
    public async Task AssignRole_InvalidRole_ThrowsArgumentException(string invalidRole)
    {
      var roleDTO = new RoleDTO(invalidRole);
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.AssignRole(1, roleDTO));
      Assert.Equal("Role invalid.", exception.Message);
    }
    [Fact]
    public async Task AssignRole_InvalidUser_ThrowsArgumentException()
    {
      var nonExistentUserId = 2;
      var roleDto = new RoleDTO("ADMIN");
      var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userService.AssignRole(nonExistentUserId, roleDto));
      Assert.Equal($"User with ID {nonExistentUserId} not found.", exception.Message);
    }
    [Fact]
    public async Task AssignRole_AddDuplicateRole_ThrowsArgumentException()
    {
      var userId = 1;
      var roleName = "ADMIN";

      var user = new User
      {
        Id = userId,
        Name = "validName",
        Password = "validPassword",
        Email = "test@example.com"
      };

      _mockUserRepository.Setup(repo => repo.GetUserAndRoles(userId)).ReturnsAsync(user);

      _mockUserRolesRepository
        .Setup(repo => repo.Add(It.IsAny<UserRoles>())).Callback<UserRoles>(ur => user.UserRoles.Add(ur)).Returns(Task.FromResult(new UserRoles()));

      await _userService.AssignRole(userId, new RoleDTO(roleName));

      var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.AssignRole(userId, new RoleDTO(roleName)));

      Assert.Equal($"User with ID {userId} already has the role {roleName}.", exception.Message);
    }
    [Fact]
    public async Task AssignRole_NewRoleAssignedSuccessfully_NoExceptionThrown()
    {
      var userId = 1;
      var roleName = "ADMIN";

      var user = new User
      {
        Id = userId,
        Name = "validName",
        Password = "validPassword",
        Email = "test@example.com",
        UserRoles = []
      };

      _mockUserRepository.Setup(repo => repo.GetUserAndRoles(userId)).ReturnsAsync(user);

      _mockUserRolesRepository
          .Setup(repo => repo.Add(It.IsAny<UserRoles>()))
          .Callback<UserRoles>(ur => user.UserRoles.Add(ur))
          .Returns(Task.FromResult(new UserRoles()));

      await _userService.AssignRole(userId, new RoleDTO(roleName));

      var rolesPresentInUser = user.UserRoles.Select(ur => ur.Role);
      Assert.Contains(roleName, rolesPresentInUser);
    }
    #endregion
    #region ReturnUserById
    [Fact]
    public async Task ReturnUserById_InvalidUser_ThrowsNotFoundException()
    {
      var nonExistentUserId = 1;
      var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userService.ReturnUserById(nonExistentUserId));
      Assert.Equal($"User with ID {nonExistentUserId} not found.", exception.Message);
    }
    [Fact]
    public async Task ReturnUserById_ValidUser_ReturnsUserWithoutRoles()
    {
      var userId = 1;
      var expectedUser = new User
      {
        Id = userId,
        Name = "User Without Roles",
        Password = "hashedpassword",
        Email = "noroles@example.com",
        UserRoles = []
      };

      _mockUserRepository.Setup(repo => repo.GetUserAndRoles(userId)).ReturnsAsync(expectedUser);

      var actualUser = await _userService.ReturnUserById(userId);

      Assert.NotNull(actualUser);
      Assert.Equal(expectedUser.Id, actualUser.Id);
      Assert.Equal(expectedUser.Name, actualUser.Name);
      Assert.Equal(expectedUser.Password, actualUser.Password);
      Assert.Equal(expectedUser.Email, actualUser.Email);
      Assert.Empty(actualUser.UserRoles);
    }
    [Fact]
    public async Task ReturnUserById_ValidUser_ReturnsUserWithRoles()
    {
      var userId = 1;
      var roleName = "ADMIN";
      var expectedUser = new User
      {
        Id = userId,
        Name = "User With Roles",
        Password = "hashedpassword",
        Email = "withroles@example.com",
        UserRoles = [new UserRoles { UserId = userId, Role = roleName }]
      };

      _mockUserRepository.Setup(repo => repo.GetUserAndRoles(userId)).ReturnsAsync(expectedUser);

      var actualUser = await _userService.ReturnUserById(userId);

      Assert.NotNull(actualUser);
      Assert.Equal(expectedUser.Id, actualUser.Id);
      Assert.Equal(expectedUser.Name, actualUser.Name);
      Assert.Equal(expectedUser.Password, actualUser.Password);
      Assert.Equal(expectedUser.Email, actualUser.Email);
      Assert.Contains(roleName, actualUser.UserRoles.Select(ur => ur.Role));
      Assert.Single(actualUser.UserRoles);
    }
    #endregion
  }
}