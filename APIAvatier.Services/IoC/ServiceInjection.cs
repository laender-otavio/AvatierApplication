using APIAvatier.Services.Interfaces;
using APIAvatier.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace APIAvatier.Services.IoC
{
  public class ServiceInjection
  {
    public static void Register(IServiceCollection services)
    {
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<ILoginService, LoginService>();
    }
  }
}