namespace APIAvatier.Domain.DTOs
{
  public class ResponseHttp
  {
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }
  }
}