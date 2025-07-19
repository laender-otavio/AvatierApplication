using APIAvatier.Domain.Entities;
using APIAvatier.Domain.Interfaces;
using APIAvatier.Domain.Utils;
using APIAvatier.Services.DTOs;
using APIAvatier.Services.Interfaces;

namespace APIAvatier.Services.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    { 
      _userRepository = userRepository;
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
  }
}