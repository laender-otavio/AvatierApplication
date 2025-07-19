using APIAvatier.Domain.Entities;

namespace APIAvatier.Domain.Interfaces
{
  public interface IUserRepository : IBaseRepository<User>
  {
    Task<User?> GetUserAndRoles(int userId);
  }
}