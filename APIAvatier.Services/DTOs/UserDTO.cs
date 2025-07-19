namespace APIAvatier.Services.DTOs
{
  public record UserDTO(
    string Name,
    string Password,
    string Email);
}