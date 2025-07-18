using APIAvatier.Domain.Entities;
using APIAvatier.Infra.Data.Maps;
using Microsoft.EntityFrameworkCore;

namespace APIAvatier.Infra.Data.Contexts
{
  public class Context : DbContext
  {
    public Context(DbContextOptions<Context> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfiguration(new UserMap());
    }
  }
}