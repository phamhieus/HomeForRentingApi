using Data.DTO;
using Data.DTO.Responses;

using Swashbuckle.AspNetCore.Filters;

using System.Collections.Generic;

namespace AspImp.SwaggerExample
{
  public class RoomSamurysResponseExample : IExamplesProvider<IEnumerable<RoomSamuryResponse>>
  {
    public IEnumerable<RoomSamuryResponse> GetExamples()
    {
      var roomImageDto1 = new RoomImageDto()
      {
        ImageId = System.Guid.Parse("8823a833-c612-472d-6709-08d986065146"),
        RoomId = System.Guid.Parse("d2fb1840-8b7f-4c81-57a5-08d9857941e7"),
        Url = "http:localhost:5000/images/room/Phong-201-Tan-triu-8823a833-c612-472d-6709-08d986065146.png",
        ImageName = "Phong-201-Tan-triu-8823a833-c612-472d-6709-08d986065146.png"
      };

      var roomImageDto2 = new RoomImageDto()
      {
        ImageId = System.Guid.Parse("186d3294-84b7-42bd-ed3d-08d98605e7b3"),
        RoomId = System.Guid.Parse("d2fb1840-8b7f-4c81-57a5-08d9857941e7"),
        Url = "http:localhost:5000/images/room/Phong-201-Tan-triu-186d3294-84b7-42bd-ed3d-08d98605e7b3.png",
        ImageName = "Phong-201-Tan-triu-186d3294-84b7-42bd-ed3d-08d98605e7b3.png"
      };

      var room1 = new RoomSamuryResponse
      {
        ShortName = "Phong 201 Tân triều",
        Address = "ngõ 197 tần triều, thanh trì, hà nội",
        Description = "Nhà trọ cho sinh viên khu tân triều",
        Service = "Điện 3k5",
        Cost = 15000000,
        Type = Data.Common.RoomType.FindingRoomate,
        ThumbnailImage = roomImageDto1,
        Status = 0
      };

      var room2 = new RoomSamuryResponse
      {
        ShortName = "Phong 201 khương đình",
        Address = "ngõ 197 tần triều, thanh trì, hà nội",
        Description = "Nhà trọ cho sinh viên khu tân triều",
        Service = "Điện 3k5",
        Cost = 15000000,
        Type = Data.Common.RoomType.FindingRoomate,
        ThumbnailImage = roomImageDto2,
        Status = 0
      };

      return new RoomSamuryResponse[] { room1, room2 };
    }
  }
}
