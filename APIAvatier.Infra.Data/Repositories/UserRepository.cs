using APIAvatier.Domain.Entities;
using APIAvatier.Domain.Interfaces;
using APIAvatier.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace APIAvatier.Infra.Data.Repositories
{
  public class UserRepository : BaseRepository<User>, IUserRepository
  {
    public UserRepository(Context contexto) : base(contexto)
    {
    }
    public async Task<User?> GetUserAndRoles(int userId)
    {
      try
      {
        return await _db.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == userId);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}