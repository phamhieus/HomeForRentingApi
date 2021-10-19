using AspImp.Services;
using AutoMapper;
using Contracts;
using Data.Common;
using Data.DTO;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AspImp.SwaggerExample;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Data.DTO.Responses;
using Data.DTO.Requests;
using GoogleMaps.LocationServices;
using Nest;
using AspImp.Extensions;
using AspImp.Services.Interfaces;

namespace AspImp.Controllers
{
  [Route("api/[controller]")]
  [ApiController, Authorize]
  public class RoomController : ControllerBase
  {
    #region Fields
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly FileService _fileService;
    private readonly GoogleLocationService locationService;

    private readonly INotificationsService _notificationsService;
    #endregion

    public RoomController(
      IRepositoryManager repository,
      ILoggerManager logger,
      IMapper mapper,
      UserManager<User> userManager,
      INotificationsService notificationsService,
      FileService fileService)
    {
      _userManager = userManager;
      _repository = repository;
      _logger = logger;
      _mapper = mapper;
      _fileService = fileService;
      _notificationsService = notificationsService;

      locationService = new GoogleLocationService("AIzaSyC3kewbfW0PJjrbevIDrddru0-rOoQPcYk");
    }

    /// <summary>
    /// get all room api
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///  Get /api/room
    ///   
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">List of room</response>
    /// <response code="401">If user didn't login </response>  
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(RoomSamurysResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoomSamurysResponseExample))]
    public IActionResult GetAll()
    {
      try
      {
        IEnumerable<Room> rooms = _repository.Room.GetAllRooms(trackChanges: false);
        IEnumerable<RoomSamuryResponse> roomDtos = _mapper.Map<IEnumerable<RoomSamuryResponse>>(rooms);

        foreach (var roomDto in roomDtos)
        {
          RoomImage thumbnail = _repository.RoomImage.GetThumbnailImagesOfRoom(roomDto.Id, false);
          RoomImageDto thumbnailDto = _mapper.Map<RoomImageDto>(thumbnail);

          GetCityName(roomDto);

          roomDto.ThumbnailImage = thumbnailDto;
        }

        return Ok(roomDtos);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong in the {nameof(GetAll)} action {ex}");
        return StatusCode(500, "Internal server error");
      }
    }

    /// <summary>
    /// get room by id api
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///  Get /api/room/{roomId}
    ///   
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Detail of room</response>
    /// <response code="401">If user didn't login </response>  
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(RoomDtoDetailResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(RoomDtoDetailResponseExample))]
    public async Task<IActionResult> GetRoomById(Guid id)
    {
      Room room = _repository.Room.GetRoom(id, trackChanges: false);
      RoomDetailResponse roomDetailRequest = _mapper.Map<RoomDetailResponse>(room);

      if (room == null)
      {
        return NotFound("Room is not found");
      }

      try
      {
        var award = _repository.Award.GetAwardAreaById(roomDetailRequest.Street, false);
        var province = _repository.Province.GetProvinceById(roomDetailRequest.Province, false);
        var city = _repository.City.GetCityById(roomDetailRequest.Street, false);

        roomDetailRequest.City = city.AreaName;
        roomDetailRequest.Province = province.AreaName;
        roomDetailRequest.Street = award.AreaName;
      }
      catch { }

      IEnumerable<RoomImage> roomDescriptionImages = _repository.RoomImage.GetImagesOfRoom(room.Id, false);
      IEnumerable<RoomImageDto> roomDescriptionImageDtos = _mapper.Map<IEnumerable<RoomImageDto>>(roomDescriptionImages);

      RoomImage roomThumbnailImage = _repository.RoomImage.GetThumbnailImagesOfRoom(room.Id, false);
      RoomImageDto roomThumbnailImageDto = _mapper.Map<RoomImageDto>(roomThumbnailImage);

      User user = await _userManager.FindByIdAsync(room.CreatedBy);
      UserDetailResponse userDetailResponse = _mapper.Map<UserDetailResponse>(user);

      UserImage userImage = _repository.UserImage.GetUserThumbnail(user.Id, false);
      UserImageDto userImageDto = _mapper.Map<UserImageDto>(userImage);

      string address = room.Address;

      try
      {
        var award = _repository.Award.GetAwardAreaById(room.Street, false);
        var province = _repository.Province.GetProvinceById(room.Province, false);
        var city = _repository.City.GetCityById(room.City, false);

        if (award != null && province != null && city != null)
        {
          address = $"{award.AreaName}, {province.AreaName}, {city.AreaName}, Việt Nam";
        }
      }
      catch { }

      var point = locationService.GetLatLongFromAddress(address);

      userDetailResponse.ThumbnailImage = userImageDto;

      roomDetailRequest.Longitude = point.Longitude;
      roomDetailRequest.Latitude = point.Latitude;

      roomDetailRequest.ThumbnailImage = roomThumbnailImageDto;
      roomDetailRequest.DescriptionImages = roomDescriptionImageDtos;
      roomDetailRequest.Owner = userDetailResponse;

      return Ok(roomDetailRequest);
    }

    /// <summary>
    /// create new room api
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    /// Post /api/room/
    /// 
    /// status:  đã cho thuê = 0, đang trống = 1, đang tim bạn cùng phòng = 2
    /// 
    /// Type: Cho thuê chính chủ = 0, tim bạn cùng phòng = 1
    /// 
    /// RoomType:  Homestay= 0, nguyên căn = 1, phòng đơn = 2
    /// 
    /// </remarks>
    /// <returns></returns>
    /// <response code="201">Detail of room</response>
    /// <response code="401">If user didn't login </response>  
    /// <response code="400">If one property is invalid </response>  
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerRequestExample(typeof(RoomDto), typeof(RoomDtoPostRequestExample))]
    [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(RoomDtoDetailResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(RoomDtoDetailResponseExample))]
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
      User user = await _userManager.FindByIdAsync(userId.Value);

      Room room = _mapper.Map<Room>(roomDto);

      room.CreateDate = DateTime.Now;
      room.UpdateDate = DateTime.Now;

      room.CreatedBy = user.Id;
      room.UpdatedBy = user.Id;

      room.Mounth = DateTime.Now.Month;
      room.Year = DateTime.Now.Month;
      room.Status = RoomStatus.Empty;

      _repository.Room.CreateRoom(room);
      _repository.Save();

      await _notificationsService.SendNotificationAsync($"Phòng {room.ShortName} đã được admin xét duyệt.", false);

      Notification notification = new Notification
      {
        Message = $"Phòng {room.ShortName} đã được admin xét duyệt.",
        SentDate = DateTime.Now,
        ToUser = user.Id
      };

      _repository.Notification.CreateNotification(notification);
      _repository.Save();

      RoomDto roomToReturn = _mapper.Map<RoomDto>(room);

      return CreatedAtAction(
        "GetRoomById",
        new
        {
          id = roomToReturn.Id
        },
        roomToReturn
      );
    }

    /// <summary>
    /// update room api
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    /// PUT /api/room/{roomId}
    /// 
    /// status:  đã cho thuê = 0, đang trống = 1, đang tim bạn cùng phòng = 2
    /// 
    /// Type: Cho thuê chính chủ = 0, tim bạn cùng phòng = 1
    /// 
    /// RoomType:  Homestay= 0, nguyên căn = 1, phòng đơn = 2
    /// 
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Detail of room</response>
    /// <response code="401">If user didn't login </response>
    /// <response code="400">If one property is invalid </response>  
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerRequestExample(typeof(RoomDto), typeof(RoomDtoPostRequestExample))]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(RoomDtoPostRequestExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoomDtoPostRequestExample))]
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
      User user = await _userManager.FindByIdAsync(userId.Value);

      _mapper.Map(roomDto, room);

      room.UpdateDate = DateTime.Now;
      room.UpdatedBy = user.Id;

      _repository.Room.UpdateRoom(room);
      _repository.Save();

      return Ok(roomDto);
    }

    /// <summary>
    /// get current user 's room 
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///  Get /api/room/{roomId}
    ///   
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Detail of room</response>
    /// <response code="401">If user didn't login </response>  
    [HttpGet("profile-room")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(RoomDtoDetailResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoomDtoDetailResponseExample))]
    public async Task<IActionResult> GetRoomMyRoom(string userId)
    {
      try
      {
        string currentUserId = userId;

        if (string.IsNullOrEmpty(userId))
        {
          Claim userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
          currentUserId = userIdClaim.Value;
        }

        User user = await _userManager.FindByIdAsync(currentUserId);

        if (user == null)
        {
          return NotFound("User not found");
        }

        IEnumerable<Room> rooms = _repository.Room.GetMyRoom(user.Id, trackChanges: false);
        IEnumerable<RoomSamuryResponse> roomDtos = _mapper.Map<IEnumerable<RoomSamuryResponse>>(rooms);

        foreach (var roomDto in roomDtos)
        {
          RoomImage thumbnail = _repository.RoomImage.GetThumbnailImagesOfRoom(roomDto.Id, false);
          RoomImageDto thumbnailDto = _mapper.Map<RoomImageDto>(thumbnail);

          roomDto.ThumbnailImage = thumbnailDto;
        }

        return Ok(roomDtos);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong in the {nameof(GetRoomMyRoom)} action {ex}");
        return StatusCode(500, "Internal server error");
      }
    }

    /// <summary>
    /// update room api
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    /// PUT /api/room/{roomId}
    /// 
    /// status:  đã cho thuê = 0, đang trống = 1, đang tim bạn cùng phòng = 2
    /// 
    /// Type: Cho thuê chính chủ = 0, tim bạn cùng phòng = 1
    /// 
    /// RoomType:  Homestay= 0, nguyên căn = 1, phòng đơn = 2
    /// 
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Detail of room</response>
    /// <response code="401">If user didn't login </response>
    /// <response code="400">If one property is invalid </response>  
    [HttpPut("room-solt-out/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(RoomDtoPostRequestExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoomDtoPostRequestExample))]
    public async Task<IActionResult> UpdateRoomSoldOut(Guid id)
    {
      Room room = _repository.Room.GetRoom(id, trackChanges: false);

      if (room == null)
      {
        _logger.LogInfo($"Room with id: {id} doesn't exist in the database.");
        return NotFound("Room not fond");
      }

      Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
      User user = await _userManager.FindByIdAsync(userId.Value);

      room.Status = RoomStatus.SoltOut;
      room.UpdateDate = DateTime.Now;
      room.UpdatedBy = user.Id;

      _repository.Room.UpdateRoom(room);
      _repository.Save();

      var roomDto = _mapper.Map<RoomDto>(room);

      return Ok(roomDto);
    }

    /// <summary>
    /// delete room api
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    /// Delete /api/room/{roomId}
    /// 
    /// status:  đã cho thuê = 0, đang trống = 1, đang tim bạn cùng phòng = 2
    /// 
    /// Type: Cho thuê chính chủ = 0, tim bạn cùng phòng = 1
    /// 
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Delete success</response>
    /// <response code="401">If user didn't login </response>
    /// <response code="404">If room is not found </response>  
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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


    /// <summary>
    /// upload new room thumbnail
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    /// Put /api/room/thumbnail/{roomId}
    /// 
    /// Type: ảnh đại diện = 1, ảnh chi tiết = 2
    /// 
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Upload success</response>
    /// <response code="401">If user didn't login </response>
    /// <response code="404">If room is not found </response> 
    /// <response code="400">If one property is invalid </response>  
    [HttpPut("thumbnail/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(RoomImageThumbnailResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoomImageThumbnailResponseExample))]
    public async Task<IActionResult> UploadRoomImageThumbnail(Guid id)
    {
      try
      {
        var imageFiles = this.Request.Form.Files;

        if (imageFiles == null || !imageFiles.Any() || imageFiles.Count > 1)
        {
          return BadRequest("Please, upload a single file");
        }

        Room room = _repository.Room.GetRoom(id, trackChanges: false);

        Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        User user = await _userManager.FindByIdAsync(userId.Value);

        if (room == null)
        {
          _logger.LogInfo($"Room with id: {id} doesn't exist in the database.");
          return NotFound("Room not fond");
        }

        return Ok(CreateNewRoomImage(room, user, imageFiles[0], ImageType.Thumbnail));
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }

    /// <summary>
    /// upload new description image
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    /// Put /api/room/description-image/{roomId}
    /// 
    /// Type: ảnh đại diện = 1, ảnh chi tiết = 2
    /// 
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Upload success</response>
    /// <response code="401">If user didn't login </response>
    /// <response code="404">If room is not found </response> 
    /// <response code="400">If one property is invalid </response>  
    [HttpPut("description-image/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(RoomImageDescriptionResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoomImageDescriptionResponseExample))]
    public async Task<IActionResult> UploadRoomImageDescription(Guid id)
    {
      try
      {
        var imageFiles = this.Request.Form.Files;

        if (imageFiles == null || imageFiles.Count <= 0)
        {
          return BadRequest("Please, upload at least a file");
        }

        var invalidFiles = imageFiles.Where(file => file.Length < 0);

        if (invalidFiles != null && invalidFiles.Any())
        {
          return BadRequest("At least a file is invalid");
        }

        if (!AspImp.Helpers.MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
        {
          return BadRequest("Not a multipart request");
        }

        Room room = _repository.Room.GetRoom(id, trackChanges: false);

        Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        User user = await _userManager.FindByIdAsync(userId.Value);

        IList<RoomImageDto> roomImageDtos = new List<RoomImageDto>();

        if (room == null)
        {
          _logger.LogInfo($"Room with id: {id} doesn't exist in the database.");
          return NotFound("Room not fond");
        }

        bool isFirstImage = true;

        foreach (var imageFile in imageFiles)
        {
          RoomImage roomImage = CreateNewRoomImage(room, user, imageFile, ImageType.DescriptionImage);
          roomImageDtos.Add(_mapper.Map<RoomImageDto>(roomImage));

          if (isFirstImage)
          {
            CreateNewRoomImage(room, user, imageFile, ImageType.Thumbnail);
            isFirstImage = false;
          }
        }

        return Ok(roomImageDtos);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, e);
      }
    }

    /// <summary>
    /// delete room image
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    /// Put /api/room/room-image/{imageId}
    /// 
    /// Type: ảnh đại diện = 1, ảnh chi tiết = 2
    /// 
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Upload success</response>
    /// <response code="401">If user didn't login </response>
    /// <response code="404">If room is not found </response> 
    [HttpDelete("room-image/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteRoomImage(Guid id)
    {
      try
      {
        RoomImage roomImage = _repository.RoomImage.GetImageById(id, false);

        if (roomImage == null)
        {
          _logger.LogInfo($"RoomImage with id: {id} doesn't exist in the database.");
          return NotFound("RoomImage doesn't exist in the database.");
        }

        _fileService.DeleteFile(Constant.IMG_DIRECTORY, Constant.ROOM_DIRECTORY, roomImage.FileName);
      }
      catch (Exception e)
      {
        _logger.LogError($"RoomImage with id: deleting had error: {e.Message}");
        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
      }

      return Ok("Images had been deleted!");
    }


    /// <summary>
    /// Searching room api
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    /// Get /api/room/advange-searching?searchKey=
    /// 
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">RoomDetail success</response>
    /// <response code="401">If user didn't login </response>
    [HttpGet("room-searching")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(RoomSamurysResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoomSamurysResponseExample))]
    public IActionResult SearchRoomByKey(string searchKey)
    {
      if (string.IsNullOrEmpty(searchKey))
      {
        return BadRequest("key is not null");
      }

      searchKey = searchKey.Replace("\"", "");

      IEnumerable<Room> rooms = _repository.Room.GetRoomsByKey(searchKey.ToLower(), trackChanges: false);
      IEnumerable<RoomSamuryResponse> roomDtos = _mapper.Map<IEnumerable<RoomSamuryResponse>>(rooms);

      foreach (var roomDto in roomDtos)
      {
        RoomImage thumbnail = _repository.RoomImage.GetThumbnailImagesOfRoom(roomDto.Id, false);
        RoomImageDto thumbnailDto = _mapper.Map<RoomImageDto>(thumbnail);
       
        GetCityName(roomDto);

        roomDto.ThumbnailImage = thumbnailDto;
      }

      return Ok(roomDtos);
    }

    [HttpPost("advange-searching")]
    public IActionResult SearchRoom(SearchingRoomRequest searchingRoomRequest)
    {
      if (searchingRoomRequest == null)
      {
        searchingRoomRequest = new SearchingRoomRequest();
      }

      IEnumerable<Room> rooms = _repository.Room.GetRoomsRequest(searchingRoomRequest, trackChanges: false);
      IEnumerable<RoomSamuryResponse> roomDtos = _mapper.Map<IEnumerable<RoomSamuryResponse>>(rooms);

      foreach (var roomDto in roomDtos)
      {
        RoomImage thumbnail = _repository.RoomImage.GetThumbnailImagesOfRoom(roomDto.Id, false);
        RoomImageDto thumbnailDto = _mapper.Map<RoomImageDto>(thumbnail);
        
        GetCityName(roomDto);

        if (searchingRoomRequest.Latitude != null && searchingRoomRequest.Longitude!=null)
        {
          var room = rooms.Where(r => r.Id.Equals(roomDto.Id)).FirstOrDefault();
          var point = new MapPoint()
          {
            Longitude = searchingRoomRequest.Longitude.Value,
            Latitude = searchingRoomRequest.Latitude.Value
          };

          roomDto.Distance = GetRoomInformationAndLocation(room, point).Distance;
        }

        roomDto.ThumbnailImage = thumbnailDto;
      }

      if (searchingRoomRequest.Latitude != null && searchingRoomRequest.Longitude != null)
        roomDtos = roomDtos.OrderBy(dto => dto.Distance).ToList();

      return Ok(roomDtos);
    }

    [HttpGet("search-by-coord")]
    public IActionResult SearchByLocation(double longitude, double latitude)
    {
      var point = new MapPoint()
      {
        Longitude = longitude, 
        Latitude = latitude
      };
      var rooms = _repository.Room.GetAllRooms(false);

      IList<RoomSamuryResponse> roomDtos = new List<RoomSamuryResponse>();

      foreach (var room in rooms)
      {
        RoomSamuryResponse roomDto = GetRoomInformationAndLocation(room, point);
     
        GetCityName(roomDto);

        roomDtos.Add(roomDto);
      }

      roomDtos = roomDtos.OrderBy(dto => dto.Distance).ToList();

      return Ok(roomDtos);
    }

    private RoomSamuryResponse GetRoomInformationAndLocation(Room room, MapPoint recentPoint)
    {
      var roomDto = _mapper.Map<RoomSamuryResponse>(room);

      RoomImage thumbnail = _repository.RoomImage.GetThumbnailImagesOfRoom(roomDto.Id, false);
      RoomImageDto thumbnailDto = _mapper.Map<RoomImageDto>(thumbnail);

      roomDto.ThumbnailImage = thumbnailDto;

      MapPoint point = GetRoomCoord(room);

      roomDto.Distance = point.GetDistanceTo(recentPoint);

      return roomDto;
    }

    [HttpGet("notication-test")]
    public Task TestNotification()
    {
       return _notificationsService.SendNotificationAsync("Phong test da duoc xet duyet", false);
    }

    [HttpPut("accessed-room/{id}")]
    public IActionResult ApproveRoom(Guid id)
    {
      Room room = _repository.Room.GetRoom(id, trackChanges: false);

      room.Status = RoomStatus.Empty;
      
      _repository.Room.UpdateRoom(room);
      _repository.Save();

      _notificationsService.SendNotificationAsync($"Phong {room.ShortName} da duoc admin xet duyet.", false);

      return Ok();
    }

    private MapPoint GetRoomCoord(Room room)
    {
      string address = room.Address;

      try
      {
        var award = _repository.Award.GetAwardAreaById(room.Street, false);
        var province = _repository.Province.GetProvinceById(room.Province, false);
        var city = _repository.City.GetCityById(room.City, false);

        if (award != null && province != null && city != null)
        {
          address = $"{award.AreaName}, {province.AreaName}, {city.AreaName}, Việt Nam";
        }
      }
      catch { }

      return locationService.GetLatLongFromAddress(address);
    }

    private RoomImage CreateNewRoomImage(
      Room room,
      User user,
      IFormFile imageFile,
      ImageType fileType)
    {
      try
      {
        RoomImage roomImage = new RoomImage()
        {
          FileType = fileType,
          FileExtension = Path.GetExtension(imageFile.FileName),
          RoomId = room.Id,

          IsActive = true,
          CreateDate = DateTime.Now,
          CreatedBy = user.Id,
          UpdateDate = DateTime.Now,
          UpdatedBy = user.Id
        };

        if (fileType == ImageType.Thumbnail)
        {
          RoomImage previousThumbnail = _repository.RoomImage.GetThumbnailImagesOfRoom(room.Id, false);

          if (previousThumbnail != null)
          {
            previousThumbnail.IsActive = false;
            _repository.RoomImage.UpdateRoomImage(previousThumbnail);
          }
        }

        _repository.RoomImage.CreateRoomImage(roomImage);
        _repository.Save();

        string fileName = Constant.RemoveSign4VietnameseString(room.ShortName);

        fileName = Regex.Replace(fileName, @"[^0-9a-z-A-Z ]", "");
        fileName = fileName.Replace(" ", "-") + "-";
        fileName += roomImage.Id;
        fileName += roomImage.FileExtension;

        roomImage.FileName = fileName;
        roomImage.FilePath = Constant.GetImageRoomPath(roomImage.FileName);

        _repository.RoomImage.UpdateRoomImage(roomImage);
        _repository.Save();

        _fileService.WriteImageFile(roomImage.FileName, Constant.ROOM_DIRECTORY, imageFile);

        return roomImage;
      }
      catch (Exception e)
      {
        _logger.LogError($"There are a error in CreateNewRoomImage : {e.Message} ");
        throw;
      }
    }
  
    private void GetCityName(RoomSamuryResponse dto)
    {
      try
      {
        var award = _repository.Award.GetAwardAreaById(dto.Street, false);
        var province = _repository.Province.GetProvinceById(dto.Province, false);
        var city = _repository.City.GetCityById(dto.City, false);

        dto.City = city.AreaName;
        dto.Province = province.AreaName;
        dto.Street = award.AreaName;
      }
      catch { }
    }
  }
}
