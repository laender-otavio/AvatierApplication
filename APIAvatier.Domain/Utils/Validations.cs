using System.Net.Mail;

namespace APIAvatier.Domain.Utils
{
  public class Validations
  {
    public static bool EmailValid(string email)
    {
      try
      {
        if (!StringValid(email, 6, 100))
          return false;

        var mailAddress = new MailAddress(email);

        if (!mailAddress.Host.Contains('.'))
          return false;

        return mailAddress.Address == email;
      }
      catch (FormatException)
      {
        return false;
      }
    }
    public static bool RoleValid(ref string role)
    {
      if (!StringValid(role))
        return false;

      role = role.ToUpper().Trim();
      var validRoles = new List<string> { "ADMIN", "SELLER", "RECEPTIONIST" };
      return validRoles.Contains(role);
    }
    public static bool StringValid(string text)
    {
      if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
        return false;

      return true;
    }
    public static bool StringValid(string text, int minimumLength, int maximumLength)
    {
      if (!StringValid(text))
        return false;

      if (text.Length < minimumLength || text.Length > maximumLength)
        return false;

      return true;
    }
  }
}