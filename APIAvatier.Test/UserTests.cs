using APIAvatier.Domain.Entities;
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
    private readonly IUserService _userService;
    public UserTests() 
    {
      _mockUserRepository = new Mock<IUserRepository>();
      _userService = new UserService(_mockUserRepository.Object);
    }
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
  }
}