using AutoMapper;

using Data.Common;
using Data.DTO;
using Data.DTO.Responses;
using Data.Entities;

namespace WebApp.Mapper
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Company, CompanyDto>()
        .ForMember(c => c.FullAddress,
          opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

      CreateMap<UserForRegistrationDto, User>();
      CreateMap<MessageDto, Message>();
      CreateMap<CommentedUserDto, User>();

      CreateMap<User, UserForRegistrationDto>();
      CreateMap<Message, MessageDto>();

      CreateMap<CommentedUser, CommentedUserDto>();

      CreateRoomMaping();
    }
 
    public void CreateRoomMaping()
    {
      CreateMap<Room, RoomSamuryResponse>();
      CreateMap<Room, RoomDetailResponse>();
      CreateMap<RoomDto, Room>().ReverseMap();

      CreateMap<RoomImage, RoomImageDto>()
        .ForMember(dto => dto.ImageId, opt => opt.MapFrom(e => e.Id))
        .ForMember(dto => dto.RoomId, opt => opt.MapFrom(e => e.RoomId))
        .ForMember(dto => dto.ImageName, opt => opt.MapFrom(e => e.FileName))
        .ForMember(dto=>dto.Url, opt=>opt.MapFrom(e=> Constant.GetFileUrl(e.FilePath)));
    }
  }
}
