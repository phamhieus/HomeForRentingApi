using AutoMapper;
using Contracts;
using Data.DTO;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AspImp.Controllers
{
  [Route("api/[controller]")]
  [ApiController, Authorize]
  public class NotificationUserController : ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public NotificationUserController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
    {
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
        Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        IEnumerable<Notification> messages = _repository.Notification.GetNotificationsOfUser(userId.Value, trackChanges: false);

        return Ok(messages);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong in the {nameof(GetAll)} action {ex}");
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMessage(Guid id)
    {
      Notification message = _repository.Notification.GetNotification(id, trackChanges: false);

      if (message == null)
      {
        _logger.LogInfo($"Notification with id: {id} doesn't exist in the database.");
        return NotFound("Message doesn't exist in the database.");
      }

      _repository.Notification.DeleteNotification(message);
      _repository.Save();

      return Ok("Message had been deleted!");
    }

    [HttpPut("seen-notification/{id}")]
    public IActionResult SeenMessage(Guid id)
    {
      Notification message = _repository.Notification.GetNotification(id, trackChanges: false);

      message.IsSeen = true;

      if (message == null)
      {
        _logger.LogInfo($"Notification with id: {id} doesn't exist in the database.");
        return NotFound("Message doesn't exist in the database.");
      }

      _repository.Notification.UpdateNotification(message);
      _repository.Save();

      return Ok("Message had been deleted!");
    }
  }
}
