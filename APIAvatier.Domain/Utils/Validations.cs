using System.Net.Mail;

namespace APIAvatier.Domain.Utils
{
  public class Validations
  {
    public static bool EmailValid(string email)
    {
      try
      {
        if (!StringValid(email))
          return false;

        var mailAddress = new MailAddress(email);
        return mailAddress.Address == email;
      }
      catch (FormatException)
      {
        return false;
      }
    }
    public static bool StringValid(string text) 
    {
      text = text.Trim();

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