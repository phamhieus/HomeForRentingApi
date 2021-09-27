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
using System.Threading.Tasks;

namespace AspImp.Controllers
{
  [Route("api/[controller]")]
  [ApiController, Authorize]
  public class MessageController : ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public MessageController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
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
        IEnumerable<Message> messages = _repository.Message.GetAllMessages(trackChanges: false);
        IEnumerable<MessageDto> messageDtos = _mapper.Map<IEnumerable<MessageDto>>(messages);

        return Ok(messageDtos);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong in the {nameof(GetAll)} action {ex}");
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpGet("{id}")]
    public IActionResult GetMessageById(Guid id)
    {
      try
      {
        var message = _repository.Message.GetMessage(id, trackChanges: false);
        var messageDto = _mapper.Map<IEnumerable<MessageDto>>(message);

        return Ok(messageDto);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong in the {nameof(GetMessageById)} action {ex}");
        return StatusCode(500, "Internal server error");
      }
    }


    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] MessageDto messageDto)
    {
      if (messageDto == null)
      {
        _logger.LogError("MessageDto object sent from client is null.");
        return BadRequest("MessageDto object is null");
      }

      if (!ModelState.IsValid)
      {
        _logger.LogError("Invalid model state for the MessageDto object");
        return UnprocessableEntity(ModelState);
      }

      Message message = _mapper.Map<Message>(messageDto);

      message.SentDate = DateTime.Now;

      _repository.Message.CreateMessage(message);
      _repository.Save();

      MessageDto messageToReturn = _mapper.Map<MessageDto>(message);

      return CreatedAtRoute(
        "GetMessageById",
        new
        {
          id = messageToReturn.Id
        },
        messageToReturn
      );
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMessage(Guid id)
    {
      Message message = _repository.Message.GetMessage(id, trackChanges: false);

      if (message == null)
      {
        _logger.LogInfo($"Message with id: {id} doesn't exist in the database.");
        return NotFound("Message doesn't exist in the database.");
      }

      _repository.Message.DeleteMessage(message);
      _repository.Save();

      return Ok("Message had been deleted!");
    }
  }
}
