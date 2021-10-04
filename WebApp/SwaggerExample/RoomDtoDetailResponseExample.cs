using Data.DTO;
using Data.DTO.Responses;

using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace AspImp.SwaggerExample
{
  public class RoomDtoDetailResponseExample : IExamplesProvider<RoomDetailResponse>
  {
    public RoomDetailResponse GetExamples()
    {
      RoomImageDto imageThumbnail = new RoomImageDto()
      {
        ImageId = System.Guid.Parse("8823a833-c612-472d-6709-08d986065146"),
        RoomId = System.Guid.Parse("d2fb1840-8b7f-4c81-57a5-08d9857941e7"),
        Url = "http:localhost:5000/images/room/Phong-201-Tan-triu-8823a833-c612-472d-6709-08d986065146.png",
        ImageName = "Phong-201-Tan-triu-8823a833-c612-472d-6709-08d986065146.png"
      };

      IEnumerable<RoomImageDto> images = new RoomImageDto[] {
      new RoomImageDto()
      {
          ImageId = System.Guid.Parse("8823a833-c612-472d-6709-08d986065146"),
          RoomId = System.Guid.Parse("d2fb1840-8b7f-4c81-57a5-08d9857941e7"),
          Url = "http:localhost:5000/images/room/Phong-201-Tan-triu-8823a833-c612-472d-6709-08d986065146.png",
          ImageName = "Phong-201-Tan-triu-8823a833-c612-472d-6709-08d986065146.png"
      },
      new RoomImageDto() {
           ImageId = System.Guid.Parse("186d3294-84b7-42bd-ed3d-08d98605e7b3"),
           RoomId =   System.Guid.Parse("d2fb1840-8b7f-4c81-57a5-08d9857941e7"),
           Url = "http:localhost:5000/images/room/Phong-201-Tan-triu-186d3294-84b7-42bd-ed3d-08d98605e7b3.png",
           ImageName = "Phong-201-Tan-triu-186d3294-84b7-42bd-ed3d-08d98605e7b3.png"
        }
    };

      return new RoomDetailResponse
      {
        ShortName = "Phong 201 Tân triều",
        Address = "ngõ 197 tần triều, thanh trì, hà nội",
        Description = "Nhà trọ cho sinh viên khu tân triều",
        Cost = 15000000,
        Type = Data.Common.RoomType.FindingRoomate,
        RoomType = Data.Common.RentingType.FullHome,
        ThumbnailImage = imageThumbnail,
        DescriptionImages = images,
        Status = 0
      };
    }
  }
}
