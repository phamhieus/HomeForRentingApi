using Data.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace AspImp.SwaggerExample
{
  public class RoomDtoPostRequestExample : IExamplesProvider<RoomDto>
  {
    public RoomDto GetExamples()
    {
      return new RoomDto
      {
        ShortName = "Phong 201 Tân triều",
        Address = "ngõ 197 tần triều, thanh trì, hà nội",
        Description = "Nhà trọ cho sinh viên khu tân triều",
        Service = "Điện 3k5",
        City = "Hà Nội",
        Province ="Thanh Trì",
        Street = "triều khuc",
        Cost = 15000000,
        Type = Data.Common.RoomType.FindingRoomate,
        RoomType =Data.Common.RentingType.FullHome,
        Status = 0
      };
    }
  }
}
