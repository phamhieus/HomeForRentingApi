using AutoMapper;

using Data.Common;
using Data.DTO;
using Data.DTO.Responses;
using Data.Entities;

namespace AspImp.Mapper
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Company, CompanyDto>()
        .ForMember(c => c.FullAddress,
          opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

      CreateUserMaping();
      CreateRoomMaping();
      CreateMessageMaping();
      CreateCommentedUserMaping();
    }

    public void CreateMessageMaping()
    {
      CreateMap<Message, MessageDto>().ReverseMap();
    }

    public void CreateUserMaping()
    {
      CreateMap<User, UserDetailResponse>();
      CreateMap<User, UserUpdateDto>().ReverseMap();
      CreateMap<User, UserForRegistrationDto>().ReverseMap();

      CreateMap<UserImage, UserImageDto>()
        .ForMember(dto => dto.ImageId, opt => opt.MapFrom(e => e.Id))
        .ForMember(dto => dto.UserId, opt => opt.MapFrom(e => e.UserId))
        .ForMember(dto => dto.ImageName, opt => opt.MapFrom(e => e.FileName))
        .ForMember(dto => dto.Url, opt => opt.MapFrom(e => Constant.GetFileUrl(e.FilePath)));
    }

    public void CreateRoomMaping()
    {
      CreateMap<Room, RoomSamuryResponse>();
      CreateMap<Room, RoomDetailResponse>();
      CreateMap<Room, RoomGRPSeacrchingResponse>();
      CreateMap<RoomDto, Room>().ReverseMap();

      CreateMap<RoomImage, RoomImageDto>()
        .ForMember(dto => dto.ImageId, opt => opt.MapFrom(e => e.Id))
        .ForMember(dto => dto.RoomId, opt => opt.MapFrom(e => e.RoomId))
        .ForMember(dto => dto.ImageName, opt => opt.MapFrom(e => e.FileName))
        .ForMember(dto=>dto.Url, opt=>opt.MapFrom(e=> Constant.GetFileUrl(e.FilePath)));
    }

    public void CreateCommentedUserMaping()
    {
      CreateMap<CommentedUser, CommentedUserDto>().ReverseMap();
    }
  }
}
