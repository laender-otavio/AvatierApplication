using APIAvatier.Domain.Entities;
using APIAvatier.Domain.Interfaces;
using APIAvatier.Infra.Data.Contexts;

namespace APIAvatier.Infra.Data.Repositories
{
  public class UserRepository : BaseRepository<User>, IUserRepository
  {
    public UserRepository(Context contexto) : base(contexto)
    {
    }
  }
}