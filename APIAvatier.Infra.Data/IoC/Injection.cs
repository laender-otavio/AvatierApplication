using APIAvatier.Domain.Interfaces;
using APIAvatier.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace APIAvatier.Infra.Data.IoC
{
  public class Injection
  {
    public static void Registrar(IServiceCollection services)
    {
      services.AddScoped<IUserRepository, UserRepository>();
    }
  }
}