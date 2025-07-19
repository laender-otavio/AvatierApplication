using APIAvatier.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIAvatier.Infra.Data.Maps
{
  public class UserMap : BaseMap<User>
  {
    public override void Configure(EntityTypeBuilder<User> builder)
    {
      base.Configure(builder);

      builder.ToTable("users");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd().HasColumnName("Id").HasColumnType("int");
      builder.Property(x => x.Name).IsRequired().HasColumnName("Name").HasColumnType("varchar(100)").HasMaxLength(100);
      builder.Property(x => x.Password).IsRequired().HasColumnName("Password").HasColumnType("varchar(100)").HasMaxLength(100);
      builder.Property(x => x.Email).IsRequired().HasColumnName("Email").HasColumnType("varchar(100)").HasMaxLength(100);
    }
  }
}