using AutoMapper;

using Contracts;

using Data.DTO;
using Data.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Repository.Interfaces;

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace AspImp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IAuthenticationManager _authManager;
    public AuthenticationController(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager)
    {
      _logger = logger;
      _mapper = mapper;
      _authManager = authManager;
      _userManager = userManager;
    }

    /// <summary>
    /// register api
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///  POST /api/authentication/register
    ///  {
    ///     "firstname": "Jonh",
    ///     "lastname": "Doe",
    ///     "username": "JDoe",
    ///     "email": "phamhieutb.dev@gmail.com",
    ///     "password": "Password100;",
    ///     "phonenumber": "33333",
    ///     "roles": [ "User"], //Default will be user
    ///   }
    ///   
    /// </remarks>
    /// <param name="userRegistry"></param>
    /// <returns></returns>
    /// <response code="201">if user is valid and created</response>
    /// <response code="400">If any property is not correct</response>  
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userRegistry)
    {
      User user = _mapper.Map<User>(userRegistry);

      if (ModelState.IsValid)
      {
        try
        {
          if (userRegistry.Roles == null || !userRegistry.Roles.Any())
          {
            userRegistry.Roles = new string[] { "User" };
          }
          var response = await _userManager.CreateAsync(user, userRegistry.Password);

          if (!response.Succeeded)
          {
            foreach (var error in response.Errors)
            {
              ModelState.TryAddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
          }

          await _userManager.AddToRolesAsync(user, userRegistry.Roles);

          return StatusCode(201);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
      }
      else
      {
        return UnprocessableEntity(ModelState);
      }
    }

    /// <summary>
    /// login api
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///    POST /Todo
    ///    {
    ///        "id": 1,
    ///        "name": "Item1",
    ///        "isComplete": true
    ///     }
    ///
    ///   
    /// </remarks>
    /// <param name="user"></param>
    /// <returns>Token</returns>
    /// <response code="200">Returns token of user</response>
    /// <response code="400">If the pass or username is imcorrect</response>  
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
    {
      try
      {
        bool validUser = await _authManager.ValidateUser(user);
        string token = string.Empty;

        if (!validUser)
        {
          _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
          return Unauthorized();
        }

        token = await _authManager.CreateToken();

        return Ok((new { accessToken = token}));
      }
      catch(Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    /// <summary>
    /// reset password api
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///  POST /api/authentication/reset-password
    ///  {
    ///     "recentPassword": "Password100",
    ///     "newPassword": "Password101",
    ///     "confirmPassword": "Password101"
    ///  }
    ///  
    /// </remarks>
    /// <param name="resetPasswordDto"></param>
    /// <returns></returns>
    /// <response code="200">Returns new token of user</response>
    /// <response code="400">If the pass is imcorrect or newpass is not matched with confirm pass</response>  
    /// <response code="401">If user didn't login </response>  
    [HttpPost("reset-password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
      if (!ModelState.IsValid)
      {
        _logger.LogError("Invalid model state for the ResetPasswordDto object");
        return UnprocessableEntity(ModelState);
      }

      Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
      User user = await _userManager.FindByNameAsync(userId.Value);

      if (user != null && await _userManager.CheckPasswordAsync(user, resetPasswordDto.RecentPassword))
      {
        string token = await (_authManager.CreateToken());

        return Ok(token);
      }

      return BadRequest("Pass is not correct!");
    }

  }
}
