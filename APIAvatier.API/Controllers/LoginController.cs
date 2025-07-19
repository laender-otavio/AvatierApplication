using APIAvatier.Domain.DTOs;
using APIAvatier.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIAvatier.API.Controllers
{
  [Route("api/login")]
  public class LoginController : BaseController
  {
    private readonly ILoginService _service;
    public LoginController(ILoginService service) => _service = service;
    /// <summary>
    /// Endpoint that logs in
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<ResponseHttp>> Login()
    {
      try
      {
        return GetResult(HttpStatusCode.OK, string.Empty, await _service.Login());
      }
      catch (Exception ex)
      {
        return GetResult(GetStatusCodeByException(ex), ex.Message, ex);
      }
    }
  }
}
