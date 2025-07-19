using APIAvatier.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIAvatier.Infra.Data.Maps
{
  public class UserRolesMap : BaseMap<UserRoles>
  {
    public override void Configure(EntityTypeBuilder<UserRoles> builder)
    {
      base.Configure(builder);

      builder.ToTable("users_roles");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd().HasColumnName("Id").HasColumnType("int");
      builder.Property(x => x.UserId).IsRequired().HasColumnName("UserId").HasColumnType("int");
      builder.Property(x => x.Role).IsRequired().HasColumnName("Role").HasColumnType("varchar(12)").HasMaxLength(12);

      builder.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
    }
  }
}