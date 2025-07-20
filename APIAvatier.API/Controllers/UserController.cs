using APIAvatier.Domain.DTOs;
using APIAvatier.Services.DTOs;
using APIAvatier.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIAvatier.API.Controllers
{
  [Route("api/users")]
  public class UserController : BaseController
  {
    private readonly IUserService _service;
    public UserController(IUserService service) => _service = service;
    /// <summary>
    /// Endpoint that creates new user
    /// </summary>
    /// <param name="value"></param>
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<ResponseHttp>> CreateUser([FromBody] UserDTO value)
    {
      try
      {
        return GetResult(HttpStatusCode.Created, string.Empty, await _service.Create(value));
      }
      catch (Exception ex)
      {
        return GetResult(GetStatusCodeByException(ex), ex.Message, ex);
      }
    }
    /// <summary>
    /// Endpoint that assign a role to an user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    [AllowAnonymous]
    [HttpPost("{id}/roles")]
    public async Task<ActionResult<ResponseHttp>> AssignRole(int id, [FromBody] RoleDTO value)
    {
      try
      {
        await _service.AssignRole(id, value);
        return GetResult(HttpStatusCode.NoContent);
      }
      catch (Exception ex)
      {
        return GetResult(GetStatusCodeByException(ex), ex.Message, ex);
      }
    }
    /// <summary>
    /// Endpoint that returns an user with roles
    /// </summary>
    /// <param name="id"></param>
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseHttp>> ReturmUserWithRoles(int id)
    {
      try
      {
        return GetResult(HttpStatusCode.OK, string.Empty, await _service.ReturnUserById(id));
      }
      catch (Exception ex)
      {
        return GetResult(GetStatusCodeByException(ex), ex.Message, ex);
      }
    }
  }
}
