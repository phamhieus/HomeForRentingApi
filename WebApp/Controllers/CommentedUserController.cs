using AspImp.Services.Interfaces;

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
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspImp.Controllers
{
  [Route("api/[controller]")]
  [ApiController, Authorize]
  public class CommentedUserController : ControllerBase
  {
    private readonly INotificationsService _notificationsService;
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public CommentedUserController(
      IRepositoryManager repository, 
      ILoggerManager logger, 
      IMapper mapper,
      INotificationsService notificationsService,
      UserManager<User> userManager)
    {
      _notificationsService = notificationsService;
      _userManager = userManager;
      _repository = repository;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      try
      {
        IEnumerable<CommentedUser> commentedUsers = _repository.CommentedUser.GetAllCommentedUsers(trackChanges: false);
        IEnumerable<CommentedUserDto> commentedUserDtos = _mapper.Map<IEnumerable<CommentedUserDto>>(commentedUsers);

        return Ok(commentedUserDtos);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong in the {nameof(GetAll)} action {ex}");
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpGet("{id}")]
    public IActionResult GetCommentedUserById(Guid id)
    {
      try
      {
        var commentedUser = _repository.CommentedUser.GetCommentedUser(id, trackChanges: false);
        var commentedUserDto = _mapper.Map<IEnumerable<CommentedUser>>(commentedUser);

        return Ok(commentedUserDto);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong in the {nameof(GetCommentedUserById)} action {ex}");

        return StatusCode(500, "Internal server error");
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommentedUser([FromBody] CommentedUserDto commentedUserDto)
    {
      if (commentedUserDto == null)
      {
        _logger.LogError("EmployeeForCreationDto object sent from client is null.");
        return BadRequest("EmployeeForCreationDto object is null");
      }

      if (!ModelState.IsValid)
      {
        _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
        return UnprocessableEntity(ModelState);
      }

      Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
      User user = await _userManager.FindByIdAsync(userId.Value);

      CommentedUser commentedUser = _mapper.Map<CommentedUser>(commentedUserDto);

      commentedUser.CreateDate = DateTime.Now;
      commentedUser.UpdateDate = DateTime.Now;

      commentedUser.CreatedBy = user.Id;
      commentedUser.UpdatedBy = user.Id;

      commentedUser.CommentByUserName = user.UserName;

      _repository.CommentedUser.CreateCommentedUser(commentedUser);
      _repository.Save();

      await _notificationsService.SendNotificationAsync($"Người dùng {user.UserName} vừa đánh giá bạn.", false);

      Notification notification = new Notification
      {
        Message = $"Người dùng {user.UserName} vừa đánh giá bạn.",
        SentDate = DateTime.Now,
        ToUser = user.Id
      };

      _repository.Notification.CreateNotification(notification);
      _repository.Save();

      CommentedUserDto commentedUserToReturn = _mapper.Map<CommentedUserDto>(commentedUser);

      return CreatedAtAction(
        "GetCommentedUserById",
        new
        {
          id = commentedUserToReturn.Id
        },
        commentedUserToReturn
      );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCommentedUser(Guid id, [FromBody] CommentedUserDto commentedUserDto)
    {
      if (commentedUserDto == null)
      {
        _logger.LogError("CommentedUser object sent from client is null.");
        return BadRequest("EmployeeForUpdateDto object is null");
      }

      if (!ModelState.IsValid)
      {
        _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
        return UnprocessableEntity(ModelState);
      }

      CommentedUser commentedUser = _repository.CommentedUser.GetCommentedUser(id, trackChanges: false);

      if (commentedUser == null)
      {
        _logger.LogInfo($"CommentedUser with id: {id} doesn't exist in the database.");
        return NotFound("CommentedUser not fond");
      }

      Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
      User user = await _userManager.FindByIdAsync(userId.Value);

      _mapper.Map(commentedUserDto, commentedUser);

      commentedUser.UpdateDate = DateTime.Now;
      commentedUser.UpdatedBy = user.Id;

      _repository.CommentedUser.UpdateCommentedUser(commentedUser);
      _repository.Save();

      return Ok(_repository.CommentedUser.GetAllCommentsOfUser(commentedUser.EvaluatedUser, trackChanges: false));
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteCommentedUser(Guid id)
    {
      CommentedUser commentedUser = _repository.CommentedUser.GetCommentedUser(id, trackChanges: false);

      if (commentedUser == null)
      {
        _logger.LogInfo($"CommentedUser with id: {id} doesn't exist in the database.");
        return NotFound("CommentedUser doesn't exist in the database.");
      }
      string userID = commentedUser.EvaluatedUser;
      _repository.CommentedUser.DeleteCommentedUser(commentedUser);
      _repository.Save();

      return Ok(_repository.CommentedUser.GetAllCommentsOfUser(userID, trackChanges: false));
    }

    [HttpGet("user")]
    public IActionResult GetCommentOfUser(string userId)
    {
      if (string.IsNullOrEmpty(userId))
      {
        userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
      }

      IEnumerable<CommentedUser> commentedUsers = _repository.CommentedUser.GetAllCommentsOfUser(userId, trackChanges: false);
      IEnumerable<CommentedUserDto> commentedUserDtos = _mapper.Map<IEnumerable<CommentedUserDto>>(commentedUsers);

      return Ok(commentedUserDtos);
    }
  }
}
