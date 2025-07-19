using APIAvatier.Infra.Data.Contexts;
using APIAvatier.Infra.Data.IoC;
using APIAvatier.Services.IoC;
using Microsoft.EntityFrameworkCore;

namespace APIAvatier.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      InfraInjection.Register(builder.Services);
      ServiceInjection.Register(builder.Services);

      builder.Services.AddEndpointsApiExplorer();

      builder.Services.AddSwaggerGen();

      builder.Services.AddDbContext<Context>(options => options.UseInMemoryDatabase("AvatierDB"));

      builder.Services.AddControllers()
        .AddNewtonsoftJson(
            options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
          );

      var app = builder.Build();

      app.UseSwagger();
      app.UseSwaggerUI();

      app.UseHttpsRedirection();

      app.MapControllers();

      app.Run();
    }
  }
}
