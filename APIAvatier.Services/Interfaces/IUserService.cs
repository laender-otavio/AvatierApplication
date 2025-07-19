using APIAvatier.Domain.Entities;
using APIAvatier.Services.DTOs;

namespace APIAvatier.Services.Interfaces
{
  public interface IUserService
  {
    Task<User> Create(UserDTO user);
    Task AssignRole(int userId, RoleDTO roleDTO);
    Task<User> ReturnUserById(int userId);
  }
}