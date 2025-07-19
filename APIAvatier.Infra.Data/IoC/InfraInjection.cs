using APIAvatier.Domain.Interfaces;
using APIAvatier.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace APIAvatier.Infra.Data.IoC
{
  public class InfraInjection
  {
    public static void Register(IServiceCollection services)
    {
      services.AddScoped<IUserRepository, UserRepository>();
    }
  }
}