using APIAvatier.Services.Services;

namespace APIAvatier.Test
{
  public class LoginTests
  {
    private readonly LoginService _loginService;
    public LoginTests()
    {
      _loginService = new LoginService();
    }
    [Fact]
    public async Task Login_AlwaysReturnsSuccessMessage()
    {
      var result = await _loginService.Login();

      Assert.NotNull(result);
      Assert.Equal("Login successful.", result);
    }
  }
}