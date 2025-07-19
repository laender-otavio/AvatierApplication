using APIAvatier.Domain.Entities;
using APIAvatier.Domain.Exceptions;
using APIAvatier.Domain.Interfaces;
using APIAvatier.Domain.Utils;
using APIAvatier.Services.DTOs;
using APIAvatier.Services.Interfaces;

namespace APIAvatier.Services.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly IUserRolesRepository _userRolesRepository;
    public UserService(IUserRepository userRepository, IUserRolesRepository userRolesRepository)
    {
      _userRepository = userRepository;
      _userRolesRepository = userRolesRepository;
    }
    public async Task<User> Create(UserDTO user)
    {
      if (!Validations.StringValid(user.Name, 6, 100))
        throw new ArgumentException("Name invalid.");

      if (!Validations.StringValid(user.Password, 6, 100))
        throw new ArgumentException("Password invalid.");

      if (!Validations.EmailValid(user.Email))
        throw new ArgumentException("Email invalid.");

      return await _userRepository.Add(new User
      {
        Name = user.Name,
        Password = user.Password,
        Email = user.Email
      });
    }
    public async Task AssignRole(int userId, RoleDTO roleDTO)
    {
      var role = roleDTO.Role;

      if (!Validations.RoleValid(ref role))
        throw new ArgumentException("Role invalid.");

      var user = await ReturnUserById(userId);

      if (user.UserRoles.Any(ur => ur.Role.Equals(role)))
        throw new ArgumentException($"User with ID {userId} already has the role {role}.");

      await _userRolesRepository.Add(new UserRoles 
      {
        User = user,
        Role = role
      });
    }
    public async Task<User> ReturnUserById(int userId)
    {
      return await _userRepository.GetUserAndRoles(userId) ??
        throw new NotFoundException($"User with ID {userId} not found.");
    }
  }
}