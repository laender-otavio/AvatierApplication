using APIAvatier.Services.Interfaces;

namespace APIAvatier.Services.Services
{
  public class LoginService : ILoginService
  {
    public async Task<string> Login()
    {
      await Task.Delay(1);
      return "Login successful.";
    }
  }
}