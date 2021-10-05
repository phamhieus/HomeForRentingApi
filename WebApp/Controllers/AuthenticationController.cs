using AspImp.Services;
using AspImp.SwaggerExample;

using AutoMapper;

using Contracts;

using Data.Common;
using Data.DTO;
using Data.DTO.Responses;
using Data.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Repository.Interfaces;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
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
    private readonly IRepositoryManager _repositoryManager;
    private readonly FileService _fileService;

    public AuthenticationController(
      IMapper mapper,
      ILoggerManager logger,
      FileService fileService,
      UserManager<User> userManager,
      IAuthenticationManager authManager,
      IRepositoryManager repositoryManager)
    {
      _logger = logger;
      _mapper = mapper;
      _fileService = fileService;
      _authManager = authManager;
      _userManager = userManager;
      _repositoryManager = repositoryManager;
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
    [SwaggerRequestExample(typeof(UserForRegistrationDto), typeof(RegisterRequestExample))]
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

          return StatusCode(StatusCodes.Status201Created, user);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
      }
      else
      {
        return BadRequest(ModelState);
      }
    }

    /// <summary>
    /// login api
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Token</returns>
    /// <response code="200">Returns token of user</response>
    /// <response code="400">If the pass or username is imcorrect</response>  
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerRequestExample(typeof(UserForAuthenticationDto), typeof(RegisterRequestExample))]
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

        return Ok((new { accessToken = token }));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    /// <summary>
    /// reset password api
    /// </summary>
    /// <param name="resetPasswordDto"></param>
    /// <returns></returns>
    /// <response code="200">Returns new token of user</response>
    /// <response code="400">If the pass is imcorrect or newpass is not matched with confirm pass</response>  
    /// <response code="401">If user didn't login </response>  
    [Authorize]
    [HttpPut("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerRequestExample(typeof(ResetPasswordDto), typeof(ResetPasswordRequestExample))]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
      if (!ModelState.IsValid)
      {
        _logger.LogError("Invalid model state for the ResetPasswordDto object");
        return UnprocessableEntity(ModelState);
      }

      Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
      User user = await _userManager.FindByIdAsync(userId.Value);

      if (user != null && await _userManager.CheckPasswordAsync(user, resetPasswordDto.RecentPassword))
      {
        if(resetPasswordDto.RecentPassword != resetPasswordDto.NewPassword)
        {
          var result = await _userManager.ChangePasswordAsync(user, resetPasswordDto.RecentPassword, resetPasswordDto.NewPassword);

          if (result == IdentityResult.Success)
          {
            return Ok("Change password success!");
          }

          return BadRequest(result);
        }

        return BadRequest("RecentPassword doesn't allow to be matched NewPassword!");
      }

      return BadRequest("Pass is not correct!");
    }

    /// <summary>
    /// get information of current user (Cần đăng nhập, có access token)
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///  Get /api/authentication/getme
    ///  
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Returns information of current user</response>
    /// <response code="401">If user didn't login </response>  
    [Authorize]
    [HttpGet("get-me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DetailUserRequestReponseExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DetailUserRequestReponseExample))]
    public async Task<IActionResult> GetMe()
    {
      Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
      User user = await _userManager.FindByIdAsync(userId.Value);
      UserImage userImage = _repositoryManager.UserImage.GetUserThumbnail(user.Id, false);

      UserDetailResponse userDetailResponse = _mapper.Map<UserDetailResponse>(user);
      UserImageDto userImageDto = _mapper.Map<UserImageDto>(userImage);

      userDetailResponse.ThumbnailImage = userImageDto;

      return Ok(userDetailResponse);
    }


    /// <summary>
    /// update information of current user (Gom tên đăng nhập, số điện thoại, không cập nhật gmail)
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///  Get /api/authentication/getme
    ///  
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Returns information of current user</response>
    /// <response code="401">If user didn't login </response>  
    [Authorize]
    [HttpPut("user-infomation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerRequestExample(typeof(UserUpdateDto), typeof(UserUpdateRequestExample))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DetailUserRequestReponseExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DetailUserRequestReponseExample))]
    public async Task<IActionResult> UpdateUserInfor(UserUpdateDto userUpdateDto)
    {
      if (!ModelState.IsValid)
      {
        _logger.LogError("Invalid model state for the UserUpdateDto object");
        return UnprocessableEntity(ModelState);
      }

      Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
      User user = await _userManager.FindByIdAsync(userId.Value);
      UserImage userImage = _repositoryManager.UserImage.GetUserThumbnail(user.Id, false);

      _mapper.Map(userUpdateDto, user);
      await _userManager.UpdateAsync(user);

      UserDetailResponse userDetailResponse = _mapper.Map<UserDetailResponse>(user);
      UserImageDto userImageDto = _mapper.Map<UserImageDto>(userImage);

      userDetailResponse.ThumbnailImage = userImageDto;

      return Ok(userDetailResponse);
    }

    [Authorize]
    [HttpPut("user-thumbnail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DetailUserRequestReponseExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DetailUserRequestReponseExample))]
    public async Task<IActionResult> UploadThumbnail()
    {
      try
      {
        var imageFiles = this.Request.Form.Files;

        if (imageFiles == null || !imageFiles.Any() || imageFiles.Count > 1)
        {
          return BadRequest("Please, upload a single file");
        }

        Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        User user = await _userManager.FindByIdAsync(userId.Value);
        UserImage previousThumbnail = _repositoryManager.UserImage.GetUserThumbnail(user.Id, false);

        UserImage userImage = new UserImage()
        {
          FileType = Data.Common.ImageType.Thumbnail,
          FileExtension = Path.GetExtension(imageFiles[0].FileName),
          UserId = user.Id,

          IsActive = true,
          CreateDate = DateTime.Now,
          CreatedBy = user.Id,
          UpdateDate = DateTime.Now,
          UpdatedBy = user.Id
        };

        if (previousThumbnail != null)
        {
          previousThumbnail.IsActive = false;
          _repositoryManager.UserImage.UpdateUserImage(previousThumbnail);
        }

        _repositoryManager.UserImage.CreateUserImage(userImage);
        _repositoryManager.Save();

        string fileName = Constant.RemoveSign4VietnameseString(user.UserName);

        fileName = Regex.Replace(fileName, @"[^0-9a-z-A-Z ]", "");
        fileName = fileName.Replace(" ", "-") + "-";
        fileName += userImage.Id;
        fileName += userImage.FileExtension;

        userImage.FileName = fileName;
        userImage.FilePath = Constant.GetImageRoomPath(userImage.FileName);

        _repositoryManager.UserImage.UpdateUserImage(userImage);
        _repositoryManager.Save();

        _fileService.WriteImageFile(userImage.FileName, Constant.ROOM_DIRECTORY, imageFiles[0]);

        UserDetailResponse userDetailResponse = _mapper.Map<UserDetailResponse>(user);
        userDetailResponse.ThumbnailImage = _mapper.Map<UserImageDto>(userImage);

        return Ok(userDetailResponse);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }
  }
}
