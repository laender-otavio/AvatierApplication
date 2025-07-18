using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIAvatier.Infra.Data.Maps
{
  public class BaseMap<T> : IEntityTypeConfiguration<T> where T : class
  {
    public BaseMap() { }
    public virtual void Configure(EntityTypeBuilder<T> builder) { }
  }
}