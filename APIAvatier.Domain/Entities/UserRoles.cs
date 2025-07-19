namespace APIAvatier.Domain.Entities
{
  public class UserRoles
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Role { get; set; } = string.Empty;
    public User User { get; set; } = new User();
  }
}