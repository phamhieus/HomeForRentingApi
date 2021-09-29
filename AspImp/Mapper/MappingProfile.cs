using AutoMapper;

using Data.DTO;
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

      CreateMap<UserForRegistrationDto, User>();
      CreateMap<MessageDto, Message>();
      CreateMap<RoomDto, Room>();
      CreateMap<CommentedUserDto, User>();

    }
  }

}
