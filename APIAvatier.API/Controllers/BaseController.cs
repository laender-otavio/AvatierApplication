using APIAvatier.Domain.DTOs;
using APIAvatier.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APIAvatier.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Produces("application/json")]
  public class BaseController : ControllerBase
  {
    protected HttpStatusCode GetStatusCodeByException(Exception ex)
    {
      return ex switch
      {
        NotFoundException => HttpStatusCode.NotFound,
        ArgumentNullException => HttpStatusCode.BadRequest,
        ArgumentException => HttpStatusCode.BadRequest,
        InvalidOperationException => HttpStatusCode.BadRequest,
        UnauthorizedAccessException => HttpStatusCode.Unauthorized,
        DbUpdateException => HttpStatusCode.Conflict,
        NotImplementedException => HttpStatusCode.NotImplemented,
        TimeoutException => HttpStatusCode.ServiceUnavailable,
        _ => HttpStatusCode.InternalServerError
      };
    }
    protected ObjectResult GetResult(HttpStatusCode statusCode, string message = "", object? data = null) =>
      StatusCode((int)statusCode, statusCode == HttpStatusCode.NoContent ? null : new ResponseHttp
      {
        StatusCode = (int)statusCode,
        Success = IsSuccessStatusCode(statusCode),
        Message = string.IsNullOrWhiteSpace(message) ? GetMessageByStatusCode(statusCode) : message,
        Data = data
      });
    #region Métodos Private
    private static bool IsSuccessStatusCode(HttpStatusCode statusCode) => (int)statusCode >= 200 && (int)statusCode <= 299;
    private static string GetMessageByStatusCode(HttpStatusCode statusCode)
    {
      return statusCode switch
      {
        HttpStatusCode.OK => "Request completed successfully.",
        HttpStatusCode.Created => "Record created successfully.",
        HttpStatusCode.NotFound => "Record not found.",
        HttpStatusCode.UnprocessableEntity => "Request processing error.",
        HttpStatusCode.InternalServerError => "Internal server error.",
        HttpStatusCode.Unauthorized => "Authorization error.",
        _ => ""
      };
    }
    #endregion
  }
}