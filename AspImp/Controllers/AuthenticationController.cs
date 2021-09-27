using AutoMapper;

using Contracts;

using Data.DTO;
using Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Repository.Interfaces;

using System.Threading.Tasks;

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

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userRegistry)
    {
      User user = _mapper.Map<User>(userRegistry);

      if(ModelState.IsValid)
      {
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

      return UnprocessableEntity(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
    {
      bool validUser = await _authManager.ValidateUser(user);
      string token = string.Empty;

      if (!validUser)
      {
        _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
        return Unauthorized();
      }

      token = await (_authManager.CreateToken());

      return Ok(token);
    }

  }
}
