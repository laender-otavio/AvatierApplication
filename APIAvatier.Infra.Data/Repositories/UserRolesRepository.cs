using APIAvatier.Domain.Entities;
using APIAvatier.Domain.Interfaces;
using APIAvatier.Infra.Data.Contexts;

namespace APIAvatier.Infra.Data.Repositories
{
  public class UserRolesRepository : BaseRepository<UserRoles>, IUserRolesRepository
  {
    public UserRolesRepository(Context contexto) : base(contexto)
    {
    }
  }
}