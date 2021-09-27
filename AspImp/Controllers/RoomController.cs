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
  public class RoomController : ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public RoomController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
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
        IEnumerable<Room> rooms = _repository.Room.GetAllRooms(trackChanges: false);
        IEnumerable<RoomDto> roomDtos = _mapper.Map<IEnumerable<RoomDto>>(rooms);

        return Ok(roomDtos);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong in the {nameof(GetAll)} action {ex}");
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpGet("{id}")]
    public IActionResult GetRoomById(Guid id)
    {
      try
      {
        var room = _repository.Room.GetRoom(id, trackChanges: false);
        var roomDto = _mapper.Map<IEnumerable<RoomDto>>(room);

        return Ok(roomDto);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong in the {nameof(GetRoomById)} action {ex}");
       
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] RoomDto roomDto)
    {
      if (roomDto == null)
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
      User user = await _userManager.FindByNameAsync(userId.Value);

      Room room = _mapper.Map<Room>(roomDto);

      room.CreateDate = DateTime.Now;
      room.UpdateDate = DateTime.Now;
      
      room.CreatedBy = user.Id;
      room.UpdatedBy = user.Id;

      _repository.Room.CreateRoom(room);
      _repository.Save();

      RoomDto roomToReturn = _mapper.Map<RoomDto>(room);

      return CreatedAtRoute(
        "GetRoomById",
        new {
          id = roomToReturn.Id
        },
        roomToReturn
      );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] RoomDto roomDto)
    {
      if (roomDto == null)
      {
        _logger.LogError("Room object sent from client is null.");
        return BadRequest("EmployeeForUpdateDto object is null");
      }

      if (!ModelState.IsValid)
      {
        _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
        return UnprocessableEntity(ModelState);
      }

      Room room = _repository.Room.GetRoom(id, trackChanges: false);

      if (room == null)
      {
        _logger.LogInfo($"Room with id: {id} doesn't exist in the database."); 
        return NotFound("Room not fond");
      }

      Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
      User user = await _userManager.FindByNameAsync(userId.Value);

      _mapper.Map(roomDto, room);

      room.UpdateDate = DateTime.Now;
      room.UpdatedBy = user.Id;

      _repository.Room.UpdateRoom(room);
      _repository.Save();

      return Ok(roomDto);
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(Guid id)
    {
      Room room = _repository.Room.GetRoom(id, trackChanges: false);

      if (room == null)
      {
        _logger.LogInfo($"Room with id: {id} doesn't exist in the database.");
        return NotFound("Room doesn't exist in the database.");
      }

      _repository.Room.DeleteRoom(room);
      _repository.Save();
      
      return Ok("Room had been deleted!");
    }
  }
}
